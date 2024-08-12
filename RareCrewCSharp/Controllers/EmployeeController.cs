using Microsoft.AspNetCore.Mvc;
using RareCrewCSharp.Services;

namespace RareCrewCSharp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employeeStats = await _employeeService.GetEmployeeWorkStatsAsync();
            return View(employeeStats); // Prosleđivanje modela view-u
        }
    }

}
