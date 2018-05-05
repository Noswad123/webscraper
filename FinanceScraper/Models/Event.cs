using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceScraper.Models
{
    public class Event
    {
        public string Artist { get; set; }
        public string City { get; set; }
        public string VenueName { get; set; }
        public string Date { get; set; }
        public string Price { get; set; }
    }
}