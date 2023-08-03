using NUnit.Allure.Core;
using test.coinbase.api.csharp.common.clients;

namespace test.coinbase.api;

[AllureNUnit]
[TestFixture]
public class CoinbaseApiTests
{
    public CoinbaseClient coinbaseClient;

    [OneTimeSetUp]
    public void Setup()
    {
        coinbaseClient = new CoinbaseClient("https://api.coinbase.com", "", 0, "", 0, null);
    }

    [Test]
    public async Task GetTheCurrentPriceOfBitcoin()
    {
        
        double getBtcUsdPrice = (double)await coinbaseClient.GetBtcUsdPrice();
        Assert.Greater(getBtcUsdPrice, double.Parse("1.00"));
        Console.WriteLine($"The price of bitcoin is: ${getBtcUsdPrice}.");
    }

    [Test]
    public async Task GetAFakePriceOfBitcoin()
    {
        double getBtcUsdPrice = (double)await coinbaseClient.GetBtcUsdPrice("/prices/BTC-USD/buy", false);
        double fakeBitCoinAmount = 1.00D;
        Assert.That(fakeBitCoinAmount, Is.EqualTo(getBtcUsdPrice));
        Console.WriteLine($"Hmmm, looks like the price of bitcoin is: ${getBtcUsdPrice} which isn't right.");
    }
}
