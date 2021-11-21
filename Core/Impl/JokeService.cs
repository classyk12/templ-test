using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using tmplltest.Core.Utilities;
using tmpltest.Core.DataModels;

namespace tmplltest.Core.Impl
{
    public interface IJokeService
    {
        Task<List<JokeResponseModel>> GetJokes(int count = 10);
    }

    public class JokeService : IJokeService
    {
        private readonly IHttpResourceService _http ;

        public JokeService(IHttpResourceService http)
        {
            _http = http;
        }
        public async Task<List<JokeResponseModel>> GetJokes(int count = 10)
        {
            try
            {
                string endpoint = $"jokes?count={count}";
                var response = await _http.Get<List<JokeResponseModel>>(endpoint: endpoint);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error($"get jokes exceptions - {ex}");
                throw ex;
            }
        }
    }
}
