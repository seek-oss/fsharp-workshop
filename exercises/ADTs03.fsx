#load "./examples.fs"

open Examples

// The need to model the absence of a value comes up quite often
// so let's separate out this concept of an absence of contact details
// from the actual values that are valid ContactDetails

type ContactDetails =
  | Email of string
  | Phone of int

type MaybeContactDetails =
  | Nothing
  | Details of ContactDetails

type Person = { Name : string; ContactDetails : MaybeContactDetails }

let bob = { Name = "Bob"; ContactDetails = Nothing }

// Now when we create Jim, we have to create the inner value first,
// then pass it to the outer constructor.

let jim  = { Name = "Jim"; ContactDetails = Email "jim@example.org" |> Details }

// Now we can re-use our original defition of printContactDetails
// from section 1

let printContactDetails = function
  | _ -> failwith "todo"

// And we can write an outer function to handle the overall task of
// printing out a person's contact details. We have to provide a type
// hint for our person variable so the type inference engine knows
// that person has a field ContactDetails. Remember to be careful with
// your indentation inside the branches of the match expression.

let howToContact (person : Person) =
    match person.ContactDetails with
    | Nothing   -> failwith "todo"
    | Details d -> failwith "todo"

test "How to contact Jim" (fun _ ->
  howToContact jim = "Jim can be contacted on email address - jim@example.org"
)

test "How to contact Bob" (fun _ ->
  howToContact bob = "Bob does not wish to be contacted"
)
// Onwards to ADTs04 for more refinement
