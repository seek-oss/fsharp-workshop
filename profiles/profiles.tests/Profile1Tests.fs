namespace profiles.tests
open System
open Xunit
open Swensen.Unquote
open profiles.server
open profiles.server.Profile1
module Profile1Tests =
    let validProfile = {
        Firstname = "Foo"
        Lastname = "Bar"
    }

    [<Fact>]
    let ``A valid user is saved`` () =
      test <@ persistProfile validProfile = Success "Saved" @>

    [<Fact>]
    let ``An empty Firstname returns error`` () =
      let profile =
        { validProfile with Firstname = "" }

      test <@ persistProfile profile = Errors ["Firstname is required"] @>

    [<Fact>]
    let ``An empty Lastname returns error`` () =
      let profile =
        { validProfile with Lastname = "" }

      test <@ persistProfile profile = Errors ["Lastname is required"] @>

    [<Fact>]
    let ``All errors are returned`` () =
      let profile =
        { validProfile with
            FirstName = ""
            Lastname = ""}

      test <@ persistProfile profile = Errors ["Firstname is required"; "Lastname is required"] @>

    [<Fact>]
    let ``Postcode is optional`` () =
      let profile =
        { validProfile with
            Postcode = "" }

      test <@ persistProfile profile = Success "Saved" @>
