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

  let persistProfile (form : ProfileForm) : SaveResult<string> =
    let errors =
        []
        |> requiredString "Firstname" form.Firstname

    match errors with
    | [] ->
        Success (DataStore.save form)
    | errors ->
        Errors (List.rev errors)
