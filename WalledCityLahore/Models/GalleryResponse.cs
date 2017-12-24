using System;

namespace WalledCityLahore.Models
{
    public class GalleryResponse
    {
		public string status { get; set; }
		public string message { get; set; }
		public GalleryItem[] data { get; set; }
    }
}
