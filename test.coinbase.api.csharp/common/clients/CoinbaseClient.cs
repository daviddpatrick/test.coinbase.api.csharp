using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using test.coinbase.api.csharp.common.DTO;

namespace test.coinbase.api.csharp.common.clients
{
    public class CoinbaseClient : HttpBase.HttpBase
    {
        private readonly string baseUrl;
        private readonly string authToken;
        private readonly int httpTimeOut;
        private readonly string contentType;
        private readonly int maxRetries;
        private readonly Dictionary<string, string>? extraHeaders;

        public CoinbaseClient(string baseUrl, string authToken, int httpTimeOut, string contentType, int maxRetries = 0, Dictionary<string, string>? extraHeaders = null)
            : base(baseUrl, authToken, httpTimeOut, contentType, maxRetries, extraHeaders)
        {
            this.baseUrl = baseUrl;
            this.authToken = authToken;
            this.httpTimeOut = httpTimeOut;
            this.contentType = contentType;
            this.maxRetries = maxRetries;
            this.extraHeaders = extraHeaders;
        }

        public async Task<double?> GetBtcUsdPrice(string getUrl="/v2/prices/BTC-USD/buy", bool ensureSucess = true)
        {
            BitCoinPriceDTO? bitCoinPriceDTO;
            HttpResponseMessage response;
            var policy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .RetryAsync(this.maxRetries);

            response = await policy.ExecuteAsync(async () => await GetRequest(getUrl, ensureSucess));

            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic? message = JsonConvert.DeserializeObject(responseBody);

            if (message?.errors is null)
            {
                double bitcoinAmount;
                bitCoinPriceDTO = JsonConvert.DeserializeObject<BitCoinPriceDTO>(responseBody);
                if (double.TryParse(bitCoinPriceDTO.data.amount, out bitcoinAmount))
                {
                    return bitcoinAmount;
                }
                else
                {
                    return double.Parse("1.00");
                }
                
            }
            else
            {
                return double.Parse("1.00");
            }
        }
    }
}

