using MongoDB.Driver;
using System.Text.Json;

namespace Auxiliary
{
    /// <summary>
    ///     Represents the configuration for a mongo database.
    /// </summary>
    public class StorageConfiguration
    {
        /// <summary>
        ///     The database to use.
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        ///     The url to connect to.
        /// </summary>
        public MongoUrl? DatabaseUri { get; set; }

        /// <summary>
        ///     The root path for local storage.
        /// </summary>
        public string LocalUri { get; set; } = string.Empty;

        /// <summary>
        ///     The serialization options for local storage.
        /// </summary>
        public JsonSerializerOptions? Serializer { get; set; }
    }
}
