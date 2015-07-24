#load "./examples.fs"

open Examples

// Now you may have already realised that this is such
// a common concept that there is a type in the F# core library that we
// use to represent a data type that may have a value or may not.
// It is the 'option' type and there are a bunch of handy functions in the
// F# core that work with options. https://msdn.microsoft.com/en-us/library/ee370544.aspx
// The option type is defined like this (the 'a is a type parameter)

// type Option<'a> =
//    | None
//    | Some of 'a

// You can see for yourself in the F# source code here
// https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L3205

// Let's use this to model our domain again

type ContactDetails =
  | Email of string
  | Phone of int

type Person = { Name : string; ContactDetails : ContactDetails option }

let bob = { Name = "Bob"; ContactDetails = None }

let jim  = { Name = "Jim"; ContactDetails = Email "jim@example.org" |> Some }

// See if you can write the updated howToContact without referring back to the previous
// worksheet. You can write it all inside one function if you'd like, or split it out into
// two like we previously did.

let howToContact (person : Person) =
    //failwith "TODO"
    let printContactDetails = function
      | Email e -> sprintf "email address - %s" e
      | Phone p -> sprintf "phone number - %010d" p
    match person.ContactDetails with
    | None   -> sprintf "%s does not wish to be contacted" person.Name
    | Some d -> let contactDetails = printContactDetails d
                sprintf "%s can be contacted on %s" person.Name contactDetails


test "How to contact Jim" (fun _ ->
  howToContact jim = "Jim can be contacted on email address - jim@example.org"
)

test "How to contact Bob" (fun _ ->
  howToContact bob = "Bob does not wish to be contacted"
)
