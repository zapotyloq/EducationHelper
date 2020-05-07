using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EHMobile.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class EventDocumentDetailViewModel : BaseViewModel
    {
        public IDataStore<UserEventDocument> UED_DataStore => new UserEventDocumentDataStore();
        public Event Event { get; set; }
        public User User { get; set; }
        public UserEvent UserEvent { get; set; }
        public UserEventDocument UserEventDocument { get; set; }
        public Command LoadItemsCommand { get; set; }
        public EventDocumentDetailViewModel(Event _event, User user,UserEventDocument ued)
        {
            Event = _event;
            User = user;
            UserEventDocument = ued;

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                UserEventDocument = await ((UserEventDocumentDataStore)UED_DataStore).GetItemAsync(UserEventDocument.Id);
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
