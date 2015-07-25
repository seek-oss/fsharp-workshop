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
    printfn "Current directory: %s"  <| System.IO.Directory.GetCurrentDirectory()

    choose [
        GET >>= path "/" >>= file "./static/index.html"
        GET >>= pathScan "/static/%s" (fun s -> (file <| sprintf "./static/%s" s))
        NOT_FOUND "Not found"
    ]
    |> startWebServer {
        defaultConfig with 
            logger = Suave.Logging.Loggers.saneDefaultsFor Logging.LogLevel.Debug
    }

    0 // return an integer exit code