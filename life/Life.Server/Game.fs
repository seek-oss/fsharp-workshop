namespace Life.Server

module Game =
    // this implementation comes from 
    // http://codereview.stackexchange.com/questions/17723/better-ways-to-implement-conways-game-of-life-in-f
    let amount_neighbours (board: bool[,]) (pos: int * int) =
        let alive board pos = 
            let (x, y) = pos
            if x < 0 || x >= Array2D.length1 board ||
               y < 0 || y >= Array2D.length2 board then
                false
            else
                board.[x, y] = true

        let vicinity x = seq { x - 1 .. x + 1 }

        seq {
            for x in vicinity (fst pos) do
            for y in vicinity (snd pos) do
            if (x, y) <> pos && alive board (x, y) then
                yield true
        } |> Seq.length

    let lifecycle (board: bool[,]) = 
        Array2D.init (Array2D.length1 board) (Array2D.length2 board) (fun i j ->
            let neighbours = amount_neighbours board (i, j)
            match neighbours with
                | 2 -> board.[i, j]
                | 3 -> true
                | _ -> false)

    let foldRows (f : 's -> 'a [] -> 's) (init: 's) (array2d : 'a [,]) : 's =
        let rows = array2d |> Array2D.length1
        let rec loop n s = 
            match n >= rows with
            | true -> s
            | false ->
                let row = array2d.[0, *]
                loop (n + 1) (f s row)

        loop 0 init
        
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
        
    let getRle (board: bool[,]) =
        let v = board.[0,*]
        let x = board.GetLength 0
        let y = board.GetLength 1
        let foldRow s r =
            let lineText = 
                Array.fold nextState LineState.Empty r
                |> complete

            if s = "" then
                lineText
            else 
                sprintf "%s$%s" s lineText
            
        let rle = foldRows foldRow "" board
        sprintf """
        #N NextState
        #O Seek Ltd
        #C Don't know what it is
        x = %d, y = %d, rule = B3/S23
        %s!""" x y rle

    let getGrid (cells : List<List<bool>>) = 
        let xLen = cells.Length
        let yLen = cells |> List.maxBy (fun x -> x.Length) |> List.length
        let getCellValue x y =
            let row = 
                cells
                |> Seq.skip x 
                |> Seq.head
                |> List.ofSeq

            if (row.Length > y) then
                List.nth row y
            else 
                false

        Array2D.init xLen yLen getCellValue

    let getNextBoard (board : BoardState) =
        let grid = getGrid board.grid
        let result = lifecycle grid
        getRle result