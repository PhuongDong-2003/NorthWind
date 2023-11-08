using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;

namespace NorthWind.Shop.Service
{
    public class OrderDetailsService
    {
         private readonly HttpClient _httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
          public OrderDetailsService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            _httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }

         public async Task InsertOrderDetail(OrderDetail orderDetail)
        {

            var apiUrl = _apiUrlsConfiguration.OrderDetailApiUrl;
            var response = await _httpClient.PostAsJsonAsync(apiUrl, orderDetail);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Nội dung phản hồi lỗi: {responseContent}");

            }
        }
    }
}