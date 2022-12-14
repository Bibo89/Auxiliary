using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Auxiliary
{
    public abstract class BsonModel : IModel
    {
        [BsonId]
        public abstract ObjectId ObjectId { get; set; }

        [BsonIgnore]
        public abstract ModelState State { get; set; }

        [BsonIgnore]
        public ModelType ModelType { get; } = ModelType.Bson;

        public static async Task<bool> ModifyAsync<T, TField>(T? entity, Expression<Func<T, TField>> expression, TField value)
            where T : BsonModel, new()
            => entity is not null && await BsonModelHelper<T>.ModifyAsync(entity, Builders<T>.Update.Set(expression, value));

        public static async Task<bool> DeleteAsync<T>(T model)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.DeleteAsync(model);

        public static async Task<T> CreateAsync<T>(Action<T> action)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.CreateAsync(action);

        public static async Task<T?> GetAsync<T>(Expression<Func<T, bool>> func, bool createOnFailedFetch = false)
            where T : BsonModel, new()
            => await BsonModelHelper<T>.GetAsync(func, createOnFailedFetch); 
    }
}
