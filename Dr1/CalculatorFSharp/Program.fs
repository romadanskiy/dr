open System

type MaybeBuilder() =
    
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a
    
    member this.Return(x) =
        Some x
        
let maybe = new MaybeBuilder()

let sum num1 num2 = Some((double)num1 + (double)num2)

let subtract num1 num2 = Some(num1 - num2)

let multiply num1 num2 = Some(num1 * num2)

let divide num1 num2 =
    if num2 = (double)0
        then None
    else
        Some(num1 / num2)
        
let calculate num1 operation num2 =
    maybe{
        let! x = match operation with
        | "+" -> sum num1 num2
        | "-" -> subtract num1 num2
        | "*" -> multiply num1 num2
        | "/" -> divide num1 num2
        | _ -> None
        return x
    }
    
let checkAndWrite (e:double option) =
    if e = None
        then Console.WriteLine("None")
    else
        Console.WriteLine(e.Value)
        
[<EntryPoint>]
let main argv =
    let num1 = Console.ReadLine() |> double
    let operation = Console.ReadLine()
    let num2 = Console.ReadLine() |> double
    let res = calculate num1 operation num2
    checkAndWrite res
    0