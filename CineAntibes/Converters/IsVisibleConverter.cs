using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using CineAntibes.Models;
using Xamarin.Forms;

namespace CineAntibes.Converters
{
    public class IsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.GetType().Equals(typeof(ObservableCollection<KeyValuePair<DateTime, List<Session>>>)))
                {
                    return (value as ObservableCollection<KeyValuePair<DateTime, List<Session>>>).Count > 0;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
