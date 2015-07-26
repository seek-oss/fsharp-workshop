namespace Chatter.Server
open Suave
open Suave.Http.Successful
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Web
open Suave.Types

module Main =
    [<EntryPoint>]
    let main argv =
        printfn "Current directory: %s"  <| System.IO.Directory.GetCurrentDirectory()

        let getPageState currentRoom =
          let serverState = ChatState.getState ()
          {
            PageState.Rooms = serverState.Rooms
            CurrentRoom = currentRoom
          }

        let mainPage currentRoom =
          getPageState currentRoom
          |> ChatPage.render
          |> toHtml

        choose [
            GET
                >>= path "/"
                >>= mainPage None
            GET
                >>= pathScan "/room/%s" (fun room -> mainPage (Some room))
            GET >>= pathScan "/static/%s" (fun s -> (file <| sprintf "./static/%s" s))
            NOT_FOUND "Not found"
        ]
        |> startWebServer {
            defaultConfig with 
                logger = Suave.Logging.Loggers.saneDefaultsFor Logging.LogLevel.Debug
        }

        0 // return an integer exit code
