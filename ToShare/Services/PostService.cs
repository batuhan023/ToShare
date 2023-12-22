using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToShare.Models;

namespace ToShare.Services
{

    public class PostService
    {
        private const string ApiUrl = "https://192.168.1.107:45456/api/";

        private readonly HttpClient _httpClient;

        public PostService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<List<Post>> GetActivePost()
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetActivePosts");
            return JsonConvert.DeserializeObject<List<Post>>(response);
        }
    }


}
