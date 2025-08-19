namespace BlackDuckWebApi;

public class BlackDuck : JsonService
{
    public BlackDuck(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public BlackDuck(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    //protected override string? AuthenticationTestUrl => "api/health";


    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.blackducksoftware.user-4+json");

        var res = await GetFromJsonAsync<HealthModel>("/api/current-user", cancellationToken);
        return res?.Version;
    }

    public async Task<Health?> GetHealthAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<HealthModel>("api/health", cancellationToken);
        return res.CastModel<Health>();
    }
}
