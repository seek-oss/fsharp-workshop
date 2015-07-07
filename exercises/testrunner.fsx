open System
open System.Reflection
let runTests (t : Type) =
  let tests =
    t.GetMethods(BindingFlags.Static ||| BindingFlags.Public)
    |> Array.filter(fun m -> m.Name.StartsWith("test_"))
  for test in tests do
    try
      let result = test.Invoke(null, [||]) :?> bool
      if result then
        printfn "%s Passed" test.Name
      else
        printfn "%s: Failed" test.Name
    with | :? System.Reflection.TargetInvocationException as e
              when e.InnerException.Message = "todo" ->
            printfn "%s: TODO" test.Name
         | e ->
            printfn "%s: Exception %A" test.Name e
