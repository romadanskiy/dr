// Learn more about F# at http://fsharp.org

open System
let sum x y = x + y
let minus x y = x - y
let multiplay x y = x * y
let divide x y = x / y

let calculate x operation y =
    match operation with
    | "+" -> sum x y
    | "-" -> minus x y
    | "*" -> multiplay x y
    | "/" -> divide x y
    | _ -> raise (NotImplementedException("Operation is not Implement"))

[<EntryPoint>]
let main argv =
    let x = Console.ReadLine() |> double
    let operation = Console.ReadLine()
    let y = Console.ReadLine() |> double
    calculate x operation y |> Console.WriteLine
    0