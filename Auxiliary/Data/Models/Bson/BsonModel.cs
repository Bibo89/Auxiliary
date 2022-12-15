using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Auxiliary
{
    public abstract class BsonModel : IModel
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        [BsonIgnore]
        public ModelState State { get; set; }

        [BsonIgnore]
        public ModelType ModelType { get; } = ModelType.Bson;

        public static async Task<bool> SaveAsync<T, TField>(T? model, Expression<Func<T, TField>> expression, TField value, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => model is not null && await BsonModelHelper<T>.SaveAsync(model, Builders<T>.Update.Set(expression, value), cancellationToken);

        public static async Task<bool> DeleteAsync<T>(T model, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.DeleteAsync(model, cancellationToken);

        public static async Task<T> CreateAsync<T>(Action<T> action, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.CreateAsync(action, cancellationToken);

        public static async Task<T?> GetAsync<T>(Expression<Func<T, bool>> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.GetAsync(func, creationAction, cancellationToken);
    }
}
