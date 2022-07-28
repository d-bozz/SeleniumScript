using NUnit.Framework;
using OpenQA.Selenium;
using plataforma_automatizada.pages;
using plataforma_automatizada.utils;
using System;
using System.Collections.Generic;

namespace plataforma_automatizada.test
{
    class CSuiteUsers : TestBase
    {
        [Test]
        [Description("Validar usuario en lista de Usuarios")]
        public void VerificarUsuarios()
        {
            userPage = new UserPage(driver);

            //Declaracion de variables
            string nombre = ParameterReader.GetTestValues("Usuario", "nombre");
            string login = ParameterReader.GetTestValues("Usuario", "login");
            string rol = ParameterReader.GetTestValues("Usuario", "rol");

            //Creamos instancia de test para agregar al reporte HTML
            extentManager.CreateTest("VerificarUsuarios", "Validar usuario en lista de Usuarios");

            //Agregamos log al reporte HTML
            extentManager.AddLog(StatusTest.INFO, "Ingresamos a: " + ParameterReader.GetEnvironment("UrlEnvironment"));

            //Validaciones
            Assert.That(userPage.ValidarTitulo, Is.EqualTo("Personas"));
            Assert.That(userPage.ValidarUsuario(nombre, login, rol), Is.True);
        }
    }
}