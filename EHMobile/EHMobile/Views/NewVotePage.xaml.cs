using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Common.Models;
using EHMobile.Services;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewVotePage : ContentPage
    {
        public Vote Item { get; set; }
        public List<VoteOption> VoteOptions { get; set; }
        public Command LoadItemsCommand { get; set; }
        public NewVotePage()
        {
            InitializeComponent();
            Item = new Vote();
            VoteOptions = new List<VoteOption>();
            VoteOptions.Add(new VoteOption
            {
                VoteId = Item.Id
            });
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await new VoteDataStore().AddItemAsync(Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
           // var item = (User)layout.BindingContext;
        }
        async void AddOption_Clicked(object sender, EventArgs e)
        {
            
            VoteOptions.Add(new VoteOption
            {
                VoteId = Item.Id
            });
            MessagingCenter.Send(this, "AddOption", new VoteOption
            {
                VoteId = Item.Id
            });
            LoadItemsCommand.Execute(true);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                VoteOptions.Add(new VoteOption
                {
                    VoteId = Item.Id
                });
                BindingContext = this;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}