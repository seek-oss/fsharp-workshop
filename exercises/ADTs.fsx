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

// Look carefully at the type of this function.
let describeColour c =
  match c with
  | Blue   -> "It's blue"
  | Red    -> failwith "TODO"
  | Yellow -> "It's yellow"

test "Describing colours" (fun _ ->
  describeColour Red = "It's red"
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
  describeColour' Red = "It's red"
)

