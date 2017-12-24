using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using Plugin.Connectivity;
using ModernHttpClient;
using System;
using WalledCityLahore.Helpers;
using WalledCityLahore.Models;
using System.Collections.Generic;

namespace WalledCityLahore.Services
{
    public class DataService
    {
        JsonSerializer _serializer = new JsonSerializer();
        public readonly HttpClient client = new HttpClient(new NativeMessageHandler());

        //private const string baseurl = "https://walledcitygc.000webhostapp.com/api/";
        private const string baseurl = "http://wclatourism.com/api/";
        private const string googleMapBaseurl = "https://maps.googleapis.com/maps/api/place/";
        private const string GOOGLE_MAPS_API_KEY = "https://maps.googleapis.com/maps/api/place/";

        public DataService()
        {
            client.BaseAddress = new System.Uri(baseurl);
            client.MaxResponseContentBufferSize = 512000;
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<AttractionResponse> getAllAttractions()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                var response = await client.GetAsync(baseurl + "attraction/read.php?token=" + Settings.AppToken);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<AttractionResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

        public async Task<RestaurantResponse> getAllRestaurants()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                var response = await client.GetAsync(baseurl + "resturant/read.php?token=" + Settings.AppToken);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<RestaurantResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

        public async Task<EventResponse> getAllEvents()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                var response = await client.GetAsync(baseurl + "event/read.php?token=" + Settings.AppToken);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<EventResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

		public async Task<BookingTypeResponse> getAllBookingsType()
		{
			if (!CrossConnectivity.Current.IsConnected)
				return null;

			try
			{
				var response = await client.GetAsync(baseurl + "booking/read_booking_type.php?token=" + Settings.AppToken);
				response.EnsureSuccessStatusCode();

				using (var stream = await response.Content.ReadAsStreamAsync())
				using (var reader = new StreamReader(stream))
				using (var json = new JsonTextReader(reader))
				{
					return _serializer.Deserialize<BookingTypeResponse>(json);
				}
			}
			catch (TaskCanceledException e)
			{
				Debug.WriteLine("Exception : " + e.Message);
				return null;
			}
			catch (Exception el)
			{
				Debug.WriteLine("Exception : " + el.Message);
				return null;
			}
		}

        public async Task<BookingResponse> getAllBookingsOf(string id)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                var response = await client.GetAsync(baseurl + "booking/read_booking.php?token=" + Settings.AppToken + "&id=" + id);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<BookingResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

        public async Task<GalleryResponse> getAllImages(string id, string type)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;
            
            try
            {
                var response = await client.GetAsync(baseurl + "image_gallery/read.php?token=" + Settings.AppToken + "&id=" + id);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<GalleryResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

        public async Task<BookingResponse> BookTrip(string bId, string name, string phone,
                                                    string persons, string email,
                                                    string date, string time)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                //var data
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string,string>("token",Settings.AppToken),
                    new KeyValuePair<string,string>("booking_id",bId),
                    new KeyValuePair<string,string>("name",name),
                    new KeyValuePair<string,string>("phone",phone),
                    new KeyValuePair<string,string>("persons",persons),
                    new KeyValuePair<string,string>("email",email),
                    new KeyValuePair<string,string>("date",date),
                    new KeyValuePair<string,string>("time",time)
                });

                var response = await client.PostAsync(baseurl + "booking/book_trip.php", content);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<BookingResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

        public async Task<BookingResponse> PostFeedback(string name, string phone, string email, string comments)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            try
            {
                //var data
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string,string>("token",Settings.AppToken),
                    new KeyValuePair<string,string>("name",name),
                    new KeyValuePair<string,string>("phone",phone),
                    new KeyValuePair<string,string>("email",email),
                    new KeyValuePair<string,string>("comments",comments)
                });

                var response = await client.PostAsync(baseurl + "feedback.php", content);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<BookingResponse>(json);
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("Exception : " + e.Message);
                return null;
            }
            catch (Exception el)
            {
                Debug.WriteLine("Exception : " + el.Message);
                return null;
            }
        }

    }
}
