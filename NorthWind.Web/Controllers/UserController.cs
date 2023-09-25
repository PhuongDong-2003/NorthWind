using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NorthWind.Web.Controllers
{

    public class UserController : Controller
    {   
    private readonly IWebHostEnvironment _env;

    public UserController(IWebHostEnvironment env)
    {
        _env = env;
    }
         
    public async Task<IActionResult> Userlist()
{
    using HttpClient httpClient = new HttpClient();
    HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5111/api/EmployeesAPI");
    
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();

         var webRootPath = _env.WebRootPath;
         var filePath = Path.Combine(webRootPath, "employees.json");

            // Lưu dữ liệu xuống tệp JSON trong thư mục wwwroot
        System.IO.File.WriteAllText(filePath, result);
        
        return View((object)result);
    }
    else
    {
        // Xử lý trường hợp lỗi khi gọi API
        return View("Error");
    }
}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}