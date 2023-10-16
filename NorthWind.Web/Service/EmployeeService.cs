using System;
using System.Collections.Generic;
using System.Linq;
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


        public EmployeeService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }

        public async Task DeleteEmployee(int id)
        {
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Employee> GetEmployeeByID(int employeeId)
        {

            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{employeeId}";
            var response =  await httpClient.GetFromJsonAsync<Employee>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {employeeId} từ API.");
            }

            return response;
        }

        public IEnumerable<Employee> GetEmployee()
        {

            string apiBaseUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            var response = httpClient.GetFromJsonAsync<IEnumerable<Employee>>(apiBaseUrl).Result;
            if (response == null)
            {

                throw new Exception("Không thử lấy danh sách nhân viên từ api");

            }

            return response;
        }

        public  async Task InsertEmployee(Employee employee)
        {

            var apiUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            // var result = JsonSerializer.Serialize(employee);
            var response = await httpClient.PostAsJsonAsync(apiUrl,employee);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
               Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");                              
                
            }           
        }
       

        public async Task UpdateEmployee (Employee employee)
        {

            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{employee.EmployeeId}";
            var response = await httpClient.PutAsJsonAsync(apiUrl, employee);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên có ID {employee.EmployeeId}: {response.ReasonPhrase}");
            }

        }
         
 

    }
}