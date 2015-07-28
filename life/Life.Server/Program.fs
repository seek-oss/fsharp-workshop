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
                >>= pathScan "/pattern/%s"
                    (fun s ctx -> async {
                        let name = System.Web.HttpUtility.UrlDecode s
                        let (rle : string) = 
                            ctx.request.rawForm
                            |> JSON.deserialise
                            |> (fun (x : BoardState) -> List.toArray2D false x.grid)
                            |> RLE.encodeWithHeader

                        let pattern : Pattern = {
                            Name = name
                            RLE = rle
                        }

                        do! dataStore |> DataStore.addPattern pattern |> Async.Ignore
                      
                        return! OK "ok" ctx
                    })
            GET
                >>= pathScan "/pattern/%s"
                    (fun s ctx -> async {
                        printfn "Finding pattern %s" s
                        let s' = System.Web.HttpUtility.UrlDecode s
                        let! state = dataStore |> DataStore.getState
                        match state.Patterns |> List.tryFind (fun x -> x.Name = s') with
                        | None -> return None
                        | Some pattern ->
                            return! OK pattern.RLE ctx
                    })
            NOT_FOUND "Not found"
        ]
        |> startWebServer {
            defaultConfig with 
                logger = Suave.Logging.Loggers.saneDefaultsFor Logging.LogLevel.Debug
        }

        0 // return an integer exit code