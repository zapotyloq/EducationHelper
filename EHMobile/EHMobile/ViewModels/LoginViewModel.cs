using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using EHMobile.Models;
using EHMobile.Views;
using EHMobile.Services;

namespace EHMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Title = "Авторизация";

        }

    }
}