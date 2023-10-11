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
using NorthWind.Web.Service;

namespace NorthWind.Web.Controllers
{
    public class UserController : Controller
    {
        // ví dụ về Di và scope, transient,..
    // private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
    // private readonly HttpClient httpClient;

    // public UserController(IOptions<ApiUrlsConfiguration> apiUrlsOptions, Print print, HttpClient httpClient)
    // {
    //     this.httpClient = httpClient;
    //     _apiUrlsConfiguration = apiUrlsOptions.Value;
    //     print.WriteLine("asdasd");
    // }
    private readonly EmployeeService employeeService;
    public UserController(EmployeeService employeeService)
    {
            this.employeeService = employeeService;
      
    }
        public async Task<IActionResult> Userlist()

        {
            var result = employeeService.GetEmployee();
            if (result!=null)
            {
             
                ViewData["employee"] =result; 
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