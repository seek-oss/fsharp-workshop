namespace profiles.server

open System

module Profile1 =
  [<CLIMutable>]
  type ProfileForm = {
    Firstname : string
    Lastname : string
    Postcode : string
  }

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

  let persistProfile (form : ProfileForm) : SaveResult<string> =
    let errors =
        []
        |> requiredString "Firstname" form.Firstname
        |> requiredString "Lastname" form.Lastname
        |> validatePostcode "Postcode" form.Postcode

    match errors with
    | [] ->
        Success (DataStore.save form)
    | errors ->
        Errors (List.rev errors)
