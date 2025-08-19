namespace BlackDuckWebApi.Model;

internal class HealthModel
{
    [JsonPropertyName("database")]
    public string? Database { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("commit")]
    public string? Commit { get; set; }
}
