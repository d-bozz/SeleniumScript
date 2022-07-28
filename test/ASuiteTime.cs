using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;

namespace plataforma_automatizada.test
{
    class ASuiteTime : TestBase
    {
        [Test]
        [Description("Agregar hora en proyecto 'TATF-202205'")]
        public void AgregarHoraProyecto()
        {
            timePage = new TimePage(driver);

            //Declaracion de variables
            string proyecto = ParameterReader.GetTestValues("Tiempo", "proyecto");
            string inicio = ParameterReader.GetTestValues("Tiempo", "inicio");
            string fin = ParameterReader.GetTestValues("Tiempo", "fin");
            string duracion = ParameterReader.GetTestValues("Tiempo", "duracion");
            string nota = ParameterReader.GetTestValues("Tiempo", "nota");
            string week_total = ParameterReader.GetTestValues("Tiempo", "week_total");
            string day_total = ParameterReader.GetTestValues("Tiempo", "day_total");

            //Creamos instancia de test para agregar al reporte HTML
            extentManager.CreateTest("AgregarHoraProyecto", "Agregar hora en proyecto 'TATF-202205'");

            //Agregamos log al reporte HTML
            extentManager.AddLog(StatusTest.INFO, "Ingresamos a: " + ParameterReader.GetEnvironment("UrlEnvironment"));

            //Pasos del script
            timePage.CompletarFormulario(proyecto, inicio, fin, duracion, nota);
            extentManager.AddLog(StatusTest.INFO, $"Completar el formulario de horas");
            Assert.That(timePage.ValidarDuration(), Is.False);
            extentManager.AddLog(StatusTest.INFO, $"Validar que el campo duracion este deshabilitado");
            timePage.ClickEnviar();
            extentManager.AddLog(StatusTest.INFO, $"Click en enviar");

            //Valdidaciones
            Assert.That(timePage.ValidarTiempo(proyecto, inicio, fin, duracion, nota), Is.True);
            Assert.That(timePage.ValidarWeekTotal, Is.EqualTo("Week total: " + week_total + ""));
            Assert.That(timePage.ValidarDayTotal, Is.EqualTo("Day total: " + day_total + ""));
        }

    }
}