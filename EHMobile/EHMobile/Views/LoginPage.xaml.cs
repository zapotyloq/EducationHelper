using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Common.Models;
using EHMobile.Views;
using EHMobile.ViewModels;
using EHMobile.Services;
using Java.Lang;

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
            if (await Auth.Login(viewModel.Login, pass.Text) != null)
            {
                if (Auth.User != null) Navigation.InsertPageBefore(new NewsPage(), this);
                await Navigation.PopAsync();
            }
        }
        void e_loginChanged(object sender, EventArgs e)
        {
            viewModel.Login = ((Editor)sender).Text;
        }
        void e_passChanged(object sender, EventArgs e)
        {
            viewModel.Password = ((Entry)sender).Text;
        }

        async protected override void OnAppearing()
        {
            if (Auth.User != null)
            {
                Navigation.InsertPageBefore(new NewsPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}