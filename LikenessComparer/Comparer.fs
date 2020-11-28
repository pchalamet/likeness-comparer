namespace Likeness
open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open System.Linq
open System.Collections.Generic


type private OrderedContractResolver() =
    inherit DefaultContractResolver()

    override _.CreateProperties(tpe, memberSerialization) =
        let props = base.CreateProperties(tpe, memberSerialization)
        let ordered = props |> Seq.sortBy (fun p -> p.PropertyName)
        ordered.ToList() :> IList<JsonProperty>


module Comparer =
    let private jsonSerializerSettings = JsonSerializerSettings(ContractResolver = OrderedContractResolver())

    let private serialize (x: obj) = JsonConvert.SerializeObject(x, jsonSerializerSettings)

    let AreAlike (x: obj) (y: obj) =
        let contentX = serialize x
        let contentY = serialize y
        contentX = contentY
