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
        public float price { get; set; }
        public string symbol { get; set; }
    }
}
