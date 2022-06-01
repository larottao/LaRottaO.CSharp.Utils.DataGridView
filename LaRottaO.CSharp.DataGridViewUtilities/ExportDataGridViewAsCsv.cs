using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class ExportDataGridViewAsCsv
    {
        public static Tuple<Boolean, String> export(DataGridView argDataGridView, String argSpacer = "|", String argFilename = null, int lastRowsToIgnore = 0)
        {
            if (argDataGridView.Rows.Count <= 0)
            {
                return new Tuple<Boolean, String>(false, "Unable to export as csv, DataGridView is empty");
            }

            DialogResult dialogResult = new DialogResult();

            String selectedFileName = null;

            if (argFilename == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Data Table.csv";
                dialogResult = sfd.ShowDialog();
                selectedFileName = sfd.FileName;
            }
            else
            {
                selectedFileName = argFilename;
            }

            if (dialogResult != DialogResult.OK && argFilename == null)
            {
                return new Tuple<Boolean, String>(false, "Export as csv cancelled by user");
            }

            if (File.Exists(selectedFileName))
            {
                try
                {
                    File.Delete(selectedFileName);
                }
                catch (Exception ex)
                {
                    return new Tuple<Boolean, String>(false, "Unable to export as csv, an existing file with the same name could not be deleted: " + ex.ToString());
                }
            }

            try
            {
                int dataGridViewColumnCount = argDataGridView.Columns.Count;

                StringBuilder sbColumnNames = new StringBuilder();

                string[] csvOutputArray = new string[argDataGridView.Rows.Count - lastRowsToIgnore + 1];

                for (int i = 0; i < dataGridViewColumnCount; i++)
                {
                    if (argDataGridView.Columns[i].Visible)
                    {
                        sbColumnNames.Append(argDataGridView.Columns[i].HeaderText.ToString() + argSpacer);
                    }
                }

                csvOutputArray[0] += sbColumnNames.ToString();

                for (int rowIndex = 1; (rowIndex - 1) < argDataGridView.Rows.Count - lastRowsToIgnore; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < dataGridViewColumnCount; columnIndex++)
                    {
                        if (argDataGridView.Columns[columnIndex].Visible)
                        {
                            var cellValue = argDataGridView.Rows[rowIndex - 1].Cells[columnIndex].Value;

                            if (cellValue != null && !String.IsNullOrEmpty(cellValue.ToString()))
                            {
                                csvOutputArray[rowIndex] += cellValue.ToString() + argSpacer;
                            }
                            else
                            {
                                csvOutputArray[rowIndex] += argSpacer;
                            }
                        }
                    }
                }

                File.WriteAllLines(selectedFileName, csvOutputArray, Encoding.UTF8);

                return new Tuple<Boolean, String>(true, "");
            }
            catch (Exception ex)
            {
                return new Tuple<Boolean, String>(false, "Unable to export as csv: " + ex.ToString());
            }
        }
    }
}