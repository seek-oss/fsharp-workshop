namespace Life.Server

module List =
    let toArray2D (defaultValue : 'a) (xs : List<List<'a>>) =
        let xLen = xs.Length
        let yLen = xs |> List.maxBy (fun x -> x.Length) |> List.length
        let getCellValue x y =
            let row =
                xs
                |> Seq.skip x 
                |> Seq.head
                |> List.ofSeq

            if (row.Length > y) then
                List.nth row y
            else 
                defaultValue

        Array2D.init xLen yLen getCellValue