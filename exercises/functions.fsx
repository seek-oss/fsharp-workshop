(* F# is a functional language - functions are important *)
#load "./examples.fs"

let add a b = failwith "todo"

Examples.test "Can add two numbers" (fun () ->
  add 1 2 = 3
)
