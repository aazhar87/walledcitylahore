using System;
namespace WalledCityLahore.Models
{
    public class BookingResponse
    {
		public string status { get; set; }
		public string message { get; set; }
        public BookingItem[] data { get; set; }
    }
}
