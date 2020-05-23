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
    public partial class EventDocumentsPage : ContentPage
    {
        EventDocumentsViewModel viewModel;

        public EventDocumentsPage(EventDocumentsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (UserEventDocument)layout.BindingContext; 
            User u = await Auth.GetUser();
            UserEvent ue =  await new UserEventDataStore().GetItemAsync(item.UserEventId);
            User e_user = await new UserDataStore().GetItemAsync(ue.UserId);
            if (u.Id == viewModel.Event.AuthorId || u.Role == "1")
            {
                await Navigation.PushAsync(new EventDocumentDetailPage(new EventDocumentDetailViewModel(viewModel.Event, e_user,item)));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}