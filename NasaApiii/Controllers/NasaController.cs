using Microsoft.AspNetCore.Mvc;
using NasaApiii.Services;
using System;
using System.Threading.Tasks;

namespace NasaApiii.Controllers
{
    public class NasaController : Controller
    {
        private readonly NasaService _nasaService;

        public NasaController(NasaService nasaService)
        {
            _nasaService = nasaService;
        }

        // 📌 Mostrar varios APOD en el Index
        public async Task<IActionResult> Index()
        {
            DateTime end = DateTime.Today;        // hasta hoy
            DateTime start = end.AddDays(-5);     // últimos 5 días

            var apods = await _nasaService.GetApodRangeAsync(start, end);

            return View(apods); // la vista Index.cshtml ahora recibirá una lista
        }
    }
}
