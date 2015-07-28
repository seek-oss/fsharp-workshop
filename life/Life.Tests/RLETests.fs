namespace Life.Tests

open Xunit
open Life.Server

module RLETests =
  [<Fact>]
  let ``Single live cell is encoded as a$`` () =
    Assert.Equal("a$", RLE.encode <| array2D [[true]])
