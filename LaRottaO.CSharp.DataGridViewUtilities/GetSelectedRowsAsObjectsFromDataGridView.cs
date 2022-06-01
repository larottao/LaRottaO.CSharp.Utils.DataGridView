using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class GetSelectedRowsAsObjectsFromDataGridView
    {
        public static List<DataGridViewRow> get(DataGridView argDataGridView)
        {
            if (argDataGridView.SelectedRows.Count == 0)
            {
                Console.WriteLine("No rows are selected");
                return new List<DataGridViewRow>();
            }

            List<DataGridViewRow> selectedRowsList = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in argDataGridView.SelectedRows)
            {
                selectedRowsList.Add(row);
            }

            return selectedRowsList;
        }
    }
}