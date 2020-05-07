using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using EHMobile.Models;
using EHMobile.Views;
using EHMobile.Services;

namespace EHMobile.ViewModels
{
    public class EventDocumentsViewModel : BaseViewModel
    {
        public IDataStore<UserEventDocument> DataStore => new UserEventDocumentDataStore();
        public ObservableCollection<UserEventDocument> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Event Event { get; set; }

        public EventDocumentsViewModel(Event _event)
        {
            Title = "Документы";
            Items = new ObservableCollection<UserEventDocument>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Event = _event;
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
                var items = await ((UserEventDocumentDataStore)DataStore).GetBEDAsync(Event.Id);
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