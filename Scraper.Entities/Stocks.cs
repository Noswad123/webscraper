using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Scraper.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Symbol { get; set; }
        public string Change { get; set; }
        public string PercentChange { get; set; }
    }
}
