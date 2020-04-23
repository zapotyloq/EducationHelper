using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mobile.Models;
using Mobile.Views;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public ObservableCollection<New> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public IDataStore<New> DataStore => DependencyService.Get<IDataStore<New>>();

        public NewsViewModel()
        {
            Title = "Новости";
            Items = new ObservableCollection<New>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewNewPage, New>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as New;
                Items.Add(newItem);
                await DataStore.AddAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetAsync(true);
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