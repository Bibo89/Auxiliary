using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auxiliary.Configuration
{
    public class AuxiliarySettings : ISettings
    {
        [JsonPropertyName("connectionstring")]
        public string ConnectionString { get; set; } = string.Empty;

        [JsonPropertyName("defaultdb")]
        public string MongoStorageName { get; set; } = string.Empty;

        [JsonPropertyName("localpath")]
        public string LocalStorageName { get; set; } = string.Empty;
    }
}
