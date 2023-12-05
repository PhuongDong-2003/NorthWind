using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;
using NorthWind.Shop.Service;


namespace NorthWind.Web.Service
{
    public class CustomerService
    {

        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        private readonly ITokenProvider _tokenProvider;

        
        public CustomerService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient, ITokenProvider tokenProvider)
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

        public async Task<IEnumerable<Customer>> GetCustomer() 
        {

            var httpClientToken = await GetAuthorizedHttpClientAsync();
            string apiBaseUrl = _apiUrlsConfiguration.CustomerApiUrl;
            var response = await httpClientToken.GetFromJsonAsync<IEnumerable<Customer>>(apiBaseUrl);
            if (response == null)
            {

                throw new Exception("Không thử lấy danh sách nhân viên từ api");

            }

            return response;
        }

        public async Task<Customer> GetCustomerByID(string CustomerID)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{CustomerID}";
            var response = await httpClientToken.GetFromJsonAsync<Customer>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {CustomerID} từ API.");
            }

            return response;
        }

        public async Task InsertCustomer(Customer customer)
        {
             var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = _apiUrlsConfiguration.CustomerApiUrl;
            var response = await httpClientToken.PostAsJsonAsync(apiUrl, customer);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}";
            var response = await httpClientToken.PutAsJsonAsync(apiUrl, customer);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên : {response.ReasonPhrase}");
            }

        }

        public async Task DeleteCustomer(string id)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{id}";
            var response = await httpClientToken.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomerPage(int page, int pageSize)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClientToken.GetFromJsonAsync<IEnumerable<Customer>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên có .");
            }

            return response;
        }



    }
}