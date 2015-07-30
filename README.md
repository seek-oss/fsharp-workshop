# F# exercises designed for a 1 day workshop

## Prerequisites

One of the following should be enough for you to run the exercises:
* [Visual Studio 2013 / 2015](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx) (Windows only)
  * If you already have Visual Studio, you *may* need to [download and install F#](http://www.microsoft.com/en-us/download/confirmation.aspx?id=44011) explicitly
* [Xamarin Studio](https://xamarin.com/download) (OS/X or Windows).
* [MonoDevelop](http://www.monodevelop.com/download/) (Linux, OS/X or Windows)
* [Vagrant](https://www.vagrantup.com/downloads.html) and [VirtualBox](https://www.virtualbox.org/wiki/Downloads) simply by running ``vagrant up`` in the root of the repository

## Exercise Order

From the exercises folder:

1. functions.fsx
2. list.fsx
3. ADTs01.fsx
4. ADTs02.fsx
5. ADTs03.fsx
6. ADTs04.fsx
7. async.fsx

## Game of Life project
The project in the life folder is a suave based game of life web application with some critical elements removed.

The UI is a modified version of http://www.julianpulgarin.com/canvaslife/ that moves the computation to the server.

Several critical elements have been removed and are left as an exercise:

1. The easiest missing piece is to enable the sample patterns to be loaded. You should use the dataStore to load the pattern and return it as the response body. (https://github.com/SEEK-Jobs/fsharp-workshop/blob/master/life/Life.Server/Program.fs#L40)
2. The next piece is to implement the game of life algorithm. The web part of this has been wired up for you but the implementation in: https://github.com/SEEK-Jobs/fsharp-workshop/blob/master/life/Life.Server/Game.fs is missing.
3. The final piece is enable patterns to be saved.

  a.  You will need to implement the run length encoding algorithm: https://github.com/SEEK-Jobs/fsharp-workshop/blob/master/life/Life.Server/RLE.fs

  b. You will then need to implement the PUT endpoint: https://github.com/SEEK-Jobs/fsharp-workshop/blob/master/life/Life.Server/Program.fs#L37

Make use of the Life.Tests project that already has xUnit configured.

To run the tests:
* Unix: `./build.sh RunTests`
* Windows: `./build.cmd RunTests`

To run the web app and restart the server when source changes:
* Unix: `./build.sh Watch`
* Windows: `./build.cmd Watch`
