using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class UserPage : BasePage
    {
        private string page_title = ParameterReader.GetScreenComponents("User", "page_title");

        public UserPage(IWebDriver driver) : base(driver)
        {
            SelectLink("Personas");
            if (Title().Equals(page_title) == false)
            {
                throw new Exception("Esta no es la pagina de Personas");
            }
        }

        public string ValidarTitulo()
        {
            return SearchByClass("page-title");
        }

        public bool ValidarUsuario(string nombre, string login, string rol)
        {
            return ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + nombre + "')]") &&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + login + "')]") &&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + rol + "')]");
        }
    }
}
