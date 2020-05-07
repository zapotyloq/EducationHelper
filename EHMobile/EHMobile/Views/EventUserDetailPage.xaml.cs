using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EHMobile.Models;
using EHMobile.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Android.Provider;
using System.Net.Http;
using EHMobile.Services;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EventUserDetailPage : ContentPage
    {
        EventUserDetailViewModel viewModel;

        public EventUserDetailPage(EventUserDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public async void RmvUser(object sender, EventArgs args)
        {
            await viewModel.UE_DataStore.DeleteItemAsync(viewModel.UserEvent.Id);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

                viewModel.IsBusy = true;
        }
    }

    
}