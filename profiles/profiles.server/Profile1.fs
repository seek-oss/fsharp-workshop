namespace profiles.server

open System

module Profile1 =
  [<CLIMutable>]
  type ProfileForm = {
    Firstname : string
    Lastname : string
    Postcode : string
  }

  let requiredString name getField form errors =
    match getField form with
    | "" -> (sprintf "%s is required" name)::errors
    | _ -> errors

  let persistProfile (form : ProfileForm) : SaveResult<string> =
    let errors =
        []
        |> requiredString "Firstname" (fun x -> x.FirstName) form
        |> requiredString "Lastname" (fun x -> x.LastName) form

    match errors with
    | [] ->
        Success (DataStore.save form)
    | errors ->
        Errors (List.rev errors)
