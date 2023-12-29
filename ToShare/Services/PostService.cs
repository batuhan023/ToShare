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
        private const string ApiUrl = "https://192.168.1.179:45455/api/";

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



        public async Task<Post> GetPostById(int id)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetPostsByPostId?postId={id}");
            return JsonConvert.DeserializeObject<Post>(response);
        }


        public async Task<Apply> ApplyPost(int postId, int userId)
        {
            try
            {
                var apply = new Apply
                {
                    PostId = postId,
                    UserId = userId,
                    ApplyTime = DateTime.Now,
                    IsActive = true,
                    IsAproved = false
                };

                // PostController'ın ApplyPost metoduna POST isteği gönder
                var jsonApply = JsonConvert.SerializeObject(apply);
                var content = new StringContent(jsonApply, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{ApiUrl}Posts/ApplyPost?postId={postId}&userId={userId}", content);

                response.EnsureSuccessStatusCode(); // İsteğin başarılı olup olmadığını kontrol et

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Apply>(result);
            }
            catch (Exception ex)
            {
                // Hata durumunda istemciye uygun bir cevap gönder
                throw new ApplicationException($"Başvuru işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }

    }



}
