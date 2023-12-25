using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
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

        public async Task<IActionResult> Order(int page = 1, int pageSize = 10)
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

        [HttpPost]
        public async Task<IActionResult> CreateForm(Order order, DateTime newOrderDate, DateTime newRequiredDate, DateTime newShippedDate)
        {

            try
            {
                DateTime date = default;
                var existingOrder = await _orderService.GetOrderByID(order.OrderID);
                // int employeeId = int.Parse(Request.Form["EmployeeId"]);

                if (newOrderDate != date)
                {
                    order.OrderDate = newOrderDate;

                }
                else
                {

                    if (existingOrder.OrderDate != null)
                    {

                        order.OrderDate = existingOrder.OrderDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin OrderDate");
                    }

                }

                if (newRequiredDate != date)
                {
                    order.RequiredDate = newRequiredDate;

                }
                else
                {

                    if (existingOrder.RequiredDate != null)
                    {
                        order.RequiredDate = existingOrder.RequiredDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin RequiredDate");
                    }


                }

                if (newShippedDate != date)
                {
                    order.ShippedDate = newShippedDate;

                }
                else
                {

                    if (existingOrder.ShippedDate != null)
                    {
                        order.ShippedDate = existingOrder.ShippedDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin RequiredDate");
                    }


                }

                await _orderService.InsertOrder(order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            int page = 1;
            int pageSize = 5;
            await Order(page, pageSize);
            return View("Order", order);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {

            try
            {


                await _orderService.InsertOrder(order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            int page = 1;
            int pageSize = 5;
            await Order(page, pageSize);
            return View("Order", order);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Order order, DateTime newOrderDate, DateTime newRequiredDate, DateTime newShippedDate)
        {
            try
            {
                DateTime date = default;
                var existingOrder = await _orderService.GetOrderByID(order.OrderID);
                // int employeeId = int.Parse(Request.Form["EmployeeId"]);

                if (newOrderDate != date)
                {
                    order.OrderDate = newOrderDate;

                }
                else
                {

                    if (existingOrder.OrderDate != null)
                    {

                        order.OrderDate = existingOrder.OrderDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin OrderDate");
                    }

                }

                if (newRequiredDate != date)
                {
                    order.RequiredDate = newRequiredDate;

                }
                else
                {

                    if (existingOrder.RequiredDate != null)
                    {
                        order.RequiredDate = existingOrder.RequiredDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin RequiredDate");
                    }


                }

                if (newShippedDate != date)
                {
                    order.ShippedDate = newShippedDate;

                }
                else
                {

                    if (existingOrder.ShippedDate != null)
                    {
                        order.ShippedDate = existingOrder.ShippedDate;
                    }
                    else
                    {
                        Console.WriteLine("Không có thông tin RequiredDate");
                    }


                }

                await _orderService.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
            }

            return RedirectToAction("Order");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int OrderID)
        {

            if (OrderID > 0)
            {
                await _orderService.DeleteOrder(OrderID);
                return RedirectToAction("Order");

            }
            else
            {
                Console.WriteLine("Delete fail");
            }
            return View("Order");
        }

        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}