using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthWind.Core.Entity;
using NorthWind.Shop.Models;

namespace NorthWind.Shop.Service
{
    public class AccountService
    {
        private readonly HttpClient httpClient;
        private readonly ApiUrlsConfiguration _apiUrlsConfiguration;
        public AccountService(IOptions<ApiUrlsConfiguration> apiUrlsOptions, HttpClient httpClient)
        {

            this.httpClient = httpClient;
            _apiUrlsConfiguration = apiUrlsOptions.Value;
        }
        public async Task<IEnumerable<Account>> GetAccount()
        {

            var apiUrl = $"{_apiUrlsConfiguration.AccountApiUrl}";
            var response = await httpClient.GetFromJsonAsync<IEnumerable<Account>>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }
        public async Task<Account> GetByUserName(string UserName)
        {

            var apiUrl = $"{_apiUrlsConfiguration.AccountApiUrl}/{UserName}";
            var response = await httpClient.GetFromJsonAsync<Account>(apiUrl);

            if (response == null)
            {
                throw new Exception($"Không thể trả về nhân viên.");
            }

            return response;
        }

    }
}