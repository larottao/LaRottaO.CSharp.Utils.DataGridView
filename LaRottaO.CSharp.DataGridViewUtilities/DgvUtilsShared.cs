using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    /// <summary>
    ///
    /// A collection of useful methods for Data Grid View on Windows Forms
    ///
    /// 2021 06 14
    ///
    /// by Felipe La Rotta
    ///
    /// </summary>
    ///

    public class DgvUtilsShared
    {
        public static List<String> getDataGridViewColumnNames(DataGridView dataGridView)
        {
            List<String> listaNombres = new List<String>();

            foreach (DataGridViewColumn columna in dataGridView.Columns)
            {
                listaNombres.Add(columna.HeaderText);
            }

            return listaNombres;
        }

        public static Tuple<Boolean, String> createColumnsFromObjectProperties(object exampleObject, DataTable dataTable)
        {
            if (exampleObject == null)
            {
                return new Tuple<Boolean, String>(false, "Unable to create columns from object, the provided object is null.");
            }

            PropertyInfo[] arrayObjectProperties = exampleObject.GetType().GetProperties();

            List<String> propertyNamesList = new List<String>();

            foreach (PropertyInfo objectProperty in arrayObjectProperties)
            {
                if (objectProperty != null)
                {
                    propertyNamesList.Add(objectProperty.Name);
                }
            }

            foreach (String propertyName in propertyNamesList)
            {
                dataTable.Columns.Add(propertyName, typeof(string));
                dataTable.Columns[propertyName].Caption = propertyName;
            }

            return new Tuple<Boolean, String>(true, "");
        }
    }
}