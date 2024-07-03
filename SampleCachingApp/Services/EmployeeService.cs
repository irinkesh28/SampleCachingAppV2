using System.Collections.Generic;
using System.Linq;

namespace SampleCachingApp.Services
{
    public class EmployeeService
    {
        private readonly SampleCachingAppContext _sampleCachingAppContext;

        public EmployeeService(SampleCachingAppContext sampleCachingAppContext)
        {
            _sampleCachingAppContext = sampleCachingAppContext;
        }

        public List<Employee> GetEmployees()
        {
            // Get employees from database based on provided parameters
            var employeesQueryable = _sampleCachingAppContext.Employee.AsQueryable();

            return employeesQueryable.ToList();
        }

        public IQueryable<Employee> GetEmployeesQueryable()
        {
            // Get employees from database based on provided parameters
            return _sampleCachingAppContext.Employee.AsQueryable();
        }
    }
}
