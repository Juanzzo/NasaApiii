using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace NasaApiii.Services
{
    public class NasaService
    {
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _config;

        public NasaService(IApiClient apiClient, IConfiguration config)
        {
            _apiClient = apiClient;
            _config = config;
        }

        // 📌 Obtener un solo APOD
        public async Task<NasaApodResponse?> GetApodAsync()
        {
            string? apiKey = _config["Nasa:ApiKey"];
            string url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";

            var json = await _apiClient.GetStringAsync(url);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<NasaApodResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // 📌 Obtener APOD en un rango de fechas
        public async Task<List<NasaApodResponse>?> GetApodRangeAsync(DateTime start, DateTime end)
        {
            string? apiKey = _config["Nasa:ApiKey"];
            string url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}&start_date={start:yyyy-MM-dd}&end_date={end:yyyy-MM-dd}";

            var json = await _apiClient.GetStringAsync(url);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<List<NasaApodResponse>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    // 📌 Modelo para la respuesta de APOD
    public class NasaApodResponse
    {
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Url { get; set; }
        public string Date { get; set; }  // Importante cuando traemos varios días
    }
}
