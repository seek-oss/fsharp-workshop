#load "./examples.fs"

open Examples

// We'd like to expand our types to be able to represent people
// who don't have any contact details. So let's just expand our
// definition of ContactDetails

type ContactDetails =
  | Email of string
  | Phone of int
  | Nothing

type Person = { Name : string; ContactDetails : ContactDetails }

// Let's create bob and sam again

let bob  = { Name = "Bob"; ContactDetails = Nothing }
let jim  = { Name = "Jim"; ContactDetails = Email "jim@example.org" }

// And let's make a function to print their contact details

let printContactDetails = function
  | Email e -> failwith "todo"
  | Phone p -> sprintf "phone number - %010d" p
  | Nothing -> sprintf "no contact details found"

test "Printing contact details #2" (fun _ ->
  printContactDetails jim.ContactDetails = "email address - jim@example.org"
)

// This is better, now we can properly model people like bob in our
// system. Let's see if we can refine this a bit further in ADTs03.fsx
