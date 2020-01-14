using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.localhost:44352/";

            //IWebDriver driver = new ChromeDriver();
            IWebDriver driver = new FirefoxDriver();

            driver.Url = url;

            driver.FindElement(By.Id("cookieKnap")).Click();

            TilføjProdukt(driver);

            FilterTest(driver);

            SletProdukt(driver);
        }


        static void FilterTest(IWebDriver driver)
        {

            driver.FindElement(By.Id("1063")).Click();

            //driver.FindElement(By.ClassName("fa-cart-arrow-down")).Click();

            driver.FindElement(By.Id("SearchString")).SendKeys("Mobil");

            IWebElement dropdownContinent = driver.FindElement(By.Id("FirmaNavn"));

            SelectElement selectContinent = new SelectElement(dropdownContinent);

            IList<IWebElement> continentList = selectContinent.Options;

            foreach (var continents in continentList)
            {
                if (continents.Text == "Samsung")
                {
                    selectContinent.SelectByText(continents.Text);
                }
            }

            driver.FindElement(By.Id("Filter")).Click();
        }

        static void TilføjProdukt(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://localhost:44352/Create");

            IWebElement firma = driver.FindElement(By.Id("Phone_CompanyID"));

            SelectElement selectFirma = new SelectElement(firma);

            IList<IWebElement> firmaList = selectFirma.Options;

            foreach (var firmaer in firmaList)
            {
                if (firmaer.Text == "Samsung")
                {
                    selectFirma.SelectByText(firmaer.Text);
                }
            }

            driver.FindElement(By.Name("Phone.PhoneName")).SendKeys("Test mobil fra Selenium 2");

            driver.FindElement(By.Name("Phone.Price")).SendKeys("10000");

            driver.FindElement(By.Id("createButton")).Click();
        }

        static void SletProdukt(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://localhost:44352/Admin?currentpage=2");

            Console.WriteLine("\nHvilket produkt skal slettets: ");

            string sletID = Console.ReadLine();

            driver.FindElement(By.Id(sletID)).Click();

            driver.FindElement(By.ClassName("btn-default")).Click();
        }
    }
}
