using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class ReportPage : BasePage
    {
        private readonly By reportTitle;
        private readonly By start_date;
        private readonly By end_date;
        private readonly By btn_generate;
        private readonly By project;
        private string page_title = ParameterReader.GetScreenComponents("Report", "page_title");

        public ReportPage(IWebDriver driver) : base(driver)
        {
            SelectLink("Reportes");
            if (Title().Equals(page_title) == false)
            {
                throw new Exception("Esta no es la pagina de Tiempo");
            }
            else
            {
                start_date = By.Id(ParameterReader.GetScreenComponents("Report", "start_date"));
                end_date = By.Id(ParameterReader.GetScreenComponents("Report", "end_date"));
                project = By.Id(ParameterReader.GetScreenComponents("Report", "project"));
                btn_generate = By.Id(ParameterReader.GetScreenComponents("Report", "btn_generate"));
            }
        }

        public void SelecPeriodo(string inicio, string fin)
        {
            Clear(start_date);
            SendKeys(start_date, inicio);
            Clear(end_date);
            SendKeys(end_date, fin);
        }

        public void SelectProyecto(string seleccion)
        {
            SelectByText(project, seleccion);
        }

        public void ClickGenerar()
        {
            Click(btn_generate);
        }

        public bool ValidarReporte(string fecha, string proyecto, string inicio, string fin, string duracion, string nota)
        {
            return ElementIsDisplayed("//td[@class='date-cell'][contains(text(),'" + fecha + "')]")&&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + proyecto + "')]")&&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + inicio + "')]")&&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + fin + "')]")&&
                   ElementIsDisplayed("//td[@class='time-cell'][contains(text(),'" + duracion + "')]")&&
                   ElementIsDisplayed("//td[@class='text-cell'][contains(text(),'" + nota + "')]");
        }

        public string ValidarTotal()
        {
            return GetElement("//td[@class='time-cell subtotal-cell']");
        }
    }
}