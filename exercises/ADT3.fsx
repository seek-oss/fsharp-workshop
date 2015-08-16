#load "./examples.fs"
open Examples

// The need to model the absence of a value comes up quite often so let's separate
// out this concept of an absence of contact details from the actual values that
// are valid ContactDetails

type ContactDetails =
  | Email of string
  | Phone of int

type MaybeContactDetails =
  | Nothing
  | Details of ContactDetails

type Person = { Name : string; ContactDetails : MaybeContactDetails }

let bob = { Name = "Bob"; ContactDetails = Nothing }

// Now when we create Jim and Tess, we have to create the inner value first,
// then pass it to the outer constructor. We could also just use parens, but
// that's not as idiomatic.

let jim  = { Name = "Jim";  ContactDetails = Email "jim@example.org" |> Details }
let tess = { Name = "Tess"; ContactDetails = Details (Phone 0411222333) }

// Let's write a slightly more ambitious function now. Instead of just printing
// out the contact details, we'll include the person's name in the message too.
// Have a look at the tests below to see what we're going to write.

// You might find it handy to re-use our function from earlier.

let printContactDetails = function
  | _ -> failwith "todo"

// And we can write an outer function to handle the overall task of printing out a
// person's contact details. We have to provide a type hint for our person variable
// so the type inference engine knows that person has a field ContactDetails.
// Remember to be careful with your indentation inside the branches of the match
// expression.

let howToContact (person : Person) =
  failwith "todo"

test "How to contact Jim" (fun _ ->
  howToContact jim = "Jim can be contacted on email address - jim@example.org"
)

test "How to contact Tess" (fun _ ->
  howToContact tess = "Tess can be contacted on phone number - 0411222333"
)

test "How to contact Bob" (fun _ ->
  howToContact bob = "Bob does not wish to be contacted"
)

// Onwards to ADTs04 for more refinement
