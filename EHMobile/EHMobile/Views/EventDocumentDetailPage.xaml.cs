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
using System.IO;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EventDocumentDetailPage : ContentPage
    {
        EventDocumentDetailViewModel viewModel;

        public EventDocumentDetailPage(EventDocumentDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            is_m_label.Text = viewModel.UserEventDocument.Is_Marked == 0 ? "Не отмечено" : "Отмечено";
        }
        public async void Mark(object sender, EventArgs args)
        {
            viewModel.UserEventDocument.Is_Marked = 1;
            is_m_label.Text = "Отмечено";
            amount_label.Text = sum.Text;
            await DisplayAlert("Уведомление", "Отмечено:" + sum.Text, "ОK");
            viewModel.UserEventDocument.Amount = Convert.ToInt32(sum.Text);
            await viewModel.UED_DataStore.UpdateItemAsync(viewModel.UserEventDocument);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

                viewModel.IsBusy = true;
        }
        void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                popupImageView.IsVisible = true;
                //imgPopup.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.UserEventDocument.File));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            popupImageView.IsVisible = false;
        }
    }

    
}