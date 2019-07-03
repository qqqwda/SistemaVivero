
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class MurosViewModel : BaseViewModel
    {
        private string filter;
        private SistemaViveroApiService svApi;
        private WeatherApiService wApi;
        private bool isRefreshing;

        public string Filter {
            get { return this.filter; }
            set { this.filter = value;
                this.RefreshList();
            }
        }


        private static RiegosViewModel instance;
        public static RiegosViewModel GetInstance()
        {
            return instance;
        }

        public ObservableCollection<MuroItemViewModel> muros;
        public ObservableCollection<MuroItemViewModel> Muros
        {
            get { return this.muros; }
            set { SetValue(ref this.muros, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public List<Wall> MyWalls { get; private set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadMuros);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        private void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myList = MyWalls.Select(p => new MuroItemViewModel
                {
                    WallId = p.WallId,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    UserId = p.UserId,
                    Place = p.Place,
                    Name = p.Name,


                });

                this.Muros = new ObservableCollection<MuroItemViewModel>(myList);
            }

            else
            {
                var myList = this.MyWalls.Select(p => new MuroItemViewModel
                {
                    WallId = p.WallId,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    UserId = p.UserId,
                    Place = p.Place,
                    Name = p.Name,


                }).Where(m => m.Name.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Muros = new ObservableCollection<MuroItemViewModel>(myList);

            }
        }

        public MurosViewModel()
        {
            this.svApi = new SistemaViveroApiService();
            this.wApi = new WeatherApiService();
            LoadMuros();
            IsRefreshing = true;

        }

        public async void LoadMuros()
        {
            this.IsRefreshing = true;
            string url, prefix, controller;
            url = "http://cursedelboom-001-site1.atempurl.com";
            prefix = "/api";
            controller = "/Walls";
            var response = await this.svApi.GetList<Wall>(url,prefix,controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "error","OK");
                this.IsRefreshing = false;
                return;
            }

            this.IsRefreshing = false;
            MyWalls = (List<Wall>)response.Result;
            RefreshList();
            

            

        }
    }
}
