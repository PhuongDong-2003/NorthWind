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
    public class OrderService
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;

        private readonly ITokenProvider _tokenProvider;
        public OrderService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient, ITokenProvider tokenProvider)
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

        public async Task <IEnumerable<Order>> GetOrder()
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            string apiBaseUrl = _apiUrlsConfiguration.OrderApiUrl;
            var response = httpClientToken.GetFromJsonAsync<IEnumerable<Order>>(apiBaseUrl).Result;
            if (response == null)
            {
                throw new Exception("Không thử lấy danh sách nhân viên từ api");
            }

            return response;
        }

        public async Task<Order> GetOrderByID(int OrderID)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.OrderApiUrl}/{OrderID}";
            var response = await httpClientToken.GetFromJsonAsync<Order>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm đơn hàng có ID {OrderID} từ API.");
            }

            return response;
        }

        public async Task InsertOrder(Order order)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = _apiUrlsConfiguration.OrderApiUrl;
            var response = await httpClientToken.PostAsJsonAsync(apiUrl, order);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }

        public async Task UpdateOrder(Order order)
        {   
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.OrderApiUrl}";
            var response = await httpClientToken.PutAsJsonAsync(apiUrl, order);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin đơn hàng : {response.ReasonPhrase}");
            }

        }

        public async Task DeleteOrder(int id)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.OrderApiUrl}/{id}";
            var response = await httpClientToken.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa đơn hàng có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Order>> GetOrderPage(int page, int pageSize)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.OrderApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClientToken.GetFromJsonAsync<IEnumerable<Order>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về đơn hàng.");
            }

            return response;
        }

        //  public async Task<Order> Find(int OrderID)
        // {

        //       var order = await GetOrderByID(OrderID);
        //       return order;
            
        // }
    }
}