using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NorthWind.Web.Models;

namespace NorthWind.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly HttpClient httpClient;
        private readonly IDistributedCache _distributedCache;


        public TokenProvider(HttpClient httpClient, IDistributedCache distributedCache)
        {
            this.httpClient = httpClient;
            _distributedCache = distributedCache;
        }
        public async Task<string> LoginAsync()
        {
            var token = "";
            var cachedToken = _distributedCache.GetString("AuthToken");
            if (!String.IsNullOrEmpty(cachedToken))
            {
                return cachedToken;
            }

            var loginData = new { Username = "web", Password = "123" };
            var response = await httpClient.PostAsJsonAsync("http://localhost:5111/api/Authentication/login", loginData);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<TokenResponse>();
                token = responseData.Token;
                _distributedCache.SetString("AuthToken", token, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
            }

            return token;

        }
    }
}