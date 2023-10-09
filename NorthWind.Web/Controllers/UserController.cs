using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Web.Models;

namespace NorthWind.Web.Controllers
{
    public class UserController : Controller
    {

     private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        private readonly HttpClient httpClient;

    public UserController(IOptions<ApiUrlsConfiguration> apiUrlsOptions, Print print, HttpClient httpClient)
    {
        this.httpClient = httpClient;
        _apiUrlsConfiguration = apiUrlsOptions.Value;
        print.WriteLine("asdasd");
    }

        public async Task<IActionResult> Userlist()

        {
            string apiBaseUrl = _apiUrlsConfiguration.EmployeesApiUrl;
            HttpResponseMessage response = await httpClient.GetAsync(apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var employees =JsonSerializer.Deserialize<List<Employee>>(result);
                ViewData["employee"] = employees; 
                Console.WriteLine(employees);
                return View();
            }
            else
            {
                // Xử lý trường hợp lỗi khi gọi API
                return View("Error");
            }
        }

  
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}