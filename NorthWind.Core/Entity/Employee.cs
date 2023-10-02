using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    public class Employee
    {   
        public int EmployeeId { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = null!;

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("titleOfCourtesy")]
        public string? TitleOfCourtesy { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; }

        [JsonPropertyName("hireDate")]
        public DateTime? HireDate { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("homePhone")]
        public string? HomePhone { get; set; }

        [JsonPropertyName("extension")]
        public string? Extension { get; set; }

        [JsonPropertyName("photo")]
        public byte[]? Photo { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("reportsTo")]
        public int? ReportsTo { get; set; }

        [JsonPropertyName("photoPath")]
        public string? PhotoPath { get; set; }
    }
}