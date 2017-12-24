using System;
using Xamarin.Forms;

namespace WalledCityLahore.Models
{
    public class EventItem
    {
        public string id { set; get; }
        public string name { set; get; }
        public string address { set; get; }
        public string longitude { set; get; }
        public string latitude { set; get; }
        public string description { set; get; }
        public string contact_numb { set; get; }
        public string contact_numb2 { set; get; }
        public string website { set; get; }
        public string image { set; get; }
        public string entry_datetime { set; get; }

        public string start_time { get; set; }
        public Color background { set; get; }

        public string event_datetime { get; set; }
        public string event_enddate { get; set; }
        public string event_stime { get; set; }
        public string event_etime { get; set; }
        public string email { get; set; }
    }
}
