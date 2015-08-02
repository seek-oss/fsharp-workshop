
(*
   Record sets are similar to tuples except it contains named fields.
*)

#load "./examples.fs"

// Examples:

//
// In the previous exercies we used tuples to express
// coordinates. We can also use a record to define
// the same thing.
// Note: the keyword 'type'
//

type Coord2d = { x : float; y : float }

//
// To create a value of a Coord2d type is easy
//

let vect1 = { x = 3.5; y = 5.6 }

//
// F#'s type inference works from the parameters defined that vect1
// is of type Coord2d.

//
// Exercise 1: Define a new composed type
//   Create a new record called Player which contains a name
//   property and the players coordinates.
//   Create a new value called player with name and location.
//


//
// By default records are immutable, so how
// do we 'change' something?
//
// Example: The 'with' keyword.
//
let vect2 = { vect1 with x = 0.0 }

//
// Exercise 2: Create a new player value with a new location at 0,0 from the value player1
//


//
// Exercise 3: Just like tuples, recrods can be deconstructed (pattern matched).
//   Deconstruct the location coordinates of the player' value.
//   (Hint: look at the tuple destructuring exercise)
//



//
// An important difference between Records and Classes:
//   Records have structural equality semantics 
//   whereas classes have reference equality semantics.

//
// The following code shows Coord2d as a class
//
type Coord2dClass(x : float, y : float) = class
    member m.X = x
    member m.Y = y
end

let vect3 = new Coord2dClass(3.5, 5.6)
let vect4 = new Coord2dClass(3.5, 5.6)

vect3 = vect4


//
// Exercise 4: Test vector equality
//   Define a new vector with the same x, y coordinates as vect1.
//   Now test the equality of the two values.
//

 