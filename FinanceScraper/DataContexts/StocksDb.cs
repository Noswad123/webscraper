using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Scraper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FinanceScraper.DataContext
{
    public class StocksDb : DbContext
    {
        public StocksDb()
            :base("DefaultConnection")
        {

        }
            public DbSet<Stock> Stocks { get; set; }
    }
}