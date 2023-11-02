using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;

namespace DCRHelper
{
    public class ApiCall
    {
        //Uri baseAddress = new Uri("https://localhost:7169/api");
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config; 

        public ApiCall()
        {
        }
        public ApiCall(IConfiguration configuration)
        {
            _config = configuration;
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = baseAddress;
        }

        public async Task<string> consumeapi(string body = "", string apiPath = "")
        {
            var baseurl = _config["BaseUri"];

            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(baseurl+ apiPath, content);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}