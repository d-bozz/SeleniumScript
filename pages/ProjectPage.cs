using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class ProjectPage : BasePage
    {
        private readonly By title;
        private string page_title = ParameterReader.GetScreenComponents("Project", "page_title");

        public ProjectPage(IWebDriver driver) : base(driver)
        {
            SelectLink("Proyectos");
            if (Title().Equals(page_title) == false)
            {
                throw new Exception("Esta no es la pagina de Proyectos");
            }
            else
            { 
                title = By.Id(ParameterReader.GetScreenComponents("Project", "title"));
            }
        }

        public bool ValidarProyectos(string nombre1, string descripcion1, string nombre2, string descripcion2)
        {
            return ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + nombre1 + "')]") &&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + descripcion1 + "')]")&&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + nombre1 + "')]") &&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + descripcion1 + "')]");
        }

        public string ValidarTitulo()
        {
            return SearchByClass("page-title");
        }
    }
}
