namespace BlackDuckWebApiUnitTest;

[TestClass]
public class BlackDuckVersionUnitTest : BlackDuckBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var BlackDuck = new BlackDuck(storeKey, appName);

        var health = await BlackDuck.GetHealthAsync();

        Assert.IsNotNull(health, "BlackDuck version is null");
        Assert.AreEqual(new Version(11,6,1), new Version(health.Version!), nameof(health.Version));
    }
}
