using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaTestConsole
{
    public static class htmlparser
    {
        private const string Dota2ProTrackerUrl = "https://dota2protracker.com/meta";

        public static List<string> FetchMetaHeroesAfterButtonClick()
        {
            var metaHeroes = new List<string>();

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            using var driver = new ChromeDriver(options);

            try
            {
                driver.Navigate().GoToUrl(Dota2ProTrackerUrl);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                var button_pos1 = driver.FindElement(By.XPath("//button[.//img[contains(@src, 'pos_1.png')]]"));
                button_pos1.Click();

                System.Threading.Thread.Sleep(2000);

                var heroElements = driver.FindElements(By.XPath("//a[contains(@href, '/hero/')]/span/text()"));
                foreach (var element in heroElements)
                {
                    var heroName = element.Text.Trim();
                    metaHeroes.Add(heroName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during parsing: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
            foreach (var hero in metaHeroes) {
                Console.WriteLine(hero);
            }
            
            return metaHeroes;
        }
    }
}
