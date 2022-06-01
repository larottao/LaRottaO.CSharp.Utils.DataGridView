using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class PasteClipboardIntoDataTable
    {
        public static Tuple<Boolean, String> paste(DataTable argDataTable, Boolean argCreateNewColumnsIfRequired)
        {
            // USAGE:
            //
            //  private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
            //  {
            //      if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            //
            //      {
            //          pasteClipboardIntoDataTable(false);
            //      }
            //  }

            if (!Clipboard.ContainsText())
            {
                return new Tuple<Boolean, String>(false, "Clipboard does not containt any text to paste.");
            }

            //Uses tab as the default separator, but if there's no tab, use the system's default

            String textSeparator = (Clipboard.GetText().Contains("\t")) ? "\t" : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            String contenidoClipboard = Clipboard.GetText();

            contenidoClipboard = contenidoClipboard.Replace("ID de referencia", "\t");
            contenidoClipboard = contenidoClipboard.Replace("ID de referencia de portin", "\t");
            contenidoClipboard = contenidoClipboard.Replace("Port-In Reference ID", "\t");
            contenidoClipboard = contenidoClipboard.Replace("\"", "");
            contenidoClipboard = contenidoClipboard.Replace(":", "");

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[\t]{2,}", options);
            contenidoClipboard = regex.Replace(contenidoClipboard, "\t");

            contenidoClipboard = contenidoClipboard.Replace("\t \t", "\t");

            Clipboard.SetText(contenidoClipboard);

            List<String> clipboardAsList = new List<String>(contenidoClipboard.Split('\n'));

            List<String[]> cleanLines = clipboardAsList
             .Select(s => s.Replace("\n", "").Replace("\r", "").Split(textSeparator.ToCharArray()))
             .ToList<String[]>()
             ;

            foreach (String[] line in cleanLines)
            {
                if (argCreateNewColumnsIfRequired && argDataTable.Columns.Count < line.Length)
                {
                    for (int i = argDataTable.Columns.Count; i < line.Length; i++)
                    {
                        argDataTable.Columns.Add();
                    }
                }

                DataRow dataRow = argDataTable.NewRow();

                //If the clipboard contains too many columns and createNewColumnsIfRequired is false

                if (line.Length > dataRow.ItemArray.Length)
                {
                    return new Tuple<Boolean, String>(false, "The clipboard contains the following " + line.Length + " colums: \n\n" + string.Join(", " + Environment.NewLine, line) + ".\n\nBut the DatagridView only contains " + dataRow.ItemArray.Length + " columns.");
                }

                for (int i = 0; i < line.Length; i++)
                {
                    dataRow[i] = line[i];
                }

                argDataTable.Rows.Add(dataRow);
            }

            return new Tuple<Boolean, String>(true, "");
        }
    }
}