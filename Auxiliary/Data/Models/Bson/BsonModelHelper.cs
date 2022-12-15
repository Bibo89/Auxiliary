using MongoDB.Driver;
using System.Linq.Expressions;

namespace Auxiliary
{
    internal static class BsonModelHelper<T>
        where T : BsonModel, new()
    {
        public static readonly BsonCollection<T> Collection = new(typeof(T).Name + "s");

        public static async Task<bool> SaveAsync(T model, UpdateDefinition<T> updateDefinition, CancellationToken cancellationToken = default)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted or ModelState.Deserializing)
                return false;

            return await Collection.ModifyDocumentAsync(model, updateDefinition, cancellationToken);
        }

        public static async Task<bool> DeleteAsync(T model, CancellationToken cancellationToken = default)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted)
                return false;

            model.State = ModelState.Deleted;

            return await Collection.DeleteDocumentAsync(model, cancellationToken);
        }

        public static async Task<T?> GetAsync(Expression<Func<T, bool>> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
        {
            var value = await Collection.FindDocumentAsync(func, cancellationToken);

            if (value is null)
            {
                if (creationAction is not null)
                    value = await CreateAsync(creationAction, cancellationToken);

                else
                    return default;
            }

            value.State = ModelState.Ready;

            return value;
        }

        public static async Task<T> CreateAsync(Action<T> action, CancellationToken cancellationToken = default)
        {
            var value = new T();

            action(value);

            await Collection.InsertOrUpdateDocumentAsync(value, cancellationToken);

            value.State = ModelState.Ready;

            return value;
        }
    }
}
