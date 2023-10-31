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

    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;

        }

        public async Task<IActionResult> CustomerList(int page = 1, int pageSize = 5)
        {
            var customerResponse = await _customerService.GetCustomerPage(page, pageSize);
            if (customerResponse != null)
            {

                ViewData["customerlist"] = customerResponse;
                return View();
            }
            else
            {

                return View("Error");
            }

        }

        public async Task<IActionResult> Edit(string CustomerID)
        {

            var customer = await _customerService.GetCustomerByID(CustomerID);
            ViewData["customer"] = customer;
            int page = 1;
            int pageSize = 5;
            await CustomerList(page, pageSize);
            return View("CustomerList");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {

            try
            {
                await _customerService.InsertCustomer(customer);
                int page = 1;
                int pageSize = 5;
                await CustomerList(page, pageSize);

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message} ");

            }

            return View("CustomerList");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {

            try
            {
                await _customerService.UpdateCustomer(customer);


            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message} ");

            }
            await CustomerList();

            return View("CustomerList", customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string CustomerID)
        {

            if (CustomerID != null)
            {
                await _customerService.DeleteCustomer(CustomerID);
                return RedirectToAction("CustomerList");

            }
            else
            {
                Console.WriteLine("Delete fail");
            }
            return View("CustomerList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }

}