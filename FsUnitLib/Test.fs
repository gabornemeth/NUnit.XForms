module FsUnitTests.Test

open NUnit.Framework
open FsUnit

// Sample tests using FsUnit

[<Test>]
let ``Success``() = 
    true |> should equal true

[<Test>]
let ``Fail``() = 
    true |> should equal false
