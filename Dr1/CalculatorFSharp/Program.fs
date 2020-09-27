// Learn more about F# at http://fsharp.org

open System
let sum x y = x + y
let subtraction x y = x - y
let multiplay x y = x * y
let divide x y = x / y

let calculate op x y =
    match op with
    | "+" -> sum x y
    | "-" -> subtraction x y
    | "*" -> multiplay x y
    | "/" -> divide x y
    | _ -> raise (System.NotImplementedException("Operation is not Implement"))

[<EntryPoint>]
let main argv =
    let x = Console.ReadLine() |> Int32.Parse
    let op = Console.ReadLine()
    let y = Console.ReadLine() |> Int32.Parse
    
    calculate op x y |> Console.WriteLine
    0