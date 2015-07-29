namespace profiles.server

open System

module Profile1 =
  [<CLIMutable>]
  type ProfileForm = {
    FirstName : string
    LastName : string
  }

  let requiredString name value =
    match value with
    | "" -> Choice2Of2 [sprintf "%s is required" name]
    | _ -> Choice1Of2 value

  let persistProfile (form : ProfileForm) =
    let validatedFirstName = requiredString "Firstname" form.FirstName
    let validatedLastName = requiredString "Lastname" form.LastName

    match validatedFirstName, validatedLastName with
    | Choice1Of2 firstName, Choice1Of2 lastName ->
        Choice1Of2 "Saved"
    | Choice2Of2 errs, Choice1Of2 _ ->
        Choice2Of2 errs
    | Choice1Of2 _, Choice2Of2 errs ->
        Choice2Of2 errs
    | Choice2Of2 errs1, Choice2Of2 errs2 ->
        Choice2Of2 (errs1 @ errs2)
