using System.Xml;

namespace plataforma_automatizada.utils
{
    /// <summary>
    /// Contiene una ruta configurada. 
    /// Dicha ruta es utilizada por metodos encargados de retornar valores parametrizados en archivos externos a la plataforma.
    /// </summary>
    class ParameterReader
    {
        private static readonly string path = @"..\..\..\resources\parameter_files\";

        /// <summary>
        /// Nos permite obtener valores de un documento externo. 
        /// </summary>
        /// <remarks>
        /// Los valores a obtener son todos los identificadores/hooks de los elementos web.
        /// </remarks>
        /// <param name="fathernode">Nodo que engloba varios valores.</param>
        /// <param name="node">Nodo clave del que queremos obtener el valor.</param>
        /// <returns>Valor del nodo.</returns>
        public static string GetTestValues(string fathernode, string node)
        {
            XmlDocument testValues = new XmlDocument();
            testValues.Load(path + "TestValues.xml");
            return ((XmlElement)testValues.GetElementsByTagName(fathernode)[0]).GetElementsByTagName(node)[0].InnerText;
        }

        /// <summary>
        /// Nos permite obtener valores de un documento externo. 
        /// </summary>
        /// <remarks>
        /// Los valores a obtener son todos los necesarios para los test.
        /// </remarks>
        /// <param name="fathernode">Nodo que engloba varios valores.</param>
        /// <param name="node">Nodo clave del que queremos obtener el valor.</param>
        /// <returns>Valor del nodo.</returns>
        public static string GetScreenComponents(string fathernode, string node)
        {
            XmlDocument screenComponents = new XmlDocument();
            screenComponents.Load(path + "ScreenComponents.xml");
            return ((XmlElement)screenComponents.GetElementsByTagName(fathernode)[0]).GetElementsByTagName(node)[0].InnerText;
        }

        /// <summary>
        /// Nos permite obtener valores de un documento externo. 
        /// </summary>
        /// <remarks>
        /// Los valores a obtener son todos los necesarios para la configuracion del ambiente.
        /// </remarks>
        /// <param name="node">Nodo clave del que queremos obtener el valor.</param>
        /// <returns>Valor del nodo.</returns>
        public static string GetEnvironment(string node)
        {
            XmlDocument screenComponents = new XmlDocument();
            screenComponents.Load(path + "Environment.xml");
            return ((XmlElement)screenComponents.GetElementsByTagName("Environment")[0]).GetElementsByTagName(node)[0].InnerText;
        }

        /// <summary>
        /// Nos permite obtener los valores de las credenciales almacenadas en un documento XML externo. 
        /// </summary>
        /// <param name="node">Nodo clave del que queremos obtener el valor.</param>
        /// <returns>Valor del nodo.</returns>
        public static string GetCredenciales(string node)
        {
            XmlDocument testValues = new XmlDocument();
            testValues.Load(@"C:\Credenciales\UsuariosSigb.xml");
            return ((XmlElement)testValues.GetElementsByTagName("UsuariosSigb")[0]).GetElementsByTagName(node)[0].InnerText;
        }
    }
}
