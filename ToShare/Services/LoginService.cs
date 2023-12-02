using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToShare.Models;

namespace ToShare.Services
{
    public class LoginService
    {
        private const string ApiUrl = "https://192.168.1.114:45456/api/";

        private readonly HttpClient _httpClient;

        public LoginService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }
        public async Task<User> Login(string email, string password)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}Users/Login?email={email}&password={password}");
            
            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<User>>(jsonString);
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
