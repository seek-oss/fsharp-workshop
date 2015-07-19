#r "packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake

let solutionFile = "./Chatter.sln"
Target "Build" (fun _ ->
    printfn "Building.."
    !! solutionFile
    |> MSBuildRelease "" "Build"
    |> ignore
)

Target "Clean" (fun _ ->
    !! solutionFile
    |> MSBuildRelease "" "Clean"
    |> ignore
)

RunTargetOrDefault "Build"
