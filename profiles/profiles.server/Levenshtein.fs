namespace profiles.server

module Levenshtein =
  // NOTE this implementation is not
  // very efficient
  let distance (word1 : string) (word2 : string) : int =
    let rec loop = function
        | [], ys -> ys |> List.length
        | xs, [] -> xs |> List.length
        | x::xs, y::ys ->
          [
            loop (xs, y::ys) + 1
            loop (x::xs, ys) + 1
            loop (xs, ys) + (if x = y then 0 else 1)
          ]
          |> List.min

    loop (word1 |> Seq.toList, word2 |> Seq.toList)
