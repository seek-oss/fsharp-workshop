#load "./examples.fs"
open Examples

// We've already used some basic pattern matching in the ADT exercises.
// Now we're going to explore more techniques we can use with match
// expressions.

//////////////// Guard clauses /////////////////////

// Let's write a function that tells us if an integer is positive, zero
// or negative.

let partition n =
  if n < 0
  then "negative"
  elif n > 0
  then "positive"
  else "zero"

// Nasty! Let's clean that up with some matching. We're going to need
// some guard clauses here.

let partition' n =
  match n with
  | x when x < 0 -> "negative"
  | x when x > 0 -> "positive"
  | _            -> "zero"

test "partitioning numbers 1" (fun _ ->
  partition' 3 = "positive"
)

test "partitioning numbers 2" (fun _ ->
  partition' 0 = "zero"
)

test "partitioning numbers 3" (fun _ ->
  partition' -5 = "negative"
)


// I hope you agree that's a whole lot more readable. The last line with
// the underscore is a wildcard match, it will match anything and discard
// the result. You may sometimes hear this referred to as a "catch all".


/////////////////// Fizz Buzz! ///////////////////////////

// In case you're not familiar with FizzBuzz, see
// http://blog.codinghorror.com/why-cant-programmers-program/
// The task is to write a program a program that prints the numbers from
// 1 to 100. But for multiples of three print "Fizz" instead of the
// number and for the multiples of five print "Buzz". For numbers which
// are multiples of both three and five print "FizzBuzz".

let first15 = [
  "1"
  "2"
  "Fizz"
  "4"
  "Buzz"
  "Fizz"
  "7"
  "8"
  "Fizz"
  "Buzz"
  "11"
  "Fizz"
  "13"
  "14"
  "FizzBuzz" ]

// See if you can write the body for the function 'fizzbuzz' below
// to pass the following test, using guard clauses.

let fizzbuzz n =
  match n with
  | x when x % 3 = 0 && x % 5 = 0 -> "FizzBuzz"
  | x when x % 3 = 0              -> "Fizz"
  | x when x % 5 = 0              -> "Buzz"
  | x                             -> string x

test "We can fizz buzz" (fun _ ->
  let result = [1 .. 15] |> List.map fizzbuzz
  result = first15
)

// There's another technique we can use to express the FizzBuzz logic
// in a more robust way. It involves a language feature called Active Patterns.
// See the bottom of this file for further reading.

let (|DivisibleBy|_|) m n = if n % m = 0 then Some DivisibleBy else None

// This might look a bit strange, until you see how we can use this to refactor
// fizzbuzz

let fizzbuzz' n =
  match n with
  | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
  | DivisibleBy 3                 -> "Fizz"
  | DivisibleBy 5                 -> "Buzz"
  | x                             -> string x


test "We can fizz buzz" (fun _ ->
  let result = [1 .. 15] |> List.map fizzbuzz
  result = first15
)


// Matching HTTP Status Codes with Active Patterns

// See http://httpstatus.es/ if you're rusty on your HTTP


let (|Informational|Success|Redirection|ClientError|ServerError|Invalid|) = function
  | x when x < 100 -> Invalid
  | x when x < 200 -> Informational           
  | x when x < 300 -> Success
  | x when x < 400 -> Redirection
  | x when x < 500 -> ClientError
  | x when x < 600 -> ServerError
  | _              -> Invalid

let logHttpStatusCode = function
  | ServerError        -> "Server error"
  | ClientError        -> "Client error"
  | Success            -> "Success"
  | Redirection        -> "Redirection"
  | _                  -> "Invalid"

test "404 is client error" (fun _ ->
  logHttpStatusCode 404 = "Client error"
)

test "200 is success" (fun _ ->
  logHttpStatusCode 200 = "Success"
)

test "500 is server error" (fun _ ->
  logHttpStatusCode 500 = "Server error"
)

test "302 is redirection" (fun _ ->
  logHttpStatusCode 302 = "Redirection"
)

// Stretch goal - Categorising our HTTP logs

let categorise codes =
  let inner = function
    | Success     -> Some "info"
    | ClientError
    | ServerError -> Some "error"
    | _           -> None
  codes
  |> Seq.choose inner
  |> Seq.groupBy id
  |> Map.ofSeq
  |> Map.map (fun _ codes -> Seq.length codes)

test "Categorising responses by code" (fun _ ->
  let input = [200; 200; 404; 200; 401; 404; 500; 500; 200; 200; 200]
  let expected = ["info", 6; "error", 5] |> Map.ofList
  categorise input = expected 
)








// The MSDN page for Active Patterns https://msdn.microsoft.com/en-us/library/dd233248.aspx 
// An interesting blog series on Active Patterns http://www.devjoy.com/series/active-patterns/