using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Helpers;
using SistemaViveroApp.Services;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public string email;
        public string password;
        public bool isRunning;
        public string confirmPassword;
        public SistemaViveroApiService svApi;

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
        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set { SetValue(ref this.confirmPassword, value); }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        public RegisterViewModel()
        {
            svApi = new SistemaViveroApiService();
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.ConfirmPassword = string.Empty;
        }

        private async void Register()
        {
            this.IsRunning = true;

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese Email", "OK");
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese Password", "OK");
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Confirme Password", "OK");
                this.IsRunning = false;
                return;
            }

            if (!RegexHelper.IsValidEmailAddress(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email no es válido", "OK");
                this.IsRunning = false;
                return;
            }

            if (this.Password != this.ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Contraseñas no coinciden", "OK");
                this.IsRunning = false;
                return;
            }
            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Contraseña tiene que ser mayor a 6 caracteres", "OK");
                this.IsRunning = false;
                return;
            }

            var userRequest = new UserRequest
            {
                Email = this.Email,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword
            };

            var response = this.svApi.Post("http://cursedelboom-001-site1.atempurl.com","/api","/users",userRequest);

            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wroooooooooooooooooooooooooong...", "OK");
                this.IsRunning = false;
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Registro exitoso", "Será redirigido al login", "OK");
            this.IsRunning = false;

            await Application.Current.MainPage.Navigation.PopAsync();

        }

    }
}
