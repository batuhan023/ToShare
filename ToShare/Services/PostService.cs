using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToShare.Models;


namespace ToShare.Services
{

    public class PostService
    {
        private const string ApiUrl = "https://192.168.1.123:45455/api/";

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


        public async Task<List<Post>> GetPostsByLetter(string letter)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/SearchPostsByLetter?letter={letter}");
            return JsonConvert.DeserializeObject<List<Post>>(response);
        }

        public async Task<Post> GetPostById(int id)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetPostsByPostId?postId={id}");
            return JsonConvert.DeserializeObject<Post>(response);
        }


        public async Task<Post> AddPost(int userid, int categorid, string name,
            string adres, int count, string description, string image, DateTime endtime)
        {
            var response = await _httpClient.PostAsync($"{ApiUrl}Posts/AddNewPost?userId={userid}&categoryId={categorid}" +
                $"&name={name}&adress={adres}&count={count}&description={description}&image={image}&endtime={endtime}",null);

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Post>(responseData);
        }

        //public async Task<Post> AddPost(int userid, int categorid, string name,
        //    string adres, int count, string description, string image, DateTime endtime)
        //{
        //    var postData = new
        //    {
        //        userId = userid,
        //        categoryId = categorid,
        //        name,
        //        adres,
        //        count,
        //        description,
        //        image,
        //        endtime
        //    };

        //    var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}Posts/AddNewPost", postData);

        //    response.EnsureSuccessStatusCode(); // HTTP hata durumlarını kontrol et

        //    var responseData = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<Post>(responseData);
        //}



        public async Task<List<Post>> GetApplied(int userid)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetUserAppliedPosts?userId={userid}");
            return JsonConvert.DeserializeObject<List<Post>>(response);
        }

        public async Task<IActionResult> ApplyPost(int userId, int postId)
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
                var deserializedResult = JsonConvert.DeserializeObject<Apply>(result);

                return new OkObjectResult(deserializedResult);

            }
            catch (Exception ex)
            {
                // Hata durumunda istemciye uygun bir cevap gönder
                throw new ApplicationException($"Başvuru işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }

    }



}
