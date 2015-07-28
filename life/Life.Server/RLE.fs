namespace Life.Server
open System

module RLE = 
    type LineState = {
        Encoded : string
        LastState : bool
        Count : int
    }
    with
        static member Empty = { Encoded = ""; LastState = false; Count = 0}

    let nextState (state: LineState) (cell : bool) = 
        if state.LastState = cell then
            { state with Count = state.Count + 1 }
        else
            if state.Count = 0 then
                { state with LastState = cell; Count = 1}
            else
                let lastStateChar = if state.LastState then "o" else "b"
                { state with LastState = cell; Count = 1; Encoded = sprintf "%s%d%s" state.Encoded state.Count lastStateChar}

    let complete (state : LineState) =
        if state.Count > 0 then
            let lastStateChar = if state.LastState then "o" else "b"
            sprintf "%s%d%s" state.Encoded state.Count lastStateChar
        else
            state.Encoded

    let foldRows (f : 's -> 'a [] -> 's) (init: 's) (array2d : 'a [,]) : 's =
        let rows = array2d |> Array2D.length1
        let rec loop n s =
            match n >= rows with
            | true -> s
            | false ->
                let row = array2d.[n, *]
                loop (n + 1) (f s row)

        loop 0 init

    let encode (board: bool[,]) =
        let foldRow s r =
            let lineText =
                Array.fold nextState LineState.Empty r
                |> complete

            if s = "" then
                lineText
            else
                sprintf "%s$%s" s lineText

        let encodedRows = foldRows foldRow "" board
        sprintf "%s!" encodedRows

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
