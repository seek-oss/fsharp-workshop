// basic list definition - difference with .NET List<T>

type List<'T> =
   | ([])  :                  'T list
   | (::)  : Head: 'T * Tail: 'T list -> 'T list

// cons operator
// creating and adding to lists
// list comprehensions
// basic list functions
// advance functions
