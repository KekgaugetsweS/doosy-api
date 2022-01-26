using System;

namespace Doosy.Framework.Domain
{
    public interface ISerializer
    {
        string Serialize(object data);

        T Deserialize<T>(string data);

        object Deserialize(string data, Type type);



    }
}
