using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelSearch.Models
{
    //public class Hotel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public double Price { get; set; }
    //    public Location Location { get; set; }
    //}

    //public class Location
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //}

    public class HotelSearchContext : DbContext
    {
        public HotelSearchContext() : base("name=Database1Entities")
        {
        }

        public DbSet<Hotels> Hotels { get; set; }
    }
}