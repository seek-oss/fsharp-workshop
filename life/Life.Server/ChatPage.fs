﻿namespace Chatter.Server

open FSharpx.Collections

module ChatPage =
    let page = """
<html>
    <head><title>Chatter</title></head>
    <script src="./static/jquery.js"></script>
    <script src="./static/canvaslife.js"></script>
    <body>
    <h2>Usage</h2>
    <ol>
        <li>Populate the grid by click and dragging your mouse to revive/kill cells, or by choosing pre-populated patterns from the drop-down.</li>
        <li>Press the "Start Life" button to see your cells evolve.</li>
    </ol>
    <p>
        <input type="button" id="start" value="Start Life" onclick="life.toggleLife();">
        Patterns:
        <select id="patterns">
        <option value="">---</option>
        </select>
        <input type="button" value="-" onclick="life.changeSpeed(false); $('#speed').text(100 - (life.speed / 10));">
        Speed: <span id="speed"></span>/100
        <input type="button" value="+" onclick="life.changeSpeed(true); $('#speed').text(100 - (life.speed / 10));">
        <input type="button" value="Next Generation" onclick="life.nextGen();">
        <input type="button" value="Clear" onclick="life.clear();">
    </p>
    <canvas id="universe" width="900" height="500">
       <strong>Your browser does not support canvas. You should probably upgrade and come back.</strong>
    </canvas><br>
    <script>
    $('document').ready(function () {
        life.initUniverse('#universe');
        $('#speed').text(100 - (life.speed / 10));
        var start_value = 'Start Life';
        var stop_value = 'Stop Life';
        $('#start').click(function () {
            if ($(this).val() == start_value) {
                $(this).val(stop_value);
            } else {
                $(this).val(start_value);
            }
        });
        $('#patterns').change(function () {
            $('#patterns option:selected').each(function () {
                url = $(this).val();
                if (url) {
                    life.loadPattern(url);
                }
            });
        });
    });
    </script>
   </body>
</html>""" 

    let renderShell = seq {
        yield page }

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
        
    let render (pageState : PageState) = 
       renderShell //(renderRoomList pageState.Rooms)