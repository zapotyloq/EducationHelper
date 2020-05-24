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

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class VoteDetailPage : ContentPage
    {
        VoteDetailViewModel viewModel;

        public VoteDetailPage(VoteDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            if (Auth.User.Id == viewModel.Item.AuthorId || Auth.User.Role == "1")
            {
                Button btn_docks = new Button
                {
                    Text = "Варианты ответа",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_docks.Clicked += OpenDocks;
                sL.Children.Add(btn_docks);

                Button btn_users = new Button
                {
                    Text = "Участники",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_users.Clicked += OpenUsers;
                sL.Children.Add(btn_users);
                Button btn_rmv = new Button
                {
                    Text = "Отправить в архив",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_rmv.Clicked += RemoveEvent;
                sL.Children.Add(btn_rmv);
            }
        }

        async void OpenDocks(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {
                await Navigation.PushAsync(new VoteOptionsPage(new VoteOptionsViewModel(viewModel.Item)));
            }
        }
        async void OpenUsers(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {
                await Navigation.PushAsync(new VoteUsersPage(new VoteUsersViewModel(viewModel.Item)));
            }
        }
        async void RemoveEvent(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {

            }
        }

        async void OplEvent(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsBusy = true;
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (VoteOption)layout.BindingContext;
            if (viewModel.UserVote.ChoiseId == 0)
            {
                viewModel.UserVote.ChoiseId = item.Id;
                await new UserVoteDataStore().UpdateItemAsync(viewModel.UserVote);
            }
        }
    }
}