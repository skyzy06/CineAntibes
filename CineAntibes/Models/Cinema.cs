using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;

namespace CineAntibes.Models
{
    [DataContract]
    public class Cinema
    {
        public Cinema() { }

        public Cinema(JToken JSONCinema)
        {
            Key = JSONCinema.Value<string>("cle");
            Name = JSONCinema.Value<string>("nom");
            Name1 = JSONCinema.Value<string>("nom1");
            Name2 = JSONCinema.Value<string>("nom2");
            Address1 = JSONCinema.Value<string>("adresse1");
            Address2 = JSONCinema.Value<string>("adresse2");
            ZipCode = JSONCinema.Value<string>("codepostal");
            City = JSONCinema.Value<string>("ville");
            Country = JSONCinema.Value<string>("pays");
            Phone = JSONCinema.Value<string>("telephone");
            Email = JSONCinema.Value<string>("email");
            if (App.DecimalSep.Equals(","))
            {
                Latitude = Decimal.Parse(JSONCinema.Value<string>("latitude").Replace('.', ','));
                Longitude = Decimal.Parse(JSONCinema.Value<string>("longitude").Replace('.', ','));
            }
            else
            {
                Latitude = Decimal.Parse(JSONCinema.Value<string>("latitude"));
                Longitude = Decimal.Parse(JSONCinema.Value<string>("longitude"));
            }

            ScheduleUrl = JSONCinema.Value<string>("horaires");
            WhatsOnUrl = JSONCinema.Value<string>("affiche");
            MoviesUrl = JSONCinema.Value<string>("films");
            KindsUrl = JSONCinema.Value<string>("genre");
            EventsUrl = JSONCinema.Value<string>("evenements");
            ThreeDUrl = JSONCinema.Value<string>("3d");
            WithinAnHourUrl = JSONCinema.Value<string>("heure");
            PreviewsUrl = JSONCinema.Value<string>("filmsevenement");
            PricesUrl = JSONCinema.Value<string>("tarifs");
            ComingSoonUrl = JSONCinema.Value<string>("prochainement");
            ReservationUrl = JSONCinema.Value<string>("reserver");
        }

        #region Cinema's Informations
        [PrimaryKey]
        [DataMember]
        public string Key
        {
            get; set;
        }

        [DataMember]
        public string Name
        {
            get; set;
        }

        [DataMember]
        public string Name1
        {
            get; set;
        }

        [DataMember]
        public string Name2
        {
            get; set;
        }

        [DataMember]
        public string Address1
        {
            get; set;
        }

        [DataMember]
        public string Address2
        {
            get; set;
        }

        [DataMember]
        public string ZipCode
        {
            get; set;
        }

        [DataMember]
        public string City
        {
            get; set;
        }

        [DataMember]
        public string Country
        {
            get; set;
        }

        [DataMember]
        public string Phone
        {
            get; set;
        }

        [DataMember]
        public string Email
        {
            get; set;
        }

        [DataMember]
        public decimal? Latitude
        {
            get; set;
        }

        [DataMember]
        public decimal? Longitude
        {
            get; set;
        }
        #endregion

        #region Requests URL
        [DataMember]
        public string ScheduleUrl
        {
            get; set;
        }

        [DataMember]
        public string WhatsOnUrl
        {
            get; set;
        }

        [DataMember]
        public string MoviesUrl
        {
            get; set;
        }

        [DataMember]
        public string KindsUrl
        {
            get; set;
        }

        [DataMember]
        public string EventsUrl
        {
            get; set;
        }

        [DataMember]
        public string ThreeDUrl
        {
            get; set;
        }

        [DataMember]
        public string WithinAnHourUrl
        {
            get; set;
        }

        [DataMember]
        public string PreviewsUrl
        {
            get; set;
        }

        [DataMember]
        public string PricesUrl
        {
            get; set;
        }

        [DataMember]
        public string ComingSoonUrl
        {
            get; set;
        }

        [DataMember]
        public string ReservationUrl
        {
            get; set;
        }
        #endregion

        [DataMember]
        public DateTime LastSynchronisation
        {
            get; set;
        }

        [DataMember]
        public string WhatsOnJson
        {
            get; set;
        }

        [Ignore]
        public List<string> WhatsOn
        {
            get
            {
                if (!string.IsNullOrEmpty(WhatsOnJson))
                {
                    return JsonConvert.DeserializeObject<List<string>>(WhatsOnJson);
                }
                return null;
            }
            set
            {
                WhatsOnJson = JsonConvert.SerializeObject(value);
            }
        }

        public override string ToString()
        {
            return Name + "\n" + Address1 + "\n" + ZipCode + " " + City + "\nLast update: " + LastSynchronisation;
        }
    }
}
