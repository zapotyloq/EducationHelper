using Android.Graphics;
using Common.Models;
using EHMobile.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EHMobile
{
    class UserFromUEDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserEvent ue = new UserEventDataStore().GetItem(System.Convert.ToInt32(value));
            return new UserDataStore().GetItem(ue.UserId).FIO;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
