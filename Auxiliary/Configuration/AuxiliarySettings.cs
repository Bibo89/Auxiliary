using System.Text.Json.Serialization;

namespace Auxiliary.Configuration
{
    public class AuxiliarySettings : ISettings
    {
        [JsonPropertyName("connectionstring")]
        public string ConnectionString { get; set; } = string.Empty;

        [JsonPropertyName("mongopath")]
        public string MongoStorageName { get; set; } = string.Empty;

        [JsonPropertyName("localpath")]
        public string LocalStorageName { get; set; } = string.Empty;
    }
}
