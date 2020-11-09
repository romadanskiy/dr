open System
open System.IO
open System.Net
open System.Net.Http
open FSharp.Data

type AsyncMaybeBuilder () =
    member this.Bind(x, f) =
        async {
            let! x' = x
            match x' with
            | Some s -> return! f s
            | None -> return None
            }
        member this.Return x =
            async { return x }
            
let asyncMaybe = AsyncMaybeBuilder()

let GetRequest num1 operation num2 =
    "http://localhost:5000/?num1=" + num1 + "&operation=" + operation + "&num2=" + num2
    
let GetResponse (request:string) =
    async{
        let httpClient = new HttpClient()
        let! response = httpClient.GetAsync(request) |> Async.AwaitTask
        if response.StatusCode = HttpStatusCode.OK
        then
            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return Some content
        else
            return None
    }

let Calculate num1 operation num2 =
   asyncMaybe{
        let request = GetRequest num1 operation num2
        let! response = GetResponse request
        return Some response
   } |> Async.RunSynchronously
   
let Write (res:string option) =
    match res with
    | Some x -> Console.WriteLine(x)
    | None -> Console.WriteLine("Error")

[<EntryPoint>]
let main argv =
    let num1 = Console.ReadLine()
    let operation = Console.ReadLine().Replace("+", "%2b")
    let num2 = Console.ReadLine()
    let res = Calculate num1 operation num2
    Write res
    0 