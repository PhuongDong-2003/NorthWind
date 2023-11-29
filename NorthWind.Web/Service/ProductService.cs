using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Web.Models;

namespace NorthWind.Web.Service
{
    public class ProductService
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;

        private readonly ITokenProvider _tokenProvider;
        public ProductService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient, ITokenProvider tokenProvider)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
            _tokenProvider = tokenProvider;
        }
        
          private async Task<string> GetTokenAsync()
        {
            return await _tokenProvider.LoginAsync();
        }

        private async Task<HttpClient> GetAuthorizedHttpClientAsync()
        {
            var token = await GetTokenAsync();
            var authorizedHttpClient = new HttpClient();
            authorizedHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return authorizedHttpClient;
        }

        public async Task<IEnumerable<Product>> GetProduct() 
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            string apiBaseUrl = _apiUrlsConfiguration.ProductApiUrl;
            var response = httpClientToken.GetFromJsonAsync<IEnumerable<Product>>(apiBaseUrl).Result;
            if (response == null)
            {
                throw new Exception("Không thử lấy danh sách nhân viên từ api");
            }

            return response;
        }

        public async Task<Product> GetProductByID(int ProductID)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{ProductID}";
            var response = await httpClientToken.GetFromJsonAsync<Product>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {ProductID} từ API.");
            }

            return response;
        }

        public async Task InsertProduct(Product product)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = _apiUrlsConfiguration.ProductApiUrl;
            var response = await httpClientToken.PostAsJsonAsync(apiUrl, product);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }

        public async Task UpdateProduct(Product product)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}";
            var response = await httpClientToken.PutAsJsonAsync(apiUrl, product);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên : {response.ReasonPhrase}");
            }

        }

        public async Task DeleteProduct(int id)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{id}";
            var response = await httpClientToken.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Product>> GetProductPage(int page, int pageSize)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.ProductApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClientToken.GetFromJsonAsync<IEnumerable<Product>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }
    }
}