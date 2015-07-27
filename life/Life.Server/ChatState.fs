namespace Chatter.Server

module ChatState = 

    let getState () : ServerState =
        {
            Rooms = ["Room1"; "Room2"]
        }