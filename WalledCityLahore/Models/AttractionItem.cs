using System;
using Xamarin.Forms;

namespace WalledCityLahore.Models
{
    public class AttractionItem
    {
        public string id { set; get; }
        public string name { set; get; }
        public string address { set; get; }
        public string near_by { set; get; }
        public string longitude { set; get; }
        public string latitude { set; get; }
        public string description { set; get; }
        public string contact_numb { set; get; }
        public string contact_numb2 { set; get; }
        public string website { set; get; }
        public string email { get; set; }
        public string image { set; get; }
        public string entry_datetime { set; get; }

		public string time { get; set; }
		public Color background { set; get; }
    }
}
