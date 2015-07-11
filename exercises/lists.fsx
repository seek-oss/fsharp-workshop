// constructing lists

let a = [] // empty list

let b = 1 :: 2 :: 3 :: 4 :: [] // constructs a list using the cons (::) operator

let c = [ 1; 2; 3; 4 ] // explicit construction of a list

let d = [ // they can be multiline
    1
    2
    3
    4
   ]

// ranges

let e = [ 1 .. 10 ] // using a range

let f = [ 1 .. 2 .. 10 ] // using a range with a skip interval

// list comprehensions

let g = [ yield 1; yield 2; yield 3 ]; // using yields

let h = [
        yield 1
        yield! [ 2; 3 ]  // yield! flattens inner lists
        yield 4
    ]

let i = [ for i in 1 .. 10 -> i * 2 ] // using a for loop

// they can also contain functions and be fairly complex
// use F# Interactive to see the what the following functions do

let j = [
        let rec loop i a b = [ // the 'rec' keyword tells the compiler the function is recursive
            if i > 0 then 
                yield b
                yield! loop (i-1) b (a+b)
        ]
        yield! loop 10 1 1
    ]

let k = [
        let incr n i =
            if n % i = 0 then 1
            else 0

        for n in 1 .. 20 do
            let rec countFactors i =
                if i = 0 then 0
                else (incr n i) + (countFactors (i-1))

            if (countFactors n) = 2 then
                yield n
    ]



// basic list functions
// advance functions
