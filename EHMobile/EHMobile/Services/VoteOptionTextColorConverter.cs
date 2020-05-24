using Android.Graphics;
using Common.Models;
using EHMobile.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EHMobile
{
    class VoteOptionTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Xamarin.Forms.Color color = Xamarin.Forms.Color.LightGray;
            var voteopt = new VoteOptionDataStore().GetItem(System.Convert.ToInt32(value));
            var r = new UserVoteDataStore().GetItemByVoteId(voteopt.VoteId);
            if (r.ChoiseId == voteopt.Id) color = Xamarin.Forms.Color.Green;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
