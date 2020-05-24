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
    public partial class NewVoteOptionPage : ContentPage
    {
        NewVoteOptionViewModel viewModel;

        public NewVoteOptionPage(NewVoteOptionViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (VoteOption)layout.BindingContext; 
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Vote.AuthorId || u.Role == "1")
            {
                VoteOption ue = new VoteOption
                {
                    VoteId = viewModel.Vote.Id,
                    //UserId = item.Id
                };
                await new VoteOptionDataStore().AddItemAsync(ue);
                await Navigation.PopModalAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public async void AddOption(object sender, EventArgs args)
        {
            await new VoteOptionDataStore().AddItemAsync(viewModel.VoteOption);
            await Navigation.PopModalAsync();
        }
    }
}