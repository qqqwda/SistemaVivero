using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsoleApp.Services
{
    public class WeatherApiService
    {
        

        public async Task<Response> GetPosition(double lat, double lon) //metodo generico que va a servir para consumir cualquier objeto servicio API
        {
            var urlBase = "https://api.openweathermap.org/";
            var sufix = "data/2.5/";
            string position = $"weather?lat={lat}&lon={lon}";
            string token = "&appid=d9967632f176ca5cf76a2ebea238c29e";

            try
            {
                var client = new HttpClient(); //comunicacion con el client
                client.BaseAddress = new Uri(urlBase);
                var url = $"{urlBase}{sufix}{position}{token}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }

                var currentWeather = JsonConvert.DeserializeObject(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = currentWeather
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,

                };
            }
            



        }


    }
}
