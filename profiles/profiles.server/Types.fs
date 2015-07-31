namespace profiles.server

open System

[<AutoOpen>]
module Types =
  type SaveResult<'a> =
    | Success of 'a
    | Errors of string list
