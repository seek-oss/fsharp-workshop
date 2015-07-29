#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake
open Fake.Testing
open System

let solutionFile = "./profiles.sln"
let testAssemblies = "./profiles.tests/bin/Release/*tests*.dll"

let buildProfiles () =
    !! solutionFile
    |> MSBuildRelease "" "Build"
    |> ignore

Target "Build" (fun _ ->
    printfn "Building.."
    buildProfiles()
)

Target "Clean" (fun _ ->
    !! solutionFile
    |> MSBuildRelease "" "Clean"
    |> ignore
)

let runWeb () =
    fireAndForget (fun startInfo ->
        startInfo.WorkingDirectory <- "./profiles.server"
        if (EnvironmentHelper.isMono) then
            startInfo.FileName <- "mono"
            startInfo.Arguments <- "./bin/Release/profiles.server.exe"
        else
            startInfo.FileName <- "./profiles.server/bin/Release/profiles.server.exe"
    )

Target "RunTests" (fun _ ->
    !! (testAssemblies)
    |> xUnit2 id
)

Target "Watch" (fun _ ->
    buildProfiles()

    use watcher = !! "./profiles.server/*.fs" |> WatchChanges (fun changes ->
        tracefn "%A" changes
        killAllCreatedProcesses ()

        buildProfiles()

        runWeb ()
    )

    runWeb ()

    System.Console.ReadLine() |> ignore

    watcher.Dispose()
)

Target "Run" (fun _ ->
    runWeb ()
)

"Build"
    ==> "Run"

"Build"
    ==> "RunTests"

RunTargetOrDefault "Build"
