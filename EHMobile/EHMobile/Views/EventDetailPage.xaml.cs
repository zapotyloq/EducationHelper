using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Common.Models;
using EHMobile.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Android.Provider;
using System.Net.Http;
using EHMobile.Services;
using System.IO;

namespace EHMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EventDetailPage : ContentPage
    {
        EventDetailViewModel viewModel;

        public EventDetailPage(EventDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            if (Auth.User.Id == viewModel.Item.AuthorId || Auth.User.Role == "1")
            {
                Button btn_docks = new Button
                {
                    Text = "Документы",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_docks.Clicked += OpenDocks;
                sL.Children.Add(btn_docks);

                Button btn_users = new Button
                {
                    Text = "Участники",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_users.Clicked += OpenUsers;
                sL.Children.Add(btn_users);

                Button btn_opl = new Button
                {
                    Text = "Оплата события",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_opl.Clicked += OplEvent;
                sL.Children.Add(btn_opl);

                Button btn_rmv = new Button
                {
                    Text = "Отправить в архив",
                    BackgroundColor = Color.SpringGreen,
                    TextColor = Color.White,
                    FontSize = 22
                };
                btn_rmv.Clicked += RemoveEvent;
                sL.Children.Add(btn_rmv);
            }
        }
        async void AddFile_Clicked(object sender, EventArgs args)
        {
            //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            //FromUri— Требуется объект URI, например. new Uri("http://server.com/image.jpg") .

            

            Image img = new Image();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                img.Source = ImageSource.FromFile(photo.Path);

                FileStream fs = new FileStream(photo.Path, FileMode.Open, FileAccess.Read);
                UserEventDocument ued = new UserEventDocument
                {
                    File = new byte[fs.Length],
                    //FilePath = "ss",
                    UserEventId = viewModel.UserEvent.Id
                };

                fs.Read(ued.File, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                await viewModel.UED_DataStore.AddItemAsync(ued);
            }
        }

        async void OpenDocks(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if(u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {
                await Navigation.PushAsync(new EventDocumentsPage(new EventDocumentsViewModel(viewModel.Item)));
            }
        }
        async void OpenUsers(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {
                await Navigation.PushAsync(new EventUsersPage(new EventUsersViewModel(viewModel.Item)));
            }
        }
        async void RemoveEvent(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {

            }
        }

        async void OplEvent(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsBusy = true;
        }
        async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                User u = await Auth.GetUser();

                if(u.Role=="1" || u.Id == viewModel.Item.AuthorId)
                {
                    var layout = (BindableObject)sender;
                    var item = (UserEventDocument)layout.BindingContext;
                    await Navigation.PushAsync(new EventDocumentDetailPage(new EventDocumentDetailViewModel(viewModel.Item, u, item)));
                }
                //imgPopup.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.UserEventDocument.File));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}