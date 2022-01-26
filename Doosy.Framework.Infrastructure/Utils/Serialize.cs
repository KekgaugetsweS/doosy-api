using System;
using Doosy.Framework.Domain;
using Newtonsoft.Json;

namespace Doosy.Framework.Infrastructure
{
    public class Serializer : ISerializer
    {
        public object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }

        public string Serialize(object data)
        {
            var results = JsonConvert.SerializeObject(data);

            return results;
        }

        public T Deserialize<T>(string data)
        {
            var results = JsonConvert.DeserializeObject<T>(data);

            return results;
        }
    }
}
