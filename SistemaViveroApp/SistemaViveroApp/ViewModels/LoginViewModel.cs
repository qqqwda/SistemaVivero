using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Helpers;
using SistemaViveroApp.Models;
using SistemaViveroApp.Services;
using SistemaViveroApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public WeatherApiService apiService;
        public SistemaViveroApiService svApiService;
        public string email;
        public string password;
        public bool isRunning;
        public bool isRemembered;

        public LoginViewModel()
        {
            this.svApiService = new SistemaViveroApiService();
            this.apiService = new WeatherApiService();
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.IsRunning = false;
            this.IsRemembered = false;
            this.LoginEnabled = true;
        }

        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemembered
        {
            get { return this.isRemembered; }
            set { SetValue(ref this.isRemembered, value); }
        }
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            
            double lat;
            double lon;
            /*
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                Weathers weatherRoot = null;
                if (location != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Posición: ",String.Format("Latitud:{0} | Longitud{1}",location.Latitude, location.Longitude),"OK");
                    lat = location.Latitude;
                    lon = location.Longitude;

                    weatherRoot = await apiService.GetWeather(lat, lon);

                   
                    
                    await Application.Current.MainPage.DisplayAlert("Posición: ", $"{weatherRoot.Name}", "OK");

                    return;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", fnsEx.Message, "OK");
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", fneEx.Message, "OK");
                return;
            }
            catch (PermissionException pEx)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", pEx.Message, "OK");
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Posición: ", ex.Message, "OK");
                return;
            }
            */


            this.isRunning = false;
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            
        }



        private async void Login()
        {

            this.IsRunning = true;
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese Email", "OK");
                this.IsRunning = false;
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese Password", "OK");
                this.IsRunning = false;
                return;
            }

            var token = await this.svApiService.GetToken("http://cursedelboom-001-site1.atempurl.com/Token", this.Email,this.Password);

            if(token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                await Application.Current.MainPage.DisplayAlert("Error","Wrong Email or Password","Try Again");
                this.IsRunning = false;
                return;
            }
            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.IsRemembered = this.IsRemembered;
            MainViewModel.GetInstance().Muros = new MurosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new MurosPage());
            this.IsRunning = false;



        }



        public bool LoginEnabled { get; set; }


       
    }
}
