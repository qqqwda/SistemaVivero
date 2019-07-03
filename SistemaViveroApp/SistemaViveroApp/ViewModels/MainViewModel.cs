using GalaSoft.MvvmLight.Command;
using SistemaViveroApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SistemaViveroApp.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public MurosViewModel Muros { get; set; }
        public RegisterViewModel Register { get; set; }
        public AddMuroViewModel AddMuro { get; set; }

        public RiegosViewModel Riegos { get; set; }

        public AddRiegoViewModel AddRiego { get; set; }

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
        }

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion





        public ICommand AddMuroCommand
        {
            get
            {
                return new RelayCommand(GoToAddMuro);
            }
        }

        private async void GoToAddMuro()
        {
            MainViewModel.GetInstance().AddMuro = new AddMuroViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddMuroPage());
        }
    }
}
