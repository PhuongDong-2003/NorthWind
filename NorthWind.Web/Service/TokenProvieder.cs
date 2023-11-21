using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Web.Models;

namespace NorthWind.Web.Service
{
    public class TokenProvieder : ITokenProvider
    {

        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;


        public TokenProvieder(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }
         public async Task<string> LoginAsync()
    {
       
            var loginData = new { Username = "web", Password = "123" };
            var response = await httpClient.PostAsJsonAsync("http://localhost:5111/api/Authentication/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return responseData.Token;
            }
            else
            {
                throw new Exception($"Login failed with status code {response.StatusCode}");
            }
        
    }
        public async Task SendApiRequestAsync(string endpoint, HttpMethod method, string token)
    {
        using (var httpClient = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://localhost:5111/api/{endpoint}"),
                Method = method
            };

            // Chèn token vào header để xác thực
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
            }
            else
            {
                Console.WriteLine($"API request failed with status code {response.StatusCode}");
            }
        }
    }
    }
}