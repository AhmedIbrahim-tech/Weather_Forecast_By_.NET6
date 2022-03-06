using Microsoft.AspNetCore.Mvc;
using WeatherForecast.OpenWeatherMap.Model;
using WeatherForecast.Repositories;

namespace WeatherForecast.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly IWForecastRepository _wForecastRepository;

        public WeatherForecastController(IWForecastRepository wForecastRepository)
        {
            _wForecastRepository = wForecastRepository;
        }

        [HttpGet]
        public IActionResult SearchByCity()
        {
            var Weather = new SearchByCity();
            return View(Weather);
        }

        [HttpPost]
        public IActionResult SearchByCity(SearchByCity model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "WeatherForecast", new { City = model.CityName });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
            WeatherResponse weatherReponse = _wForecastRepository.GetWeatherResponse(city);
            City viewModel = new City();
            if (weatherReponse != null)
            {
                viewModel.Name = weatherReponse.Name;
                viewModel.Temperature = weatherReponse.Main.Temp;
                viewModel.Humidity = weatherReponse.Main.Humidity;
                viewModel.Pressure = weatherReponse.Main.Pressure;
                viewModel.Weather = weatherReponse.Weather[0].Main;
                viewModel.Wind = weatherReponse.Wind.Speed;
            }
            return View(viewModel);
        }
    }
}