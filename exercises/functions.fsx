(* F# is a functional language - functions are important *)

let add a b = failwith "todo"

let test_Can_add_two_numbers () =
  add 1 2 = 3

type Marker = class end
#load "./testrunner.fsx"
let runTests() = Testrunner.runTests (typeof<Marker>.DeclaringType)
runTests()
