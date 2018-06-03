﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CineAntibes.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;

namespace CineAntibes.Models
{
    [DataContract]
    public class Movie
    {
        public Movie() { }

        public Movie(JToken JSONMovie)
        {
            ID = Int32.Parse(JSONMovie.Value<string>("film_id"));
            Title = StringTools.Sanitize(JSONMovie.Value<string>("nom"));
            Synopsis = StringTools.Sanitize(JSONMovie.Value<string>("synopsis"));
            Director = JSONMovie.Value<string>("realisateur");
            Stars = JSONMovie.Value<string>("acteurs");
            Kinds = JSONMovie.Value<string>("type");
            if (App.DecimalSep.Equals(","))
            {
                Rating = Decimal.Parse(JSONMovie.Value<string>("note").Replace('.', ','));
            }
            else
            {
                Rating = Decimal.Parse(JSONMovie.Value<string>("note"));
            }
            Duration = JSONMovie.Value<string>("duree");
            ReleaseDate = DateTools.FormatReleaseDate(JSONMovie.Value<string>("date"));
            VFSessions = DateTools.FormatSessions(JSONMovie.Value<JToken>("seances_vf"));
            VOSessions = DateTools.FormatSessions(JSONMovie.Value<JToken>("seances_vo"));
            Thumbnail = JSONMovie.Value<string>("image");
            ImagePreview = JSONMovie.Value<string>("imagelarge");
        }

        [PrimaryKey]
        [DataMember]
        public int ID
        {
            get; set;
        }

        [DataMember]
        public string Title
        {
            get; set;
        }

        [DataMember]
        public string Synopsis
        {
            get; set;
        }

        [DataMember]
        public string Director
        {
            get; set;
        }

        [DataMember]
        public string Stars
        {
            get; set;
        }

        [DataMember]
        public string Kinds
        {
            get; set;
        }

        [DataMember]
        public decimal Rating
        {
            get; set;
        }

        [DataMember]
        public string Duration
        {
            get; set;
        }

        [DataMember]
        public DateTime ReleaseDate
        {
            get; set;
        }

        [Ignore]
        public List<DateTime> VFSessions
        {
            get
            {
                return JsonConvert.DeserializeObject<List<DateTime>>(VFSessionsJson);
            }
            set
            {
                VFSessionsJson = JsonConvert.SerializeObject(value);
            }
        }

        [DataMember]
        public string VFSessionsJson
        {
            get; set;
        }

        [Ignore]
        public List<DateTime> VOSessions
        {
            get
            {
                return JsonConvert.DeserializeObject<List<DateTime>>(VOSessionsJson);
            }
            set
            {
                VOSessionsJson = JsonConvert.SerializeObject(value);
            }
        }

        [DataMember]
        public string VOSessionsJson
        {
            get; set;
        }

        [DataMember]
        public string Thumbnail
        {
            get; set;
        }

        [DataMember]
        public string ImagePreview
        {
            get; set;
        }

        public override string ToString()
        {
            return Title + " by " + Director;
        }
    }
}
