﻿module Falco.Tests.Html

open Falco.ViewEngine
open FsUnit.Xunit
open Xunit
        
[<Fact>]
let ``Text should not be encoded`` () =
    let rawText = raw "<div>"
    renderNode rawText |> should equal "<div>"

[<Fact>]
let ``Text should be encoded`` () =
    let encodedText = enc "<div>"
    renderNode encodedText |> should equal "&lt;div&gt;"

[<Fact>]
let ``Self-closing tag should render with trailing slash`` () =
    let t = selfClosingTag "hr" [ _class "my-class" ]
    renderNode t |> should equal "<hr class=\"my-class\" />"

[<Fact>]
let ``Standard tag should render with multiple attributes`` () =
    let t = tag "div" [ attr "class" "my-class"; _autofocus; attr "data-bind" "slider" ] []
    renderNode t |> should equal "<div class=\"my-class\" autofocus data-bind=\"slider\"></div>"

[<Fact>]
let ``Should produce valid html doc`` () =
    let doc = html [] [
            body [] [
                    div [ _class "my-class" ] [
                            h1 [] [ raw "hello" ]
                        ]
                ]
        ]
    renderHtml doc |> should equal "<!DOCTYPE html><html><body><div class=\"my-class\"><h1>hello</h1></div></body></html>"
