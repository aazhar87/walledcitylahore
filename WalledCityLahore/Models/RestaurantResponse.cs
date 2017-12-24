using System;

namespace WalledCityLahore.Models
{
    public class RestaurantResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public RestaurantItem[] data { get; set; }
    }
}