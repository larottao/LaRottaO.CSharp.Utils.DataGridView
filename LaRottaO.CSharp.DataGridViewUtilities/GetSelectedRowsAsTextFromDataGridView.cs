using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class GetSelectedRowsAsTextFromDataGridView
    {
        public static List<String> get(String argDesiredColumnHeader, DataGridView argDataGridView)
        {
            if (argDataGridView.SelectedRows.Count == 0)
            {
                Console.WriteLine("No rows are selected");
                return new List<String>();
            }

            List<String> selectedCellTextList = new List<String>();

            foreach (DataGridViewRow row in argDataGridView.SelectedRows)
            {
                selectedCellTextList.Add(row.Cells[argDesiredColumnHeader].Value.ToString());
            }

            return selectedCellTextList;
        }
    }
}