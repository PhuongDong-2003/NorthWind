using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Web.Service;

namespace NorthWind.Web.Controllers
{

    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Order(int page = 1, int pageSize = 5)
        {

            var orderResponse = await _orderService.GetOrderPage(page, pageSize);
            ViewData["orderResponse"] = orderResponse;
            return View();

        }
    [HttpPost]
        public async Task<IActionResult> Edit(int OrderID)
        {

            var order = await _orderService.GetOrderByID(OrderID);
            ViewData["order"] = order;
            int page = 1;
            int pageSize = 5;
            await Order(page, pageSize);
            return View("Order");

        }
        [HttpPost]
        public async Task<IActionResult> Find(int OrderID)
        {

            var order = await _orderService.GetOrderByID(OrderID);
            ViewData["orderResponse"] = order;
            int page = 1;
            int pageSize = 5;
            await Order(page, pageSize);
            return View("Order");

        }

        // [HttpPost]
        // public async Task<IActionResult> Create(Employee employee, DateTime newBirthDate, DateTime newHireDate)
        // {

        //     try
        //     {
        //         DateTime date = default;
        //         var existingEmployee = await _employeeService.GetEmployeeByID(employee.EmployeeId);
        //         int employeeId = int.Parse(Request.Form["EmployeeId"]);
        //         IFormFile newPhoto = Request.Form.Files["NewPhoto"];

        //         if (newPhoto == null)
        //         {
        //             if (existingEmployee.Photo != null)
        //             {

        //                 employee.Photo = existingEmployee.Photo;
        //             }
        //         }
        //         else
        //         {
        //             using (var memoryStream = new MemoryStream())
        //             {
        //                 await newPhoto.CopyToAsync(memoryStream);

        //                 if (employee != null)
        //                 {
        //                     employee.Photo = memoryStream.ToArray();

        //                 }

        //             }


        //         }

        //         if (newBirthDate != date)
        //         {
        //             employee.BirthDate = newBirthDate;

        //         }
        //         else
        //         {

        //             if (existingEmployee.BirthDate != null)
        //             {

        //                 employee.BirthDate = existingEmployee.BirthDate;
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Không có thông tin BirthDate");
        //             }

        //         }

        //         if (newHireDate != date)
        //         {
        //             employee.HireDate = newHireDate;

        //         }
        //         else
        //         {

        //             if (existingEmployee.HireDate != null)
        //             {
        //                 employee.HireDate = existingEmployee.HireDate;
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Không có thông tin HireDate");
        //             }


        //         }

        //         await _employeeService.InsertEmployee(employee);
        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", $"Lỗi: {ex.Message}");
        //     }

        //     int page = 1;
        //     int pageSize = 5;
        //     await this.Userlist(page, pageSize);
        //     return View("Userlist", employee);
        // }

        // [HttpPost]
        // public async Task<IActionResult> UpdateList(Employee employee, DateTime newBirthDate, DateTime newHireDate)
        // {
        //     try
        //     {
        //         DateTime date = default;
        //         var existingEmployee = await _employeeService.GetEmployeeByID(employee.EmployeeId);
        //         int employeeId = int.Parse(Request.Form["EmployeeId"]);
        //         IFormFile newPhoto = Request.Form.Files["NewPhoto"];

        //         if (newPhoto != null && newPhoto.Length > 0)
        //         {
        //             using (var memoryStream = new MemoryStream())
        //             {
        //                 await newPhoto.CopyToAsync(memoryStream);

        //                 if (employee != null)
        //                 {
        //                     employee.Photo = memoryStream.ToArray();
        //                 }

        //             }

        //         }
        //         else
        //         {

        //             if (employee != null)
        //             {

        //                 if (existingEmployee.Photo != null)
        //                 {
        //                     // Lấy dữ liệu hình ảnh từ nhân viên hiện tại và gán cho nhân viên được chỉnh sửa
        //                     employee.Photo = existingEmployee.Photo;
        //                 }

        //             }
        //             else
        //             {
        //                 Console.WriteLine("Lỗi khi cập nhật tin nhân viên");
        //             }

        //         }

        //         if (newBirthDate != date)

        //         {
        //             employee.BirthDate = newBirthDate;

        //         }
        //         else
        //         {

        //             if (existingEmployee.BirthDate != null)
        //             {
        //                 // Lấy dữ liệu hình ảnh từ nhân viên hiện tại và gán cho nhân viên được chỉnh sửa
        //                 employee.BirthDate = existingEmployee.BirthDate;
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Không có thông tin BirthDate");
        //             }

        //         }

        //         if (newHireDate != date)
        //         {
        //             employee.HireDate = newHireDate;

        //         }
        //         else
        //         {

        //             if (existingEmployee.HireDate != null)
        //             {
        //                 employee.HireDate = existingEmployee.HireDate;
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Không có thông tin HireDate");
        //             }

        //         }

        //         await _employeeService.UpdateEmployee(employee);

        //     }
        //     catch (Exception ex)
        //     {
        //         ModelState.AddModelError("", $"Lỗi: {ex.Message}");
        //     }

        //     return RedirectToAction("Userlist");

        // }

        // [HttpPost]
        // public async Task<IActionResult> DeleteList(int EmployeeId)
        // {

        //     if (EmployeeId > 0)
        //     {
        //         await _employeeService.DeleteEmployee(EmployeeId);
        //         return RedirectToAction("Userlist");

        //     }
        //     else
        //     {
        //         Console.WriteLine("Delete fail");
        //     }
        //     return View("Userlist");
        // }

        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }


    }
}