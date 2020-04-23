using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Mobile.Models;
using Mobile.ViewModels;

namespace Mobile.Views
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
        }

        public NewDetailPage()
        {
            InitializeComponent();

            var item = new New
            {
                Title = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new NewDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}