using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary.Data.Delegates
{
    public static class JsonCreateDefinition
    {
        public static BsonCreateDefinition<T> Empty<T>()
            where T : BsonModel, new()
            => new(x => { });

        public static BsonCreateDefinition<T> FromAction<T>(Action<T> action)
            where T : BsonModel, new()
            => action;
    }

    public readonly struct BsonCreateDefinition<T>
        where T : BsonModel, new()
    {
        public Action<T> Definition { get; }

        internal BsonCreateDefinition(Action<T> definition)
        {
            Definition = definition;
        }

        public static implicit operator BsonCreateDefinition<T>(Action<T> definition)
        {
            return new(definition);
        }

        public static implicit operator Action<T>(BsonCreateDefinition<T> definition)
        {
            return definition.Definition;
        }
    }
}
