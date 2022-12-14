using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary
{
    public sealed class JsonCollection<T>
        where T : JsonModel, new()
    {
        private readonly List<T> _collection;

        public JsonCollection(string name)
        {

        }
    }
}
