using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HttpsTestWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public WeatherForecast[] Forecasts { get; private set; } = Array.Empty<WeatherForecast>();

        public async Task OnGet()
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7077")
            };

            Forecasts = (await client.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast"))!;
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}