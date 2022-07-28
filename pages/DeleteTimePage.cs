using OpenQA.Selenium;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace plataforma_automatizada.pages
{
    class DeleteTimePage : BasePage
    {
        private readonly By delete_button;

        public DeleteTimePage(IWebDriver driver) : base(driver)
        {
            delete_button = By.Id(ParameterReader.GetScreenComponents("Delete_time", "delete_button"));
        }

        public void ConfirmarEliminacion()
        {
            Click(delete_button);
        }

    }
}
