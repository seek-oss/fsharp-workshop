namespace profiles.server

open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Web

open Profile1

module Main =
  let asJson (f : ('a -> Choice<'r, string list>)) (request : HttpRequest) : WebPart =
    request.rawForm
    |> JsonSerializer.deserialize
    |> f
    |> function
    | Choice1Of2 result ->
        result
        |> JsonSerializer.serialize
        |> Suave.Http.Response.response HTTP_200
    | Choice2Of2 errors ->
        errors
        |> JsonSerializer.serialize
        |> Suave.Http.Response.response HTTP_400


  let suaveApp =
    choose
      [ GET >>= choose
         [ path "/" >>= file "./static/index.html"
           pathScan "/static/%s" (sprintf "./static/%s" >> file) ]
        PUT >>= path "/profile"
                >>= request (asJson persistProfile)
                >>= Writers.setMimeType "application/json" ]

  [<EntryPoint>]
  let main argv =
    startWebServer defaultConfig suaveApp
    0
