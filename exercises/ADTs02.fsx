#load "./examples.fs"

open Examples

// We'd like to expand our types to be able to represent people who don't have any
// contact details. So let's just expand our definition of ContactDetails.

type ContactDetails =
  | Email of string
  | Phone of int
  | Nothing

type Person = { Name : string; ContactDetails : ContactDetails }

let jim  = { Name = "Jim";  ContactDetails = Email "jim@example.org" }
let tess = { Name = "Tess"; ContactDetails = Phone 0411222333 }

// Now we can add Bob to our system!

let bob  = { Name = "Bob";  ContactDetails = Nothing }

// And let's make a new function to print their contact details

let printContactDetails = function
  // Remove the following line and complete the function
  | _ -> failwith "todo"

test "Printing contact details #1" (fun _ ->
  printContactDetails jim.ContactDetails = "email address - jim@example.org"
)

test "Printing contact details #2" (fun _ ->
  printContactDetails bob.ContactDetails = "no contact details found"
)

test "Printing contact details #3" (fun _ ->
  printContactDetails tess.ContactDetails = "phone number - 0411222333"
)

// This is better, now we can properly model people like Bob in our
// system. Let's see if we can refine this a bit further in ADTs03.fsx

