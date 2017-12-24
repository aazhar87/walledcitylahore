using System;
namespace WalledCityLahore.Models
{
    public class BookingTypeResponse
    {
		public string status { get; set; }
		public string message { get; set; }
        public BookingTypeItem[] data { get; set; }
    }
}
