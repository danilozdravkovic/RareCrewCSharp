using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using RareCrewCSharp.Services;

namespace RareCrewCSharp.Controllers
{
    public class ChartController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly ChartService _chartService;


        public ChartController(EmployeeService employeeService, ChartService chartService)
        {
            _employeeService = employeeService;
            _chartService = chartService;
        }

        public async Task<IActionResult> GenerateChart()
        {
            var employeeData = await _employeeService.GetEmployeeWorkStatsAsync();

            // Use WebRootPath to get the path to the wwwroot folder
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "charts", "employee_work_hours.png");

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            _chartService.GeneratePieChart(employeeData, filePath);

            return PhysicalFile(filePath, "image/png");
        }
    }
}
