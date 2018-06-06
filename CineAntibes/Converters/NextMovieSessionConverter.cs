using System;
using System.Globalization;
using System.Linq;
using CineAntibes.Models;
using Xamarin.Forms;

namespace CineAntibes.Converters
{
    public class NextMovieSessionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Movie currentMovie = App.MovieTable.GetMovieByID((int)value);
            string formatedParameter = (string)parameter;

            if (currentMovie != null)
            {
                DateTime nextSession = DateTime.MinValue;
                string format = Resources.AppResources.MovieNextSession;

                if (formatedParameter == "VF" && currentMovie.VFSessions != null)
                {
                    IOrderedEnumerable<DateTime> result = currentMovie.VFSessions.Where(d => d > DateTime.Now).OrderBy(d => d);
                    if (result.Count() > 0)
                    {
                        nextSession = result.First();
                    }
                }
                else if (formatedParameter == "VO" && currentMovie.VOSessions != null)
                {
                    IOrderedEnumerable<DateTime> result = currentMovie.VOSessions.Where(d => d > DateTime.Now).OrderBy(d => d);
                    if (result.Count() > 0)
                    {
                        nextSession = result.First();
                    }
                }

                string dateAsString = nextSession != DateTime.MinValue ?
                                                             nextSession.ToString(Resources.AppResources.SessionDateFormat)
                                                             : Resources.AppResources.NoNextSession;
                
                return String.Format(format, formatedParameter, dateAsString);

            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
