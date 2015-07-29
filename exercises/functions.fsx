(* 
F# is a functional language - functions are important
In this first set of exercises we are going to write some 
simple functions and see how to combine them
 *)
#load "./examples.fs"

// this is NOT a function this is just an
// integer
let a = 1

// functions are first class values in F# so
// the syntax is idential to the above except that
// it has a parameter
let addOne a = a + 1

// types are mostly optional in F#, the compiler
// does a very good job in infering types when
// we leave them off. Here is the addOne function
// above with the type shown explicitely
// NOTE: there is nothing special about the ' it's just 
// a common convention for defining something that is related
// or similar to a previous definition 
let addOne' (a : int) = a + 1

// F# also allows functions to be defined inline
// using the fun keyword. This is similar to the lambda
// syntax in C# (a => a + 1)
let addOne'' = fun a -> a + 1

// a function to add two numbers together
let add a b = failwith "todo"

Examples.test "Can add two numbers" (fun () ->
  add 1 2 = 3
)

// F# uses -> to indicate function types
// the type of int -> int says that the function
// takes a single integer and returns an integer
let addOne''' : int -> int = fun a -> a + 1

// the multiply function has two 
// parameters
let multiply a b = a * b

// the type syntax might not be what you expected
let multiply' : int -> int -> int = multiply

// functions only really take one argument at a time
// so you can think of (int -> int -> int) as really
// a type of (int -> (int -> int))
// ie a function of two parameters is really a function
// that takes one parameter and returns a function that 
// takes on parameter
let multiplyBy1 : int -> int = multiply 1

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
let rec fib n =
    failwith "todo"

Examples.test "Calculate the 10th fibonacci number" (fun () ->
    fib 10 = 55
)

// the pipe operator has a very simple implementation
// try implementing ||> to do the same things as pipe

let (||>) x f = failwith "todo"

Examples.test "Custom pipe" (fun () -> 
    10
    ||> (add 2)
    ||> (multiply 2)
    ||> (fun x -> x = 24)
)