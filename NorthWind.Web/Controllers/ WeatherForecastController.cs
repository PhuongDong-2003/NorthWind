using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NorthWind.Web.Controllers
{
    [Route("[controller]")]
    public class  WeatherForecastController : Controller
    {
    public async Task<IActionResult> DownloadAndSaveJson()
    {
        string apiUrl = "http://localhost:5111/WeatherForecast";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    string jsonFilePath = "weather_forecast.json";
                    System.IO.File.WriteAllText(jsonFilePath, jsonContent);
                    ViewBag.Message = "Dữ liệu đã được lưu thành công vào tệp JSON.";
                }
                else
                {
                    ViewBag.Message = $"Lỗi: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        catch (Exception ex)
        {
            ViewBag.Message = $"Lỗi: {ex.Message}";
        }

        return View();
    }
      
    }
}