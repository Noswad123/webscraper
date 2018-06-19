using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanceScraper.Scrapers;
using FinanceScraper.DataContext;
using Scraper.Entities;

namespace FinanceScraper.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {

        private StocksDb db = new StocksDb();
        
        // GET: Stocks
        public ActionResult Index(bool isLoading=false)
        {
           
            ViewBag.isLoading = isLoading;
            return View(db.Stocks.ToList());
        }
    
        public void HapUpdate()
        {
            HapScraper yahooScraper = new HapScraper();
            Stock tempStock = new Stock();
            foreach(var stock in db.Stocks)
            {
                tempStock= yahooScraper.GetSingleStock(stock.Symbol);
                stock.Price = tempStock.Price;
                stock.Change = tempStock.Change;
                stock.PercentChange = tempStock.PercentChange;
            }
            db.SaveChanges();
        }
        public RedirectResult SeleniumScrape()
        {             
            SeleniumScraper yahoo = new SeleniumScraper();
            List<Stock> stockList = new List<Stock>();
            yahoo.Login(stockList);
            
            foreach (var stock in db.Stocks)
            {
                for (var i = 1; i < stockList.Count; i++)
                {
                    if (stock.Symbol.ToUpper() == stockList[i].Symbol)
                    {
                        stock.Price = stockList[i].Price;
                        stock.Change = stockList[i].Change;
                        stock.PercentChange = stockList[i].PercentChange;
                    }
                }
            }
            db.SaveChanges();
            return Redirect("Index");
        }
        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }
      
        public RedirectResult AddStock(string symbol)
        {
            HapScraper yahooScraper = new HapScraper();
            Stock tempStock = new Stock();
            tempStock = yahooScraper.GetSingleStock(symbol);
            if (tempStock == null)
                return Redirect("Index");
            db.Stocks.Add(tempStock);
            db.SaveChanges();
            return Redirect("Index");
        }
        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,price,change,percentchange,symbol")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,price,symbol")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
