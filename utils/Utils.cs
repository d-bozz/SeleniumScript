using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace plataforma_automatizada.utils
{
    /// <summary>
    /// Contiene metodos para auxiliar a las pruebas.
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Configura y retorna un IWebDriver.
        /// </summary>
        /// <returns>Driver configurado.</returns>
        public static IWebDriver DriverConfiguration()
        {
            string browser = ParameterReader.GetEnvironment("Browser").ToUpper();
            ChromeOptions options1 = new ChromeOptions();
            FirefoxOptions options2 = new FirefoxOptions();
            EdgeOptions options3 = new EdgeOptions();
            options1.AddArguments("start-maximized");
            options2.AddArguments("start-maximized");
            options3.AddArguments("start-maximized");
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
            new DriverManager().SetUpDriver(new FirefoxConfig());

            IWebDriver driverRetorno;

            switch (browser)
            {
                case "CHROME":
                     driverRetorno = new ChromeDriver(options1);
                     break;
                
                case "FIREFOX":
                    driverRetorno = new FirefoxDriver(options2);
                    break;
                
                case "EDGE":
                    driverRetorno = new EdgeDriver(options3);
                    break;

                default:
                    driverRetorno = new ChromeDriver(options1);
                    break;
            }
            driverRetorno.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driverRetorno;
        }

        /// <summary>
        /// Toma una captura de pantalla.
        /// </summary>
        /// <param name="driver">Driver previamente configurado.</param>
        /// <returns>Captura codificada en Base64.</returns>
        public static string GetScreenshot(IWebDriver driver)
        {
            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }
    }
}