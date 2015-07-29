namespace Life.Server
open Suave
open Suave.Http.Successful
open Suave.Http
open Suave.Json
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Web
open Suave.Types
open System.IO

module Main = 

    [<EntryPoint>]
    let main argv =
        printfn "Current directory: %s"  <| System.IO.Directory.GetCurrentDirectory()
        let startState = { ServerState.Patterns = PatternSamples.all }
        let dataStore = DataStore.create startState
        choose [
            GET 
                >>= path "/" 
                >>= (fun ctx -> async {
                        let! state = dataStore |> DataStore.getState
                        let content = Page.render state
                        return! toHtml content ctx
                    })
            GET >>= pathScan "/static/%s" (fun s -> (file <| sprintf "./static/%s" s))
            POST
                >>= path "/getNext"
                >>= Types.request(fun r ->
                      let (board : BoardState) = JSON.deserialise r.rawForm
                      let grid = List.toArray2D false board.grid
                      let result = Game.computeNext grid
                      JSON.serialize result |> Successful.OK )
            PUT
                // todo save the pattern
                >>= pathScan "/pattern/%s" (fun s -> OK "ok") 
            GET
                // todo load the pattern
                >>= pathScan "/pattern/%s" (fun s -> OK "ok")
            NOT_FOUND "Not found"
        ]
        |> startWebServer {
            defaultConfig with 
                logger = Suave.Logging.Loggers.saneDefaultsFor Logging.LogLevel.Debug
        }

        0 // return an integer exit code