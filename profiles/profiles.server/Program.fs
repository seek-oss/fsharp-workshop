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
  let withJson (f : ('a -> SaveResult<'r>)) (request : HttpRequest) : WebPart =
    request.rawForm
    |> JsonSerializer.deserialize
    |> f
    |> function
    | Success result ->
        result
        |> JsonSerializer.serialize
        |> Suave.Http.Response.response HTTP_200
    | Errors errors ->
        errors
        |> JsonSerializer.serialize
        |> Suave.Http.Response.response HTTP_400

  let suaveApp =
    choose
      [ GET >>= choose
         [ path "/" >>= file "./static/index.html"
           pathScan "/static/%s" (sprintf "./static/%s" >> file) ]
        PUT >>= path "/profile"
                >>= request (withJson persistProfile)
                >>= Writers.setMimeType "application/json" ]

  [<EntryPoint>]
  let main argv =
    startWebServer defaultConfig suaveApp
    0
