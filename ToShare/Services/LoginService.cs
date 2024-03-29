﻿using System;
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
        private const string ApiUrl = "https://192.168.1.167:45455/api/";

        private readonly HttpClient _httpClient;

        public LoginService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }


        public async Task<User> Register(string username, string usersurname, string email,
            string password, string  phone, string photo, double salary)
        {
            var response = await _httpClient.PostAsync($"{ApiUrl}Users/Register?username={username}&usersurnema={usersurname}" +
                $"&email={email}&password={password}&phone={phone}&photo={photo}&salary={salary}", null);

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(responseData);
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Users/GetUsersByUserId?useremail={email}");
            return JsonConvert.DeserializeObject<User>(response);
        }


        public async Task<List<Category>> Category()
        {
            var response = await _httpClient.GetStringAsync($"{ApiUrl}Posts/GetCategories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public async Task<User> Login(string email, string password)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}Users/Login?email={email}&password={password}");
            
            if(response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<User>>(jsonString);
                
                if(data != null && data.Count>0) 
                {
                    var user = data[0];
                    Preferences.Set("username", user.UserName);
                    Preferences.Set("userid", user.Id);
                    Preferences.Set("useremail", user.UserEmail);
                    return user;
                  
                }

                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
