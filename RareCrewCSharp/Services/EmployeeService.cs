using Newtonsoft.Json;
using RareCrewCSharp.Models;
using System.Diagnostics;

namespace RareCrewCSharp.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeWorkStats>> GetEmployeeWorkStatsAsync()
        {
            var response = await _httpClient.GetAsync("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==");
            response.EnsureSuccessStatusCode();

            var employees = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());

            // Grupisanje radnika po imenu i sumiranje ukupnog broja radnih sati
            var employeeWorkTime = employees
                .GroupBy(e => e.EmployeeName)
                .Select(g => new EmployeeWorkStats
                {
                    Name = g.Key,
                    TotalTimeWorked = Math.Round( g.Sum(e => (e.EndTimeUtc - e.StarTimeUtc).TotalHours))
                })
                .OrderByDescending(e => e.TotalTimeWorked)
                .ToList();

            return employeeWorkTime;
        }
    }

}
