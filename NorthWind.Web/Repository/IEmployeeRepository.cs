using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Web.Repository
{
    public interface IEmployeeRepository : IDisposable
    {
        public IEnumerable<Employee> GetStudents();
        public Employee GetStudentByID(int  employeeId);
        public void InsertStudent(Employee employee);
        public void DeleteStudent(int id);
        public void UpdateStudent( int id, Employee employee);
    }
}