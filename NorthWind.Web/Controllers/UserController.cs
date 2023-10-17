using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
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
        public async Task<IActionResult> UserAction(int page=1, int pageSize=5)

         {
             var employeeResponse = await employeeService.GetEmployeePage(page, pageSize);
            if (employeeResponse != null)
            {

                ViewData["employee"] = employeeResponse;
                return View();
            }
            else
            {
                // Xử lý trường hợp lỗi khi gọi API
                return View("Error");
            }
        }
        public async Task<IActionResult> Userlist(int page=1, int pageSize=5)
        {

            var employeeResponse = await employeeService.GetEmployeePage(page, pageSize);
            return View(employeeResponse);
            
         }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, IFormFile photo)
        {

            try
            {
                if (photo != null && photo.Length > 0)
                {

                    byte[] photoData;
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);

                        photoData = memoryStream.ToArray();
                    }
                    employee.Photo = photoData;

                    await employeeService.InsertEmployee(employee);

                    return RedirectToAction("UserAction");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }


            return View("UserAction", employee);
        }
       


        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            try
            {
                int employeeId = int.Parse(Request.Form["EmployeeId"]);
                IFormFile newPhoto = Request.Form.Files["NewPhoto"];

                if (newPhoto != null && newPhoto.Length > 0 ) 
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newPhoto.CopyToAsync(memoryStream);

                        if (employee != null)
                        {
                            employee.Photo = memoryStream.ToArray();
                        await employeeService.UpdateEmployee(employee);
                           
                        }
                        
                    }
                } 
                else
                {
                   
                    if (employee != null )
                    {
                        var existingEmployee = await employeeService.GetEmployeeByID(employeeId);
                        if (existingEmployee.Photo != null)
                             {
                        // Lấy dữ liệu hình ảnh từ nhân viên hiện tại và gán cho nhân viên được chỉnh sửa
                        employee.Photo = existingEmployee.Photo;
                             }
                      await  employeeService.UpdateEmployee(employee);
                       
                    }else
                    {
                        Console.WriteLine("Lỗi khi cập nhật tin nhân viên");
                    }
                   

                }
           
           
            }
            catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }

                return RedirectToAction("UserAction");
        
        }

         [HttpPost]
        public async Task<IActionResult> UpdateSecord(Employee employee, int EmployeeId, IFormFile newPhoto )
        {

            var result = await employeeService.GetEmployeeByID(EmployeeId);
            if (result != null)
            {

                ViewData["employeereq"] = result;
                return View("UserAction");
                
            }
            else
            {
              
              Console.WriteLine("Lỗi không lấy được nhân viên");
            }
              try
            {
               
                if (newPhoto != null && newPhoto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newPhoto.CopyToAsync(memoryStream);

                        if (employee != null)
                        {
                            employee.Photo = memoryStream.ToArray();
                            employeeService.UpdateEmployee(employee);
                           
                        }
                        
                    }
                } 
                else
                {
                   
                    if (employee != null)
                    {

                        employeeService.UpdateEmployee(employee);
                       
                    }else
                    {
                        Console.WriteLine("Không có thông tin nhân viên");
                    }
                   

                }
            }
            catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                }

                return RedirectToAction("UserAction");
        
          
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int EmployeeId){
          
            if(EmployeeId >0){
             await   employeeService.DeleteEmployee(EmployeeId);
                return RedirectToAction("UserAction");

            }else{
                Console.WriteLine("Delete fail");
            }
            return View("UserAction");
        }

        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}