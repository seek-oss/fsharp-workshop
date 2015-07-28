namespace Life.Server

module Game =
    // todo compute the next generation
    // see: http://www.conwaylife.com/wiki/Conway's_Game_of_Life
    let computeNext (board: bool[,]) = board