(* F# is a functional language - functions are important *)
#load "./examples.fs"

// first sample function
let multiply a b = a * b

// first a note on types for functions
// here the type of a is explicitely int
let a : int = 1

// the type of a function with one parameter is like ParameterType -> ResultType
// ie:
let add1 : int -> int = fun a -> a + 1

// here is the type for a function that takes two parameters
// Parameter1Type -> Parameter2Type -> ResultType
let multiplyWithType : int -> int -> int = multiply

// We can apply the first parameter to leave us with a function
// that takes one parameter and returns the result type
// Parameter2Type -> ResultType
let mulitplyBy2 : int -> int = multiplyWithType 1

// a function to add two numbers together
let add a b = a + b

Examples.test "Can add two numbers" (fun () ->
  add 1 2 = 3
)

// passing functions as arguments to other functions
// is a really powerful technique. This is a silly example.
let applyFunctionThenAdd2 f n =
    (f n) + 2

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
    n
    |> applyFunctionThenAdd2 (multiply 10)
    |> applyFunctionThenAdd2 (multiply 2)

Examples.test "(n * 10 + 2) * 2 + 2 using |>" (fun () ->
    examplePipe2 10 = 206
)

// the next number in the fibonacci sequence is computed
// by adding the previous two together
// ie: 1, 1, 2, 3, 5
// hint: 'rec' keyword allows you to call a function recursively
// you'll probably need that for this exercise.
let rec fib n =
    match n with
    | 0 -> 0
    | 1 -> 1
    | n -> (fib (n - 1)) + (fib (n - 2))

Examples.test "Calculate the 10th fibonacci number" (fun () ->
    fib 10 = 55
)

// the pipe operator has a very simple implementation
// try implementing ||> to do the same things as pipe

let (||>) x f = f x

Examples.test "Custom pipe" (fun () -> 
    10
    ||> (add 2)
    ||> (multiply 2)
    ||> (fun x -> x = 24)
)