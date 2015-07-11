module Examples
  let test name f =
    try
      match f () with
      | true -> printfn "%s: passed" name
      | false -> printfn "%s: failed" name
    with
      | e when e.Message = "todo" ->
        printfn "%s: TODO" name
      | e ->
        printfn "%s: Exception %A" name e
