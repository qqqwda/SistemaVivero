using Newtonsoft.Json;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveroApp.Services
{
    public class SistemaViveroApiService
    {
        public async Task<TokenResponse> GetToken(
                string urlBase,
                string username,
                string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
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

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller) //metodo generico que va a servir para consumir listas de cualquier servicio API
        {

            try
            {
                var client = new HttpClient(); //comunicacion con el client
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";//es equivalente a hacer string.format
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();//recibe el JSON como string

                if (!response.IsSuccessStatusCode) //Si la respuesta no es success
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,


                    };

                }
                //
                var list = JsonConvert.DeserializeObject<List<T>>(answer);//Deserializa con "Newtonsoft" la respuesta "answer" y la pasa a un var llamado "list"
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
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

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller, int id) //metodo generico que va a servir para consumir listas de cualquier servicio API
        {

            try
            {
                var client = new HttpClient(); //comunicacion con el client
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";//es equivalente a hacer string.format
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();//recibe el JSON como string

                if (!response.IsSuccessStatusCode) //Si la respuesta no es success
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,


                    };

                }
                //
                var list = JsonConvert.DeserializeObject<List<T>>(answer);//Deserializa con "Newtonsoft" la respuesta "answer" y la pasa a un var llamado "list"
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
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


        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient(); //comunicacion con el client
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";//es equivalente a hacer string.format
                var response = await client.DeleteAsync(url);//pide url y el contenido del objeto serializado
                var answer = await response.Content.ReadAsStringAsync();//recibe el JSON como string

                if (!response.IsSuccessStatusCode) //Si la respuesta no es success
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };

                }
                //
                return new Response
                {
                    IsSuccess = true,
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
