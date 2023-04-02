using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

[WebService(Namespace = "http://example.com/hotels")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class HotelService : WebService
{
    private static List<Hotel> hotels = new List<Hotel>();

    [WebMethod]
    public Hotel AddHotel(string name, double price, double latitude, double longitude)
    {
        Hotel hotel = new Hotel(name, price, new Location(latitude, longitude));
        hotels.Add(hotel);
        return hotel;
    }

    [WebMethod]
    public List<Hotel> GetAllHotels()
    {
        return hotels;
    }

    [WebMethod]
    public void UpdateHotel(int id, string name, double price, double latitude, double longitude)
    {
        Hotel hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel != null)
        {
            hotel.Name = name;
            hotel.Price = price;
            hotel.Location = new Location(latitude, longitude);
        }
    }

    [WebMethod]
    public void DeleteHotel(int id)
    {
        hotels.RemoveAll(h => h.Id == id);
    }
}

public class Hotel
{
    private static int lastId = 0;

    public int Id { get; private set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Location Location { get; set; }

    public Hotel(string name, double price, Location location)
    {
        this.Id = ++lastId;
        this.Name = name;
        this.Price = price;
        this.Location = location;
    }
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Location(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}
