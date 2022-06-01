using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public class ShowObjectPropertiesAsTwoColumns
    {
        public Task show(object @object, DataTable dataTable, Boolean cleanBeforeAdd = false, String columnAName = "item", String columnBName = "value", List<String> undesiredItemsList = null, Boolean showDebug = true)
        {
            if (cleanBeforeAdd)
            {
                dataTable.Clear();
                dataTable.Columns.Clear();

                dataTable.Columns.Add(columnAName, typeof(string));
                dataTable.Columns.Add(columnBName, typeof(string));
            }

            PropertyInfo[] properties = @object.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != null && property.GetValue(@object) != null)
                {
                    var objectType = property.PropertyType;

                    if (showDebug)
                    {
                        Console.WriteLine(property.Name + "|" + property.PropertyType);
                    }

                    if (undesiredItemsList != null && undesiredItemsList.Contains(property.Name))
                    {
                        continue;
                    }

                    if (objectType.ToString().Equals("System.Collections.Generic.List`1[System.String]"))
                    {
                        try
                        {
                            List<String> objectsToAddList = (List<String>)property.GetValue(@object);

                            foreach (Object item in objectsToAddList)
                            {
                                String[] arrayElementosRow = new string[] { property.Name, item.ToString() };
                                if (arrayElementosRow.Any())
                                {
                                    dataTable.Rows.Add(arrayElementosRow);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (showDebug)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                    else if (

                        objectType.ToString().Equals("System.String") ||
                        objectType.ToString().Equals(" System.Boolean") ||
                        objectType.ToString().Contains("System.Int") ||
                        objectType.ToString().Contains("System.Decimal"))

                    {
                        String[] arrayElementosRow = new string[] { property.Name, property.GetValue(@object).ToString() };
                        if (arrayElementosRow.Any())
                        {
                            dataTable.Rows.Add(arrayElementosRow);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}