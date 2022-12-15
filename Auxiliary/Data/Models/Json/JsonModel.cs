using System.Text.Json.Serialization;

namespace Auxiliary
{
    public abstract class JsonModel : IModel
    {
        [JsonIgnore]
        public ModelType ModelType { get; } = ModelType.Json;

        public static async Task<bool> SaveAsync<T>(T model, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.SaveAsync(model, cancellationToken);

        public static async Task<bool> DeleteAsync<T>(T model, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.DeleteAsync(model, cancellationToken);

        public static async Task<T> CreateAsync<T>(Action<T> action, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.CreateAsync(action, cancellationToken);

        public static async Task<T?> GetAsync<T>(Func<T, bool> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.GetAsync(func, creationAction, cancellationToken); 
    }
}
