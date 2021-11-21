using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using tmpltest.Utilities;

namespace tmplltest.Core.Utilities
{
    public interface IHttpResourceService
    {
        Task<T> Get<T>(string endpoint);

        Task<HttpResponseMessage> SendRequest(string endpoint, HttpMethod method);
    }

    public class HttpResourceService : IHttpResourceService
    {
       // private readonly IConfiguration _config;
        private readonly Uri BaseUrl;

        public HttpResourceService()
        {
            BaseUrl = new Uri(DatabaseCred.BASEURL);
            // _config = config;
            // BaseUrl = new Uri(_config["JokesConfig:BaseUrl"]);
        }

        public async Task<T> Get<T>(string endpoint)
        {
            var response = await SendRequest(endpoint, HttpMethod.Get);

            return await SendResponse<T>(response);
        }

        private async Task<string> ReadResponse(HttpResponseMessage httpResponse)
        {
            var content = await httpResponse.Content.ReadAsStringAsync();
            return content;
        }

        private async Task<T> SendResponse<T>(HttpResponseMessage httpResponse)
        {
            var content = await ReadResponse(httpResponse);
            if (content != null && content.Contains("<"))
            {
                throw new HttpRequestException();
            }
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<HttpResponseMessage> SendRequest(string endpoint, HttpMethod method)
        {
            try
            {
                var apiUrl = new Uri(BaseUrl, endpoint);
                Log.Information(apiUrl.AbsoluteUri.ToString());

                var request = new HttpRequestMessage(method, apiUrl);

                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                };
                var client = new HttpClient(handler);

                var response = await client.SendAsync(request);
                client.Dispose();
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "HttpResourceService");
                throw ex;
            }
        }
    }
}
