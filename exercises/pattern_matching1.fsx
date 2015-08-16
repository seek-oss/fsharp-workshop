#load "./examples.fs"
open Examples

// We've already used some basic pattern matching in the ADT exercises.
// Now we're going to explore more techniques we can use with match
// expressions.

///////////////////// Guard clauses /////////////////////

// Let's write a function that tells us if an integer is positive, zero
// or negative.

let partition n =
  if n < 0
  then "negative"
  elif n > 0
  then "positive"
  else "zero"

test "partitioning numbers 1" (fun _ ->
  partition 1 = "positive"
)

// So it works, but it's pretty nasty! If expressions like that are
// difficult to read, and easy to overlook missing cases.
// Let's clean that up with some matching. We're going to need
// some guard clauses here.

let partition' n =
  match n with
  | x when x < 0 -> "negative"
  | x when x > 0 -> "positive"
  | _            -> "zero"

// I hope you agree that's a whole lot more readable. The last line with
// the underscore is a wildcard match, it will match anything and discard
// the result. You may sometimes hear this referred to as a "catch all".

test "partitioning numbers 1" (fun _ ->
  partition' 3 = "positive"
)

test "partitioning numbers 2" (fun _ ->
  partition' 0 = "zero"
)

test "partitioning numbers 3" (fun _ ->
  partition' -5 = "negative"
)

// For a full description of the syntax for match expressions you can read
// the MSDN docs here https://msdn.microsoft.com/en-us/library/dd547125.aspx

// We can match deep inside data types and look for just the parts we
// care about. Here are a few examples:

//////////////////// Matching tuples ////////////////////

let thirdElementIsEven n =
  match n with
  | _,_,x when x % 2 = 0 -> true
  | _                    -> false

test "pattern matching into tuples 1" (fun _ ->
  thirdElementIsEven ("a", 14.3, 2)
)

// We can actually use pattern matching when declaring the arguments to
// a function, and in this case it actually removes quite a lot of clutter

let thirdElementIsEven' (_,_,n) = n % 2 = 0

test "pattern matching into tuples 2" (fun _ ->
  thirdElementIsEven' ("a", 14.3, 2)
)

//////////////////// Matching records ////////////////////

// Let's imagine a simple postal system, where all items must not exceed 90cm
// in any dimension nor 2kg in mass. Postage for items within these limits is
// calculated at $0.01/g. In addition, items that are taller than 50cm incur an
// additional charge of $5.

type DimensionsInMetres  = { Height : decimal; Base : decimal * decimal }
type PostageSatchel = { Dimensions: DimensionsInMetres ; MassInGrams : decimal }

type PostagePrice = | Dollars of decimal
type PostageError = | TooBig | TooHeavy

type PostagePriceCalculator = PostageSatchel -> Choice<PostagePrice, PostageError>

// Feel free to remove any of the partial implementation below when completing your
// calculatePostage function, but you may find these pieces useful.

let calculatePostage satchel =
  let costPerGram = 0.01M
  let maximumSizeInMetres = 0.9M
  let maximumMassInGrams = 2000M

  let tooBig { Height = y; Base = x,z } = [x;y;z] |> List.forall (fun n -> n < maximumSizeInMetres) |> not
  let tooHeavy m     = m > maximumMassInGrams

  match satchel with
  | { Dimensions = d }  when d |> tooBig                           -> TooBig   |> Choice2Of2
  | { MassInGrams = m } when m |> tooHeavy                         -> TooHeavy |> Choice2Of2
  | { MassInGrams = m; Dimensions = { Height = y } } when y > 0.5M -> (m * costPerGram) + 5M |> Dollars |> Choice1Of2
  | { MassInGrams = m }                                            -> m * costPerGram |> Dollars |> Choice1Of2

test "Calculating postage 1" (fun _ ->
  { Dimensions = { Height = 0.12M; Base = 0.1M, 0.1M }; MassInGrams = 700M }
  |> calculatePostage = Choice1Of2 (Dollars 7M)
)

test "Calculating postage 2" (fun _ ->
  { Dimensions = { Height = 0.2M; Base = 0.2M, 0.02M }; MassInGrams = 1200M }
  |> calculatePostage = Choice1Of2 (Dollars 12M)
)

test "Calculating postage - too big" (fun _ ->
  { Dimensions = { Height = 2M; Base = 0.2M, 0.2M }; MassInGrams = 200M }
  |> calculatePostage = Choice2Of2 (TooBig)
)

test "Calculating postage - too heavy" (fun _ ->
  { Dimensions = { Height = 0.2M; Base = 0.2M, 0.2M }; MassInGrams = 2001M }
  |> calculatePostage = Choice2Of2 (TooHeavy)
)

test "Calculating postage - height fee" (fun _ ->
  { Dimensions = { Height = 0.6M; Base = 0.5M, 0.5M }; MassInGrams = 1400M }
  |> calculatePostage = Choice1Of2 (Dollars 19M)
)

