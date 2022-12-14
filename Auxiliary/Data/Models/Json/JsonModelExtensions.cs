using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary
{
    public static class JsonModelExtensions
    {
        public static async Task<bool> ModifyAsync<T>(this T? entity)
            where T : JsonModel, new()
            => entity is not null && await JsonModelHelper<T>.ModifyAsync(entity);

        public static async Task<bool> DeleteAsync<T>(this T? entity)
            where T : JsonModel, new()
            => entity is not null && await JsonModelHelper<T>.DeleteAsync(entity);
    }
}
