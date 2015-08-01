namespace profiles.tests
open System
open Xunit
open Swensen.Unquote
open profiles.server
open profiles.server.Profile1
module Profile1Tests =
    let validProfile = {
        FirstName = "Foo"
        LastName = "Bar"
    }

    [<Fact>]
    let ``A valid user is saved`` () =
      test <@ persistProfile validProfile = Success "Saved" @>

    [<Fact>]
    let ``An empty FirstName returns error`` () =
      let profile =
        { validProfile with FirstName = "" }

      test <@ persistProfile profile = Errors ["Firstname is required"] @>

    [<Fact>]
    let ``An empty LastName returns error`` () =
      let profile =
        { validProfile with LastName = "" }

      test <@ persistProfile profile = Errors ["Lastname is required"] @>

    [<Fact>]
    let ``All errors are returned`` () =
      let profile =
        { validProfile with
            FirstName = ""
            LastName = ""}

      test <@ persistProfile profile = Errors ["Firstname is required"; "Lastname is required"] @>
