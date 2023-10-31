using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Web.Models
{
    public class ApiUrlsConfiguration
    {
        public const string CONFIG_NAME = "ApiUrls";
        public string EmployeesApiUrl { get; set; }
        public string CustomerApiUrl { get; set; }
        public string ProductApiUrl { get; set; }
        public string OrderApiUrl { get; set; }

    }

}