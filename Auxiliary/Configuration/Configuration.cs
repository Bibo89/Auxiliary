using System.Text.Json;

namespace Auxiliary.Configuration
{
    /// <summary>
    ///     Represents a configuration implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Configuration<T>
        where T : ISettings, new()
    {
        private static string? _basePath;

        /// <summary>
        ///     The default serialization options.
        /// </summary>
        public static readonly JsonSerializerOptions DefaultOptions = new()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            WriteIndented = true
        };

        /// <summary>
        ///     If the settings were read from file or written to it.
        /// </summary>
        public static bool Loaded { get; private set; } = false;

        private static T _settings = new();
        /// <summary>
        ///     The settings implementation itself.
        /// </summary>
        public static T Settings
        {
            get
            {
                if (!Loaded)
                    throw new NotSupportedException("This operation cannot be completed without calling 'Load()' on the generic target.");

                return _settings;
            }
            private set
                => _settings = value;
        }

        /// <summary>
        ///     Reloads the active configuration.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public static T Load(string root)
        {
            if (_basePath is null)
                _basePath = Path.Combine("tshock", $"{root}.json");

            if (!File.Exists(_basePath))
            {
                var obj = new T();

                var content = JsonSerializer.Serialize(obj, DefaultOptions);

                File.WriteAllText(_basePath, content);

                Settings = obj;
            }
            else
            {
                var content = File.ReadAllText(_basePath);

                var obj = JsonSerializer.Deserialize<T>(content, DefaultOptions);

                if (obj is null)
                    throw new JsonException($"Encountered invalid JSON in file: {_basePath}.");

                Settings = obj;
            }

            Loaded = true;

            return Settings;
        }
    }
}
