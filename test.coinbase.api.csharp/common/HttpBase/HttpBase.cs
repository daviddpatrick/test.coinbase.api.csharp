using System.Net.Http.Headers;

namespace test.coinbase.api.csharp.common.HttpBase
{
    public class HttpBase
	{
        private readonly string baseUrl;
        private readonly string authToken;
        private readonly int httpTimeOut;
        private readonly string contentType;
        private readonly int maxRetries;
        private readonly Dictionary<string, string>? extraHeaders;

        public static HttpClient client = new HttpClient();

        public HttpBase(string baseUrl, string authToken, int httpTimeOut, string contentType, int maxRetries = 0, Dictionary<string, string>? extraHeaders = null)
		{
            this.baseUrl = baseUrl;
            this.authToken = authToken;
            this.httpTimeOut = httpTimeOut;
            this.contentType = contentType is null ? "application/json" : contentType;
            this.maxRetries = maxRetries;
            this.extraHeaders = extraHeaders;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (this.extraHeaders is not null)
            {
                foreach (KeyValuePair<string, string> header in this.extraHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            if (this.httpTimeOut is 0)
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            }

            client.BaseAddress = new Uri(baseUrl);
        }

        public static async Task<HttpResponseMessage> GetRequest(string url, bool ensureSucess = true)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(url);
                if (ensureSucess)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return response;
        }

    }
}

