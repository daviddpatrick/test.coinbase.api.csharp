using Newtonsoft.Json;

namespace test.coinbase.api.csharp.common.config
{
    public class ConfigurationPoc
    {
        [JsonProperty("env")]
        public Env? Env { get; set; }

        [JsonProperty("coinbase_url")]
        public string? CoinbaseUrl { get; set; }

        public ConfigurationPoc GetConfigurationPoc()
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            using StreamReader reader = new(dir + @"/resources/us.json");
            var json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<ConfigurationPoc>(json);
        }
    }

    public class Env
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

}

