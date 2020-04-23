using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EHMobile.Services;
using EHMobile.Views;

namespace EHMobile
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<EventDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
