using System;
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
    public partial class VotesPage : ContentPage
    {
        VotesViewModel viewModel;

        public VotesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VotesViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Vote)layout.BindingContext;
            var uv = await new UserVoteDataStore().GetItemAsync(item.Id, Auth.User.Id);
            var vos = await new VoteOptionDataStore().GetItemsByVoteIdAsync(item.Id);
            if (Auth.User.Role == "2")
            {
                
                //var ueds = await new UserEventDocumentDataStore().GetUEDAsync(ue.Id);
                await Navigation.PushAsync(new VoteDetailPage(new VoteDetailViewModel(item,uv,vos)));
            }
            else await Navigation.PushAsync(new VoteDetailPage(new VoteDetailViewModel(item,uv,vos)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            User u = Auth.User;
            if (u.Role == "3" || u.Role == "1")
            {
                await Navigation.PushModalAsync(new NavigationPage(new NewVotePage()));
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