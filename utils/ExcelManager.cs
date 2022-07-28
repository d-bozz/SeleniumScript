using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace plataforma_automatizada.utils
{
    class ExcelManager
    {
        private readonly SLDocument sLDocument;
        private readonly DataTable dataTable;
        private readonly string pathSave;

        /// <summary>
        /// Crea instancia para un nuevo documento excel.
        /// </summary>
        /// <returns>SLDocument.</returns>
        public ExcelManager()
        {
            sLDocument = new SLDocument();
            dataTable = new DataTable();
            string carpetaProyecto = typeof(ExcelManager).Namespace.Replace(".utils", string.Empty);
            pathSave = ParameterReader.GetEnvironment("PathCreatedExcel") == "" ?
                Directory.GetCurrentDirectory().Replace(@"bin\Debug\netcoreapp3.1", @"reportExcel\")
                :
                ParameterReader.GetEnvironment("PathCreatedExcel") + carpetaProyecto;

            Directory.CreateDirectory(pathSave);
            CreateHeader(ParameterReader.GetEnvironment("CreateHeader").Split(","));
        }

        /// <summary>
        /// Guarda archivo excel.
        /// <para>Se debe indicar path y nombre para el archivo.</para>
        /// </summary>
        public void SaveAs()
        {
            string fechaHora = DateTime.Now.ToString("dd-MM-yyyy HH_mm_ss");
            sLDocument.ImportDataTable(1, 1, dataTable, true);
            sLDocument.SaveAs(pathSave + @"\" + fechaHora + " " + ParameterReader.GetEnvironment("CreatedExcelName"));
        }

        /// <summary>
        /// Guarda archivo excel.
        /// <para>Se debe indicar path y nombre para el archivo.</para>
        /// </summary>
        /// <param name="nameFile">Nombre de archivo excel. El nombre debe contener .xlsx</param>
        public void SaveAs(string nameFile)
        {
            sLDocument.ImportDataTable(1, 1, dataTable, true);
            sLDocument.SaveAs(pathSave + @"\" + nameFile);
        }

        /// <summary>
        /// Crea el cabezal que representara los datos a guardar.
        /// </summary>
        /// <param name="header">El o los  nombres de los cabezales.</param>
        private void CreateHeader(string[] header)
        {
            foreach (var item in header)
                dataTable.Columns.Add(item);
        }

        public void AddData(string[] data)
        {
            dataTable.Rows.Add(data);
        }

        public static List<string[]> GetExcelValues()
        {
            SLDocument sLDocument = new SLDocument(ParameterReader.GetEnvironment("PathExcel"));
            int row = 2;
            string column1;
            string column2;
            string column3;
            string column4;
            List<string[]> lista = new List<string[]>();
            while (!string.IsNullOrEmpty(sLDocument.GetCellValueAsString(row, 1)))
            {
                column1 = sLDocument.GetCellValueAsString(row, 1);
                column2 = sLDocument.GetCellValueAsString(row, 2);
                column3 = sLDocument.GetCellValueAsString(row, 3);
                column4 = sLDocument.GetCellValueAsString(row, 4);
                lista.Add(new string[] { column1, column2, column3, column4 });
                row++;
            }
            return lista;
        }
    }
}
