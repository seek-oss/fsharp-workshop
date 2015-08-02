// this file contains some extra
// function examples. Try these if you are bored
// by the first set
#load "./examples.fs"

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

Examples.test "Calculate the 100th fibonacci number" (fun () ->
    fib 15 = 610
)
