(*
    List are a big part of functional programming langages and F# is no exception. You may be familar with List<T> in C# however F#
    lists are quite different, essentially being based on good old dependable singly-linked lists (but using tuples) whereas
    C# lists use arrays.

    A naive implementation of an F# list could look like:

    type List<'T> = 
    | Empty of List<'T>
    | Cons of Head: 'T * Tail: List<'T>

    In F# an empty list is represented by [] and cons by the :: operator

    Let's see some examples for constructing lists.
*)
#load "./examples.fs"

// empty list
let a = []

// constructs a list using the cons (::) operator
let b = 1 :: 2 :: 3 :: 4 :: []

// explicit construction of a list
let c = [ 1; 2; 3; 4 ]

// they can be declared multiline
let d = [
    1
    2
    3
    4
   ]


// using a range
let e = [ 1 .. 10 ]

// using a range with a skip interval
let f = [ 1 .. 2 .. 10 ]




// list comprehensions

let g = [ yield 1; yield 2; yield 3 ]; // using yields

let h = [
        yield 1
        yield! [ 2; 3 ]  // yield! flattens inner lists
        yield 4
    ]

let i = [ for i in 1 .. 10 -> i * 2 ] // using a for loop

// they can also contain functions and be fairly complex
// use F# Interactive to see the what the following functions do

let j = [
        let rec loop i a b = [ // the 'rec' keyword tells the compiler the function is recursive
            if i > 0 then 
                yield b
                yield! loop (i-1) b (a+b)
        ]
        yield! loop 10 1 1
    ]

let k = [
        let incr n i =
            if n % i = 0 then 1
            else 0

        for n in 1 .. 20 do
            let rec countFactors i =
                if i = 0 then 0
                else (incr n i) + (countFactors (i-1))

            if (countFactors n) = 2 then
                yield n
    ]



// basic list functions
//map
//filter

// advance functions
//reduce
//fold


(*
    References:
        F# list declaration     - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/prim-types.fs#L3268
        C# List<T> declaration  - https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Collections/Generic/List.cs#L33
        MSDN F# List module     - https://msdn.microsoft.com/en-us/library/ee353738.aspx
        F# List module source   - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/list.fs

    Note:
        * The cons (::) operator in F# is actually a symbolic keyword, you can find this in section 3.6 of the spec (http://fsharp.org/specs/language-spec/3.1/FSharpSpec-3.1-working.docx)
        * Cons comes from construct, originally a concept from lisp
*)

// arrays
// sequences
// sets
// maps