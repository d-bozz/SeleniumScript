using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;

namespace plataforma_automatizada.test
{
    [Order(2)]
    class SuiteReport : TestBase
    {
        [Test]
        [Description("Generar reporte para proyecto 'TATF - 202205'")]
        public void GenerarReporteProyecto()
        {
            reportPage = new ReportPage(driver);

            //Declaracion de variables
            string fecha = DateTime.Today.ToString("yyyy-MM-dd");
            string usuario = ParameterReader.GetTestValues("ReportResult", "user");
            string proyecto = ParameterReader.GetTestValues("ReportResult", "project");
            string inicio = ParameterReader.GetTestValues("ReportResult", "start");
            string fin = ParameterReader.GetTestValues("ReportResult", "finish");
            string duracion = ParameterReader.GetTestValues("ReportResult", "duration");
            string nota = ParameterReader.GetTestValues("ReportResult", "note");
            string total = ParameterReader.GetTestValues("ReportResult", "total");

            //Creamos instancia de test para agregar al reporte HTML
            extentManager.CreateTest("GenerarReporteProyecto", "Generar reporte para proyecto 'TATF - 202205'");

            //Agregamos log al reporte HTML
            extentManager.AddLog(StatusTest.INFO, "Ingresamos a: " + ParameterReader.GetEnvironment("UrlEnvironment"));

            //Pasos del script
            reportPage.SelecPeriodo(fecha, fecha);
            extentManager.AddLog(StatusTest.INFO, $"Ingresar el periodo de horas");
            reportPage.SelectProyecto(proyecto);
            extentManager.AddLog(StatusTest.INFO, $"Se seleciona el proyecto");
            reportPage.ClickGenerar();
            extentManager.AddLog(StatusTest.INFO, $"se hace click en generar reporte");

            //verificaciones
            Assert.That(reportPage.ValidarReporte(fecha, proyecto, inicio, fin, duracion, nota), Is.True);
            Assert.That(reportPage.ValidarTotal(), Is.EqualTo(total));
        }
    }
}