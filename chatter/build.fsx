#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake

let solutionFile = "./Chatter.sln"
let buildChatter () =
    !! solutionFile
    |> MSBuildRelease "" "Build"
    |> ignore

Target "Build" (fun _ ->
    printfn "Building.."
    buildChatter()
)

Target "Clean" (fun _ ->
    !! solutionFile
    |> MSBuildRelease "" "Clean"
    |> ignore
)

let runChatter () =
    fireAndForget (fun startInfo ->
        startInfo.FileName <- "./Chatter.Server/bin/Release/Chatter.Server.exe"
        startInfo.WorkingDirectory <- "./Chatter.Server"
    )
    
Target "Watch" (fun _ ->
    buildChatter()

    use watcher = !! "Chatter.Server/**/*.*" |> WatchChanges (fun changes ->
        tracefn "%A" changes
        killAllCreatedProcesses ()

        buildChatter()

        runChatter ()
    )

    runChatter ()

    System.Console.ReadLine() |> ignore

    watcher.Dispose()
)

Target "Run" (fun _ ->
    runChatter ()
)

"Build"
    ==> "Run"

RunTargetOrDefault "Build"
