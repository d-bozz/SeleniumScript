using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class TimePage : BasePage
    {

        private readonly By user;
        private readonly By project;
        private readonly By start;
        private readonly By finish;
        private readonly By duration;
        private readonly By note;
        private readonly By btn_submit;
        private string page_title = ParameterReader.GetScreenComponents("Time", "page_title");

        public TimePage(IWebDriver driver) : base(driver)
        {
            SelectLink("Tiempo");
            if (Title().Equals(page_title) == false)
            {
                throw new Exception("Esta no es la pagina de Tiempo");
            }
            else
            { 
                user = By.Id(ParameterReader.GetScreenComponents("Time", "user"));
                project = By.Id(ParameterReader.GetScreenComponents("Time", "project"));
                start = By.Id(ParameterReader.GetScreenComponents("Time", "start"));
                finish = By.Id(ParameterReader.GetScreenComponents("Time", "finish"));
                duration = By.Id(ParameterReader.GetScreenComponents("Time", "duration"));
                note = By.Id(ParameterReader.GetScreenComponents("Time", "note"));
                btn_submit = By.Id(ParameterReader.GetScreenComponents("Time", "btn_submit"));
            }
        }

        public void CompletarFormulario(string proyecto, string inicio, string fin, string duracion, string nota)
        {
            SelectByText(project, proyecto);
            SendKeys(start, inicio);
            SendKeys(finish, fin);
            SendKeys(note, nota);
        }

        public bool ValidarDuration()
        {
            return IsEnabled(duration);
        }

        public void ClickEnviar()
        {
            Click(btn_submit);
        }

        public void ClickEliminar(string proyecto)
        {
            ClickByXpath("//td[@class='text-cell'][contains(text(),'" + proyecto + "')]//..//img[@alt='Eliminar']");
        }

        public bool ValidarTiempo(string proyecto, string inicio, string fin, string duracion, string nota)
        {
            return ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + proyecto + "')]") &&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + inicio + "')]") &&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + fin + "')]") &&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + duracion + "')]") &&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + nota + "')]"); 
        }

        public string ValidarWeekTotal()
        {
            return GetElement("//td[@class='day-totals-col1']");
        }

        public string ValidarDayTotal()
        {
            return GetElement("//td[@class='day-totals-col2']");
        }

        public bool ValidarHoras(string proyecto)
        {
            return ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + proyecto + "')]");
        }

        public string ValidarTitulo()
        {
            return SearchByClass("page-title");
        }
    }
}