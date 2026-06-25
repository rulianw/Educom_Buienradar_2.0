using Microsoft.AspNetCore.Mvc;

namespace WeatherApp
{
    public class BuienradarController : Controller
    {
        private readonly BuienradarService _service;

        public BuienradarController (BuienradarService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cities = await _service.GetCities();
            ViewBag.Cities = cities;
            return View();
        }

        public async Task<IActionResult> GetWeatherInfo(string city)
        {
            var measurements = await _service.GetCityInformation(city);
            ViewBag.Measurements = measurements;
            var cities = await _service.GetCities();
            ViewBag.Cities = cities;
            ViewBag.SelectedCity = city;
            return View("Index");
        }
    }
}