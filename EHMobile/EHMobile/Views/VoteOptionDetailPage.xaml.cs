using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Common.Models;
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
    public partial class VoteOptionDetailPage : ContentPage
    {
        VoteOptionDetailViewModel viewModel;

        public VoteOptionDetailPage(VoteOptionDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public async void RmvOption(object sender, EventArgs args)
        {
            await new VoteOptionDataStore().DeleteItemAsync(viewModel.VoteOption.Id);
            await Navigation.PopAsync();
        }
        public async void SaveOption(object sender, EventArgs args)
        {
            await new VoteOptionDataStore().UpdateItemAsync(viewModel.VoteOption);
            await Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

                viewModel.IsBusy = true;
        }
    }

    
}