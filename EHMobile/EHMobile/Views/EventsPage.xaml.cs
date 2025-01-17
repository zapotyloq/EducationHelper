﻿using System;
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

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EventsPage : ContentPage
    {
        EventsViewModel viewModel;

        public EventsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EventsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Event)layout.BindingContext;
            if (Auth.User.Role != "1" && Auth.User.Id != item.AuthorId)
            {
                var ue = await new UserEventDataStore().GetItemAsync(item.Id, Auth.User.Id);
                var ueds = await new UserEventDocumentDataStore().GetUEDAsync(ue.Id);
                await Navigation.PushAsync(new EventDetailPage(new EventDetailViewModel(item, ue, ueds)));
            }
            else await Navigation.PushAsync(new EventDetailPage(new EventDetailViewModel(item, null)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            User u = Auth.User;
            if(u.Role == "3" || u.Role == "1")
                await Navigation.PushModalAsync(new NavigationPage(new NewEventPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}