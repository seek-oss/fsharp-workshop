(* F# is a functional language - functions are important *)
#load "./examples.fs"

// first sample function
let multiply a b = a * b

// first function example - just a function to add two numbers together
let add a b = failwith "todo"

Examples.test "Can add two numbers" (fun () ->
  add 1 2 = 3
)

// passing functions as arguments to other functions
// is a really powerful technique. This is a silly example.
let applyFunctionThenAdd2 f n =
    failwith "todo"

Examples.test "Multiply by two then add two" (fun () ->
    applyFunctionThenAdd2 (fun x -> x * 2) 10 = 22
)

// the pipe operator (|>) allows us to chain operations together
// for example if we wanted to implement n * 10 + 2 * 40
// we could do it like:
let examplePipe n =
    n 
    |> multiply 10
    |> add 2
    |> multiply 40

// the previous value is passed as the last argument to the next 
// function.

// Implement the following equation using applyFunctionThenAdd2
// pipe and multiply (you shouldn't need add).
// (n * 10 + 2) * 2 + 2

let examplePipe2 n =
    failwith "todo"

Examples.test "(n * 10 + 2) * 2 + 2 using |>" (fun () ->
    examplePipe2 10 = 206
)

// the next number in the fibonacci sequence is computed
// by adding the previous two together
// ie: 1, 1, 2, 3, 5
// hint: 'rec' keyword allows you to call a function recursively
// you'll probably need that for this exercise.
let rec fib n = failwith "todo"

Examples.test "Calculate the 10th fibonacci number" (fun () ->
    fib 10 = 55
)

// the pipe operator has a very simple implementation
// try implementing ||> to do the same things as pipe

let (||>) f x = failwith "todo"

Examples.test "Custom pipe" (fun () -> 
    10
    ||> add 2
    ||> multiply 2
    ||> (fun x -> x = 24)
)