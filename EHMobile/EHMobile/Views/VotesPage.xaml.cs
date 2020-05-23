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
            if (Auth.User.Role == "2")
            {
                
                //var ueds = await new UserEventDocumentDataStore().GetUEDAsync(ue.Id);
                await Navigation.PushAsync(new VoteDetailPage(new VoteDetailViewModel(item,uv)));
            }
            else await Navigation.PushAsync(new VoteDetailPage(new VoteDetailViewModel(item,uv)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            User u = await Auth.GetUser();
            if (u.Role == "3" || u.Role == "1")
            {
                //await Navigation.PushModalAsync(new NavigationPage(new NewVotePage()));
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