using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToShare.Models;

namespace ToShare.Services
{
    public class CategoryService
    {
        private const string ApiUrl = "https://192.168.1.167:45455/api/";

        private readonly HttpClient _httpClient;

        public CategoryService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<List<Post>> GetPostsByCategoryId(int id)
        {

            var response = await _httpClient.GetAsync($"{ApiUrl}Posts/GetPostsByCategoryId?categoryId={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<Post>>(content);
                return posts;
            }
            else
            {
                // Hata durumunu burada işleyin, örneğin, hata günlüğüne kaydetme veya boş bir liste döndürme
                return new List<Post>();
            }

            //var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetPostsByCategoryId?id={id}");
            //return JsonConvert.DeserializeObject<List<Post>>(response);
        }

    }
}
