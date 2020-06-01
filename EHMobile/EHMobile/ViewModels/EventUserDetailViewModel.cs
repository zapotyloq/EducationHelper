using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class EventUserDetailViewModel : BaseViewModel
    {
        public IDataStore<UserEvent> UE_DataStore => new UserEventDataStore();
        public IDataStore<UserEventDocument> UED_DataStore => new UserEventDocumentDataStore();
        public Event Event { get; set; }
        public User User { get; set; }
        public UserEvent UserEvent { get; set; }
        public List<UserEventDocument> UserEventDocuments { get; set; }
        public Command LoadItemsCommand { get; set; }
        public EventUserDetailViewModel(Event _event, User _user, UserEvent ue)
        {
            User = _user;
            Event = _event;
            UserEventDocuments = new List<UserEventDocument>();
            UserEvent = ue;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                UserEvent = await ((UserEventDataStore)UE_DataStore).GetItemAsync(Event.Id, User.Id);
                UserEventDocuments = await ((UserEventDocumentDataStore)UED_DataStore).GetUEDAsync(UserEvent.Id);
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
