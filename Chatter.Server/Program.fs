open Suave
open Suave.Http.Successful
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Web
open Suave.Types

[<EntryPoint>]
let main argv = 
    choose [
        GET >>= path "/" >>= file "./static/index.html"
        GET >>= pathScan "/static/%s" (fun s -> (file <| sprintf "./static/%s" s))
        NOT_FOUND "Not found"
    ]
    |> startWebServer defaultConfig

    0 // return an integer exit code