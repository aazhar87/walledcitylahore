using System;
using Xamarin.Forms;

namespace WalledCityLahore.Models
{
    public class BookingItem
    {
        public string id { get; set; }
        public string route { get; set; }
        public string shrt_desc { get; set; }
        public string booking_type_id { get; set; }
        public string dates { get; set; }
        public string[] times { get; set; }
        public string image { get; set; }
        public string main_sites { get; set; }
        public string eating_points { get; set; }
        public string cost_per_person { get; set; }
        public string average_duration_of_tour { get; set; }
        public string booking_time { get; set; }
        public string admin_id { get; set; }

        public Color background { set; get; }

        public void Randomise()
        {
            Random r = new Random();
            int rInt = r.Next(0, MultiColors.Length);
            this.background = MultiColors[rInt];
        }

        private static Color[] MultiColors = {Color.FromHex("#A7414A"),Color.FromHex("#282726"),
                                       Color.FromHex("#6A8A82"),Color.FromHex("#A37C27"),
                                       Color.FromHex("#563838"),Color.FromHex("#595775"),Color.FromHex("#192E5B") };
    }
}