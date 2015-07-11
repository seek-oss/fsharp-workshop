open Suave
open Suave.Http.Successful
open Suave.Web
open Suave.Types
// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    startWebServer defaultConfig (OK "Hello World!")
    0 // return an integer exit code