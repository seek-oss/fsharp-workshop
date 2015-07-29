namespace Life.Tests

open Xunit
open Life.Server

// Test for the run length encoding algorithm
module RLETests =
  [<Fact>]
  let ``Single live cell is encoded as 1o!`` () =
    Assert.Equal("1o!", RLE.encode <| array2D [[true]])