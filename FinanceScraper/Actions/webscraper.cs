using FinanceScraper.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FinanceScraper.Actions
{
    public class WebScraper
    {
        public void GetHtml()
        {
            HtmlWeb web = new HtmlWeb();
            var eventlist = new List<Event>();

            HtmlDocument document = web.Load("http://www.wegottickets.com/searchresults/all");

            HtmlNode[] links =
                document.DocumentNode.SelectNodes("//a")
                    .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("event_link")).ToArray();


            var eventsList = new List<HtmlDocument>();
            foreach (HtmlNode item in links)
            {
                var link = item.Attributes["href"].Value;
                var events = web.Load(link);
                eventsList.Add(events);
            }

            var venuedetails = new List<string>();
            foreach (var eventss in eventsList)
            {
                var deetrs = eventss.DocumentNode.SelectSingleNode("//div[@class='event-information']");
                var asdf = deetrs.Descendants("h1").SingleOrDefault().InnerHtml;
                var evendd = new Event
                {
                    Artist = asdf


                };
                eventlist.Add(evendd);
                venuedetails.Add(deetrs.InnerHtml);
            }

        }

    }
}