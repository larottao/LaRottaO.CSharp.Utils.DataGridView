using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public class ShowMultipleObjectsAsRows
    {
        /*******************************************************************/
        // USAGE
        // dataTable = showMultipleObjectsAsRows(new List<Object>(yourObjectsList);

        /*******************************************************************/

        public Task<DataTable> show(List<object> argObjectList, Boolean argShowDebug = true)
        {
            return Task.Run(() =>
            {
                DataTable outputDataTable = new DataTable();

                outputDataTable.Clear();
                outputDataTable.Columns.Clear();

                if (argObjectList.Count == 0)
                {
                    if (argShowDebug)
                    {
                        Console.WriteLine("There are no objects to show on the Datatable.");
                    }
                    return outputDataTable;
                }

                Tuple<Boolean, String> columnCreationResult = DgvUtilsShared.createColumnsFromObjectProperties(argObjectList[0], outputDataTable);

                if (!columnCreationResult.Item1)
                {
                    Console.WriteLine("Unable to create the Datatable Columns: " + columnCreationResult.Item2);
                }

                PropertyInfo[] propertiesToRetrieveList = argObjectList[0].GetType().GetProperties();

                foreach (Object objectOnList in argObjectList)
                {
                    List<String> listOfValuesForDataTableRow = new List<String>();

                    foreach (PropertyInfo propertyToRetrieve in propertiesToRetrieveList)
                    {
                        try
                        {
                            /*******************************************************************/
                            // If the property value is null, the word is added to the Datatable row
                            /*******************************************************************/

                            if (propertyToRetrieve == null || propertyToRetrieve.GetValue(objectOnList) == null)
                            {
                                listOfValuesForDataTableRow.Add("null");
                                continue;
                            }

                            /*******************************************************************/
                            // If the property value is a list<String>, the list items are added
                            // as comma separated values
                            /*******************************************************************/

                            if (propertyToRetrieve.PropertyType.ToString().Contains("System.Collections.Generic.List") &&
                            propertyToRetrieve.GetValue(objectOnList).GetType().ToString().Contains("System.Collections.Generic.List`1[System.String]"))

                            {
                                StringBuilder sb = new StringBuilder();

                                try
                                {
                                    List<String> propertiesList = (List<String>)propertyToRetrieve.GetValue(objectOnList);

                                    foreach (Object item in propertiesList)
                                    {
                                        sb.Append(item.ToString() + ",");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (argShowDebug)
                                    {
                                        Console.WriteLine("Unable to cast object property to Datatable: " + ex.StackTrace);
                                    }
                                }

                                listOfValuesForDataTableRow.Add(sb.ToString());
                            }

                            /*******************************************************************/
                            // If the property is not a List<String>, just try to brute-force
                            // add the value of the property
                            /*******************************************************************/
                            else
                            {
                                listOfValuesForDataTableRow.Add(propertyToRetrieve.GetValue(objectOnList).ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            if (argShowDebug)
                            {
                                Console.WriteLine("Unable to cast object properties to Datatable: " + ex.StackTrace);
                            }
                        }
                    }

                    /*******************************************************************/
                    // If the operation resulted on a useful row, add it to the
                    // Datatable
                    /*******************************************************************/

                    if (listOfValuesForDataTableRow.Count > 0)
                    {
                        outputDataTable.Rows.Add(listOfValuesForDataTableRow.ToArray());
                    }
                    else
                    {
                        if (argShowDebug)
                        {
                            Console.WriteLine("Unable to cast object properties to Datatable. ");
                        }
                    }
                }

                return outputDataTable;
            });
        }
    }
}