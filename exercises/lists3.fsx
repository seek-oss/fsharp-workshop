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


(*********************************************************************************************************************)
// For the following exercises we're going to use some occupations taken from the Australian Bureau of Statistics.
// We'll load this up using a familar .NET library and then convert it to an F# list for our purposes

let occupations = System.IO.File.ReadLines(__SOURCE_DIRECTORY__ + "/ANZSCO-occupations.txt") |> Seq.toList

// This is a simple text file that needs to be cleaned up before using it. Firstly there are lines begining with #
// that are comments and need to be removed, as well and empty lines.
// NOTE: as F# strings are just .NET strings you can use .StartsWith and .Length to help you.

let cleaner (list: string list) = failwith "todo"

// How many occupations does the file contain?

let number_of_occupations clean_list = failwith "todo"

test "How many occupations does the file contain?" (fun () ->
  number_of_occupations (cleaner occupations) = 3207
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
  false
)

// For bonus points, see if you can return a tuple containing the counts of all classifications
let anzsco_classification_counts anzsco_list = failwith "todo"

(*********************************************************************************************************************)
//  References:
//      MSDN F# List module     - https://msdn.microsoft.com/en-us/library/ee353738.aspx
//      F# List module source   - https://github.com/fsharp/fsharp/blob/master/src/fsharp/FSharp.Core/list.fs
//      ABS Occupation list     - http://www.abs.gov.au/AUSSTATS/abs@.nsf/DetailsPage/1220.02013,%20Version%201.2?OpenDocument
(*********************************************************************************************************************)