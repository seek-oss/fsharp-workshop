namespace profiles.server

open System

module Profile3 =
  [<CLIMutable>]
  type ProfileForm = {
    FirstName : string
    LastName : string
  }

  let apply (a : SaveResult<'a>) (f : SaveResult<'a -> 'b>) =
     match f, a with
      | Success f, Success a ->
        Success (f a)
      | Errors f, Success _ ->
        Errors f
      | Success _, Errors a ->
        Errors a
      | Errors f, Errors a ->
        Errors (f @ a)

  let requiredString name value =
    match value with
    | "" -> Errors [sprintf "%s is required" name]
    | _ -> Success value

  let mkValidatedForm firstName lastName : ProfileForm =
    { FirstName = firstName
      LastName = lastName }

  let persistProfile (form : ProfileForm) =
    let validatedFirstName = requiredString "Firstname" form.FirstName
    let validatedLastName = requiredString "Lastname" form.LastName

    (Success mkValidatedForm)
    |> apply validatedFirstName
    |> apply validatedLastName
