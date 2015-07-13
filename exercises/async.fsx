(* F# supports composable async blocks
   they are superficially similar
   to the async and await capabilities of
   C# but more flexible about when and how they
   are run *)
#load "./examples.fs"
#r "../packages/FSharp.Control.AsyncSeq/lib/net45/FSharp.Control.AsyncSeq.dll"
#r "../packages/FSharpx.Async/lib/net40/FSharpx.Async.dll"
open System.IO
open System.Net

// add AsyncReadToEnd method
open FSharpx.Control.StreamReaderExtensions 

// This function will be useful in many of our
// later examples. It downloads the content
// of a url asynchronously
let fetchAsync (url : string) = async {
    let req = WebRequest.Create(url)
    let! resp = req.AsyncGetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let! content = reader.AsyncReadToEnd()
    return content
}

let fetchGoogle () = async {
  let! content = fetchAsync "http://www.google.com.au"
  return content
}

// the above function was written to demonstrate
// this use of an async block. The function below is
// exactly the same (ie we didn't need the block in this case):
let fetchGoogle2 () = fetchAsync "http://www.google.com.au"

Examples.test "Can get google homepage" (fun () ->
    let content = fetchGoogle () |> Async.RunSynchronously
    content.Contains("Google")
)

let testPages = [
  ("Google", "http://www.google.com.au")
  ("Facebook", "http://facebook.com")
  ("GitHub", "http://github.com")
]

// start by retrieving the pages
// sequentially.
// can you use Async.Parallel to retrieve
// them in parallel?
let fetchAllPages
    (pages : list<(string * string)>)
    : Async<list<(string * string)>> =
    failwith "todo"

Examples.test "Can get all pages" (fun () ->
    let result =
        testPages
        |> fetchAllPages
        |> Async.RunSynchronously

    let checkContainsString state (site, _) =
        match state with
        | false -> false
        | true ->
            match result |> List.tryFind (fun (site',_) -> site' = site) with
            | Some (_, content) ->
                content.Contains (site)
            | None -> false

    testPages
    |> List.fold checkContainsString true
)
