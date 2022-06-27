using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class GetAllDgvCellsAs2DList
    {
        public static Tuple<Boolean, List<List<String>>> get(DataGridView dataGridView)
        {
            try
            {
                List<List<String>> rowsDataList = new List<List<String>>();

                for (int row = 0; row < dataGridView.RowCount; row++)
                {
                    List<String> columnDataList = new List<String>();

                    for (int column = 0; column < dataGridView.Columns.Count; column++)
                    {
                        var cellValue = dataGridView.Rows[row].Cells[column].Value;

                        if (cellValue != null)
                        {
                            columnDataList.Add(cellValue.ToString());
                        }
                        else
                        {
                            columnDataList.Add("");
                        }
                    }

                    rowsDataList.Add(columnDataList);
                }

                return new Tuple<Boolean, List<List<String>>>(true, rowsDataList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to extract text from dataGridView " + ex.ToString());

                return new Tuple<Boolean, List<List<String>>>(false, new List<List<String>>());
            }
        }
    }
}