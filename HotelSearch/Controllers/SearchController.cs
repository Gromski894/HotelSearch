using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using HotelSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotelSearch.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private static List<Hotels> hotels = new List<Hotels>();

        // POST api/search
        //[HttpPost]
        public string Get()
        {
            //TODO: staviti dohvat pozicije s klijenta
            //var lokacija = JsonConvert.DeserializeObject<Location>(value.ToString());
            double Latitude = 45.326908; double Longitude = 14.441000;
            Location myLocation = new Location(); //JsonConvert.DeserializeObject<Location>(value.ToString());
            myLocation.Latitude = Latitude;
            myLocation.Longitude = Longitude;


            using (var db = new HotelSearchContext())
            {
                var results = db.Hotels
                .AsEnumerable()
                .Select(h => new
                {
                    h.Id,
                    h.Name,
                    h.Price,
                    h.Longitude,
                    h.Latitude,
                    Distance = CalculateDistance(myLocation, new Location { Latitude = (double)h.Latitude, Longitude = (double)h.Longitude })
                })
                .OrderBy(r => r.Price)
                .ThenBy(r => r.Distance)
                .ToList();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(results);

                return json;
            }
        }

        private static double CalculateDistance(Location location1, Location location2)
        {
            //Haversine formula
            const double EARTH_RADIUS = 6371; // in km
            double lat1 = DegreesToRadians(location1.Latitude);
            double lon1 = DegreesToRadians(location1.Longitude);
            double lat2 = DegreesToRadians(location2.Latitude);
            double lon2 = DegreesToRadians(location2.Longitude);
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;
            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EARTH_RADIUS * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public class Location
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }

}
