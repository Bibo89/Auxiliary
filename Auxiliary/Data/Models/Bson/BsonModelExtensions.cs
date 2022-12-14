using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary
{
    public static class BsonModelExtensions
    {
        public static async Task<bool> ModifyAsync<T, TField>(this T? entity, Expression<Func<T, TField>> expression, TField value)
            where T : BsonModel, new()
            => entity is not null && await BsonModelHelper<T>.ModifyAsync(entity, Builders<T>.Update.Set(expression, value));

        public static async Task<bool> DeleteAsync<T>(T model)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.DeleteAsync(model);
    }
}
