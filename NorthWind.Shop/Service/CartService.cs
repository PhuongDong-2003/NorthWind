using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;

namespace NorthWind.Shop.Service
{
    public class CartService
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        private readonly ITokenProvider _tokenProvider;

        
        public CartService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient, ITokenProvider tokenProvider)
        {
            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
            _tokenProvider = tokenProvider;
        }

        private  Task<string> GetTokenAsync()
        {
            return  _tokenProvider.LoginAsync();
        }

        private async Task<HttpClient> GetAuthorizedHttpClientAsync()
        {
            var token = await GetTokenAsync();
            var authorizedHttpClient = new HttpClient();
            authorizedHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return authorizedHttpClient;
        }


        public async Task<Product> GetByID(int ProductID)
        
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{ProductID}";
            var response = await httpClientToken.GetFromJsonAsync<Product>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }
    }
}