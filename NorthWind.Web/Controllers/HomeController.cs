using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Web.Models;

namespace NorthWind.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly B b;

    public HomeController(ILogger<HomeController> logger, B b, Print print )
    {
        _logger = logger;
        this.b = b;
        
    }

                public IActionResult Index()
                {
                
                    return View();
                    
                }


    public IActionResult Privacy()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
