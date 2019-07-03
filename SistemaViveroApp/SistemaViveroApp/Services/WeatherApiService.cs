using Newtonsoft.Json;
using Plugin.Connectivity;
using SistemaViveroApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveroApp.Services
{
    public class WeatherApiService
    {
        public async Task<Weathers> GetWeather(double lat, double lon)
        {
            var urlBase = "https://api.openweathermap.org/";
            var sufix = "data/2.5/";
            string position = $"weather?lat={lat}&lon={lon}";
            string token = "&appid=d9967632f176ca5cf76a2ebea238c29e";
            var url = $"{urlBase}{sufix}{position}{token}";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return JsonConvert.DeserializeObject<Weathers>(json);
            }

        }

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

                var currentWeather = JsonConvert.DeserializeObject<Weathers>(answer);
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

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No hay conectividad."

                };
            }

            bool IsReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!IsReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Chequea tu conexión a internet."

                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "OK"
            };

        }

        



    }
}
