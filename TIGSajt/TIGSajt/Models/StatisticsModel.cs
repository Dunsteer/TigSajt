using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIGSajt.DB;

namespace TIGSajt.Models
{
    public class StatisticsModel
    {
        public string Token { get; set; }
        public string Home { get; set; }
        public string Guest { get; set; }
        public int? HomePoints { get; set; }
        public int? GuestPoints { get; set; }
        public int? HomeScore { get; set; }
        public int? GuestScore { get; set; }
        public eDbEntryType Type { get; set; }

        internal bool HasValue()
        {
            return Home != null 
                && Guest != null 
                && HomePoints != null 
                && GuestPoints != null 
                && HomeScore != null 
                && GuestScore != null 
                && Token !=null 
                && Token == "9dd444c6-91d5-4b26-922f-3823790eb832";
        }
    }
}
