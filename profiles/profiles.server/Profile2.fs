namespace profiles.server

open System

module Profile2 =
  [<CLIMutable>]
  type ProfileForm = {
    FirstName : string
    LastName : string
  }

  let requiredString name value =
    match value with
    | "" -> Errors [sprintf "%s is required" name]
    | _ -> Success value

  let persistProfile (form : ProfileForm) : SaveResult<ProfileForm> =
    let validatedFirstName = requiredString "Firstname" form.FirstName
    let validatedLastName = requiredString "Lastname" form.LastName

    match validatedFirstName, validatedLastName with
    | Success firstName, Success lastName ->
        Success { FirstName = firstName; LastName = lastName }
    | Errors errs, Success _ ->
        Errors errs
    | Success _, Errors errs ->
        Errors errs
    | Errors errs1, Errors errs2 ->
        Errors (errs1 @ errs2)
