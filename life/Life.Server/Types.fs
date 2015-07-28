namespace Life.Server

type Pattern = {
    Name: string
    RLE: string
}

type ServerState = {
    Patterns: list<Pattern>
}

[<CLIMutable>]
type BoardState = {
    grid : list<list<bool>>
}