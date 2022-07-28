using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;

namespace plataforma_automatizada.utils
{
    /// <summary>
    /// Clase encargada de manipular el framework ExtentReport.
    /// </summary>
    class ExtentManager
    {

        private readonly ExtentReports extentReports;
        private ExtentTest extentTest;

        public ExtentManager()
        {
            string path = ParameterReader.GetEnvironment("PathReportHtml") == "" ?
                Directory.GetCurrentDirectory().Replace(@"bin\Debug\netcoreapp3.1", @"reportHTML")
                :
                ParameterReader.GetEnvironment("PathReportHtml");
            string carpetaProyecto = typeof(ExtentManager).Namespace.Replace(".utils", string.Empty);
            string fechaHora = "reporte " + DateTime.Now.ToString("dd-MM-yyyy HH_mm_ss");
            string pathAndFileName = ParameterReader.GetEnvironment("PathReportHtml") == "" ?
                @path + @"\" + fechaHora + @"\"
                :
                @path + carpetaProyecto + @"\" + fechaHora + @"\"; ;

            var htmlReporter = new ExtentHtmlReporter(pathAndFileName);
            htmlReporter.Config.Theme = (Theme)int.Parse(ParameterReader.GetEnvironment("Theme"));
            htmlReporter.Config.DocumentTitle = ParameterReader.GetEnvironment("DocumentTitle");
            htmlReporter.Config.ReportName = ParameterReader.GetEnvironment("ReportName");
            htmlReporter.Config.Encoding = ParameterReader.GetEnvironment("Encoding");
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// Crea un nodo test para agregar al reporte HTML.
        /// </summary>
        /// <param name="name">Nombre del test</param>
        /// <param name="description">Descripcion del test</param>
        public void CreateTest(string name, string description)
        {
            extentTest = extentReports.CreateTest(name, description);
        }

        /// <summary>
        /// Agrega un log al test con un determinado estado.
        /// </summary>
        /// <param name="detail">Mensaje para registrar en log</param>
        /// <param name="status">Estado para asignar al log</param>
        public void AddLog(StatusTest status, string detail)
        {
            extentTest.Log((Status)status, detail);
        }

        /// <summary>
        /// Inserta un log de tipo info en nuestro reporte con formato lista de html.
        /// </summary>
        /// <param name="extentTest">ExtentTest previamente configurado/inicializado.</param>
        /// <param name="message">Mensaje personalizado</param>
        public void AddLog(StatusTest status, string[] message)
        {
            string li = null;

            for (int i = 1; i < message.Length; i++)
                li += "<li>" + message[i] + "</li>";

            string ul = "<ul>" + li + "</ul>";

            extentTest.Log((Status)status, message[0] + ul);
        }

        /// <summary>
        /// Agrega un estado al test.
        /// </summary>
        /// <param name="detail">Mensaje para el registrar en el estado</param>
        /// <param name="status">Estado para asignar</param>
        public void AddEstate(StatusTest status, string detail)
        {
            extentTest.Log((Status)status, detail);
        }

        /// <summary>
        /// Agrega una captura de pantalla al test con un determinado estado.
        /// </summary>
        /// <param name="capture">Captura de pantalla en BASE64</param>
        /// <param name="status">Estado para asignar a la captura</param>
        public void AddScreenshot(StatusTest status, string capture)
        {
            extentTest.Log((Status)status, MediaEntityBuilder.CreateScreenCaptureFromBase64String(capture).Build());
        }

        /// <summary>
        /// Registra un estado en conjunto con una captura de pantalla. 
        /// <para>Los estados evaluados son:</para>
        /// <list type="bullet">
        ///     <item>
        ///         <description>Failed</description>
        ///     </item>
        ///     <item>
        ///         <description>Skipped</description>
        ///     </item>
        ///     <item>
        ///         <description>Passed</description>
        ///     </item>
        ///     <item>
        ///         <description>Inconclusive</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="driver">Driver previamente configurado.</param>
        /// <param name="extentTest">Intancia previamente inicializada.</param>
        public void AddTestResult(IWebDriver driver)
        {
            TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            string testMessage = TestContext.CurrentContext.Result.Message;
            string capturapantalla = Utils.GetScreenshot(driver);

            switch (testStatus)
            {
                case TestStatus.Failed:
                    extentTest
                        .Fail(testMessage)
                        .Fail("Screenshot of the fail", MediaEntityBuilder.CreateScreenCaptureFromBase64String(capturapantalla).Build())
                        .Fail("Test incomplete");
                    break;
                case TestStatus.Skipped:
                    extentTest
                         .Skip(testMessage)
                         .Skip("Screenshot of the fail", MediaEntityBuilder.CreateScreenCaptureFromBase64String(capturapantalla).Build())
                         .Skip("Test incomplete");
                    break;
                case TestStatus.Passed:
                    extentTest.Pass("Test complete", MediaEntityBuilder.CreateScreenCaptureFromBase64String(capturapantalla).Build());
                    break;
                case TestStatus.Inconclusive:
                    extentTest.Warning("Unexpected error")
                        .Warning("screenshot of the unexpected error ", MediaEntityBuilder.CreateScreenCaptureFromBase64String(capturapantalla).Build())
                        .Warning("Test incomplete");
                    break;
                default:
                    extentTest.Warning("Unexpected error");
                    break;
            }
        }

        /// <summary>
        /// Registra el nodo test configurado al reporte HTML.
        /// </summary>
        public void EndTest()
        {
            extentReports.Flush();
        }
    }
}
