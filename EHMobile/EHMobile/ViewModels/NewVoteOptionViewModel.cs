using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Common.Models;
using EHMobile.Views;
using EHMobile.Services;

namespace EHMobile.ViewModels
{
    public class NewVoteOptionViewModel : BaseViewModel
    {
        public IDataStore<VoteOption> UDataStore => new VoteOptionDataStore();
        public VoteOption VoteOption { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Vote Vote { get; set; }

        public NewVoteOptionViewModel(Vote vote)
        {
            Title = "Добавить вариант ответа";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Vote = vote;
            VoteOption = new VoteOption
            {
                VoteId = Vote.Id
            };
            /*MessagingCenter.Subscribe<NewEventPage, Event>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Event;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });*/
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                
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