namespace profiles.server

open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Web

module Main =
  [<CLIMutable>]
  type ProfileForm = {
    Name : string
  }

  let asJson (f : ('a -> Choice<'r, string list>)) : WebPart =
    failwith "todo"

  let persistProfile (form : ProfileForm) =
    Choice2Of2 ["Didn't like it"]

  let suaveApp =
    choose
      [ GET >>= choose
         [ path "/" >>= file "./static/index.html"
           pathScan "/static/%s" (sprintf "./static/%s" >> file) ]
        PUT >>=
           path "/profile" >>= asJson persistProfile ]

  [<EntryPoint>]
  let main argv =
    startWebServer defaultConfig suaveApp
    0
