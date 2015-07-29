namespace profiles.tests
open System
open Xunit
open Swensen.Unquote
open profiles.server.Profile1

module Profile1Tests = 
    [<Fact>]
    let ``A valid user is saved`` () =
        test
            <@ persistProfile {
                    FirstName = "Foo"
                    LastName = "Bar"
               } = Choice1Of2 "Saved" @>

    [<Fact>]
    let ``An empty FirstName returns error`` () =
        test
            <@ persistProfile {
                    FirstName = ""
                    LastName = "Bar"
               } = Choice2Of2 ["Firstname is required"] @>

    [<Fact>]
    let ``An empty LastName returns error`` () =
        test
            <@ persistProfile {
                    FirstName = "Foo"
                    LastName = ""
            } = Choice2Of2 ["Lastname is required"] @>

    [<Fact>]
    let ``All errors are returned`` () =
        test
            <@ persistProfile {
                    FirstName = ""
                    LastName = ""
            } = Choice2Of2 ["Firstname is required"; "Lastname is required"] @>
