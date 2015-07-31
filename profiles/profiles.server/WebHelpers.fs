namespace profiles.server
open System

open Suave
open Suave.Types

module WebHelpers =
    let saveJson (f : ('a -> SaveResult<'r>)) : WebPart =
        request (fun (r : HttpRequest) ->
            r.rawForm
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
        )
