using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary.Data.Delegates
{
    public static class JsonFetchRequest
    {
        public static JsonFetchRequest<T> Empty<T>()
            where T : JsonModel, new()
            => new(x => true);

        public static JsonFetchRequest<T> FromFunc<T>(Func<T, bool> func)
            where T : JsonModel, new()
            => new(func);
    }

    public readonly struct JsonFetchRequest<T>
        where T : JsonModel, new()
    {
        public Func<T, bool> Request { get; }

        internal JsonFetchRequest(Func<T, bool> request)
        {
            Request = request;
        }

        public static implicit operator JsonFetchRequest<T>(Func<T, bool> request)
        {
            return new JsonFetchRequest<T>(request);
        }

        public static implicit operator Func<T, bool>(JsonFetchRequest<T> request)
        {
            return request.Request;
        }
    }
}
