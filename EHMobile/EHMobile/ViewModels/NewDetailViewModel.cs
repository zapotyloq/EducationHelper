using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class NewDetailViewModel : BaseViewModel
    {
        public IDataStore<New> DataStore => DependencyService.Get<IDataStore<New>>();
        public New Item { get; set; }
        public Command LoadItemsCommand { get; set; }
        public NewDetailViewModel(New item)
        {
            Title = item?.Name;
            Item = item;
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
               //UserEventDocuments = await new UserEventDocumentDataStore().GetUEDAsync(UserEvent.Id);
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
