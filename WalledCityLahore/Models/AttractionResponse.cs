using System;

namespace WalledCityLahore.Models
{
    public class AttractionResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public AttractionItem[] data { get; set; }
    }
}