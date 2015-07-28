namespace Life.Tests

open Xunit
open Life.Server

// Test for the actual game of life algorithm
module GameTests =
    [<Fact>]
    let ``blinker blinks`` () =
        let input = 
            array2D [ 
                [ false; true; false;]; 
                [ false; true; false;]; 
                [ false; true; false;]; 
            ]
        let expected = 
            array2D [ 
                [ false; false; false;]; 
                [ true; true; true;]; 
                [ false; false; false;]; 
            ]
        let result =
           Game.computeNext input

        Assert.Equal(expected, result)