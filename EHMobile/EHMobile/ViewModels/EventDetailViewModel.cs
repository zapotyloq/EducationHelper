using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class EventDetailViewModel : BaseViewModel
    {
        public IDataStore<UserEvent> DataStore => DependencyService.Get<IDataStore<UserEvent>>();
        public IDataStore<UserEventDocument> UED_DataStore => new UserEventDocumentDataStore();
        public Event Item { get; set; }
        public UserEvent UserEvent {get; set; }
        public List<UserEventDocument> UserEventDocuments { get; set; }
        public Command LoadItemsCommand { get; set; }
        public EventDetailViewModel(Event item, UserEvent userEvent, List<UserEventDocument> ued = null)
        {
            Title = item?.Name;
            Item = item;
            UserEvent = userEvent;
            UserEventDocuments = ued;
            //UserEventDocuments = new UserEventDocumentDataStore().GetUEDAsync(UserEvent.Id).Result;
            //UserEventDocuments = userEventDocuments != null ? userEventDocuments : new List<UserEventDocument>();
            //UserEvent = ((UserEventDataStore)DataStore).GetItemAsync(Item.Id, Auth.User.Id).Result;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
               //UserEvent = await ((UserEventDataStore)DataStore).GetItemAsync(Item.Id,.Id);
               UserEventDocuments = await new UserEventDocumentDataStore().GetUEDAsync(UserEvent.Id);
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
