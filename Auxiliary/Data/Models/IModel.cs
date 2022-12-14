using Auxiliary.Data.Delegates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;

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

        public static async Task<T> CreateAsync<T>(JsonCreateDefinition<T> definition)
            where T : JsonModel, new()
            => await JsonModel.CreateAsync(definition.Definition);

        public static async Task<T> CreateAsync<T>(BsonCreateDefinition<T> definition)
            where T : BsonModel, new()
            => await BsonModel.CreateAsync(definition.Definition);

        public static async Task<T?> GetAsync<T>(JsonFetchRequest<T> request, bool createOnFailedFetch = false)
            where T : JsonModel, new()
            => await JsonModel.GetAsync(request.Request, createOnFailedFetch);

        public static async Task<T?> GetAsync<T>(BsonFetchRequest<T> request, bool createOnFailedFetch = false)
            where T : BsonModel, new()
            => await BsonModel.GetAsync(request.Request, createOnFailedFetch);
    }
}
