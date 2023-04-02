using System.Collections.Generic;
using System.Web.Services;
using System;

//[WebService(Namespace = "http://example.com/hotels")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//public class HotelSearchService : WebService
//{
//    private const int PAGE_SIZE = 10;

//    [WebMethod]
//    public List<HotelResult> SearchHotels(double latitude, double longitude, int page = 1)
//    {
//        List<HotelResult> results = hotels.Select(h => new HotelResult(h, latitude, longitude)).ToList();
//        results.Sort();
//        int startIndex = (page - 1) * PAGE_SIZE;
//        if (startIndex >= results.Count)
//        {
//            return new List<HotelResult>();
//        }
//        int endIndex = Math.Min(startIndex + PAGE_SIZE, results.Count);
//        return results.GetRange(startIndex, endIndex - startIndex);
//    }
//}

public class HotelResult : IComparable<HotelResult>
{
    public string Name { get; private set; }
    public double Price { get; private set; }
    public double Distance { get; private set; }

    public HotelResult(Hotel hotel, double latitude, double longitude)
    {
        this.Name = hotel.Name;
        this.Price = hotel.Price;
        this.Distance = CalculateDistance(hotel.Location, new Location(latitude, longitude));
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

    public int CompareTo(HotelResult other)
    {
        if (this.Price != other.Price)
        {
            return this.Price.CompareTo(other.Price);
        }
        else
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }
}
