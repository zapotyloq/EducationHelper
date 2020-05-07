﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using EHMobile.Models;
using EHMobile.Views;
using EHMobile.Services;

namespace EHMobile.ViewModels
{
    public class NewEventUserViewModel : BaseViewModel
    {
        public IDataStore<User> UDataStore => new UserDataStore();
        public ObservableCollection<User> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Event Event { get; set; }

        public NewEventUserViewModel(Event _event)
        {
            Title = "Добавить участника";
            Items = new ObservableCollection<User>();
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
                var items = await ((UserDataStore)UDataStore).GetUsersForEventAsync(Event.Id);
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