using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;


namespace NorthWind.Web.Service
{
    public class CustomerService
    {

        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;


        public CustomerService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }

        public async Task<IEnumerable<Customer>> GetCustomer() 
        {

            string apiBaseUrl = _apiUrlsConfiguration.CustomerApiUrl;
            var response = await httpClient.GetFromJsonAsync<IEnumerable<Customer>>(apiBaseUrl);
            if (response == null)
            {

                throw new Exception("Không thử lấy danh sách nhân viên từ api");

            }

            return response;
        }

        public async Task<Customer> GetCustomerByID(string CustomerID)
        {

            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{CustomerID}";
            var response = await httpClient.GetFromJsonAsync<Customer>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {CustomerID} từ API.");
            }

            return response;
        }

        public async Task InsertCustomer(Customer customer)
        {

            var apiUrl = _apiUrlsConfiguration.CustomerApiUrl;
            var response = await httpClient.PostAsJsonAsync(apiUrl, customer);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }

        public async Task UpdateCustomer(Customer customer)
        {

            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}";
            var response = await httpClient.PutAsJsonAsync(apiUrl, customer);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên : {response.ReasonPhrase}");
            }

        }

        public async Task DeleteCustomer(string id)
        {
            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomerPage(int page, int pageSize)
        {

            var apiUrl = $"{_apiUrlsConfiguration.CustomerApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClient.GetFromJsonAsync<IEnumerable<Customer>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên có .");
            }

            return response;
        }



    }
}