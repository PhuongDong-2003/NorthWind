using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;


namespace NorthWind.Shop.Service
{
    public class ProductService
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        public ProductService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }

       public async Task<IEnumerable<Product>> GetProductPage(int page=1, int pageSize=10)
        {

            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClient.GetFromJsonAsync<IEnumerable<Product>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về sản phẩm.");
            }

            return response;
        }

        public async Task<Product> GetByName(string ProductName)
        {

            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{$"Name?ProductName={ProductName}"}";
            var response = await httpClient.GetFromJsonAsync<Product>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }

       
    }
}