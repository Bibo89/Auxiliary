using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auxiliary
{
    public abstract class JsonModel : IModel
    {
        [JsonIgnore]
        public ModelType ModelType { get; } = ModelType.Json;

        public static async Task<bool> ModifyAsync<T>(T model)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.ModifyAsync(model);

        public static async Task<bool> DeleteAsync<T>(T model)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.DeleteAsync(model);

        public static async Task<T> CreateAsync<T>(Action<T> action)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.CreateAsync(action);

        public static async Task<T?> GetAsync<T>(Func<T, bool> func, bool createOnFailedFetch = false)
            where T : JsonModel, new()
            => await JsonModelHelper<T>.GetAsync(func, createOnFailedFetch);
    }
}
