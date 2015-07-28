namespace Life.Server

open System.IO
open Newtonsoft.Json.FSharp
open Newtonsoft.Json

module JSON = 
    let jsonSerializer = 
        JsonSerializer.Create (JsonSerializerSettings() 
        |> Newtonsoft.Json.FSharp.Serialisation.extend)

    let deserialise<'t> (data:byte array) : 't =
        use ms = new MemoryStream(data)
        use jsonReader = new JsonTextReader(new StreamReader(ms))
        jsonSerializer.Deserialize<'t>(jsonReader)

    let serialize (data:'t) : string =
        use sw = new StringWriter()
        use jsonWriter = new JsonTextWriter(sw)
        jsonSerializer.Serialize(jsonWriter, data)
        sw.ToString()