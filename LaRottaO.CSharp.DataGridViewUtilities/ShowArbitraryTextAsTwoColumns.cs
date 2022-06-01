using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class ShowArbitraryTextAsTwoColumns
    {
        public static Task show(String argLeftColumnValue, String argRightColumnValue, DataTable argDataTable, Boolean argCleanBeforeAdding = false, String argLeftColumnHeader = "Item", String argRightColumnName = "Value")
        {
            if (argCleanBeforeAdding)
            {
                argDataTable.Clear();
                argDataTable.Columns.Clear();
            }

            if (argDataTable.Columns.Count == 0)
            {
                argDataTable.Columns.Add(argLeftColumnHeader, typeof(string));
                argDataTable.Columns.Add(argRightColumnName, typeof(string));
            }

            String[] arrayElementosRow = new string[] { argLeftColumnValue, argRightColumnValue };

            if (arrayElementosRow.Any())
            {
                argDataTable.Rows.Add(arrayElementosRow);
            }

            return Task.CompletedTask;
        }
    }
}