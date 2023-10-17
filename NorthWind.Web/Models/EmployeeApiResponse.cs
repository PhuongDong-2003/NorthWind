using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Web.Models
{
    public class EmployeeApiResponse
    {
    public List<Employee> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    }
}