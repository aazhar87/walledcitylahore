using System;

namespace WalledCityLahore.Models
{
    public class EventResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public EventItem[] data { get; set; }
    }
}