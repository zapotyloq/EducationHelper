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

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EventDetailPage : ContentPage
    {
        EventDetailViewModel viewModel;

        public EventDetailPage(EventDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public EventDetailPage()
        {
            InitializeComponent();

            var item = new Event
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new EventDetailViewModel(item);
            BindingContext = viewModel;

           
        }
        async void AddFile_Clicked(object sender, EventArgs args)
        {
            Image img = new Image();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                img.Source = ImageSource.FromFile(photo.Path);

                var uri = new Uri(string.Format("E://Files/Upload/", string.Empty));
                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(photo.GetStream()),
                    "\"file\"",
                    $"\"{photo.Path}\"");

                var httpClient = new HttpClient();
                var httpResponseMessage = await httpClient.PostAsync(uri, content);
            }
        }
    }
}