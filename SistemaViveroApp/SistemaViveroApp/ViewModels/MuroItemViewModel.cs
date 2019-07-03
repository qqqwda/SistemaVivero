using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Services;
using SistemaViveroApp.Views;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class MuroItemViewModel : Wall
    {
        private SistemaViveroApiService svApi;
        private WeatherApiService wApi;
        
        public MuroItemViewModel()
        {
            this.svApi = new SistemaViveroApiService();
            this.wApi = new WeatherApiService();
        }

        public ICommand GoToRiegosCommand
        {
            get
            {
                return new RelayCommand(GoToRiegos);
            }
        }


        private async void GoToRiegos()
        {

            MainViewModel.GetInstance().Riegos = new RiegosViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new RiegosPage());
        }

        public ICommand DeleteMuroCommand
        {
            get
            {
                return new RelayCommand(DeleteMuro);
            }
        }

        private async void DeleteMuro()
        {
            string url = "http://cursedelboom-001-site1.atempurl.com";
            string prefix = "/api";
            string controller = "/Walls";


            var ansAlert = await Application.Current.MainPage.DisplayAlert("Confirmación", $"¿Estás seguro que quieres eliminar el muro {this.Name}?","Sí","No");
            if (!ansAlert)
            {
                return;
            }

            var response = await svApi.Delete(url, prefix, controller, this.WallId);

            
        }
    }
}
