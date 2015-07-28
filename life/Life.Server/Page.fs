namespace Life.Server

open FSharpx.Collections

module Page =
    let pageStart = """
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
        <option value="">---</option>"""

    let pageEnd = """
        </select>
        <input type="button" value="-" onclick="life.changeSpeed(false); $('#speed').text(100 - (life.speed / 10));">
        Speed: <span id="speed"></span>/100
        <input type="button" value="+" onclick="life.changeSpeed(true); $('#speed').text(100 - (life.speed / 10));">
        <input type="button" value="Next Generation" onclick="life.nextGen();">
        <input type="button" value="Clear" onclick="life.clear();">
    </p>
    <p>
        <input type="text" value="pattern name" id="patternName"></input>
        <input type="button" value="Save" onclick="life.save();">
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

    let renderShell patterns = seq {
        yield pageStart
        yield! patterns 
        yield pageEnd }

    let renderPattern (pattern : Pattern) = seq {
        yield sprintf """<option value="/pattern/%s">%s</option>""" pattern.Name pattern.Name
    }

    let renderPatternList (patterns : list<Pattern>) = seq {
        for pattern in patterns do
            yield! renderPattern pattern
    }

    let render (pageState : ServerState) = 
       renderShell (renderPatternList pageState.Patterns)
