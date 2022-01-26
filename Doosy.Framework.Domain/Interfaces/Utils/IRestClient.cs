using System.Collections.Generic;

namespace Doosy.Framework.Domain
{
    public interface IRestClientBase
    {
        T Get<T>(string url, Dictionary<string, string> headers);
        void Get(string url, Dictionary<string, string> headers);
        void Post(string url, object data, Dictionary<string, string> headers);
        T Post<T>(object data, string url, Dictionary<string, string> headers);
    }
}
