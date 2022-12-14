using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auxiliary.Data.Delegates
{
    public static class BsonCreateDefinition
    {
        public static JsonCreateDefinition<T> Empty<T>()
            where T : JsonModel, new()
        {
            return new(x => { });
        }

        public static JsonCreateDefinition<T> FromAction<T>(Action<T> action)
            where T : JsonModel, new()
        {
            return action;
        }
    }

    public readonly struct JsonCreateDefinition<T>
        where T : JsonModel, new()
    {
        public Action<T> Definition { get; }

        internal JsonCreateDefinition(Action<T> definition)
        {
            Definition = definition;
        }

        public static implicit operator JsonCreateDefinition<T>(Action<T> definition)
        {
            return new(definition);
        }

        public static implicit operator Action<T>(JsonCreateDefinition<T> definition)
        {
            return definition.Definition;
        }
    }
}
