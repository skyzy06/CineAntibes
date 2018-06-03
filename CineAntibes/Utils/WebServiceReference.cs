using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CineAntibes.Models;
using CineAntibes.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CineAntibes.Utils
{
    public class WebServiceReference
    {
        static string ServerUrl = "http://www.lecasinocinema.fr/json?";
        static string GoogleWSUrl = "https://script.google.com/macros/s/AKfycbysXi-D8XWA4-q3dgknLzb_7F70zAn8EyFLq5TbQwbeop5WpD0/exec?";
        static string Platform = Device.RuntimePlatform == Device.Android ? "android" : "iphone";

        Cinema currentCinema;

        public async Task GetCinemaInformations()
        {
            try
            {
                currentCinema = App.CinemaTable.GetCurrentCinema();
                TimeSpan timeSinceLastSynchro = new TimeSpan(42, 0, 0);

                if (currentCinema != null)
                {
                    timeSinceLastSynchro = DateTime.Now.Subtract(currentCinema.LastSynchronisation);
                }

                if (timeSinceLastSynchro.Hours > 1)
                {
                    IEnumerable<KeyValuePair<string, string>> requestParams = new[]
                    {
                        new KeyValuePair<string, string>("p", "cinemas"),
                        new KeyValuePair<string, string>("app",Platform)
                    };

                    string response = await SendRequest(ServerUrl, requestParams);
                    JToken jsonResponse = JsonConvert.DeserializeObject<JArray>(response)[0];

                    App.CinemaTable.Save(new Cinema(jsonResponse));

                    currentCinema = App.CinemaTable.GetCurrentCinema();

                    await GetMovies();

                    Parallel.Invoke(
                        async () => await GetCinemaPrice(),
                        async () => await GetWhatsOn()
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task GetCinemaPrice()
        {
            try
            {
                IEnumerable<KeyValuePair<string, string>> requestParameters = new[]
                {
                    new KeyValuePair<string, string>("key", currentCinema.Key)
                };

                string response = await SendRequest(GoogleWSUrl, requestParameters);
                JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);

                App.PriceTable.Save(new PriceProfile(jsonResponse));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task GetMovies()
        {
            try
            {
                string response = await SendRequest(currentCinema.MoviesUrl);
                JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
                IJEnumerable<JToken> listJsonMovies = jsonResponse.Values();
                IEnumerator<JToken> movieEnumerator = listJsonMovies.GetEnumerator();

                while (movieEnumerator.MoveNext())
                {
                    Movie movie = new Movie(movieEnumerator.Current);
                    App.MovieTable.Save(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task GetWhatsOn()
        {
            try
            {
                string response = await SendRequest(currentCinema.WhatsOnUrl);
                JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response);
                IEnumerator<JToken> whatsonEnumerator = jsonResponse.GetEnumerator();
                List<string> whatsOnList = new List<string>();
                while (whatsonEnumerator.MoveNext())
                {
                    whatsOnList.Add(whatsonEnumerator.Current.ToString());
                }
                Debug.WriteLine(App.MovieTable.GetMovieByID(187334).Synopsis);

                currentCinema.WhatsOn = whatsOnList;
                currentCinema.LastSynchronisation = DateTime.Now;

                App.CinemaTable.Save(currentCinema);

                (((Application.Current.MainPage as MasterDetailPage).Detail as NavigationPage).CurrentPage as WhatsOn)
                    .GetContext().ListMovies = new ObservableCollection<Movie>(App.MovieTable.GetWhatsOnMovie());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task<string> SendRequest(string url, IEnumerable<KeyValuePair<string, string>> parameters = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string formatedParameters = "";

                    if (parameters != null)
                    {
                        formatedParameters = FormatGetParameters(parameters);
                    }

                    HttpResponseMessage response = await client.GetAsync(url + formatedParameters);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return string.Empty;
            }

        }

        string FormatGetParameters(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            string output = "";

            foreach (KeyValuePair<string, string> param in parameters)
            {
                output += WebUtility.UrlEncode(param.Key) + "=" + WebUtility.UrlEncode(param.Value);
                if (!param.Equals(parameters.Last()))
                {
                    output += "&";
                }
            }

            return output;
        }
    }
}
