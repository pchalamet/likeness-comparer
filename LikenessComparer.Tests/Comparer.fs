module Likeness.Comparer.Tests
open NUnit.Framework
open FsUnit
open Likeness.Comparer

[<Test>]
let TestScalar () =
    areAlike 42 42 |> should be True
    areAlike 42 -42 |> should be False

    areAlike 10.2 10.2 |> should be True
    areAlike 10.2 -10.2 |> should be False

    areAlike System.Double.NaN  System.Double.NaN |> should be True
    areAlike 5 System.Double.NaN |> should be False
    areAlike System.Double.NaN 5 |> should be False

    areAlike "string" "string" |> should be True
    areAlike "string" "" |> should be False

    areAlike null null |> should be True

[<Test>]
let TestCollection () =
    areAlike List.empty List.empty |> should be True
    areAlike [| 5 |] [| 5 |] |> should be True
    areAlike [| 5 |] [| 5; 6 |] |> should be False
    areAlike [| 5 |] [| 15 |] |> should be False

    areAlike [| |] Seq.empty |> should be True
    areAlike [| |] Map.empty |> should be False

    areAlike Map.empty Map.empty |> should be True
    areAlike (Map ["A", 42]) (Map ["A", 42]) |> should be True
    areAlike (Map ["A", 42; "B", 30]) (Map["B", 30; "A", 42]) |> should be True

    areAlike Set.empty Set.empty |> should be True
    areAlike (Set ["A", 42]) (Set["A", 42]) |> should be True
    areAlike (Set ["A", 42; "B", 30]) (Set["B", 30; "A", 42]) |> should be True

    areAlike (seq { 5; 6 }) (seq { 5; 6 }) |> should be True
    areAlike [ 5; 6 ] (seq { 5; 6 }) |> should be True
    areAlike [ 5; 6 ] (seq { 5 }) |> should be False


[<Test>]
let TestCSharp () =
    AreAlike(42, 42) |> should be True

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

    areAlike struct1 struct1 |> should be True
    areAlike struct2 struct2 |> should be True
    areAlike struct1 struct2 |> should be False
    areAlike struct2 struct1 |> should be False

    areAlike struct1 {| A = 42; B = Some "string" |} |> should be True
    areAlike struct1 {| A = 42; B = "string" |} |> should be False
    areAlike struct1n {| A = 42; B = None |} |> should be True
    areAlike struct1n {| A = 42; B = null |} |> should be True

