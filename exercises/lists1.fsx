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


let forLoop           = [ for i in 1 .. 10 -> i * 2 ]

// another example of for using do and yield.

let forLoopWithYield  = [
  for i in 1 .. 10 do
    yield i * 3
]



// some basic examples using List module functions with LINQ comparisons
