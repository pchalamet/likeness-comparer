module Likeness.Comparer.Tests
open NUnit.Framework
open FsUnit
open Likeness.Comparer

[<Test>]
let TestScalar () =
    AreAlike 42 42 |> should be True
    AreAlike 42 -42 |> should be False

    AreAlike 10.2 10.2 |> should be True
    AreAlike 10.2 -10.2 |> should be False

    AreAlike System.Double.NaN  System.Double.NaN |> should be True
    AreAlike 5 System.Double.NaN |> should be False
    AreAlike System.Double.NaN 5 |> should be False

    AreAlike "string" "string" |> should be True
    AreAlike "string" "" |> should be False

    AreAlike null null |> should be True

[<Test>]
let TestCollection () =
    AreAlike List.empty List.empty |> should be True
    AreAlike [| 5 |] [| 5 |] |> should be True
    AreAlike [| 5 |] [| 5; 6 |] |> should be False
    AreAlike [| 5 |] [| 15 |] |> should be False

    AreAlike [| |] Seq.empty |> should be True
    AreAlike [| |] Map.empty |> should be False

    AreAlike Map.empty Map.empty |> should be True
    AreAlike (Map ["A", 42]) (Map ["A", 42]) |> should be True
    AreAlike (Map ["A", 42; "B", 30]) (Map["B", 30; "A", 42]) |> should be True

    AreAlike Set.empty Set.empty |> should be True
    AreAlike (Set ["A", 42]) (Set["A", 42]) |> should be True
    AreAlike (Set ["A", 42; "B", 30]) (Set["B", 30; "A", 42]) |> should be True

    AreAlike (seq { 5; 6 }) (seq { 5; 6 }) |> should be True
    AreAlike [ 5; 6 ] (seq { 5; 6 }) |> should be True
    AreAlike [ 5; 6 ] (seq { 5 }) |> should be False


type Struct1 =
    { A: int 
      B: string option }

type Struct2 =
    { A: int
      B: string
      C: float }

type DU =
    | Case1
    | Case2 of string

 [<Test>]
 let TestStructs () =
    let struct1 = {Struct1.A = 42; B = Some "string"}
    let struct1n = {Struct1.A = 42; B = None}
    let struct2 = {Struct2.A = 42; B = "string"; C = 37.2 }

    AreAlike struct1 struct1 |> should be True
    AreAlike struct2 struct2 |> should be True
    AreAlike struct1 struct2 |> should be False
    AreAlike struct2 struct1 |> should be False

    AreAlike struct1 {| A = 42; B = Some "string" |} |> should be True
    AreAlike struct1 {| A = 42; B = "string" |} |> should be False
    AreAlike struct1n {| A = 42; B = None |} |> should be True
    AreAlike struct1n {| A = 42; B = null |} |> should be True

