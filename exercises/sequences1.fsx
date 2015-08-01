#load "./examples.fs"
open Examples
(*********************************************************************************************************************)
// Ok, let's move onto sequences and quite likely a collection type you're familiar with if you've done any C#
// as these are your very recognisable IEnumerables.


// A sequence expression can look quite like a list comprehension, eg:

let range         = seq { 1 .. 10 }

let forLoop       = seq { for i in 1 .. 10 -> i * 2 }

let forLoopWithYield = seq {
  for i in 1 .. 10 do
    yield i * 2
}

// And just like an IEnumerable, sequences are lazy so they're not realised until they're iterated over. You can check
// this by running the following two samples in the REPL and seeing which one executes the print statements.

let lazySequence = seq {
  printfn "seq: start"
  for i in 1 .. 10 do
    printfn "seq: %d" i
    yield i * 2
  printfn "seq: end"
}

let notLazyList = [
  printfn "list: start"
  for i in 1 .. 10 do
    printfn "list: %d" i
    yield i * 2
  printfn "list: end"
]


// Ok, there's probably little point in revisting the common functions we covered with lists, so let's look at some
// different ones instead.

// The following will generate a list of integers up to Int32.MaxValue
let integers = Seq.initInfinite (fun i -> i + 1)

// More interesting than initInfinite, unfold will let us do this and more. In an opposite to fold, unfold takes
// an input and applies some logic to generate a sequence. The above sequences of natural numbers could be done
// like:
let numbers = Seq.unfold (fun state -> Some (state, state + 1)) 0

// The thing to note here is that return Some continues the sequence whereas None will end it
let numbers20 = Seq.unfold (fun state -> if state <= 20 then Some (state, state + 1) else None) 0

// Alright, lets have look at how we'd generate the Fibonacci sequence using unfold
let fibs = Seq.unfold (fun (a, b) -> Some(a + b, (b, a + b))) (0, 1)

// So, using the above as inspiration, try creating a sequence of triangular numbers - take a look through lists4.fsx
// if you need a refresher on triangular numbers, but the short of it is you take the previous number in the sequence
// and add the current iteration to it.
let triangles = Seq.unfold (fun (a, b) -> Some(a + b, (a+1, a + b))) (1, 0)

test "Return a sequence of tiangular numbers" (fun () ->
  triangles |> Seq.take 20 |> Seq.toList = [1; 3; 6; 10; 15; 21; 28; 36; 45; 55; 66; 78; 91; 105; 120; 136; 153; 171;
   190; 210]
)


(*********************************************************************************************************************)
//    References:
//        F# Sequences          - https://msdn.microsoft.com/en-AU/library/dd233209.aspx
//        Seq.unfold            - https://msdn.microsoft.com/en-us/library/ee340363.aspx
//        F# Seq module         - https://msdn.microsoft.com/en-us/library/ee353635.aspx
//        F# Seq module source  - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/seq.fs
//        IEnumerable methods   - https://msdn.microsoft.com/en-us/library/vstudio/system.linq.enumerable_methods(v=vs.100).aspx
(*********************************************************************************************************************)