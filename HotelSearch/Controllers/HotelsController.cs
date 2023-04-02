using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using HotelSearch.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotelSearch.Controllers
{
    public class HotelsController : ApiController
    {
        //TODO: dodati tokene za sigurnost
        // GET api/hotels
        public string Get()
        {
            using (var db = new HotelSearchContext())
            {
                var hotels = db.Hotels.ToList();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(hotels);

                return json;
            }
        }

        //TODO: dodati paging
        // GET api/hotels
        //public string Get(int page = 1, int pageSize = 10)
        //{
        //    using (var db = new HotelSearchContext())
        //    {
        //        var hotels = db.Hotels
        //                        .Skip((page - 1) * pageSize)
        //                        .Take(pageSize)
        //                        .ToList();
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        string json = serializer.Serialize(hotels);

        //        return json;
        //    }
        //}

        // GET api/hotels/5
        public string Get(int id)
        {
            using (var db = new HotelSearchContext())
            {
                var hotels = db.Hotels.Find(id);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(hotels);

                return json;
            }
        }

        // POST api/hotels
        public void Post([FromBody] JObject value)
        {
            var hotel = JsonConvert.DeserializeObject<Hotels>(value.ToString());

            using (var db = new HotelSearchContext())
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
            }
        }

        // PUT api/hotels/5
        public void Put(int id, [FromBody] JObject value)
        {
            var hotel = JsonConvert.DeserializeObject<Hotels>(value.ToString());
            hotel.Id = id;

            using (var db = new HotelSearchContext())
            {
                db.Entry(hotel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/hotels/5
        public void Delete(int id)
        {
            using (var db = new HotelSearchContext())
            {
                var hotel = db.Hotels.Find(id);
                db.Hotels.Remove(hotel);
                db.SaveChanges();
            }
        }
    }


    //public class Hotel
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public double Price { get; set; }
    //    public Location Location { get; set; }
    //}

    //public class Location
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //}

}
