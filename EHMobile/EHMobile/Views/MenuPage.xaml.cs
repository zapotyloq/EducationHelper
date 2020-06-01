using Common.Models;
using EHMobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EHMobile.Models;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>();
            ChangeMenu();
            Auth.UserChanged += ChangeMenu;

            //ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (id == 6)
                {
                    id = 1;
                    App.Current.Properties["Token"] = "";
                    Auth.User = null;
                }
                await RootPage.NavigateFromMenu(id);
                
            };
        }

        async void ChangeMenu()
        {
            menuItems.Clear();
            //menuItems.Add(new HomeMenuItem { Id = MenuItemType.Browse, Title = "Browse" });
            
            if (Auth.User == null)
            {
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Login, Title = "Авторизация" });
            }
            else
            {
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.News, Title = "Новости" });
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Events, Title = "Мероприятия" });
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Votes, Title = "Голосования" });
               
            }
            menuItems.Add(new HomeMenuItem { Id = MenuItemType.About, Title = "О приложении" });
            if(Auth.User != null)
            {
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Logout, Title = "Выход" });
            }
            ListViewMenu.ItemsSource = null;
            ListViewMenu.ItemsSource = menuItems;
        }
    }
}