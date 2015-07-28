namespace Life.Server
open Newtonsoft.Json.FSharp
open Newtonsoft.Json
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
    let jsonSerializer = JsonSerializer.Create (JsonSerializerSettings() |> Newtonsoft.Json.FSharp.Serialisation.extend)
    let deserialise<'t> (data:byte array) : 't =
        use ms = new MemoryStream(data)
        use jsonReader = new JsonTextReader(new StreamReader(ms))
        jsonSerializer.Deserialize<'t>(jsonReader)

    [<EntryPoint>]
    let main argv =
        printfn "Current directory: %s"  <| System.IO.Directory.GetCurrentDirectory()

        let deadSparkCoil = {
            Pattern.Name = "Dead Spark Coil"
            RLE =
                "#N Dead spark coil\r\n\
                #C An 18-cell still life.\r\n\
                #C http://www.conwaylife.com/wiki/index.php?title=Dead_spark_coil\r\n\
                x = 7, y = 5, rule = B3/S23\r\n\
                2o3b2o$obobobo$2bobo2b$obobobo$2o3b2o!"
        }

        let acorn = {
            Pattern.Name = "Acorn"
            RLE =
                "#N Acorn\r\n\
                 #O Charles Corderman\r\n\
                 #C A methuselah with lifespan 5206.\r\n\
                 #C www.conwaylife.com/wiki/index.php?title=Acorn\r\n\
                 x = 7, y = 3, rule = B3/S23\r\n\
                 bo5b$3bo3b$2o2b3o!"
        }
        let startState = { ServerState.Patterns = [acorn; deadSparkCoil]}
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
                >>=Types.request(fun r ->
                      let (board : BoardState) = deserialise r.rawForm
                      Game.getNextBoard board
                      |> Successful.OK )
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
