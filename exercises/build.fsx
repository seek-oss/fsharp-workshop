#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake

Target "Functions" (fun _ ->
    let result, output = FSIHelper.executeFSI "." "functions.fsx" [] //[("use", "functions.fsx")]
    for msg in output do
      printfn "%s" msg.Message
    ()
)

Target "Async" (fun _ ->
    let result, output = FSIHelper.executeFSI "." "async.fsx" [] //[("use", "functions.fsx")]
    for msg in output do
      printfn "%s" msg.Message
    ()
)

Target "Examples" (fun _ ->
    trace "Testing stuff..."
)

Target "Deploy" (fun _ ->
    trace "Heavy deploy action"
)

"Functions"            // define the dependencies
   ==> "Examples"
"Async"
   ==> "Examples"

RunTargetOrDefault "Examples"
