using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;

namespace plataforma_automatizada.test
{
    class TestBase
    {
        protected ExtentManager extentManager;
        protected ExcelManager excelManager;
        protected static IWebDriver driver;
        protected LoginPage loginPage;
        protected TimePage timePage;
        protected ReportPage reportPage;
        protected ProjectPage projectPage;
        protected UserPage userPage;
        protected DeleteTimePage deleteTimePage;
        protected string login = ParameterReader.GetTestValues("Login", "login");
        protected string password = ParameterReader.GetTestValues("Login", "password");

        [OneTimeSetUp]
        public void AntesDeTodosLosTest()
        {
            extentManager = new ExtentManager();
            excelManager = new ExcelManager();
        }

        [SetUp]
        public void AntesDeCadaTest()
        {
            driver = Utils.DriverConfiguration();
            loginPage = new LoginPage(driver);
            loginPage.IngresarUsuarioPass(login,password);
            loginPage.ClickIniciar();
        }

        [TearDown]
        public void DespuesDeCadaTest()
        {
            extentManager.AddTestResult(driver);
            extentManager.EndTest();
            loginPage.CerrarNavegador();
        }

        [OneTimeTearDown]
        public void DespuesDeTodosLosTest()
        {
            excelManager.SaveAs();
        }
    }
}
