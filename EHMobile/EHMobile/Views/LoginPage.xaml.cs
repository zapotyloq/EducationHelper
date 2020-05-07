using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EHMobile.Models;
using EHMobile.Views;
using EHMobile.ViewModels;
using EHMobile.Services;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LoginViewModel();
        }

        async void Login(object sender, EventArgs e)
        {
            if (await Auth.Login(viewModel.Login, viewModel.Password) != null)
            {
                await Navigation.PushAsync(new AboutPage());
            }
        }
        void e_loginChanged(object sender, EventArgs e)
        {
            viewModel.Login = ((Editor)sender).Text;
        }
        void e_passChanged(object sender, EventArgs e)
        {
            viewModel.Password = ((Editor)sender).Text;
        }

    }
}