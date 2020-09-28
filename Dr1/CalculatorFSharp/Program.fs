open System

type MaybeBuilder() =
    
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a
    
    member this.Return(x) =
        Some x
        
let maybe = new MaybeBuilder()

let sum num1 num2 = Some(num1 + num2)

let subtract num1 num2 = Some(num1 - num2)

let multiply num1 num2 = Some(num1 * num2)

let divide num1 num2 =
    if num2 = 0.0 then
        None
    else
        Some(num1 / num2)
        
let calculate num1 operation num2 =
    maybe {
        let! x =
            match operation with
            | "+" -> sum num1 num2
            | "-" -> subtract num1 num2
            | "*" -> multiply num1 num2
            | "/" -> divide num1 num2
            | _ -> None
        return x
    }
    
let checkAndWrite (e:float option) =
    if (e.IsNone)
        then Console.WriteLine("Ошибка: Нельзя делить на 0")
    else
        Console.WriteLine("Результат: {0}", e.Value)
    
let getOperation() =
    Console.WriteLine("Введите оператор:")
    let str = Console.ReadLine()
    if (str = "+" || str = "-" || str = "*" || str = "/") then
        Some(str)
    else
        None
        
let getNumber() =
    Console.WriteLine("Введите число:")
    let str = Console.ReadLine()
    let isNumber, n = Double.TryParse str
    if isNumber then
        Some(n)
    else
        None
        
[<EntryPoint>]
let main argv =
    let num1 = getNumber()
    let operation = getOperation()
    let num2 = getNumber()
    
    Console.WriteLine()
    if (num1.IsNone) then Console.WriteLine("Ошибка: Проверьте первое число")
    if (operation.IsNone) then Console.WriteLine("Ошибка: Неверный оператор")
    if (num2.IsNone) then Console.WriteLine("Ошибка: Провертье второе число")
    if not (num1.IsNone || num2.IsNone || operation.IsNone) then
        let res = calculate num1.Value operation.Value num2.Value
        checkAndWrite res
    0