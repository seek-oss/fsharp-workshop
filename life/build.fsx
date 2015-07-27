#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake

let solutionFile = "./Life.sln"
let buildLife () =
    !! solutionFile
    |> MSBuildRelease "" "Build"
    |> ignore

Target "Build" (fun _ ->
    printfn "Building.."
    buildLife()
)

Target "Clean" (fun _ ->
    !! solutionFile
    |> MSBuildRelease "" "Clean"
    |> ignore
)

let runChatter () =
    fireAndForget (fun startInfo ->
        startInfo.FileName <- "./Life.Server/bin/Release/Life.Server.exe"
        startInfo.WorkingDirectory <- "./Life.Server"
    )
    
Target "Watch" (fun _ ->
    buildLife()

    use watcher = !! "Life.Server/**/*.*" |> WatchChanges (fun changes ->
        tracefn "%A" changes
        killAllCreatedProcesses ()

        buildLife()

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
