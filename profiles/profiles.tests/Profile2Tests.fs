namespace profiles.tests
open System
open Xunit
open profiles.server
open profiles.server.Profile2

module Profile2Tests =
    [<Fact>]
    let ``A valid user is saved`` () =
      let inputProfile : ProfileForm =
        { FirstName = "Foo"
          LastName = "Bar"}
      Assert.Equal(
            persistProfile inputProfile,
            Success inputProfile)

    [<Fact>]
    let ``An empty FirstName returns error`` () =
        Assert.Equal(
            persistProfile {
                FirstName = ""
                LastName = "Bar"
            },
            Errors ["Firstname is required"])

    [<Fact>]
    let ``An empty LastName returns error`` () =
        Assert.Equal(
            persistProfile {
                FirstName = "Foo"
                LastName = ""
            },
            Errors ["Lastname is required"])

    [<Fact>]
    let ``All errors are returned`` () =
        Assert.Equal(
            persistProfile {
                FirstName = ""
                LastName = ""
            },
            Errors ["Firstname is required"; "Lastname is required"])
