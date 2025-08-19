namespace BlackDuckWebApi;

public class Health
{
    internal Health(HealthModel model)
    {
        Database = model.Database;
        Version = model.Version;
        Commit = model.Commit;
    }

    public string? Database { get; }

    public string? Version { get; }

    public string? Commit { get; }
}
