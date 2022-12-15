using MongoDB.Driver;
using System.Linq.Expressions;

namespace Auxiliary
{
    public static class BsonModelExtensions
    {
        public static async Task<bool> SaveAsync<T, TField>(this T? model, Expression<Func<T, TField>> expression, TField value, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => model is not null && await BsonModelHelper<T>.SaveAsync(model, Builders<T>.Update.Set(expression, value), cancellationToken);

        public static async Task<bool> DeleteAsync<T>(this T model, CancellationToken cancellationToken = default)
            where T : BsonModel, new()
            => model is not null && await BsonModelHelper<T>.DeleteAsync(model, cancellationToken);
    }
}
