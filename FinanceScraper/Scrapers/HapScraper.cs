using FinanceScraper.Models;
using HtmlAgilityPack;
using Scraper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FinanceScraper.Scrapers
{
    public class HapScraper
    {
        public Stock GetSingleStock(string symbol)
        {
            Stock myStock = new Stock();
            bool useYahoo = true;
            string yahooUrl = "https://finance.yahoo.com/" + "quote/" + symbol + "?p=" + symbol;
            string googleUrl = "https://www.google.com/search?q=" + symbol;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            string symbolString="";
            string priceString = "";
            string changeString = "";
            if (useYahoo)
            {
                htmlDoc = web.Load(yahooUrl);
                symbolString = "//h1[@class='D(ib) Fz(18px)']";
                priceString="//span[@class='Trsdu(0.3s) Fw(b) Fz(36px) Mb(-4px) D(ib)']";               
            }
            else
            {
                htmlDoc = web.Load(googleUrl);
                symbolString = "//h1[@class='D(ib) Fz(18px)']";
                priceString = "//span[@class='Trsdu(0.3s) Fw(b) Fz(36px) Mb(-4px) D(ib)']";
            }
            HtmlNode symbolNode = htmlDoc.DocumentNode.SelectSingleNode(symbolString);
            HtmlNode priceNode = htmlDoc.DocumentNode.SelectSingleNode(priceString);
            HtmlNode changeNode = priceNode.NextSibling;

            if (symbolNode == null)
               return null;
            else
            {

                var fullName = string.Concat(symbolNode.InnerHtml);
                var change = string.Concat(changeNode.InnerHtml);

                myStock.Name = extractParens(fullName);
                myStock.Symbol = symbol;
                myStock.Price = string.Concat(priceNode.InnerHtml);
                myStock.PercentChange = change.Remove(0, change.IndexOf("(") + 1).TrimEnd(')');
                myStock.Change = extractParens(change);

                //myStock.PrintInfo();
            }
            return myStock;
        }
        public static string extractParens(string fullString)
        {
            fullString = fullString.Remove(fullString.IndexOf("(") - 1, fullString.Length - fullString.IndexOf("(") + 1);
            return fullString;
        }
    }
   
}