#load "./examples.fs"
open Examples
(**********************************************************************************************************************
    Lists are a big part of functional programming langages and F# is no exception. You may be familar with List<T>
    in C# however F# lists are quite different, essentially being based on good old dependable singly-linked lists (but
    using tuples) whereas C# lists use arrays.

    A naive implementation of an F# list could look like:

    type List<'T> =
    | ([]) of List<'T>
    | (::) of Head: 'T * Tail: List<'T>

    In F# an empty list is represented by [] and constructing by the :: (cons) operator. Head represents the first item
    in the list and tail all the items apart from the first.
*)


(**********************************************************************************************************************
    Let's see some examples for constructing lists - copy the following into the REPL to see the result.              *)

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

let downRange     = [ 10 .. (-1) .. 1 ]


(**********************************************************************************************************************
    You can append to a list using the '@' symbol, however this iterates through the items in the first list before
    it can return the newly combined list.
*) 


let appendedList  = [ 1; 2; 3 ] @ [ 4; 5; 6 ]

(**********************************************************************************************************************
    Write a list comprehension that produces the numbers one to ten followed by ten down to one.
*)

let tenTo10 () = [ 1 .. 10 ] @ [ 10 .. (-1) .. 1]

test "Write a list comprehension that produces the numbers one to ten followed by ten down to one" (fun () ->
  tenTo10 () = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 10; 9; 8; 7; 6; 5; 4; 3; 2; 1]
)


(**********************************************************************************************************************
    Another way to create lists is through list comprehensions using the for .. in syntax.
*) 


let forLoop           = [ for i in 1 .. 10 -> i * 2 ]

let forLoopDown       = [ for i in 10 .. (-1) .. 1 -> i + 1 ]

(**********************************************************************************************************************
    Write a list comprehension that produces every third square of the numbers 1 to 20
*)

let thirdSquares () = [ for i in 1 .. 3 .. 20 -> i * i ]

test "Write a list comprehension that produces every third square of the numbers 1 to 20" (fun () ->
  thirdSquares () = [1; 16; 49; 100; 169; 256; 361]
)


(**********************************************************************************************************************
    An alternative way to use the for .. in syntax is with do and yield, allowing for more complex logic.
*) 

let forLoopWithYield  = [
  for i in 1 .. 10 do
    yield i * 2
]


let twoForsWithYield = [
  for i in 1 .. 5 do
    for j in i .. 5 do
      yield (i, j)
]


(**********************************************************************************************************************
    Write a list comprehension that produces the even squares of the numbers 1 to 20.
*) 

let evenSquares () = [
  for i in 1 .. 20 do
    if i % 2 = 0 then
      yield i * i
]

test "Write a list comprehension that produces the even squares of the numbers 1 to 20" (fun () ->
  evenSquares () = [4; 16; 36; 64; 100; 144; 196; 256; 324; 400]
)


