using System;
using System.Collections.Generic;
using System.Linq;
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
        public CartService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }

        public async Task<Product> GetByID(int ProductID)
        {

            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{ProductID}";
            var response = await httpClient.GetFromJsonAsync<Product>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }
    }
}