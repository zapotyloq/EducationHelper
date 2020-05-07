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
    public partial class EventUsersPage : ContentPage
    {
        EventUsersViewModel viewModel;

        public EventUsersPage(EventUsersViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (User)layout.BindingContext; 
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Event.AuthorId || u.Role == "1") ;
                await Navigation.PushAsync(new EventUserDetailPage(new EventUserDetailViewModel(viewModel.Event,item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Event.AuthorId || u.Role == "1") ;
                //await Navigation.PushModalAsync(new NavigationPage(new NewEventPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}