(**********************************************************************************************************************
    Lists are a big part of functional programming langages and F# is no exception. You may be familar with List<T>
    in C# however F# lists are quite different, essentially being based on good old dependable singly-linked lists (but
    using tuples) whereas C# lists use arrays.

    A naive implementation of an F# list could look like:

    type List<'T> =
    | ([]) of List<'T>
    | (::) of Head: 'T * Tail: List<'T>

    Head and tail.

    Explain tuples

    In F# an empty list is represented by [] and cons by the :: operator
*)

#load "./examples.fs"
open Examples


(**********************************************************************************************************************
    Let's see some examples for constructing lists - copy the following into the REPL to see the result.
*)

let empty         = []

let madeWithCons  = 1 :: 2 :: 3 :: 4 :: []

let explicit      = [ 1; 2; 3; 4 ]

let multiline     = [
                      1
                      2
                      3
                      4
                    ]

let range         = [ 1 .. 10 ]

let rangeWithSkip = [ 1 .. 2 .. 10 ]

let alphaRange    = [ 'a' .. 'z' ]

let floatRange    = [ 0.1 .. 0.1 .. 1.0 ]


(**********************************************************************************************************************
    You can append to a list using the '@' symbol, however this iterates through the items in the first list before
    it can return the newly combined list.
*) 


let appendedList  = [ 1; 2; 3 ] @ [ 4; 5; 6 ]

(**********************************************************************************************************************
    Another way to create lists is through list comprehensions.
*) 


let forLoop = [ for i in 1 .. 10 -> i * 2 ]

// another example of for using do and yield.

// some basic examples using List module functions with LINQ comparisons

(**********************************************************************************************************************
    They can also contain functions and be fairly complex. Use F# Interactive to see the result of the list
    comprehensions below
*)

let fib25 = [
    let rec loop i a b = [
      if i > 0 then
        yield b
        yield! loop (i-1) b (a+b)
    ]

    yield! [ 0; 1 ]
    yield! loop 23 1 1
  ]

let prime100 = [
    let isprime n =
      let rec check i =
        i > n/2 || (n % i <> 0 && check (i + 1))
      check 2

    for n in 2 .. 100 do
      if isprime n then
        yield n
  ]


(**********************************************************************************************************************
    Write a list comprehension that returns pairs of the first twenty five numbers from the Fibonacci sequence

    eg. (0, 1), (1, 1), (1, 2), (2, 3), ...
*)

// fibPairs: () -> (int * int) list
let fibPairs () = [
    let rec loop i a b = [
      if i > 0 then
        yield (a, b)
        yield! loop (i-1) b (a+b)
    ]

    yield! [ (0, 1); ]
    yield! loop 23 1 1
  ]

test "Create a list comprehension that returns pairs of the first 25 Fibonacci numbers" (fun () ->
  fibPairs () = [(0, 1); (1, 1); (1, 2); (2, 3); (3, 5); (5, 8); (8, 13); (13, 21); (21, 34);
    (34, 55); (55, 89); (89, 144); (144, 233); (233, 377); (377, 610);
    (610, 987); (987, 1597); (1597, 2584); (2584, 4181); (4181, 6765);
    (6765, 10946); (10946, 17711); (17711, 28657); (28657, 46368)]
)


(**********************************************************************************************************************
    Write a list comprehension that outputs the first 20 triangular numbers. 
    A triangular number is the number of objects that are need to make up an equilateral triangle, with the length of
    the sides being the iteration.

    eg.
                                                             *
                                          *                 * *
                          *              * *               * * *
    1 (1):  *    2 (3):  * *    3 (6):  * * *    4 (10):  * * * *
*)

// triangle10: () -> int list
let triangle10 () = [
    let rec loop n i x = [
      if i <= n then
        yield x + i
        yield! loop n (i+1) (i+x)
    ]

    yield! loop 10 1 0
  ]

test "Create a list comprehension that calculates the first ten triangular numbers" (fun () ->
  triangle10 () = [1; 3; 6; 10; 15; 21; 28; 36; 45; 55]
)


(**********************************************************************************************************************
    A useful feature of lists is the ability to pattern match them. Using the cons and empty list symbols
    you can match pretty much anywhere in a list.

    The following two example shows some simple matching on a list by splitting between the head and the tail. 
*)

let firstFib =
  match fib25 with
  | x :: xs   -> Some x
  | _         -> None

let fibTail =
  match fib25 with
  | _ :: xs   -> xs
  | _         -> []


let firstTwoFib =
  match fib25 with
  | x :: y :: _ -> Some (x, y)
  | _           -> None

let isLast =
  function
  | x :: [] -> true
  | _       -> false


(**********************************************************************************************************************
    Using only a match expression, return the fourth item from the Fibonacci sequence (use fib25)
*)

// fourthFib: () -> int option
let fourthFib () =
    match fib25 with
    | _ :: _ :: _ :: x :: _   -> Some x
    | _                       -> None

test "Return the fourth item in the Fibonacci sequence" (fun () ->
  match fourthFib () with
  | Some x  -> x = 2
  | _       -> false
)

(**********************************************************************************************************************
    The following example shows walking through the list to perform an action on each item
*)

// walkFib: () -> ()
let walkFib () =
  let rec loop action =
    function
    | []      ->
      action None
    | x :: xs ->
      action (Some x)
      loop action xs
  loop (function | Some x -> printfn "%d" x | _ -> printfn "End of list") fib25


(**********************************************************************************************************************
    Using the above few functions as inspiration see if you can write a function to return the last item in a list.
*)

// last: 'a list -> a
let last list =
  let rec loop =
    function
    | []      -> failwith "empty list!"
    | x :: [] -> x
    | x :: xs -> loop xs
  loop list


test "Write a function to return the last item in a list" (fun () ->
  last fib25 = 46368

  &&

  last prime100 = 97
)

(**********************************************************************************************************************
    Mapping items within lists from one form to another is a common task. Write a generic list map function that
    takes as input a function to be applied to each item in the list and yields the result.

    It's acceptable to return unit () when yielding items if you do not want to yield a value at that point.
*)

// map: ('a -> 'b) -> 'a list -> 'b list
let map action list = [
  let rec loop list' = [
    match list' with
    | []      -> ()
    | x :: xs ->
      yield action x
      yield! loop xs
  ]
  yield! loop list
]

test "Create a list map function" (fun () ->
  map (fun x -> x * 2) fib25 = [0; 2; 2; 4; 6; 10; 16; 26; 42; 68; 110; 178; 288; 466; 754; 1220; 1974;
    3194; 5168; 8362; 13530; 21892; 35422; 57314; 92736]

  &&

  map (fun x -> x * x) prime100 = [4; 9; 25; 49; 121; 169; 289; 361; 529; 841; 961; 1369; 1681; 1849; 2209;
    2809; 3481; 3721; 4489; 5041; 5329; 6241; 6889; 7921; 9409]
)


(**********************************************************************************************************************
    This time instead of mapping items in a list from one value to the next, write a filter function that takes a
    predicate and returns portion of a list based on that.
*)

// filter: ('a -> bool) -> 'a list -> 'a list
let filter predicate list = [
  let rec loop list' = [
    match list' with
    | []      -> ()
    | x :: xs ->
      if predicate x then
        yield x
      yield! loop xs
  ]
  yield! loop list
]

test "Write a function that filters a list using the given predicate" (fun () ->
  filter (fun x -> x % 2 = 0) fib25 = [0; 2; 8; 34; 144; 610; 2584; 10946; 46368]

  &&

  filter (fun x -> x % 10 = 3) prime100 = [3; 13; 23; 43; 53; 73; 83]
)


(**********************************************************************************************************************
    Write a function that uses match expressions to returns pairs of items
*)

// pairwise: ('a list) -> ('a * 'a) list
let pairwise list = [
  let rec loop list' = [
    match list' with
    | x :: y :: tail  ->
      yield (x, y)
      yield! loop (y :: tail)
    | _               -> ()
  ]
  yield! loop list
]


test "Create a function that uses a match expression to return pairs of items from a list" (fun () ->
  pairwise fib25 = [(0, 1); (1, 1); (1, 2); (2, 3); (3, 5); (5, 8); (8, 13); (13, 21); (21, 34);
    (34, 55); (55, 89); (89, 144); (144, 233); (233, 377); (377, 610);
    (610, 987); (987, 1597); (1597, 2584); (2584, 4181); (4181, 6765);
    (6765, 10946); (10946, 17711); (17711, 28657); (28657, 46368)]

  &&

  pairwise prime100 = [(2, 3); (3, 5); (5, 7); (7, 11); (11, 13); (13, 17); (17, 19); (19, 23);
    (23, 29); (29, 31); (31, 37); (37, 41); (41, 43); (43, 47); (47, 53);
    (53, 59); (59, 61); (61, 67); (67, 71); (71, 73); (73, 79); (79, 83);
    (83, 89); (89, 97)]
)

(**********************************************************************************************************************
    Write a function can zip two lists of the same length together.

    eg. zip [ 1; 2; 3 ] [ 4; 5; 6] = [ (1, 4); (2, 5); (3, 6) ]
*)

// zip: a' list -> 'b list -> (a' list
let zip list1 list2 = [
  let rec loop list1' list2' = [
    match list1', list2' with
    | x :: xs, y :: ys  ->
      yield (x, y)
      yield! loop xs ys
    | _                 -> ()
  ]
  yield! loop list1 list2
]

test "Write a function can zip two lists of the same length together" (fun () ->
  zip fib25 prime100 = [(0, 2); (1, 3); (1, 5); (2, 7); (3, 11); (5, 13); (8, 17); (13, 19);
    (21, 23); (34, 29); (55, 31); (89, 37); (144, 41); (233, 43); (377, 47);
    (610, 53); (987, 59); (1597, 61); (2584, 67); (4181, 71); (6765, 73);
    (10946, 79); (17711, 83); (28657, 89); (46368, 97)]
)


(**********************************************************************************************************************
    Write a function that can sum the integers in a list.
*)

// sum: int list -> int
let sum list =
  let rec loop acc =
    function
    | []      -> acc
    | x :: xs -> (loop (x + acc) xs)
  loop 0 list

test "Write a function that can sum the integers in a list" (fun () ->
  sum fib25 = 121392

  &&

  sum prime100 = 1060
)

(**********************************************************************************************************************
    You can make the function numerically generic by declaring it with 'inline' and using a special function
    for a generic zero:

    let inline sum list =
      ...
      loop (LanguagePrimitives.GenericZero<'a>) list
*)

// sum: 'a list -> 'a
let inline sum2 list =
  let rec loop acc =
    function
    | []      -> acc
    | x :: xs -> (loop (x + acc) xs)
  loop (LanguagePrimitives.GenericZero<'a>) list


test "Write a function that can sum numeric values in a list" (fun () ->
  let floats = [0.02; 0.03; 0.05; 0.07; 0.11; 0.13; 0.17; 0.19; 0.23; 0.29; 0.31; 0.37;
    0.41; 0.43; 0.47; 0.53; 0.59; 0.61; 0.67; 0.71; 0.73; 0.79; 0.83; 0.89;
    0.97]

  let decimals = [0.02M; 0.03M; 0.05M; 0.07M; 0.11M; 0.13M; 0.17M; 0.19M; 0.23M; 0.29M; 0.31M;
    0.37M; 0.41M; 0.43M; 0.47M; 0.53M; 0.59M; 0.61M; 0.67M; 0.71M; 0.73M; 0.79M;
    0.83M; 0.89M; 0.97M]

  decimal (sum2 floats) = 10.6M

  &&

  sum2 decimals = 10.6M
)

(**********************************************************************************************************************
    Lets extend the previous function into something more generic. This time it needs to take a function (the folder)
    that performs the action on the item and accumulated value (replacing the '+' in in sum function. Next argument
    is state, effecitively the initializing value (like the zero in sum), and finally the list of values.
*)

// fold: ('state -> 'a -> 'state) -> 'state -> 'a list -> 'state
let fold folder state list =
  let rec loop acc =
    function
    | []      -> acc
    | x :: xs -> loop (folder acc x) xs
  loop state list


test "Write a function to fold a list into a single value" (fun () ->
  fold (+) 0 fib25 = 121392

  &&

  fold (-) 0 prime100 = -1060

  &&
 
  fold (fun acc x -> acc + (sprintf "%d " x)) "Fib: " fib25 =
    "Fib: 0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584 4181 6765 10946 17711 28657 46368 "
)

(**********************************************************************************************************************
    BONUS: Try rewriting both the integer version and generic version of sum using fold.
*)

// sum: int list -> int
let sum3 = fold (+) 0

// sum: 'a list -> 'a
let inline sum4 list = fold (+) (LanguagePrimitives.GenericZero<'a>) list

test "Write a function that can sum the integers in a list using fold" (fun () ->
  sum3 fib25 = 121392

  &&

  sum3 prime100 = 1060
)


test "Write a function that can sum numeric values in a list using fold" (fun () ->
  let floats = [0.02; 0.03; 0.05; 0.07; 0.11; 0.13; 0.17; 0.19; 0.23; 0.29; 0.31; 0.37;
    0.41; 0.43; 0.47; 0.53; 0.59; 0.61; 0.67; 0.71; 0.73; 0.79; 0.83; 0.89;
    0.97]

  let decimals = [0.02M; 0.03M; 0.05M; 0.07M; 0.11M; 0.13M; 0.17M; 0.19M; 0.23M; 0.29M; 0.31M;
    0.37M; 0.41M; 0.43M; 0.47M; 0.53M; 0.59M; 0.61M; 0.67M; 0.71M; 0.73M; 0.79M;
    0.83M; 0.89M; 0.97M]

  decimal (sum4 floats) = 10.6M

  &&

  sum4 decimals = 10.6M
)


(**********************************************************************************************************************
    Now lets rethink fold in a different way, this time to reduce a list down to a single value using the supplied
    reducer function, but unlike fold, there's no initial state passed in as it starts with the first two items
    in the list first.

    Use the fold function in your solution.
*)

// reduce: ('a -> 'a -> 'a) -> 'a list -> 'a
let reduce reducer =
  function
  | []      -> failwith "cannot reduce an empty list"
  | x :: xs -> fold reducer x xs

 
test "Reduce a list down to a single value using the supplied function" (fun () ->
  reduce (*) prime100 = 833294374

  &&

  reduce (-) prime100 = -1056

  &&

  reduce (fun acc _ -> acc + 1) fib25 = 24

  &&

  reduce (+) ["a";"b";"c"] = "abc"
)

(**********************************************************************************************************************
    BONUS: Can you rewrite 'last' using reduce?
*)

let last2 list = reduce (fun x y -> y) list

test "Write a function to return the last item of list using reduce" (fun () ->
  last2 fib25 = 46368

  &&

  last2 prime100 = 97
)


(**********************************************************************************************************************
    Now you should have a reasonable understand of some common list functions, including the much hyped map reduce.


*)

// arrays
// sequences
//unfold - let triangles = Seq.unfold (fun (a, b) -> Some(a + b, (a+1, a + b))) (1, 0)

// sets
// maps



(*
    References:
        F# list declaration     - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L3268
        C# List<T> declaration  - https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/Generic/List.cs#L33
        MSDN F# List module     - https://msdn.microsoft.com/en-us/library/ee353738.aspx
        F# List module source   - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/list.fs
        Triangular number       - https://en.wikipedia.org/wiki/Triangular_number
        Inline functions        - https://msdn.microsoft.com/en-us/library/Dd548047.aspx
        GenericZero declaration - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L2398

    Note:
        * The cons (::) operator in F# is actually a symbolic keyword, you can find this in section 3.6 of the spec (http://fsharp.org/specs/language-spec/3.1/FSharpSpec-3.1-working.docx)
        * Cons comes from construct, originally a concept from lisp
*)