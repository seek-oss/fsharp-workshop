namespace Chatter.Server

type Room = string

type ServerState = {
    Rooms: list<Room>
}

type PageState = {
    Rooms: list<Room>
    CurrentRoom : option<Room>
}