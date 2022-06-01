using System;
using System.Data;
using System.Linq;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class ShowArbitraryTextAsInfiniteColumns
    {
        public static Tuple<Boolean, String> show(String[] arhColumnHeadersArray, String[] argColumnContentsArray, DataTable argDataTable, Boolean argCleanBeforeAdd = false)
        {
            try
            {
                if (arhColumnHeadersArray.Length != argColumnContentsArray.Length)
                {
                    return new Tuple<Boolean, String>(false, "Column header count " + arhColumnHeadersArray.Length + " does not match column data count " + argColumnContentsArray.Length);
                }

                if (argCleanBeforeAdd)
                {
                    argDataTable.Clear();
                    argDataTable.Columns.Clear();
                }
                if (argDataTable.Columns.Count == 0)
                {
                    foreach (String columnHeader in arhColumnHeadersArray)
                    {
                        argDataTable.Columns.Add(columnHeader, typeof(string));
                    }
                }

                if (argColumnContentsArray.Any())
                {
                    argDataTable.Rows.Add(argColumnContentsArray);
                }

                return new Tuple<Boolean, String>(true, "");
            }
            catch (Exception ex)
            {
                return new Tuple<Boolean, String>(false, ex.ToString());
            }
        }
    }
}