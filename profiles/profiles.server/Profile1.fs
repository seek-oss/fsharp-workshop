namespace profiles.server

open System

module Profile1 =
  [<CLIMutable>]
  type ProfileForm = {
    Firstname : string
    Lastname : string
    Description : string
    Postcode : string
  }

  // Set stores each unique value once.
  // https://msdn.microsoft.com/en-us/library/ee340244.aspx
  let badWords : Set<string> =
    ["bad"; "naughty"]
    |> Set.ofList

  let requiredString name value errors =
    match value with
    | "" -> (sprintf "%s is required" name)::errors
    | _ -> errors

  let validatePostcode name (value : string) errors =
      let checks = [
        value.Length = 4 || value.Length = 0
        Seq.forall Char.IsDigit value
      ]

      if checks |> Seq.forall id then
        errors
      else
        (sprintf "%s must be 4 digits" name)::errors

  let validateNoBadWords name (value : string) errors =
    let hasBadWords =
        value.Split(' ')
        |> Seq.map (fun x -> x.ToLower())
        |> Seq.map (fun x -> x |> Seq.filter Char.IsLetter |> String.Concat)
        |> Seq.exists (fun word -> Set.contains word badWords)
    if hasBadWords then
        (sprintf "%s must not contain bad words" name)::errors
    else
        errors

  let persistProfile (form : ProfileForm) : SaveResult<string> =
    let errors =
        []
        |> requiredString "Firstname" form.Firstname
        |> requiredString "Lastname" form.Lastname
        |> validateNoBadWords "Description" form.Description
        |> validatePostcode "Postcode" form.Postcode

    match errors with
    | [] ->
        Success (DataStore.save form)
    | errors ->
        Errors (List.rev errors)
