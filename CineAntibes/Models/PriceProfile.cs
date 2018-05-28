using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using SQLite;

namespace CineAntibes.Models
{
    [DataContract]
    public class PriceProfile
    {
        public PriceProfile() { }

        public PriceProfile(JToken prices)
        {
            CinemaKey = prices.Value<string>("Key");
            Price1 = prices.Value<decimal>("Price1");
            Price2 = prices.Value<decimal>("Price2");
            Price3 = prices.Value<decimal>("Price3");
            Price4 = prices.Value<decimal>("Price4");
            Price5 = prices.Value<decimal>("Price5");
            Price6 = prices.Value<decimal>("Price6");
            ThreeDPrice = prices.Value<decimal>("Price3D");
            ThreeDGlassesPrice = prices.Value<decimal>("Price3DGlasses");
            Subscription5Price = prices.Value<decimal>("Subscription5");
            Subscription10Price = prices.Value<decimal>("Subscription10");
        }

        [DataMember]
        [PrimaryKey]
        public string CinemaKey
        {
            get; set;
        }

        [DataMember]
        public decimal? Price1
        {
            get; set;
        }

        [DataMember]
        public decimal? Price2
        {
            get; set;
        }

        [DataMember]
        public decimal? Price3
        {
            get; set;
        }

        [DataMember]
        public decimal? Price4
        {
            get; set;
        }

        [DataMember]
        public decimal? Price5
        {
            get; set;
        }

        [DataMember]
        public decimal? Price6
        {
            get; set;
        }

        [DataMember]
        public decimal? ThreeDPrice
        {
            get; set;
        }

        [DataMember]
        public decimal? ThreeDGlassesPrice
        {
            get; set;
        }

        [DataMember]
        public decimal? Subscription5Price
        {
            get; set;
        }

        [DataMember]
        public decimal? Subscription10Price
        {
            get; set;
        }
    }
}
