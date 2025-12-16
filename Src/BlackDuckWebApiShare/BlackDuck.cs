

namespace BlackDuckWebApi;

public partial class BlackDuck : JsonService
{
    public BlackDuck(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public BlackDuck(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Configures the provided <see cref="HttpClient"/> instance with specific default headers required for API requests.
    /// This includes setting the User-Agent, Accept, and API version headers.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> to configure for GitHub API usage.</param>
    /// <param name="appName">The name of the application, used as the User-Agent header value.</param>
    protected override void InitializeClient(HttpClient client, string appName)
    {
        client.DefaultRequestHeaders.Add("User-Agent", appName);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    //protected override string? AuthenticationTestUrl => "api/health";

    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var main = await GetStringAsync("", cancellationToken);
        
        var match = ScriptRegex().Match(main!);
        if (match.Success)
        {
            var path792 = match.Groups[1].Value; 
            var text792 = await GetStringAsync(path792, cancellationToken);

            match = ConvRegex().Match(text792!);
            if (match.Success)
            {
                var path729 = match.Groups[1].Value;
                var text729 = await GetStringAsync($"/static/js/729.{path729}.js", cancellationToken);
                match = VersionRegex().Match(text729!);
                if (match.Success)
                {
                    var version = match.Groups[1].Value; 
                    return version;
                }
            }
        }
        return "0.0.0";
    }

    public async Task<Health?> GetHealthAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<HealthModel>("api/health", cancellationToken);
        return res.CastModel<Health>();
    }

    [GeneratedRegex("<script\\sdefer=\"defer\"\\ssrc=\"([^\"]+)\">")]
    private static partial Regex ScriptRegex();

    [GeneratedRegex(",729:\"([^\"]+)\",")]
    private static partial Regex ConvRegex();

    [GeneratedRegex("\"common\\.hubVersion\":\"v(\\d+\\.\\d+\\.\\d+)\"")]
    private static partial Regex VersionRegex();
}
