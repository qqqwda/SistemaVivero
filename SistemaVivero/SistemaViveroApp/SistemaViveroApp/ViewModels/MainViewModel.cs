using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaViveroApp.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public MurosViewModel Muros { get; set; }

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
    }
}
