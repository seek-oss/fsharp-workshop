#load "./examples.fs"
open Examples

(*********************************************************************************************************************)
// In this section we'll go through implementing some of the common list functions that you would have come across
// in the previous exercises and maybe a few others.
//
// Firstly, lets have a quick review of list comprehensions and introduce a twist to the 'yield' keyword. As you can
// see from below, list comprehensions can be quite complex including containing functions.
//
// Don't forget to send these expressions to the REPL to see the result.

let prime100 = [
  let isprime n =
    let rec check i =
      i > n/2 || (n % i <> 0 && check (i + 1))
    check 2

  for n in 2 .. 100 do
    if isprime n then
      yield n
]


// A useful pattern in functional languages is to use a recursive function in place of a loop - you'll see this
// repeated several times in the examples below. However, this would cause an annoyance as we'd have then use a
// for .. in expression to be able yield values, as you can see from the following example:

let fib25WithFor = [
  let rec loop i a b = [
    if i > 0 then
      yield b
      for x in loop (i-1) b (a+b) do
        yield x
  ]

  yield 0
  yield 1
  for x in loop 23 1 1 do
    yield x
]


// Compare this to a version using 'yield!' (called yield bang) that eliminates the need for the for .. in:

let fib25 = [
  let rec loop i a b = [
    if i > 0 then
      yield b
      yield! loop (i-1) b (a+b)
  ]

  yield! [ 0; 1 ]
  yield! loop 23 1 1
]

(*********************************************************************************************************************)
// While we're looking at the Fibonacci sequence, let's take a brief tangent and examine the typical way of calculating
// a specific Fibonacci number:

let rec fibr =
  function
  | 0 -> 0
  | 1 -> 1
  | n -> fibr (n-1) + fibr (n-2)

// Compared to the one below. Why do you think we may choose one over the other?

let fibl n =
  let rec loop a b =
    function
    | 0 -> b
    | i -> loop b (a+b) (i-1) 
  loop 0 1 (n-1)

// Ok, we now return to normal transmission...


(*********************************************************************************************************************)
// Taking inspiration from the above, write a list comprehension that returns pairs (tuples) of the first twenty five
// numbers  from the Fibonacci sequence
//
// eg. (0, 1), (1, 1), (1, 2), (2, 3), ...

// fibPairs: () -> (int * int) list
let fibPairs () = failwith "todo"

test "Create a list comprehension that returns pairs of the first 25 Fibonacci numbers" (fun () ->
  fibPairs () = (fib25 |> Seq.pairwise |> Seq.toList)
)


// Let's take a look at another sequence that depends on the previous value to calculate the current: triangular
// numbers.  A triangular number represents the number of objects that are needed to make up an equilateral triangle.
// To calculate it you take the previous number in the sequence and add the current iteration to it.
//
// eg.
//                                                          *
//                                       *                 * *
//                       *              * *               * * *
// 1 (1):  *    2 (3):  * *    3 (6):  * * *    4 (10):  * * * *

// triangle10: () -> int list
let triangle10 () = failwith "todo"

test "Create a list comprehension that calculates the first ten triangular numbers" (fun () ->
  triangle10 () = [1; 3; 6; 10; 15; 21; 28; 36; 45; 55]
)




// This time instead of generating a sequence, we're going to show how to walk through a list and apply an action
// to each item in the list. You'll also note that we're using a new keyword 'function', that creates a function
// with one parameter and immediately matches on it.

// walkList:  -> ()
let walkList list =
  let printer = function | Some x -> printf "%d, " x | _ -> printfn "end of list"
  let rec loop action =
    function
    | []      ->
      action None
    | x :: xs ->
      action (Some x)
      loop action xs
  loop printer list

walkList fib25
walkList prime100


// Using the above few functions as inspiration see if you can write a function to return the last item in a list.
// You'll need our common loop pattern and a simple match expression.

// last: 'a list -> a
let last list = failwith "todo"


test "Write a function to return the last item in a list" (fun () ->
  last fib25 = 46368 && last prime100 = 97
)



// This time we need to we're going to attempt to write a generic list map function that
// takes as input a function to be applied to each item in the list and yields the result.
//
// It's acceptable to return unit () when yielding items if you do not want to yield a value at that point.

// map: ('a -> 'b) -> 'a list -> 'b list
let map action list = failwith "todo"

test "Create a list map function" (fun () ->
  map (fun x -> x * 2) fib25 = List.map (fun x -> x * 2) fib25

  &&

  map (fun x -> x * x) prime100 = List.map (fun x -> x * x) prime100 
)


(*********************************************************************************************************************)
// Instead of mapping items in a list from one value to the next, write a filter function that takes a
// predicate and returns portion of a list based on that.
//
// filter: ('a -> bool) -> 'a list -> 'a list
let filter predicate list = failwith "todo"

test "Write a function that filters a list using the given predicate" (fun () ->
  filter (fun x -> x % 2 = 0) fib25 = List.filter (fun x -> x % 2 = 0) fib25

  &&

  filter (fun x -> x % 10 = 3) prime100 = List.filter (fun x -> x % 10 = 3) prime100
)


// Write a function that uses match expressions to returns pairs of items
//
// pairwise: ('a list) -> ('a * 'a) list
let pairwise list = failwith "todo"


test "Create a function that uses a match expression to return pairs of items from a list" (fun () ->
  pairwise fib25 = (Seq.pairwise fib25 |> Seq.toList)

  &&

  pairwise prime100 = (Seq.pairwise prime100 |> Seq.toList)
)

// Write a function can zip two lists of the same length together.
//
// eg. zip [ 1; 2; 3 ] [ 4; 5; 6] = [ (1, 4); (2, 5); (3, 6) ]
//
// zip: a' list -> 'b list -> (a' list
let zip list1 list2 = failwith "todo"

test "Write a function can zip two lists of the same length together" (fun () ->
  zip fib25 prime100 = List.zip fib25 prime100
)



// Write a function that can sum the integers in a list.
//
// sum: int list -> int
let sum list = failwith "todo"

test "Write a function that can sum the integers in a list" (fun () ->
  sum fib25 = List.sum fib25

  &&

  sum prime100 = List.sum prime100
)


// You can make the function numerically generic by declaring it with 'inline' and using a special function
// for a generic zero:
//
// let inline sum list =
//  ...
//  loop (LanguagePrimitives.GenericZero<'a>) list
//
// sum: 'a list -> 'a
let inline sum2 list = failwith "todo"


test "Write a function that can sum numeric values in a list" (fun () ->
  let floats = prime100 |> List.map (fun x -> (float x) / 100.0)
  let decimals = prime100 |> List.map (fun x -> (decimal x) / 100M)

  decimal (sum2 floats) = decimal (List.sum floats)

  &&

  sum2 decimals = List.sum decimals
)


// Lets extend the previous function into something more generic. This time it needs to take a function (the folder)
// that performs the action on the item and accumulated value (replacing the '+' in in sum function. Next argument
// is state, effecitively the initializing value (like the zero in sum), and finally the list of values.
//
// fold: ('state -> 'a -> 'state) -> 'state -> 'a list -> 'state
let fold folder state list = failwith "todo"


test "Write a function to fold a list into a single value" (fun () ->
  fold (+) 0 fib25 = List.fold (+) 0 fib25

  &&

  fold (-) 0 prime100 = List.fold (-) 0 prime100

  &&
    
  fold (fun acc x -> acc + (sprintf "%d " x)) "Fib: " fib25 =
    List.fold (fun acc x -> acc + (sprintf "%d " x)) "Fib: " fib25
)

// BONUS: Try rewriting both the integer version and generic version of sum using fold.
// 
// sum: int list -> int
let sum3 list = failwith "todo"

// sum: 'a list -> 'a
let inline sum4 list = failwith "todo"

test "Write a function that can sum the integers in a list using fold" (fun () ->
  sum3 fib25 = List.sum fib25

  &&

  sum3 prime100 = List.sum prime100
)


test "Write a function that can sum numeric values in a list using fold" (fun () ->
  let floats = prime100 |> List.map (fun x -> (float x) / 100.0)
  let decimals = prime100 |> List.map (fun x -> (decimal x) / 100M)

  decimal (sum4 floats) = decimal (List.sum floats)

  &&

  sum4 decimals = List.sum decimals
)



// Now lets rethink fold in a different way, this time to reduce a list down to a single value using the supplied
// reducer function, but unlike fold, there's no initial state passed in as it starts with the first two items
// in the list.
//
// reduce: ('a -> 'a -> 'a) -> 'a list -> 'a
let reduce reducer = failwith "todo"

 
test "Reduce a list down to a single value using the supplied function" (fun () ->
  reduce (*) prime100 = List.reduce (*) prime100

  &&

  reduce (-) prime100 = List.reduce (-) prime100

  &&

  reduce (+) ["a";"b";"c"] = List.reduce (+) ["a";"b";"c"]
)

// BONUS: Can you rewrite 'last' using reduce?
//
// last2: 'a list -> 'a
let last2 list = reduce (fun x y -> y) list

test "Write a function to return the last item of list using reduce" (fun () ->
  last2 fib25 = 46368

  &&

  last2 prime100 = 97
)


(*********************************************************************************************************************)
//    References:
//        Triangular number       - https://en.wikipedia.org/wiki/Triangular_number
//        Inline functions        - https://msdn.microsoft.com/en-us/library/Dd548047.aspx
//        GenericZero declaration - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L2398
//        Fibonacci sequence      - https://en.wikipedia.org/wiki/Fibonacci_number
(*********************************************************************************************************************)