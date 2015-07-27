namespace Chatter.Server

open FSharpx.Collections

module ChatPage =
    let renderShell inner = seq {
        yield
            """<html>
                 <head><title>Chatter</title></head>
                 <body>"""

        yield! inner

        yield
            """  </body>
               </html>""" }

    let renderRoom (room : Room) = seq {
        yield sprintf """<a href="/room/%s">%s</a>""" room room
    }

    let renderRoomList (rooms : list<Room>) = seq {
        yield "<ul>"

        for room in rooms do
            yield "<li>"
            yield! renderRoom room
            yield "</li>"

        yield "</ul>"
    }

    let renderCurrentRoom room = seq {
        match room with
        | Some room ->
            yield sprintf "<div>Current Room: %s</div>" room
        | None ->
            yield sprintf "<div>No room selected</div>" 

    }

    let render (pageState : PageState) =
      renderShell (seq {
        yield! renderCurrentRoom pageState.CurrentRoom
        yield! renderRoomList pageState.Rooms
      })
