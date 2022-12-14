using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auxiliary.Data
{
    public sealed class LocalCollectionBase<T>
    {
        public string Path { get; }

        public List<T> Values { get; }

        public JsonSerializerOptions SerializerOptions { get; }

        public LocalCollectionBase(string path, JsonSerializerOptions options)
        {
            Path = path;
            SerializerOptions = options;

            Values = ReadValues();
        }

        public List<T> ReadValues()
        {
            
        }

        public List<T> CreateValues()
        {

        }

        public void SaveValues()
        {

        }
    }
}
