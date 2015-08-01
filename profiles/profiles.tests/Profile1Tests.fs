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
        Postcode = "1234"
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
            Firstname = ""
            Lastname = ""}

      test <@ persistProfile profile = Errors ["Firstname is required"; "Lastname is required"] @>

    [<Fact>]
    let ``Postcode is optional`` () =
      let profile =
        { validProfile with Postcode = "" }

      test <@ persistProfile profile = Success "Saved" @>

    [<Fact>]
    let ``ABCD is not an acceptable postcode`` () =
      let profile =
        { validProfile with Postcode = "ABCD" }

      test <@ persistProfile profile = Errors ["Postcode must be 4 digits"] @>

    [<Fact>]
    let ``123 is not an acceptable postcode`` () =
      let profile =
          { validProfile with Postcode = "123" }

      test <@ persistProfile profile = Errors ["Postcode must be 4 digits"] @>

    [<Fact>]
    let ``12345 is not an acceptable postcode`` () =
      let profile =
          { validProfile with Postcode = "123" }

      test <@ persistProfile profile = Errors ["Postcode must be 4 digits"] @>
