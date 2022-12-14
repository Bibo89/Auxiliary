using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary
{
    internal static class JsonModelHelper<T>
        where T : JsonModel, new()
    {
        private static readonly JsonCollection<T> _collection = new(typeof(T).Name + "s");

        public static async Task<bool> ModifyAsync(T model)
        {

        }

        public static async Task<bool> DeleteAsync(T model)
        {

        }

        public static async Task<T?> GetAsync(Func<T, bool> func, bool createOnFailedFetch)
        {

        }

        public static async Task<T> CreateAsync(Action<T> action)
        {

        }
    }
}
