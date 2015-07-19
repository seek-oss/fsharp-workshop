(* Types are very terse and powerful in F# *)
#load "./examples.fs"

open Examples

// Discriminated Unions (DU) are used to represent data
// that can be one of a variety of choices. They are a bit
// like enums in other languages including C#, but they
// are more powerful.

type Colour =
  | Red
  | Blue
  | Yellow

// We create values of this type by calling one of the
// case labels as if it were a function
let stopSign = Red

// Look carefully at the type of this function.
let describeColour c =
  match c with
  | Blue   -> "It's blue"
  | Red    -> failwith "TODO"
  | Yellow -> "It's yellow"

test "Describing colours" (fun _ ->
  describeColour stopSign = "It's red"
)

// It's quite common to create functions whose entire body
// is a match expression and the final or only argument is
// the value that is matched upon, so there's some syntax
// sugar for this case.

let describeColour' = function
  | Blue   -> "It's blue"
  | Red    -> failwith "TODO"
  | Yellow -> "It's yellow"

// The second version of describeColour is exactly the same
test "Describing colours again" (fun _ ->
  describeColour' stopSign = "It's red"
)

// One way in which DUs differ from enums is in the fact that
// each case (or tag) in the DU can take data of any type

type ContactDetails =
  | Email of string
  | Phone of int

type Person = { Name : string; ContactDetails : ContactDetails }

let jim  = { Name = "Jim";  ContactDetails = Email "jim@example.org" }
let tess = { Name = "Tess"; ContactDetails = Phone 0411222333 }

// Let's write a function to show how we can contact a person
// We'll use a really handy function 'sprintf' which we can use
// to create a string from a format string and a variable number of arguments
// that are interpolated into the format string.
// See https://msdn.microsoft.com/en-us/library/ee370560.aspx
// and http://fsharpforfunandprofit.com/posts/printf/ for more detail

let printContactDetails = function
  | Email e -> sprintf "email address - %s" e
  | Phone p -> failwith "TODO" // sprintf "phone number - %d" p

test "Printing contact details" (fun _ ->
  printContactDetails tess.ContactDetails = "phone number - 0411222333"
)

// But what if we wanted to represent people who have no contact details?
// Try executing the following in FSI

let bob = { Name = "Bob"; ContactDetails = null }

// No nulls allowed here!
// So what do we do? Well, let's just expand our definition of ContactDetails

type ContactDetails' =
  | Email' of string
  | Phone' of int
  | Nothing

type Person' = { Name : string; ContactDetails : ContactDetails' }

let bob = { Name = "Bob"; ContactDetails = Nothing }

// That's better, but we could also model it like this

type MaybeContactDetails =
  | Nothing
  | Details of ContactDetails

type Person'' = { Name : string; ContactDetails : MaybeContactDetails }

let sam = {
  Name = "Sam"
  ContactDetails = Details (Email "sam@example.org")
  }

// We can now write a function to print out the details of a person
// using pattern matching

let howToContact (person : Person'') =
    match person.ContactDetails with
    | Nothing   -> sprintf "%s does not wish to be contacted." person.Name
    | Details d -> sprintf "%s can be contacted on %s." person.Name (printContactDetails d)

test "How to contact Sam" (fun _ ->
  howToContact sam = failwith "FILL ME IN"
)

// Now you may have already realised that this is such
// a common concept that there is a type in the F# core library that we
// use to represent a type that may have a value or may not.
// It is the 'option' type and there are a bunch of handy functions in the
// F# core that work with options. https://msdn.microsoft.com/en-us/library/ee370544.aspx

type Person''' = { Name : string; ContactDetails : ContactDetails option }

let howToContact' (person : Person''') =
  match person.ContactDetails with
  | None   -> sprintf "%s does not wish to be contacted." person.Name
  | Some d -> sprintf "%s can be contacted on %s." person.Name (printContactDetails d)
