#load "./examples.fs"
open Examples

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
  string n

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
  // TODO
  | x                             -> string x

// So as you can see, active patterns can take parameters and they can be
// combined with & and | They can be a very handy technique to use when you
// don't control the definition of a data type but you still want to build up a
// declarative set of terms to express your rules in.

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
  // TODO
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
  // TODO
  { Success = 0; Error = 0 }

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
