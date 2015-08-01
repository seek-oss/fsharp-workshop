#load "./examples.fs"
open Examples

(*********************************************************************************************************************)
//  Now that you have good idea of what lists are and how to construct them, let's look at some of the common functions
//  that are used with them.

// List.map lets you apply a function to each item in the list.
// The LINQ equivalent is Select
[ 1 .. 10 ]
|> List.map (fun x -> x * x)

// List.filter lets you filter items in the list that conform to the predicate passed in.
// The LINQ equivalent is Where
[ 1 .. 10 ]
|> List.filter (fun x -> x % 2 = 0)

// List.sum will return the sum of the items in the list.
[ 1 .. 10]
|> List.sum

// List.length will return the number of items in the list.
[ 1 .. 10 ]
|> List.length

// List.rev will reverse the items in the list
[ 1 .. 10 ]
|> List.rev

// List.reduce will successively apply a function to each item in the list, passing in the accumulated value with
// the list item on each iteration result in a value of the same type as the list
[ 1 .. 10 ]
|> List.reduce (fun acc x -> acc + x)

// List.fold, like List.reduce, will apply a function to each item in the list, however it starts off with an initial
// value that is passed in and does not have to be of the same type as the list. 
// The LINQ equivalent for this is Aggregate
[ 1 .. 10 ]
|> List.fold (fun state x -> sprintf "%s %d" state x) "Numbers:"

// sum the all the numbers divisible by three and five from 1 to 100
let sum35 () = failwith "todo"

test "Sum the all the numbers divisible by three and five from 1 to 100" (fun () ->
  sum35  () = 315
)

// Write a function that calculates the numbers divisible by three and five from 1 to 100 and outputs as a
// comma separated list.
let print35 () = failwith "todo"

test "Numbers divisible by three and five as a comma separated list" (fun () ->
  print35 () = "15, 30, 45, 60, 75, 90"
)


// Now let's see if we can write a function that can take a list of numbers and split them into a tuple of two
// lists of even numbers and odd numbers
let evenOdds numbers = failwith "todo"

test "Split a list of numbers into a tuple of even numbers and odd numbers" (fun () ->
  [ 23 .. 37 ]
  |> evenOdds
  |> fun (e, o) -> (List.sort e, List.sort o) = ([24; 26; 28; 30; 32; 34; 36], [23; 25; 27; 29; 31; 33; 35; 37])
)


// Let's try something a little more challenging and see if we can write bubble sort using List.fold and a simple
// match expression. A useful feature of match expressions you mat not have come across are 'when guards', which are
// conditions you can attach to the matches:
//      match something with
//      | head :: tail when head > x  -> "head is greater than x"
//      | head :: tail when head < x  -> "head is less than x"
//      | _                           -> "..."
let rec sorter (numbers: int list): int list = failwith "todo"


test "Write a simple sort algorithm using List.fold and a match expression" (fun () ->
  sorter [ 34; 21; 4; 88; 32; 12; 77; 45; 47; 99; 100; 23; ] = [4; 12; 21; 23; 32; 34; 45; 47; 77; 88; 99; 100]
)


(*********************************************************************************************************************)
// For the following exercises we're going to use some occupations taken from the Australian Bureau of Statistics.
// We'll load this up using a familar .NET library and then convert it to an F# list for our purposes

let occupations = lazy (System.IO.File.ReadLines(__SOURCE_DIRECTORY__ + "/ANZSCO-occupations.txt") |> Seq.toList)

// This is a simple text file that needs to be cleaned up before using it. Firstly there are lines begining with #
// that are comments and need to be removed, as well and empty lines.
// NOTE: as F# strings are just .NET strings you can use .StartsWith and .Length to help you.

let cleaner (list: string list) = failwith "todo"

// How many occupations does the file contain?

let number_of_occupations clean_list = failwith "todo"

test "How many occupations does the file contain?" (fun () ->
  number_of_occupations (cleaner occupations.Value) = 3207
)

// Another aspect of this list off occupations is that it contains a simple key that broadly classifies the title
// (A) -- alternative title
// (N) -- occupation in nec category - not elsewhere classified
// (P) -- principal title
// (S) -- specialisation
//
// Let's create a union type that can handle this:

type AnzscoClassification =
  | Alternative of string
  | NotElsewhereClassified of string
  | Principal of string
  | Specialisation of string

// Now, you'll find the key on the end of each occupation, preceded by a space. See if you can convert the list of
// occupations in a list of AnszcoClassifications. You will need to use a match expression inside conversion function
// and you can use the .EndsWith to help as well.
// eg.
// match occ with
// | s when s.EndsWith(" (A)") -> Alternative s
let anzsco_occupations (clean_list: string list) = failwith "todo"

// Let's see if we can get the number of occupations that fall into "not elsewhere classified" using one of the standard
// list functions as well as a simple match expression.
let nec_counts anzsco_list = failwith "todo"


test "How many occupations are 'not elswhere classified'?" (fun () ->
  occupations.Value |> cleaner |> anzsco_occupations |> nec_counts = 331
)

// For bonus points, see if you can return a tuple containing the counts of all classifications
let anzsco_classification_counts anzsco_list = failwith "todo"

test "Return a tuple containing counts of each group" (fun () ->
  occupations.Value |> cleaner |> anzsco_occupations |> anzsco_classification_counts = (420, 331, 1047, 1409)
)

(*********************************************************************************************************************)
//  References:
//      MSDN F# List module     - https://msdn.microsoft.com/en-us/library/ee353738.aspx
//      F# List module source   - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/list.fs
//      MSDN F# Match Expressions - https://msdn.microsoft.com/en-us/library/dd233242.aspx
//      Bubble sort             - https://en.wikipedia.org/wiki/Bubble_sort
//      F# lazy computation     - https://msdn.microsoft.com/en-us/library/dd233247.aspx
//      ABS Occupation list     - http://www.abs.gov.au/AUSSTATS/abs@.nsf/DetailsPage/1220.02013,%20Version%201.2?OpenDocument
(*********************************************************************************************************************)