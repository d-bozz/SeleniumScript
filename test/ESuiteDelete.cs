using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;

namespace plataforma_automatizada.test
{
    class ESuiteDelete : TestBase
    {
        [Test]
        [Description("Eliminar hora de proyecto 'TATF-202205'")]
        public void EliminarHoraProyecto()
        {
            timePage = new TimePage(driver);

            //Declaracion de variables
            string proyecto = ParameterReader.GetTestValues("DeleteHoras", "proyecto");
            string week_total = ParameterReader.GetTestValues("DeleteHoras", "week_total");
            string day_total = ParameterReader.GetTestValues("DeleteHoras", "day_total");

            //Creamos instancia de test para agregar al reporte HTML
            extentManager.CreateTest("EliminarHoraProyecto", "Eliminar hora de proyecto 'TATF-202205'");

            //Agregamos log al reporte HTML
            extentManager.AddLog(StatusTest.INFO, "Ingresamos a: " + ParameterReader.GetEnvironment("UrlEnvironment"));

            Assert.That(timePage.ValidarHoras(proyecto), Is.True);
            timePage.ClickEliminar(proyecto);
            Assert.That(timePage.ValidarTitulo, Is.EqualTo("Eliminando el historial de tiempo"));
            deleteTimePage = new DeleteTimePage(driver);

            deleteTimePage.ConfirmarEliminacion();
            extentManager.AddLog(StatusTest.INFO, $"Click en confirmar eliminacion");

            //Validaciones
            Assert.That(timePage.ValidarWeekTotal, Is.EqualTo("Week total: " + week_total + ""));
            Assert.That(timePage.ValidarDayTotal, Is.EqualTo("Day total: " + day_total + ""));
        }
    }
}