namespace Auxiliary
{
    /// <summary>
    ///     Represents a JSON or BSON model.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        ///     Gets the type of this model. This is either <see cref="ModelType.Json"/> or <see cref="ModelType.Bson"/>.
        /// </summary>
        public ModelType ModelType { get; }

        public static async Task<T> CreateAsync<T>(JsonCreateRequest<T> definition)
            where T : JsonModel, new()
            => await JsonModel.CreateAsync(definition.Definition);

        public static async Task<T> CreateAsync<T>(BsonCreateRequest<T> definition)
            where T : BsonModel, new()
            => await BsonModel.CreateAsync(definition.Definition);

        public static async Task<T?> GetAsync<T>(JsonGetRequest<T> request, Action<T>? creationAction = null)
            where T : JsonModel, new()
            => await JsonModel.GetAsync(request.Request, creationAction);

        public static async Task<T?> GetAsync<T>(BsonGetRequest<T> request, Action<T>? creationAction = null)
            where T : BsonModel, new()
            => await BsonModel.GetAsync(request.Request, creationAction);
    }
}
