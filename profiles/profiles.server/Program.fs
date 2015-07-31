namespace profiles.server

open Suave
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Files
open Suave.Web

open profiles.server.WebHelpers
open Profile1

module Main =

  let suaveApp =
    choose
      [ GET >>= choose
         [ path "/" >>= file "./static/index.html"
           pathScan "/static/%s" (sprintf "./static/%s" >> file) ]
        PUT >>= path "/profile"
                >>= saveJson persistProfile
                >>= Writers.setMimeType "application/json" ]

  [<EntryPoint>]
  let main argv =
    startWebServer defaultConfig suaveApp
    0
