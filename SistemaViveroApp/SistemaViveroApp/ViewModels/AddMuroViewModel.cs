using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Models;
using SistemaViveroApp.Services;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class AddMuroViewModel : BaseViewModel
    {
        public WeatherApiService apiService;
        public SistemaViveroApiService svApi;
        public string nombreMuro;
        public string pais;
        public string lugar;
        public string coordenadas;
        public string descripcion;
        public string imagen;
        public bool isRunning;
        public double lat;
        public double lon;


        public string NombreMuro
        {
            get { return this.nombreMuro; }
            set { SetValue(ref this.nombreMuro, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public string Pais
        {
            get { return this.pais; }
            set { SetValue(ref this.pais, value); }
        }

        public string Lugar
        {
            get { return this.lugar; }
            set { SetValue(ref this.lugar, value); }
        }

        public string Coordenadas
        {
            get { return this.coordenadas; }
            set { SetValue(ref this.coordenadas, value); }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
            set { SetValue(ref this.descripcion, value); }
        }
        public string Imagen
        {
            get { return this.imagen; }
            set { SetValue(ref this.imagen, value); }
        }

        public AddMuroViewModel()
        {
            apiService = new WeatherApiService();
            svApi = new SistemaViveroApiService();
            InicializarNuevoMuro();
        }

        private async void InicializarNuevoMuro()
        {
            this.IsRunning = true;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                Weathers weatherRoot = null;
                if (location != null)
                {
                    lat = location.Latitude;
                    lon = location.Longitude;
                    weatherRoot = await apiService.GetWeather(lat, lon);
                    this.Descripcion = weatherRoot.Weather[0].Description;
                    this.Lugar = weatherRoot.Name;
                    this.Coordenadas = string.Format("{0},{1}",lat,lon);
                    this.Pais = weatherRoot.Sys.Country;
                    this.Imagen = string.Format("http://openweathermap.org/img/w/{0}.png",weatherRoot.Weather[0].Icon);
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

        public ICommand AgregarMuroCommand
        {
            get
            {
                return new RelayCommand(AgregarMuro);
            }
        }

        private async void AgregarMuro()
        {
            this.IsRunning = true;
            var wall = new Wall
            {
                IsWatering = false,
                Name = this.NombreMuro,
                Latitude = this.lat,
                Longitude = this.lon,
                Place = this.Lugar,
                UserId = "1",
            };

            string url, prefix, controller;
            url = "http://cursedelboom-001-site1.atempurl.com";
            prefix = "/api";
            controller = "/Walls";
            var response = await this.svApi.Post(url,prefix,controller,wall);

            if(response == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error con servidor", "Ok");
                this.IsRunning = false;
                return;
            }

            MainViewModel.GetInstance().Muros.LoadMuros();
            await Application.Current.MainPage.Navigation.PopAsync();
            this.IsRunning = false;

        }
    }
}
