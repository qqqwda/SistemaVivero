using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaViveroApp.Services;
using Xamarin.Essentials;
using SistemaViveroApp.Models;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading;

namespace SistemaViveroApp.ViewModels
{
    public class AddRiegoViewModel : BaseViewModel
    {
        private SistemaViveroApiService svApi;
        private WeatherApiService wApi;
        private double lat,lon;
        public Weathers weatherRoot;
        public Irrigation riego;
        public Irrigation riegoAgregado;
        public SistemaViveros.Common.Models.Weather clima;
        public string nombreMuro;
        public string pos;
        public string icono;
        public string descripcion;
        public string presion;
        public string humedad;
        public bool isRunning;
        public string temperatura;
        public string lugar;

        public string NombreMuro
        {
            get { return this.nombreMuro; }
            set { SetValue(ref this.nombreMuro, value); }
        }

        public string Lugar
        {
            get { return this.lugar; }
            set { SetValue(ref this.lugar, value); }
        }

        public string Temperatura
        {
            get { return this.temperatura; }
            set { SetValue(ref this.temperatura, value); }
        }

        public string Pos
        {
            get { return this.pos; }
            set { SetValue(ref this.pos, value); }
        }

        public string Icono
        {
            get { return this.icono; }
            set { SetValue(ref this.icono, value); }
        }

        public string Descripcion
        {
            get { return this.descripcion; }
            set { SetValue(ref this.descripcion, value); }
        }

        public string Presion
        {
            get { return this.presion; }
            set { SetValue(ref this.presion, value); }
        }

        public string Humedad
        {
            get { return this.humedad; }
            set { SetValue(ref this.humedad, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        


        public Wall Wall{ get; set; }
        public AddRiegoViewModel(Wall wall)
        {
            riego = new Irrigation();
            clima = new SistemaViveros.Common.Models.Weather();
            this.svApi = new SistemaViveroApiService();
            this.wApi = new WeatherApiService();
            this.Wall = wall;
            GetWeather();
            InicializarDatos();
            


        }
        private async void InicializarDatos()
        {
            var weatherRootInit = await wApi.GetWeather(Wall.Latitude, Wall.Longitude);

            this.IsRunning = true;
            this.NombreMuro = $"{Wall.Name}";
            this.Pos = $"Coordenadas: {Wall.Latitude},{Wall.Longitude}";
            this.Icono = string.Format("http://openweathermap.org/img/w/{0}.png", weatherRootInit.Weather[0].Icon);
            this.Descripcion = $"{weatherRootInit.Weather[0].Description}";
            this.Lugar = Wall.Place;
            this.Presion = weatherRootInit.Main.Pressure.ToString();
            var tempHelper = (weatherRootInit.Main.Temp - 273.15);
            this.Temperatura = $"Temperatura: {tempHelper.ToString()}°C";
            this.Humedad = $"Humedad: {weatherRootInit.Main.Humidity.ToString()}";
            this.Presion = $"Presión del aire: {weatherRootInit.Main.Pressure.ToString()}";
            this.IsRunning = false;

        }
        public ICommand AgregarNuevoRiegoCommand
        {
            get
            {
                return new RelayCommand(AgregarRiego);
            }
        }
        private async void AgregarRiego()
        {
            AddRiego();
            AddClima();
            Thread.Sleep(1500);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async void AddRiego()
        {
            this.IsRunning = true;
            
            riego.Day = DateTime.Now;
            riego.WallId = Wall.WallId;

            string url, prefix, controller;
            url = "http://cursedelboom-001-site1.atempurl.com";
            prefix = "/api";
            controller = "/Irrigations";

            try
            {
                var response = await this.svApi.Post(url, prefix, controller, this.riego);

                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error con agregar riego", "Ok");
                    this.IsRunning = false;
                    return;
                }
                riegoAgregado = (Irrigation)response.Result;
                this.IsRunning = false;
                return;
            }
            catch (Exception ex)
            {

                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
                return;
            }

            

            

        }
        private async void AddClima()
        {

            this.IsRunning = true;
            try
            {
                var weatherRootInit = await wApi.GetWeather(Wall.Latitude, Wall.Longitude);
                clima.Temperature = (double)weatherRootInit.Main.Temp - 273.15;
                clima.IrrigationId = riegoAgregado.IrrigationId;
                clima.Icon = string.Format("http://openweathermap.org/img/w/{0}.png", weatherRootInit.Weather[0].Icon);
                clima.Pressure = (long)weatherRootInit.Main.Pressure;
                clima.Humidity = (long)weatherRootInit.Main.Humidity;
                clima.Description = weatherRootInit.Weather[0].Description;

                string url, prefix, controller;
                url = "http://cursedelboom-001-site1.atempurl.com";
                prefix = "/api";
                controller = "/Weathers";
                var response = await this.svApi.Post(url, prefix, controller, this.clima);
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error con agregar clima", "Ok");
                    this.IsRunning = false;
                    return;
                }
            }
            catch (Exception ex)
            {

                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error",ex.Message.ToString(),"OK");
                return;
            }
            
            

        }

        public async void GetWeather()
        {
            this.IsRunning = true;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    lat = location.Latitude;
                    lon = location.Longitude;
                    weatherRoot = await wApi.GetWeather(lat, lon);
                    this.IsRunning = false;
                    return;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", fnsEx.Message, "OK");
                this.IsRunning = false;
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", fneEx.Message, "OK");
                this.IsRunning = false;
                return;
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", pEx.Message, "OK");
                this.IsRunning = false;
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", ex.Message, "OK");
                this.IsRunning = false;
                return;
            }

        }


    }
}
