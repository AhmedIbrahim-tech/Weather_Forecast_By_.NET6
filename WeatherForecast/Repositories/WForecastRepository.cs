using WeatherForecast.OpenWeatherMap.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace WeatherForecast.Repositories
{
    public class WForecastRepository : IWForecastRepository
    {
        public WeatherResponse GetWeatherResponse(string city)
        {
            string APP_ID = Configuration.Values.OPEN_WEATHER_APP_ID;
            var Client = new RestClient($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={APP_ID}");
            var request = new RestRequest(Method.Get);
            IRestResponse response = Client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return content?.ToObject<WeatherResponse>();
            }
            else
                return null;
        }
    }
}