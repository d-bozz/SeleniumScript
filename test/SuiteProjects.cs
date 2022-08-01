using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;

namespace plataforma_automatizada.test
{
    [Order(3)]
    class SuiteProjects : TestBase
    {
        [Test]
        [Description("Verificar proyectos  'IATF-202205'")]
        public void VerificarProyectos()
        {
            projectPage = new ProjectPage(driver);

            //Declaracion de variables
            string nombre1 = ParameterReader.GetTestValues("Proyecto", "nombre1");
            string description1 = ParameterReader.GetTestValues("Proyecto", "description1");
            string nombre2 = ParameterReader.GetTestValues("Proyecto", "nombre2");
            string description2 = ParameterReader.GetTestValues("Proyecto", "description2");

            //Creamos instancia de test para agregar al reporte HTML
            extentManager.CreateTest("VerificarProyectos", "Verificar proyectos  'IATF-202205'");

            //Agregamos log al reporte HTML
            extentManager.AddLog(StatusTest.INFO, "Ingresamos a: " + ParameterReader.GetEnvironment("UrlEnvironment"));

            //Pasos del script
            Assert.That(projectPage.ValidarTitulo, Is.EqualTo("Proyectos"));
            Assert.That(projectPage.ValidarProyectos(nombre1, description1, nombre2, description2), Is.True);
        }
    }
}