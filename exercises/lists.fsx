
(*
    Lists are a big part of functional programming langages and F# is no exception. You may be familar with List<T> in C# however F#
    lists are quite different, essentially being based on good old dependable singly-linked lists (but using tuples) whereas
    C# lists use arrays.

    A naive implementation of an F# list could look like:

    type List<'T> =
    | Empty of List<'T>
    | Cons of Head: 'T * Tail: List<'T>

    In F# an empty list is represented by [] and cons by the :: operator
*)
#load "./examples.fs"
open Examples


(* Let's see some examples for constructing lists. *)

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


(* Another way to create lists is through list comprehensions. *) 

let yields = [ yield 1; yield 2; yield 3 ];

let yieldBang = [
        yield 1
        yield! [ 2; 3 ]  // yield! flattens inner lists
        yield 4
    ]

let forLoop = [ for i in 1 .. 10 -> i * 2 ]

(*  They can also contain functions and be fairly complex.
    Use F# Interactive to see the result of the list comprehensions below *)

let fib20 = [
    let rec loop i a b = [
      if i > 0 then
        yield b
        yield! loop (i-1) b (a+b)
    ]

    yield! [ 0; 1 ]
    yield! loop 20 1 1
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

(*  Write a list comprehension that outputs the first 20 triangular numbers. 
    A triangular number is the number of objects that are need to make up an equilateral triangle, with the length of
    the sides being the iteration.

    eg.
                                                             *
                                          *                 * *
                          *              * *               * * *
    1 (1):  *    2 (3):  * *    3 (6):  * * *    4 (10):  * * * *
*)

let triangle20 () = [
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


(* Write a list comprehension that takes the first twenty numbers from the Fibonacci sequence and pairs them

  eg. (0, 1), (1, 1), (1, 2), (2, 3), ...
*)

let fibPairs () = failwith "todo"

test "Create a list comprehension that returns pairs of the first 20 Fibonacci numbers" (fun () ->
  fibPairs () = [(0, 1); (1, 1); (1, 2); (2, 3); (3, 5); (5, 8); (8, 13); (13, 21); (21, 34);
   (34, 55); (55, 89); (89, 144); (144, 233); (233, 377); (377, 610);
   (610, 987); (987, 1597); (1597, 2584); (2584, 4181); (4181, 6765);
   (6765, 10946)]
)


// pattern matching
// basic list functions
//map
//filter

// advance functions
//reduce
//fold
//unfold - let triangles = Seq.unfold (fun (a, b) -> Some(a + b, (a+1, a + b))) (1, 0)



// arrays
// sequences
// sets
// maps



(*
    References:
        F# list declaration     - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L3268
        C# List<T> declaration  - https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/Generic/List.cs#L33
        MSDN F# List module     - https://msdn.microsoft.com/en-us/library/ee353738.aspx
        F# List module source   - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/list.fs
        Triangular number       - https://en.wikipedia.org/wiki/Triangular_number

    Note:
        * The cons (::) operator in F# is actually a symbolic keyword, you can find this in section 3.6 of the spec (http://fsharp.org/specs/language-spec/3.1/FSharpSpec-3.1-working.docx)
        * Cons comes from construct, originally a concept from lisp
*)