using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Web.Models;

namespace NorthWind.Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {

        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;

        public EmployeeRepository(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        
        }
        public async void DeleteStudent(int id)
        {
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{id}";
            var response = await httpClient.DeleteAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi xóa nhân viên có ID {id}: {response.ReasonPhrase}");
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Employee GetStudentByID(int employeeId)
        {
           
            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{employeeId}"; 
            var response =  httpClient.GetFromJsonAsync<Employee>(apiUrl).Result;

            if (response == null)
            {
                throw new Exception($"Không thể tìm nhân viên có ID {employeeId} từ API."); 
            }

            return response;
        }

        public IEnumerable<Employee> GetStudents()
        {

           string apiBaseUrl = _apiUrlsConfiguration.EmployeesApiUrl;
           var response =  httpClient.GetFromJsonAsync<IEnumerable<Employee>>(apiBaseUrl).Result;
        if(response ==null)
            {

                    throw new Exception("Không thử lấy danh sách nhân viên từ api");
            
            }
              
            return response;
        }

        public async  void InsertStudent(Employee employee)
        {

            var apiUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            var response = await httpClient.PostAsJsonAsync(apiUrl, employee);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi thêm nhân viên: {response.ReasonPhrase}");
            }
        }

        public async void UpdateStudent(int id, Employee employee)
        {

            var apiUrl = $"{_apiUrlsConfiguration.EmployeesApiUrl}/{id}";
            var response = await httpClient.PutAsJsonAsync(apiUrl, employee);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi cập nhật thông tin nhân viên có ID {id}: {response.ReasonPhrase}");
            }

        }
    }
}