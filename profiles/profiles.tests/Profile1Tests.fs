namespace profiles.tests
open System
open Xunit
open profiles.server.Profile1

module Profile1Tests = 
    [<Fact>]
    let ``A valid user is saved`` () =
        Assert.Equal(
            persistProfile {
                FirstName = "Foo"
                LastName = "Bar"
            },
            Choice1Of2 "Saved")

    [<Fact>]
    let ``An empty FirstName returns error`` () =
        Assert.Equal(
            persistProfile {
                FirstName = ""
                LastName = "Bar"
            },
            Choice2Of2 ["Firstname is required"])

    [<Fact>]
    let ``An empty LastName returns error`` () =
        Assert.Equal(
            persistProfile {
                FirstName = "Foo"
                LastName = ""
            },
            Choice2Of2 ["Lastname is required"])

    [<Fact>]
    let ``All errors are returned`` () =
        Assert.Equal(
            persistProfile {
                FirstName = ""
                LastName = ""
            },
            Choice2Of2 ["Firstname is required"; "Lastname is required"])