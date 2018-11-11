namespace MyNamespace

open System
open Newtonsoft.Json

type ByteArrayConverter() =
    inherit JsonConverter()
    override __.CanConvert(t) = t = typeof<byte[]>
    override __.WriteJson(writer, value, serializer) =
        let bytes = value :?> byte[]
        let str = System.Convert.ToBase64String(bytes)
        serializer.Serialize(writer, str)
    override __.ReadJson(reader, _, _, serializer) =
        let value = serializer.Deserialize(reader, typeof<string>) :?> string
        Convert.FromBase64String(value) :> obj


// Put any utilities here
[<AutoOpen>]
module internal Utilities = 

    let x = 1

// Put any runtime constructs here
type DataSource(filename:string) = 
    member this.FileName = filename


// Put the TypeProviderAssemblyAttribute in the runtime DLL, pointing to the design-time DLL
[<assembly:CompilerServices.TypeProviderAssembly("LemonadeInheritProvider.DesignTime.dll")>]
do ()
