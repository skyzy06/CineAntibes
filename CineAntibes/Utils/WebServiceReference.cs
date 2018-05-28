using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using CineAntibes.Models;
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

        public async void GetCinemaInformations()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string formatedParameters = FormatGetParameters(new[]
                    {
                        new KeyValuePair<string, string>("p", "cinemas"),
                        new KeyValuePair<string, string>("app",Platform)
                    });

                    HttpResponseMessage response = await client.GetAsync(ServerUrl + formatedParameters);

                    JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(
                        await response.Content.ReadAsStringAsync()
                    );

                    foreach (JToken token in jsonResponse.Children())
                    {
                        App.CinemaTable.Save(new Cinema(token));
                    }

                    GetCinemaPrice();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async void GetCinemaPrice()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Cinema currentCinema = App.CinemaTable.GetCurrentCinema();

                    string formatedParameters = FormatGetParameters(new[]
                    {
                        new KeyValuePair<string, string>("key", currentCinema.Key)
                    });

                    HttpResponseMessage response = await client.GetAsync(GoogleWSUrl + formatedParameters);

                    JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(
                        await response.Content.ReadAsStringAsync()
                    );

                    App.PriceTable.Save(new PriceProfile(jsonResponse));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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
