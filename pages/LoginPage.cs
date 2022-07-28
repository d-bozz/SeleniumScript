using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class LoginPage : BasePage
    {
        private readonly By login;
        private readonly By password;
        private readonly By btn_login;
        private readonly By resultado;
        private string page_title = ParameterReader.GetScreenComponents("Login", "page_title");


        public LoginPage(IWebDriver driver) : base(driver)
        {
            driver.Navigate().GoToUrl(ParameterReader.GetEnvironment("UrlEnvironment"));
            if (Title().Equals(page_title) == false)
            {
                throw new Exception("Esta no es la pagina de Login");
            }

            else
            { 
                login = By.Id(ParameterReader.GetScreenComponents("Login", "login"));
                password = By.Id(ParameterReader.GetScreenComponents("Login", "password"));
                btn_login = By.Id(ParameterReader.GetScreenComponents("Login", "btn_login"));
            }
        }

        public void IngresarUsuarioPass(string usuario, string pass)
        {
            SendKeys(login, usuario);
            SendKeys(password, pass);
        }

        public void ClickIniciar()
        {
            Click(btn_login);
        }

        public string OtenerTituloPagina()
        {
            return Title();
        }

        public string OtenerUrlPagina()
        {
            return Url();
        }

        public void CerrarNavegador()
        {
            Quit();
        }
    }
}
