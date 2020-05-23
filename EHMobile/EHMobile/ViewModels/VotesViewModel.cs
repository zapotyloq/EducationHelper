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
    public class VotesViewModel : BaseViewModel
    {
        public IDataStore<Vote> DataStore => new VoteDataStore();
        public ObservableCollection<Vote> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public VotesViewModel()
        {
            Title = "Голосования";
            Items = new ObservableCollection<Vote>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

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
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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