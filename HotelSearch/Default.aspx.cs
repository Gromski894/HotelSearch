using HotelSearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Diagnostics;
using static HotelSearch.Controllers.SearchController;

namespace HotelSearch
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindHotels();
            }
        }

        private void BindHotels()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/hotels").Result;

                if (response.IsSuccessStatusCode)
                {
                    //string hotelData = response.Content.ReadAsStringAsync().Result;
                    string hotelData = response.Content.ReadAsAsync<string>().Result;
                    List<Hotels> hotels = JsonConvert.DeserializeObject<List<Hotels>>(hotelData);

                    gvHotels.DataSource = hotels;
                    gvHotels.DataBind();
                }
                else
                {
                    lblMessage.Text = "Error retrieving hotels. " + response.StatusCode;
                }
            }
        }

        private void ClearFields()
        {
            hdHotelId.Value = "";
            txtHotelName.Text = "";
            txtPrice.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string hotelName = txtHotelName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal latitude = Convert.ToDecimal(txtLatitude.Text);
            decimal longitude = Convert.ToDecimal(txtLongitude.Text);

            Hotels newHotel = new Hotels()
            {
                Name = hotelName,
                Price = price,
                Latitude = latitude,
                Longitude = longitude
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/hotels", newHotel).Result;

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Hotel added successfully.";
                    ClearFields();
                    BindHotels();
                }
                else
                {
                    lblMessage.Text = "Error adding hotel. " + response.StatusCode;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int hotelId = Convert.ToInt32(hdHotelId.Value);
            string hotelName = txtHotelName.Text;
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal latitude = Convert.ToDecimal(txtLatitude.Text);
            decimal longitude = Convert.ToDecimal(txtLongitude.Text);

            Hotels updatedHotel = new Hotels()
            {
                Id = hotelId,
                Name = hotelName,
                Price = price,
                Latitude = latitude,
                Longitude = longitude
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PutAsJsonAsync($"api/hotels/{hotelId}", updatedHotel).Result;

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Hotel updated successfully.";
                    ClearFields();
                    BindHotels();
                }
                else
                {
                    lblMessage.Text = "Error updating hotel. " + response.StatusCode;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int hotelId = int.Parse(btn.CommandArgument);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync($"api/hotels/{hotelId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Hotel deleted successfully.";
                    ClearFields();
                    BindHotels();
                }
                else
                {
                    lblMessage.Text = "Error deleting hotel. " + response.StatusCode;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtHotelName.Text))
            {
                lblMessage.Text = "Please enter hotel name";
                return;
            }
            else if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                lblMessage.Text = "Please enter a valid decimal number for price";
                return;
            }
            else if (!IsValidLatitude(txtLatitude.Text))
            {
                lblMessage.Text = "Please enter a valid latitude";
                return;
            }
            else if (!IsValidLongitude(txtLongitude.Text))
            {
                lblMessage.Text = "Please enter a valid longitude";
                return;
            }

            if (string.IsNullOrEmpty(hdHotelId.Value))
                btnAdd_Click(sender, e);
            else
                btnUpdate_Click(sender, e);
            
        }

        private bool IsValidLatitude(string latitude)
        {
            double result;
            bool isValid = double.TryParse(latitude, out result);
            isValid = isValid && result >= -90 && result <= 90;

            return isValid;
        }
        private bool IsValidLongitude(string longitude)
        {
            double result;
            bool isValid = double.TryParse(longitude, out result);
            isValid = isValid && result >= -180 && result <= 180;

            return isValid;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void PrepareEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int hotelId = int.Parse(btn.CommandArgument);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/hotels/"+hotelId.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string hotelData = response.Content.ReadAsAsync<string>().Result;
                    Hotels hotels = JsonConvert.DeserializeObject<Hotels>(hotelData);

                    hdHotelId.Value = hotels.Id.ToString();
                    txtHotelName.Text = hotels.Name;
                    txtPrice.Text = hotels.Price.ToString();
                    txtLatitude.Text = hotels.Latitude.ToString();
                    txtLongitude.Text = hotels.Longitude.ToString();
                }
                else
                {
                    lblMessage.Text = "Error retrieving hotels. " + response.StatusCode;
                }

            }
        
        }


        #region Search

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!IsValidLatitude(txtMyLatitude.Text))
            {
                lblMessage.Text = "Please enter a valid latitude";
                return;
            }
            else if (!IsValidLongitude(txtMyLongitude.Text))
            {
                lblMessage.Text = "Please enter a valid longitude";
                return;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                double latitude = Convert.ToDouble(txtMyLatitude.Text);
                double longitude = Convert.ToDouble(txtMyLongitude.Text);

                Location lokacija = new Location()
                {
                    Latitude = latitude,
                    Longitude = longitude
                };


                //HttpResponseMessage response = client.PutAsJsonAsync("api/search/", lokacija).Result;
                HttpResponseMessage response = client.GetAsync("api/search").Result;

                if (response.IsSuccessStatusCode)
                {
                    string hotelData = response.Content.ReadAsAsync<string>().Result;
                    var hotels = JsonConvert.DeserializeObject(hotelData);

                    gvSearch.DataSource = hotels;
                    gvSearch.DataBind();
                }
                else
                {
                    lblMessage.Text = "Error retrieving search. " + response.StatusCode;
                }
            }

        }

        #endregion

        
    }
}