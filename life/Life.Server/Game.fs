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

    let computeNext (board: bool[,]) = 
        Array2D.init (Array2D.length1 board) (Array2D.length2 board) (fun i j ->
            let neighbours = amount_neighbours board (i, j)
            match neighbours with
                | 2 -> board.[i, j]
                | 3 -> true
                | _ -> false)