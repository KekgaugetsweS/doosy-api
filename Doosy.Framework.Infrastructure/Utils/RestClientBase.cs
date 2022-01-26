using System;
using System.Collections.Generic;
using Doosy.Framework.Domain;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Doosy.Framework.Infrastructure
{

    public class RestClientBase : IRestClientBase
    {
        ILogger<RestClient> logger;
        ISerializer serializer;

        public RestClientBase(ILogger<RestClient> logger
            , ISerializer serializer)
        {
            this.logger = logger;
            this.serializer = serializer;
        }

        public T Get<T>(string url, Dictionary<string, string> headers)
        {
            var client = new RestClient(url);

            var request = new RestRequest();

            request.Method = Method.Get;

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                    request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }
            var response = client.ExecuteAsync(request, new System.Threading.CancellationToken()).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger.LogError($"Error occured when calling the GET URl:{url} ");
                return default(T);
            }

            T results = this.serializer.Deserialize<T>(response.Content);
            return results;
        }

        public void Get(string url, Dictionary<string, string> headers)
        {
            var baseUrl = url;
            var client = new RestClient(baseUrl);

            var request = new RestRequest();

            request.Method = Method.Get;


            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                    request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }
            var response = client.ExecuteAsync(request, new System.Threading.CancellationToken()).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger.LogError($"Error occured when calling the GET URl:{url}");
            }
        }

        public void Post(string url, object data, Dictionary<string, string> headers)
        {
            var baseUrl = url;
            var client = new RestClient(baseUrl);

            var request = new RestRequest();

            request.Method = Method.Post;


            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                    request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            request.AddJsonBody(data);
            var response = client.ExecuteAsync(request, new System.Threading.CancellationToken()).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger.LogError($"Error occured when calling the POST URl:{url} {data} ");


                throw new Exception("Request Token Step Failed");
            }
        }

        public T Post<T>(object data, string url, Dictionary<string, string> headers)
        {
            var baseUrl = url;
            var client = new RestClient(baseUrl);

            var request = new RestRequest();
            request.Method = Method.Post;


            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                    request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            if (data != null)
                request.AddJsonBody(data);

            var response = client.ExecuteAsync(request,new System.Threading.CancellationToken()).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger.LogError($"Error occured when calling the POST URl:{url} {data} ");

                throw new Exception("Request Token Step Failed");
            }

            T results = serializer.Deserialize<T>(response.Content);

            return results;
        }
    }
}
