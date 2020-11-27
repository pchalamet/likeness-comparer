# LikenessComparer

[![Build status](https://github.com/pchalamet/likeness-comparer/workflows/build/badge.svg)](https://github.com/pchalamet/likeness-comparer/actions?query=workflow%3Abuild) 

[![Nuget](https://img.shields.io/nuget/v/LikenessComparer?logo=nuget)](https://nuget.org/packages/LikenessComparer)

This is a brain dead duck like comparer. It just tries to compare objects of maybe different types and check if they look alike.

In order to do this, objects are serialized to Json (with ordered fields) and simply compared. That's it.

.net objects (C# and F#) are supported, especially F# collections (Map, Set, List, Seq) and F# anonymous records.

NOTE1: do not use this on production system. It's been done to perform comparison in unit tests.

NOTE2: peformance can't be good since serialization to Json is done.

NOTE3: this use Newtonsoft.Json.
