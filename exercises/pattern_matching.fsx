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

// Let's imagine a simple postal system, where all items must not exceed 30cm
// in any dimension nor 2kg in mass. Postage for items within these limits is
// calculated at $0.01/g

type PostageSatchel = { DimensionsInMetres : decimal * decimal * decimal; MassInGrams : decimal }

type PostagePrice =
  | Dollars of decimal
  | TooBig
  | TooHeavy

// Feel free to remove any of the partial implementation below when completing your
// calculatePostage function, but you may find these pieces useful.

let calculatePostage satchel =
  let costPerGram = 0.01M
  let maximumSizeInMetres = 0.3M
  let maximumMassInGrams = 2000M

  let anyExceedSize (dimensions : decimal list) =
    not <| List.forall (fun n -> n < maximumSizeInMetres) dimensions

  match satchel with
  | { DimensionsInMetres = x,y,z; MassInGrams = _ } when [x;y;z] |> anyExceedSize
      -> TooBig 
  | { DimensionsInMetres = _;     MassInGrams = m } when m > maximumMassInGrams
      -> TooHeavy
  | { DimensionsInMetres = _;     MassInGrams = m } -> m * costPerGram |> Dollars

test "Calculating postage 1" (fun _ ->
  calculatePostage { DimensionsInMetres = 0.12M, 0.1M, 0.1M; MassInGrams = 700M } = Dollars 7M
)

test "Calculating postage 2" (fun _ ->
  calculatePostage { DimensionsInMetres = 0.2M, 0.2M, 0.02M; MassInGrams = 1200M } = Dollars 12M
)

test "Calculating postage 3" (fun _ ->
  calculatePostage { DimensionsInMetres = 1M, 0.2M, 0.02M; MassInGrams = 200M } = TooBig
)


//////////////////// Fizz Buzz! ////////////////////

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

test "We can fizz buzz 1" (fun _ ->
  let result = [1 .. 15] |> List.map fizzbuzz
  result = first15
)

// There's another technique we can use to express the FizzBuzz logic
// in a more robust way. It involves a language feature called Active Patterns.
// See the following for further information on Active Patterns
// The MSDN page for Active Patterns https://msdn.microsoft.com/en-us/library/dd233248.aspx 
// An interesting blog series on Active Patterns http://www.devjoy.com/series/active-patterns/

let (|DivisibleBy|_|) m n = if n % m = 0 then Some DivisibleBy else None

// This might look a bit strange, until you see how we can use this to refactor
// fizzbuzz

let fizzbuzz' n =
  match n with
  | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
  | DivisibleBy 3                 -> "Fizz"
  | DivisibleBy 5                 -> "Buzz"
  | x                             -> string x

// So as you can see, active patterns can take parameters and they can be combined with & and |
// They can be a very handy technique to use when you don't control the definition of a data type
// but you still want to build up a declarative set of terms to express your rules in.

test "We can fizz buzz 2" (fun _ ->
  let result = [1 .. 15] |> List.map fizzbuzz'
  result = first15
)


/////////// Matching HTTP Status Codes with Active Patterns ///////////

// Now we're going to move from FizzBuzz towards a more practical example of pattern
// matching; workign with HTTP response data.
// See http://httpstatus.es/ if you're rusty on your HTTP

let (|Informational|Success|Redirection|ClientError|ServerError|Invalid|) = function
  | x when x < 100 -> Invalid
  | x when x < 200 -> Informational           
  | x when x < 300 -> Success
  | x when x < 400 -> Redirection
  | x when x < 500 -> ClientError
  | x when x < 600 -> ServerError
  | _              -> Invalid

// We can use the above Total Active Pattern to build up other functions, e.g.

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

// That was very basic, now let's try a more difficult one; categorising our HTTP logs

// Let's suppose that we've been asked to give some very basic statistics on our HTTP
// logs. We'd like to provide a function that can take a list of HTTP response status
// codes and return the total number of successful responses (in the 200 range) and
// the total number of error responses (in the 400 or 500 range)

type HttpTotals = { Success : int; Error : int }

let categorise codes =
  let folder acc = function
    | ClientError
    | ServerError -> { acc with Error = acc.Error + 1 }
    | Success     -> { acc with Success = acc.Success + 1 }
    | _           -> acc
  codes
  |> Seq.fold folder { Success = 0; Error = 0 }

test "Categorising HTTP responses by status code 1" (fun _ ->
  [200; 200; 404; 200; 401; 404; 500; 500; 200; 200; 200]
  |> categorise = { Success = 6; Error = 5 }
)

test "Categorising HTTP responses by status code 2" (fun _ ->
  categorise [] = { Success = 0; Error = 0 } 
)

test "Categorising HTTP responses by status code 3" (fun _ ->
  let input = [100; 200; 401; 404; 302; 500; 500; 200; 200; 200]
  categorise input = { Success = 4; Error = 4 } 
)

