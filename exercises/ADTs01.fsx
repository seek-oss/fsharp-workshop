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

// Let's suppose we want to write a function of type
// Colour -> string, i.e., it takes a Colour and returns
// a string describing that colour. The most natural way
// of working with union types is by using a 'match'
// expression.

let describeColour c =
  match c with
  | Blue   -> "It's blue"
  | Red    -> failwith "todo"
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
  | Red    -> failwith "todo"
  | Yellow -> "It's yellow"

// The second version of describeColour is exactly the same
test "Describing colours again" (fun _ ->
  describeColour' stopSign = "It's red"
)

// A powerful feature of match expressions is that the F# compiler
// can tell you if you have forgotten to cover some cases! Delete
// one of the cases from our definition of describeColour above
// and execute it again in FSI. Do you see a warning that starts
// like this:
// warning FS0025: Incomplete pattern matches on this expression.
// How handy! F# is able to see that we've not covered all cases.


//////////// Discriminated Unions with data //////////////////////

// One way in which DUs differ from enums is in the fact that each case (or tag)
// in the DU can take data of any type. For the rest of the ADT exercises we're
// going to work with a domain model for people and their contact details.

// Suppose we wish every person to have one contact detail, either an email
// address or a phone number. We're not going to get into email address and
// phone number validation, it's not necessary for this exercise, so let's
// ignore that and just assume that a string is sufficient for storing email
// addresses, and likewise an int for a phone number.

type ContactDetails =
  | Email of string
  | Phone of int

// A person has a Name which we will represent as a string AND they have contact
// details wich uses our DU defined above.

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
  | Phone p -> sprintf "phone number - %010d" p

test "Printing contact details" (fun _ ->
  printContactDetails jim.ContactDetails = failwith "todo"
)

test "Printing contact details #2" (fun _ ->
  printContactDetails tess.ContactDetails = failwith "todo"
)

// A nice feature of DUs and Records in F# is that we get structural equality
// for free.

test "Are Jim and Tess the same?" (fun _ ->
  let areEqual = jim = tess
  areEqual = failwith "todo"
)

test "Can we compare Jim to himself?" (fun _ ->
  let areEqual = jim = { Name = "Jim"; ContactDetails = Email "jim@example.org"}
  areEqual = failwith "todo"
)

let phone1 = Phone 91234567
let phone2 = Phone 99999999
let phone3 = Phone 91234567

test "Compare phone1 and phone2" (fun _ ->
  let areEqual = phone1 = phone2
  areEqual = failwith "todo"
)

test "Compare phone1 and phone3" (fun _ ->
  let areEqual = phone1 = phone3
  areEqual = failwith "todo"
)

// So we can see that the values are equal if they are of the same case
// AND the values for that case are equal too.

// Now what if we wanted to represent people who have no contact details?
// We've got a user, Bob, who wants to sign up in our system but he doesn't want
// to give his contact details. What can we do?

// Try uncommenting the following line or executing it in FSI

// let bob = { Name = "Bob"; ContactDetails = null }

// No nulls allowed here!
// So what do we do? Continue to ADTs02.fsx to find out

