namespace test.coinbase.api.csharp.common.DTO
{
    public class BitCoinPriceDTO
	{
        public required Data data { get; set; }
    }

    public class Data
    {
        public string? @base { get; set; }
        public string? currency { get; set; }
        public string? amount { get; set; }
    }
}

