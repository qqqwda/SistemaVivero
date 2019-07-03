using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Services;
using SistemaViveroApp.Views;
using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class RiegosViewModel : BaseViewModel
    {
        private string filter;
        private SistemaViveroApiService svApi;
        private WeatherApiService wApi;
        private bool isRefreshing;
        private string namePage;


        public List<IrrigationWeather> MyRiegos { get; set; }
        public ObservableCollection<RiegoItemViewModel> listaRiegos;


        public Wall Wall { get; set; }
        public ObservableCollection<RiegoItemViewModel> ListaRiegos
        {

            get { return this.listaRiegos; }
            set { SetValue(ref this.listaRiegos, value); }

        }
        public bool IsRefreshing
        {

            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }

        }

        public string NamePage
        {

            get { return this.namePage; }
            set { SetValue(ref this.namePage, value); }

        }

        public RiegosViewModel(Wall wall)
        {
            svApi = new SistemaViveroApiService();
            wApi = new WeatherApiService();
            
            this.Wall = wall;
            this.NamePage = this.Wall.Name;
            LoadRiegos();
        }

        public async void LoadRiegos()
        {
            this.IsRefreshing = true;
            string url, prefix, controller;
            url = "http://cursedelboom-001-site1.atempurl.com";
            prefix = "/api";
            controller = "/irrigationWeather";
            var response = await this.svApi.GetList<IrrigationWeather>(url, prefix, controller,Wall.WallId);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "error", "OK");
                this.IsRefreshing = false;
                return;
            }


            
            this.IsRefreshing = false;
            MyRiegos = (List<IrrigationWeather>)response.Result;
            RefreshList();
        }

        private void RefreshList()
        {
            var myList = MyRiegos.Select(p => new RiegoItemViewModel
            {
                Description = p.Description,
                Humidity = p.Humidity,
                Icon = p.Icon,
                Pressure = p.Pressure,
                Temperature = Math.Round((double)p.Temperature,2),
                WeatherId = p.WeatherId,
                Day = p.Day,
                IrrigationId = p.IrrigationId,
                WallId = p.WallId,
                
                
            });

            this.ListaRiegos = new ObservableCollection<RiegoItemViewModel>(myList);
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRiegos);
            }
        }


        public ICommand GoToAddRiegoCommand
        {
            get
            {
                return new RelayCommand(GoToAddRiego);
            }
        }

        private async void GoToAddRiego()
        {
            MainViewModel.GetInstance().AddRiego = new AddRiegoViewModel(this.Wall);
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddRiegoPage());
        }
    }
}
