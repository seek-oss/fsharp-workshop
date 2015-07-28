namespace Life.Server
open System

module RLE = 
    // run length encoding is a standard format for 
    // game of life boards: http://www.conwaylife.com/wiki/Run_Length_Encoded
    let encode (board: bool[,]) =
        "TODO"

    let encodeWithHeader (board: bool[,]) = 
        let x = board.GetLength 0
        let y = board.GetLength 1
        let rle = encode board
        sprintf """
        #N NextState
        #O Seek Ltd
        #C Don't know what it is
        x = %d, y = %d, rule = B3/S23
        %s""" x y rle