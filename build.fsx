#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake

Target "Functions" (fun _ ->
    let result, output = FSIHelper.executeFSI "./exercises" "functions.fsx" [] //[("use", "functions.fsx")]
    for msg in output do
      printfn "%s" msg.Message
    ()
)

Target "Examples" (fun _ ->
    trace "Testing stuff..."
)

Target "BuildChatter" (fun _ ->
    !! "Chatter.sln"
    |> MSBuildRelease "" "Rebuild"
    |> ignore
)

Target "ChatterWatch" (fun _ ->
    use watcher = !! "Chatter.Server/**/*.*" |> WatchChanges (fun changes ->
        tracefn "%A" changes
        Run "BuildChatter"
    )

    System.Console.ReadLine() |> ignore

    watcher.Dispose()
)

"Functions"            // define the dependencies
   ==> "Examples"
"BuildChatter"
   ==> "ChatterWatch"

RunTargetOrDefault "BuildChatter"
