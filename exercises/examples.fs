module Examples
  let test name f =
    try
      match f () with
      | true -> printf "%s: passed" name
      | false -> printf "%s: failed" name
    with
      | e when e.Message = "todo" ->
        printf "%s: TODO" name
      | e ->
        printf "%s: Exception %A" name e
