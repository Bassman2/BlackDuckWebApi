namespace BlackDuckWebApi.Model;

[JsonSourceGenerationOptions(
    JsonSerializerDefaults.Web,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true,
    AllowTrailingCommas = true
    //Converters = [ 
    //    //typeof(TextJsonConverter), 
    //    typeof(BooleanJsonConverter) ]
    )]

[JsonSerializable(typeof(HealthModel))]

internal partial class SourceGenerationContext : JsonSerializerContext
{ }