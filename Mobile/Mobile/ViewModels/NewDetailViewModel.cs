using System;

using Mobile.Models;

namespace Mobile.ViewModels
{
    public class NewDetailViewModel : BaseViewModel
    {
        public New Item { get; set; }
        public NewDetailViewModel(New item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
