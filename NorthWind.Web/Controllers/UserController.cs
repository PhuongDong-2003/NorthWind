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

        private readonly EmployeeService _employeeService;
        public UserController(EmployeeService employeeService)
        {
            _employeeService = employeeService;

        }
        public async Task<IActionResult> UserForm(int page = 1, int pageSize = 5)
        {
            var employeeResponse = await _employeeService.GetEmployeePage(page, pageSize);
            if (employeeResponse != null)
            {

                ViewData["employee"] = employeeResponse;
                return View();
            }
            else
            {

                return View("Error");
            }
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

                    await _employeeService.InsertEmployee(employee);

                    return RedirectToAction("UserForm");
                }
            }
            catch (Exception ex)
            {

                if (ex is OutOfMemoryException)
                {
                    Console.WriteLine($"Erro: {ex.Message} ");
                }
            }

            return View("UserForm", employee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Employee employee, DateTime newBirthDate, DateTime newHireDate)
        {
            try
            {
                int employeeId = int.Parse(Request.Form["EmployeeId"]);
                IFormFile newPhoto = Request.Form.Files["NewPhoto"];
                DateTime date = default;
                if (newPhoto != null && newPhoto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newPhoto.CopyToAsync(memoryStream);

                        if (employee != null)
                        {
                            employee.Photo = memoryStream.ToArray();

                        }

                    }
                }
                else
                {

                    if (employee != null)
                    {
                        var existingEmployee = await _employeeService.GetEmployeeByID(employee.EmployeeId);
                        if (existingEmployee.Photo != null)
                        {

                            employee.Photo = existingEmployee.Photo;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Lỗi khi cập nhật tin nhân viên");
                    }

                }

                if (newBirthDate != date)
                {
                    employee.BirthDate = newBirthDate;

                }

                if (newHireDate != date)
                {
                    employee.HireDate = newHireDate;

                }

                await _employeeService.UpdateEmployee(employee);

            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            return RedirectToAction("UserForm");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int EmployeeId)
        {

            if (EmployeeId > 0)
            {
                await _employeeService.DeleteEmployee(EmployeeId);
                return RedirectToAction("UserForm");

            }
            else
            {
                Console.WriteLine("Delete fail");
            }
            return View("UserForm");
        }

        //Userlist
        public async Task<IActionResult> Userlist(int page = 1, int pageSize = 5)
        {

            var employeeResponse = await _employeeService.GetEmployeePage(page, pageSize);
            ViewData["employeerep"] = employeeResponse;
            return View();

        }
        public async Task<IActionResult> Edit(int EmployeeId)
        {

            var employee = await _employeeService.GetEmployeeByID(EmployeeId);
            ViewData["employee"] = employee;
            int page = 1;
            int pageSize = 5;
            await this.Userlist(page, pageSize);
            return View("Userlist");

        }

        [HttpPost]
        public async Task<IActionResult> CreateList(Employee employee, DateTime newBirthDate, DateTime newHireDate)
        {

            try
            {
                DateTime date = default;
                var existingEmployee = await _employeeService.GetEmployeeByID(employee.EmployeeId);
                int employeeId = int.Parse(Request.Form["EmployeeId"]);
                IFormFile newPhoto = Request.Form.Files["NewPhoto"];

                if (newPhoto == null)
                {
                    if (existingEmployee.Photo != null)
                    {

                        employee.Photo = existingEmployee.Photo;
                    }
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newPhoto.CopyToAsync(memoryStream);

                        if (employee != null)
                        {
                            employee.Photo = memoryStream.ToArray();

                        }

                    }


                }

                if (newBirthDate != date)
                {
                    employee.BirthDate = newBirthDate;

                }
                else
                {

                    if (existingEmployee.BirthDate != null)
                    {

                        employee.BirthDate = existingEmployee.BirthDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin BirthDate");
                    }

                }

                if (newHireDate != date)
                {
                    employee.HireDate = newHireDate;

                }
                else
                {

                    if (existingEmployee.HireDate != null)
                    {
                        employee.HireDate = existingEmployee.HireDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin HireDate");
                    }


                }

                await _employeeService.InsertEmployee(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            int page = 1;
            int pageSize = 5;
            await this.Userlist(page, pageSize);
            return View("Userlist", employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateList(Employee employee, DateTime newBirthDate, DateTime newHireDate)
        {
            try
            {
                DateTime date = default;
                var existingEmployee = await _employeeService.GetEmployeeByID(employee.EmployeeId);
                int employeeId = int.Parse(Request.Form["EmployeeId"]);
                IFormFile newPhoto = Request.Form.Files["NewPhoto"];

                if (newPhoto != null && newPhoto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newPhoto.CopyToAsync(memoryStream);

                        if (employee != null)
                        {
                            employee.Photo = memoryStream.ToArray();
                        }

                    }

                }
                else
                {

                    if (employee != null)
                    {

                        if (existingEmployee.Photo != null)
                        {
                            // Lấy dữ liệu hình ảnh từ nhân viên hiện tại và gán cho nhân viên được chỉnh sửa
                            employee.Photo = existingEmployee.Photo;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Lỗi khi cập nhật tin nhân viên");
                    }

                }

                if (newBirthDate != date)

                {
                    employee.BirthDate = newBirthDate;

                }
                else
                {

                    if (existingEmployee.BirthDate != null)
                    {
                        // Lấy dữ liệu hình ảnh từ nhân viên hiện tại và gán cho nhân viên được chỉnh sửa
                        employee.BirthDate = existingEmployee.BirthDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin BirthDate");
                    }

                }

                if (newHireDate != date)
                {
                    employee.HireDate = newHireDate;

                }
                else
                {

                    if (existingEmployee.HireDate != null)
                    {
                        employee.HireDate = existingEmployee.HireDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin HireDate");
                    }

                }

                await _employeeService.UpdateEmployee(employee);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            return RedirectToAction("Userlist");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteList(int EmployeeId)
        {

            if (EmployeeId > 0)
            {
                await _employeeService.DeleteEmployee(EmployeeId);
                return RedirectToAction("Userlist");

            }
            else
            {
                Console.WriteLine("Delete fail");
            }
            return View("Userlist");
        }

        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}