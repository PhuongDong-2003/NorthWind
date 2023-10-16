using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IEmployeeRepository : IDisposable
    {
        public IEnumerable<Employee> GetEmployee();
        public Employee GetEmployeeByID(int  employeeId);
        public void InsertEmployee(Employee employee);
        public void DeleteEmployee(int id);
        public void UpdateEmployee(Employee employee);
    }
}