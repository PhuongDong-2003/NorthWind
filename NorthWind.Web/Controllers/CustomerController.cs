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
        private readonly ILogger<CustomerController> logger;
        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            this.logger = logger;
            _customerService = customerService;

        }

        public async Task<IActionResult> CustomerList(int page = 1, int pageSize = 5)
        {
            var customerResponse = await _customerService.GetCustomerPage(page, pageSize);
            if (customerResponse != null)
            {

                ViewData["customerlist"] = customerResponse;
                logger.LogInformation("Load Custmers successfully");
                return View();
            }
            else
            {
                logger.LogError("Load Custmers fail");
                return View("Error");
            }

        }

        public async Task<IActionResult> Edit(string CustomerID)
        {

            var customer = await _customerService.GetCustomerByID(CustomerID);
            ViewData["customer"] = customer;
            logger.LogInformation("Find Customer with CustomerId {customerId} successfully", CustomerID);
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
                logger.LogInformation("Load Custmers successfully");

            }

            catch (Exception ex)
            {
               logger.LogError("Load Customers fail {erro}", ex.Message);

            }

            return View("CustomerList");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {

            try
            {
                await _customerService.UpdateCustomer(customer);
                logger.LogInformation("Update Customer successfully {customerId}", customer.CustomerID);


            }

            catch (Exception ex)
            {
                logger.LogError("Update Customer fail {erro}", ex.Message);
                

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
                logger.LogInformation("Delete Customer successfully {customerId}", CustomerID);
                return RedirectToAction("CustomerList");

            }
            else
            {
                logger.LogError("Delete Customer fail {customerId}", CustomerID);
             
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