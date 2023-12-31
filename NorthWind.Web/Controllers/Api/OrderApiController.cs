using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Web.Service;

namespace NorthWind.Web.Controllers.Api
{
    public class OrderApiController : Controller
    {
        private readonly ILogger<OrderApiController> _logger;
        private readonly OrderService _orderService;

        public OrderApiController(ILogger<OrderApiController> logger, OrderService orderService )
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<JsonResult>  GetByPage()
        {

            var orderResponse = await _orderService.GetOrder();
            if(orderResponse !=null)
            {
                _logger.LogInformation("Load Orders");
                return Json(orderResponse);

            }
            else
            {
                _logger.LogError("Load Orders fail");
                return Json(new { message = "Load Prouducts failed" } );
            }

        
        }

        [HttpPost]
        public async Task  Create([FromBody] Order order)
        {
            await _orderService.InsertOrder(order);
             _logger.LogInformation("Create Order");
        }
  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}