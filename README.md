# F# exercises designed for a 1 day workshop

## Prerequisites

One of the following should be enough for you to run the exercises:
* [Visual Studio 2013 / 2015](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx) (Windows only)
  * If you already have Visual Studio, you *may* need to [download and install F#](http://www.microsoft.com/en-us/download/confirmation.aspx?id=44011) explicitly
* [Xamarin Studio](https://xamarin.com/download) (OS/X or Windows).
* [MonoDevelop](http://www.monodevelop.com/download/) (Linux, OS/X or Windows)
* [Vagrant](https://www.vagrantup.com/downloads.html) and [VirtualBox](https://www.virtualbox.org/wiki/Downloads) simply by running ``vagrant up`` in the root of the repository

## Workshop Timing

Here are some guidelines as to how the workshop will be broken down:

| Start time | Exercise | Files |
| --- | --- | --- |
| 09:00 | Introductions, setup your REPL | |
| 09:30 | Functions, the core of the language! | [`functions.fsx`](exercises/functions.fsx) |
| 10.30 | Tuples | [`tuples.fsx`](exercises/tuples.fsx) |
| 11.00 | Coffee break | |
| 11.15 | Records | [`records.fsx`](exercises/records.fsx) |
| 11.45 | Discriminated unions & pattern matching (part 1) | [`ADT1.fsx`](exercises/ADT1.fsx) [`ADT2.fsx`](exercises/ADT2.fsx) [`ADT3.fsx`](exercises/ADT3.fsx) [`ADT4.fsx`](exercises/ADT4.fsx) |
| 12.15 | Lunch | |
| 13.00 | Recap of concepts covered so far, opportunity to ask questions | |
| 13.30 | Discriminated unions & pattern matching (part 2) | [`pattern_matching1.fsx`](exercises/pattern_matching1.fsx) [`pattern_matching2.fsx`](exercises/pattern_matching2.fsx)|
| 14.15 | Working with lists | [`lists1.fsx`](exercises/lists1.fsx) [`lists2.fsx`](exercises/lists2.fsx) [`lists3.fsx`](exercises/lists3.fsx) [`lists4.fsx`](exercises/lists4.fsx) |
| 15.15 | Real world example: form validation for a web application | [`profiles`](profiles) |

## Profiles project
The project in the profiles folder is a simple web project that implements simple API validation using the suave web framework.

The validation should be implemented in `profiles.server/Profile1.fs`. The sample provided shows validating that the firstname is a required field.

Tests for all validations are provided in `profiles.tests/Profile1Tests.fs`. You should keep adding validations until all the tests pass.

To run the tests:
* Unix: `./build.sh RunTests`
* Windows: `./build.cmd RunTests`

To run the web app:
* Unix: `./build.sh Run`
* Windows: `./build.cmd Run`

The site should be available on `http://localhost:3000`.

Some useful functions:

* `Char.IsDigit : char -> bool`
* `Char.IsLetter : char -> bool`
* `String.Concat : seq<'t> -> string`

Also remember that string is a sequence of characters. For example the following expression results in the string "1":
`"profile1" |> Seq.filter Char.IsDigit |> String.Concat`

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
