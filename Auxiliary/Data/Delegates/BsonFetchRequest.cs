using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary.Data.Delegates
{
    public static class BsonFetchRequest
    {
        public static BsonFetchRequest<T> Empty<T>()
            where T : BsonModel, new()
            => new(x => true);

        public static BsonFetchRequest<T> FromFunc<T>(Expression<Func<T, bool>> func)
            where T : BsonModel, new()
            => new(func);
    }

    public readonly struct BsonFetchRequest<T>
        where T : BsonModel, new()
    {
        public Expression<Func<T, bool>> Request { get; }

        internal BsonFetchRequest(Expression<Func<T, bool>> request)
        {
            Request = request;
        }

        public static implicit operator BsonFetchRequest<T>(Expression<Func<T, bool>> request)
        {
            return new BsonFetchRequest<T>(request);
        }

        public static implicit operator Expression<Func<T, bool>>(BsonFetchRequest<T> request)
        {
            return request.Request;
        }
    }
}
