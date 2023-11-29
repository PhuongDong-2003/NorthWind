using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Web.Models;

namespace NorthWind.Web.Service
{
    public class EmployeeService

    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        private readonly ITokenProvider _tokenProvider;

        public EmployeeService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient, ITokenProvider tokenProvider)
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
        public async Task DeleteEmployee(int id)
        {  
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{id}";
            var response = await httpClientToken.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Employee> GetEmployeeByID(int employeeId)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{employeeId}";
            var response = await httpClientToken.GetFromJsonAsync<Employee>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {employeeId} từ API.");
            }

            return response;
        }
        public async Task<IEnumerable<Employee>> GetEmployeePage(int page, int pageSize)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{$"p?page={page}&pageSize={pageSize}"}";
            var response = await httpClientToken.GetFromJsonAsync<IEnumerable<Employee>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên có .");
            }

            return response;
        }
        public async Task<IEnumerable<Employee>> GetEmployeePage(int page, int pageSize, string search) 
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            string apiBaseUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            var response = httpClientToken.GetFromJsonAsync<IEnumerable<Employee>>(apiBaseUrl).Result;
            if (response == null)
            {

                throw new Exception("Không thử lấy danh sách nhân viên từ api");

            }

            return response;
        }
        public async Task InsertEmployee(Employee employee)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            var response = await httpClientToken.PostAsJsonAsync(apiUrl, employee);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }


        public async Task UpdateEmployee(Employee employee)
        {
            var httpClientToken = await GetAuthorizedHttpClientAsync();
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{employee.EmployeeId}";
            var response = await httpClientToken.PutAsJsonAsync(apiUrl, employee);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên có ID {employee.EmployeeId}: {response.ReasonPhrase}");
            }

        }

    }
}