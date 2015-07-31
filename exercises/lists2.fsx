#load "./examples.fs"
open Examples
(*********************************************************************************************************************)
//  A useful feature of lists is the ability to pattern match them. Using the cons and empty list symbols
//  you can match pretty much anywhere in a list.
//
//  The following two example shows some simple matching on a list by splitting between the head and the tail. 

let fibonacci = [0; 1; 1; 2; 3; 5; 8; 13; 21; 34; 55; 89]

let firstFib =
  match fibonacci with
  | x :: xs   -> Some x
  | _         -> None

let fibTail =
  match fibonacci with
  | _ :: xs   -> xs
  | _         -> []


let firstTwoFib =
  match fibonacci with
  | x :: y :: _ -> Some (x, y)
  | _           -> None

let isLast =
  function
  | x :: [] -> true
  | _       -> false

// Using only a match expression, return the fourth item from the Fibonacci sequence (use fib25)

// fourthFib: () -> int option
let fourthFib () = failwith "todo"

test "Return the fourth item in the Fibonacci sequence" (fun () ->
  match fourthFib () with
  | Some x  -> x = 2
  | _       -> false
)
