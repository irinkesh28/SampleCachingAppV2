using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Caching.Memory;
using SampleCachingApp.Services;
using System.Text;

namespace SampleCachingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeService _employeeService;
        private readonly IMemoryCache _cache;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService employeeService, IMemoryCache cache)
        {
            _logger = logger;
            _employeeService = employeeService;
            _cache = cache;
        }

        //Sample API Call
        // http://localhost:5067/Employee?filters[Name]=Amit&filters[Designation]=Manager&pageNo=0&pageSize=10&sortProperty=Name&ascendingSort=true
        [HttpGet]
        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Employee> options)
        {
            var cacheKey = GenerateCacheKey(options);

            if (_cache.TryGetValue(cacheKey, out var cachedEmployees))
            {
                return Ok(cachedEmployees);
            }

            IQueryable<Employee>? query = _employeeService.GetEmployeesQueryable();
            query = options.ApplyTo(query) as IQueryable<Employee>;
            var employees = query?.ToList();

            _cache.Set(cacheKey, employees, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            });

            return Ok(employees);
        }

        private static string GenerateCacheKey(ODataQueryOptions<Employee> options)
        {
            var sb = new StringBuilder();
            sb.Append("employee");

            sb.Append($"_filter={options.Filter?.RawValue}");
            sb.Append($"_orderby={options.OrderBy?.RawValue}");
            sb.Append($"_skip={options.Skip?.RawValue}");
            sb.Append($"_top={options.Top?.RawValue}");

            return sb.Append(EmployeeServiceHelper.GenerateILByteCodeString("GetEmployeesQueryable")).ToString();
        }
    }
}
