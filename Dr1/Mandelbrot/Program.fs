open System
open System.Numerics
open System.Drawing
open System.Windows.Forms
    
let checkModulus (z: Complex) =
    z.Real * z.Real + z.Imaginary * z.Imaginary <= 4.0

let rec isInMandelbrotSet z c iter count =
    if (checkModulus(z)) && (count < iter) then
        isInMandelbrotSet (z * z + c) c iter (count + 1)
    else count
  
let mutable scaling = 0.005
let scalingStep = 0.00005
let mutable deltaX = -2.0
let mutable deltaY = -1.3
let deltaStep = 20.0 * (scaling * 0.5)

let mapPlane x y s mx my =
    let fx = ((double x) * scaling) + mx
    let fy = ((double y) * scaling) + my
    Complex(fx, fy)
    
let colorize c =
    let r = (4 * c) % 255
    let g = (6 * c) % 255
    let b = (8 * c) % 255
    Color.FromArgb(r, g, b)
    
let createImage s mx my iter =
    let image = new Bitmap(800, 600)
    for x = 0 to image.Width - 1 do
        for y = 0 to image.Height - 1 do
            let count = isInMandelbrotSet Complex.Zero (mapPlane x y s mx my) iter 0
            if count = iter then
                image.SetPixel(x, y, Color.Black)
            else
                image.SetPixel(x, y, colorize(count))
    image
    
let onKeyDown (args:KeyEventArgs) (form:Form) =
    match args.KeyData with
    | Keys.Right ->
        deltaX <- deltaX + deltaStep
        form.Invalidate()
    | Keys.Left ->
        deltaX <- deltaX - deltaStep
        form.Invalidate()
    | Keys.Up ->
        deltaY <- deltaY - deltaStep
        form.Invalidate()
    | Keys.Down ->
        deltaY <- deltaY + deltaStep
        form.Invalidate()
    | _ -> ignore()   
    
let onScroll (args:MouseEventArgs) (form:Form) =
    if args.Delta > 0 then
        scaling <- scaling * 0.9
    else
        scaling <- scaling * 1.1
    form.Invalidate()
    
type MyForm() =
    inherit Form ()
    override this.OnLoad (e : EventArgs) =
        this.DoubleBuffered <- true

[<EntryPoint>]
let main argv =
    let form = new MyForm()
    form.Size <- Size(800, 600)
    form.MaximumSize <- form.Size
    form.MinimumSize <- form.Size
    form.MaximizeBox <- false
    form.Text <- "Mandelbrot Set"
    form.MouseWheel.Add(fun args -> onScroll args form)
    form.KeyDown.Add(fun args -> onKeyDown args form)
    form.Paint.Add(fun e -> e.Graphics.DrawImage(createImage scaling deltaX deltaY 80, 0, 0))   
    
    async { 
    while true do
      do! Async.Sleep(1)
      form.Invalidate()
      scaling <- scaling * 0.99
    } |> Async.StartImmediate
    
    Application.Run form
    0