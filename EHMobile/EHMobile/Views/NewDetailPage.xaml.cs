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
    public partial class NewDetailPage : ContentPage
    {
        NewDetailViewModel viewModel;

        public NewDetailPage(NewDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            if (Auth.User.Id == viewModel.Item.AuthorId || Auth.User.Role == "1")
            {
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
        //async void AddFile_Clicked(object sender, EventArgs args)
        //{
        //    //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
        //    //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
        //    //FromUri— Требуется объект URI, например. new Uri("http://server.com/image.jpg") .

            

        //    Image img = new Image();
        //    if (CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
        //        img.Source = ImageSource.FromFile(photo.Path);

        //        FileStream fs = new FileStream(photo.Path, FileMode.Open, FileAccess.Read);
        //        UserEventDocument ued = new UserEventDocument
        //        {
        //            File = new byte[fs.Length],
        //            //FilePath = "ss",
        //            UserEventId = viewModel.UserEvent.Id
        //        };

        //        fs.Read(ued.File, 0, Convert.ToInt32(fs.Length));
        //        fs.Close();

        //        await viewModel.UED_DataStore.AddItemAsync(ued);
        //    }
        //}

        //async void OpenDocks(object sender, EventArgs args)
        //{
        //    User u = await Auth.GetUser();
        //    if(u.Id == viewModel.Item.AuthorId || u.Role == "1")
        //    {
        //        await Navigation.PushAsync(new EventDocumentsPage(new EventDocumentsViewModel(viewModel.Item)));
        //    }
        //}
        //async void OpenUsers(object sender, EventArgs args)
        //{
        //    User u = await Auth.GetUser();
        //    if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
        //    {
        //        await Navigation.PushAsync(new EventUsersPage(new EventUsersViewModel(viewModel.Item)));
        //    }
        //}
        async void RemoveEvent(object sender, EventArgs args)
        {
            User u = await Auth.GetUser();
            if (u.Id == viewModel.Item.AuthorId || u.Role == "1")
            {
                await new NewDataStore().DeleteItemAsync(viewModel.Item.Id);
                await Navigation.PopAsync();
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
    }
}