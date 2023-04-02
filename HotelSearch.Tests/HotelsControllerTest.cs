using HotelSearch.Controllers;
using HotelSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static HotelSearch.Controllers.SearchController;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HotelSearch.Tests
{
    [TestClass]
    public class HotelsControllerTests
    {
        [TestMethod]
        public void TestGetHotelsReturnsAllHotels()
        {
            // Arrange
            var controller = new HotelsController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            var hotels = JsonConvert.DeserializeObject<List<Hotels>>(result);
            Assert.AreEqual(3, hotels.Count);
        }

        [TestMethod]
        public void TestGetHotelReturnsCorrectHotel()
        {
            // Arrange
            var controller = new HotelsController();

            // Act
            var result = controller.Get(2);

            // Assert
            Assert.IsNotNull(result);
            var hotel = JsonConvert.DeserializeObject<Hotels>(result);
            Assert.AreEqual("Hotel B", hotel.Name);
        }

        [TestMethod]
        public void TestPostAddsNewHotel()
        {
            // Arrange
            var controller = new HotelsController();
            var newHotel = new Hotels
            {
                Name = "New Hotel",
                Price = 100,
                Latitude = 45.5231M,
                Longitude = -122.6765M
            };

            // Act
            controller.Post(JObject.FromObject(newHotel));

            // Assert
            using (var db = new HotelSearchContext())
            {
                var hotel = db.Hotels.FirstOrDefault(h => h.Name == "New Hotel");
                Assert.IsNotNull(hotel);
                Assert.AreEqual(100, hotel.Price);
                Assert.AreEqual(45.5231, hotel.Latitude);
                Assert.AreEqual(-122.6765, hotel.Longitude);
            }
        }

        [TestMethod]
        public void TestPutUpdatesHotel()
        {
            // Arrange
            var controller = new HotelsController();
            var updatedHotel = new Hotels
            {
                Name = "Updated Hotel",
                Price = 150,
                Latitude = 47.6062M,
                Longitude = -122.3321M
            };

            // Act
            controller.Put(2, JObject.FromObject(updatedHotel));

            // Assert
            using (var db = new HotelSearchContext())
            {
                var hotel = db.Hotels.FirstOrDefault(h => h.Id == 2);
                Assert.IsNotNull(hotel);
                Assert.AreEqual("Updated Hotel", hotel.Name);
                Assert.AreEqual(150, hotel.Price);
                Assert.AreEqual(47.6062, hotel.Latitude);
                Assert.AreEqual(-122.3321, hotel.Longitude);
            }
        }

        [TestMethod]
        public void TestDeleteRemovesHotel()
        {
            // Arrange
            var controller = new HotelsController();

            // Act
            controller.Delete(3);

            // Assert
            using (var db = new HotelSearchContext())
            {
                var hotel = db.Hotels.FirstOrDefault(h => h.Id == 3);
                Assert.IsNull(hotel);
            }
        }
    }

}
