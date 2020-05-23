using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EHMobile
{
    class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Xamarin.Forms.Color color = Xamarin.Forms.Color.Red;
            if (System.Convert.ToInt32(value) == 1) color = Xamarin.Forms.Color.Green;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
