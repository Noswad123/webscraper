using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FinanceScraper.Models;
using Scraper.Entities;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using Microsoft.AspNet.Identity;

namespace FinanceScraper.Scrapers
{
    public class SeleniumScraper
    {
        public void Login(List<Stock> stockList)
        {
            
            string myPassword = "lauren123";
            IWebDriver chromeDriver = new ChromeDriver();
            //This is because 'get' is a keyword in C#

            WebDriverWait waitForElement = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));

            chromeDriver.Navigate().GoToUrl("http://login.yahoo.com/");
            // Find and enter the username
            IWebElement usernameQuery = chromeDriver.FindElement(By.Name("username"));
            usernameQuery.SendKeys("jamal.a.dawson@intracitygeeks.org");
            usernameQuery.Submit();

            //wait for password page to load
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("password")));

            //find and enter password
            IWebElement passwordQuery = chromeDriver.FindElement(By.Name("password"));
            IWebElement passwordButton = chromeDriver.FindElement(By.Name("verifyPassword"));
            passwordQuery.SendKeys(myPassword);
            passwordButton.Click();

            chromeDriver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@title='Close']")));

            IWebElement element = chromeDriver.FindElement(By.XPath("//button[@title='Close']"));
            element.Click();

            List<IWebElement> elements = new List<IWebElement>();
            elements = chromeDriver.FindElements(By.XPath("//tbody/tr")).ToList<IWebElement>();
      
            this.PrepStockData(elements, stockList);

            chromeDriver.Quit();
            
        }

        public void ConvertString(string stockInfo, List<Stock> stockArray)
        {
            Stock tempStock = new Stock();

            String[] substrings;
            substrings = stockInfo.Split(' ');

            if (substrings[0] != null && substrings[1] != null && substrings[2] != null && substrings[3] != null)
            {
                Console.WriteLine(substrings[0]);
                Console.WriteLine(substrings[1]);
                Console.WriteLine(substrings[2]);
                Console.WriteLine(substrings[3]);

                tempStock.Symbol = substrings[0];
                tempStock.Price = substrings[1];
                tempStock.Change = substrings[2];
                tempStock.PercentChange = substrings[3];
              
                stockArray.Add(tempStock);
            }
        }
        public void PrepStockData(List<IWebElement> stockList, List<Stock> stockArray)
        {
            foreach (var stock in stockList)
            {
                ConvertString(stock.Text, stockArray);
            }
        }

    }
}