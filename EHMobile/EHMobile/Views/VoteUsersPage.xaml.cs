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
    public partial class VoteUsersPage : ContentPage
    {
        VoteUsersViewModel viewModel;

        public VoteUsersPage(VoteUsersViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (User)layout.BindingContext; 
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Vote.AuthorId || u.Role == "1")
            {
                 await Navigation.PushAsync(new VoteUserDetailPage(new VoteUserDetailViewModel(viewModel.Vote,item)));
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Vote.AuthorId || u.Role == "1") { }
                await Navigation.PushModalAsync(new NavigationPage(new NewVoteUserPage(new NewVoteUserViewModel(viewModel.Vote))));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}