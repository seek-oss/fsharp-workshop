namespace profiles.server

open System.IO
open Newtonsoft.Json.FSharp
open Newtonsoft.Json

module JsonSerializer = 
    let jsonSerializer = 
        JsonSerializer.Create (JsonSerializerSettings() 
        |> Newtonsoft.Json.FSharp.Serialisation.extend)

    let deserialize<'t> (data:byte array) : 't =
        use ms = new MemoryStream(data)
        use jsonReader = new JsonTextReader(new StreamReader(ms))
        jsonSerializer.Deserialize<'t>(jsonReader)

    let serialize (data:'t) : byte array =
        use sw = new StringWriter()
        use jsonWriter = new JsonTextWriter(sw)
        jsonSerializer.Serialize(jsonWriter, data)
        System.Text.Encoding.UTF8.GetBytes(sw.ToString())