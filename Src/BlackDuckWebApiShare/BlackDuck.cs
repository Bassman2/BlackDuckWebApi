using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace BlackDuckWebApi;

public partial class BlackDuck : JsonService
{
    public BlackDuck(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public BlackDuck(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    //protected override string? AuthenticationTestUrl => "api/health";


    // https://blackduck.ebgroup.elektrobit.com/api/whats-new


    //<script defer = "defer" src="/static/js/792.68b7581c.js"></script>

    //https://blackduck.ebgroup.elektrobit.com/static/js/729.05c05479.js

    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
                

        //client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.blackducksoftware.user-4+json");

        //var res = await GetFromJsonAsync<HealthModel>("/api/current-user", cancellationToken);

        //var entry = await GetStringAsync("", cancellationToken);

        ////var xDoc = XDocument.Parse(entry!.Replace("<!doctype html>", ""));
        ////xDoc.XPathSelectElement("script[@defer='defer']");

        //var htmlDoc = new HtmlDocument();
        //htmlDoc.LoadHtml(entry!);

        ////var node1 = htmlDoc.DocumentNode.SelectSingleNode("script[@defer='defer']");
        //var node = htmlDoc.DocumentNode.SelectSingleNode("//script[@defer='defer']");
        //var path = node.GetAttributeValue("src", "");

        var j = "https://blackduck.ebgroup.elektrobit.com/static/js/729.05c05479.js";

        var scr = await GetStringAsync(j, cancellationToken);

        var match = VersionRegex().Match(scr!);
        if (match.Success)
        {
            var version = match.Groups[1].Value; // "2025.4.1"
            return version;
        }

        return "0.0.0";
    }

    public async Task<Health?> GetHealthAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<HealthModel>("api/health", cancellationToken);
        return res.CastModel<Health>();
    }

    [GeneratedRegex("\"common\\.hubVersion\":\"v(\\d+\\.\\d+\\.\\d+)\"")]
    private static partial Regex VersionRegex();
}
