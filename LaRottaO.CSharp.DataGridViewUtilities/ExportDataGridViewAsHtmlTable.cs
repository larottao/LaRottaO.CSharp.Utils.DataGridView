using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public class ExportDataGridViewAsHtmlTable
    {
        public String export(DataGridView dataGridView, String argHtmlTableTitle = null, String argHtmlTableDetails = null, String[] argRedColorIfContainsAnyOf = null, Boolean argAddHtmlHeaderAndFooter = true)
        {
            StringBuilder sbHtmlOutput = new StringBuilder();
            String NORMAL_TEXT_COLOR = "black";
            String ALARM_TEXT_COLOR = "red";

            /*******************************************************************/
            // Add header if required
            /*******************************************************************/

            if (argAddHtmlHeaderAndFooter)
            {
                sbHtmlOutput.Append("<html>" + Environment.NewLine);
                sbHtmlOutput.Append("<head>" + Environment.NewLine);
                sbHtmlOutput.Append("<style>" + Environment.NewLine);
                sbHtmlOutput.Append("table {" + Environment.NewLine);
                sbHtmlOutput.Append("font-family: arial, sans-serif;" + Environment.NewLine);
                sbHtmlOutput.Append("border-collapse: collapse;  " + Environment.NewLine);
                sbHtmlOutput.Append("width: 100%;" + Environment.NewLine);
                sbHtmlOutput.Append("}" + Environment.NewLine);
                sbHtmlOutput.Append("td, th {" + Environment.NewLine);
                sbHtmlOutput.Append("border: 1px solid #dddddd;" + Environment.NewLine);
                sbHtmlOutput.Append("text-align: left;" + Environment.NewLine);
                sbHtmlOutput.Append("padding: 8px;" + Environment.NewLine);
                sbHtmlOutput.Append("}" + Environment.NewLine);
                sbHtmlOutput.Append("tr:nth-child(even) {" + Environment.NewLine);
                sbHtmlOutput.Append("background-color: #dddddd;" + Environment.NewLine);
                sbHtmlOutput.Append("}" + Environment.NewLine);
                sbHtmlOutput.Append("</style>" + Environment.NewLine);
                sbHtmlOutput.Append("</head>" + Environment.NewLine);
                sbHtmlOutput.Append("<body style=\"font-family: arial, sans-serif\">" + Environment.NewLine);
                sbHtmlOutput.Append("<BR>" + Environment.NewLine);
            }

            /*******************************************************************/
            // Add title if required
            /*******************************************************************/

            if (argHtmlTableTitle != null)
            {
                sbHtmlOutput.Append("<h3>" + argHtmlTableTitle + "</h3>" + Environment.NewLine);
            }

            /*******************************************************************/
            // Add details if required
            /*******************************************************************/

            if (argHtmlTableDetails != null)
            {
                sbHtmlOutput.Append("<h4>" + argHtmlTableDetails + "</h4>" + Environment.NewLine);
            }

            sbHtmlOutput.Append("<BR>" + Environment.NewLine);

            sbHtmlOutput.Append("</body>" + Environment.NewLine);
            sbHtmlOutput.Append("</html>" + Environment.NewLine);

            /*******************************************************************/
            // Add Table headers from object properties
            /*******************************************************************/

            List<String> columnNamesList = DgvUtilsShared.getDataGridViewColumnNames(dataGridView);

            sbHtmlOutput.Append("<table>" + Environment.NewLine);
            sbHtmlOutput.Append("<tr>" + Environment.NewLine);

            foreach (String columnName in columnNamesList)
            {
                sbHtmlOutput.Append("<th>" + columnName + "</th>" + Environment.NewLine);
            }

            sbHtmlOutput.Append("</tr>" + Environment.NewLine);

            /*******************************************************************/
            // Dump DataGridView rows content
            /*******************************************************************/

            int columnCount = dataGridView.Columns.Count;

            for (int rowIndex = 1; (rowIndex - 1) < dataGridView.Rows.Count; rowIndex++)
            {
                StringBuilder sbRow = new StringBuilder();

                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    if (dataGridView.Columns[columnIndex].Visible)
                    {
                        var cellValue = dataGridView.Rows[rowIndex - 1].Cells[columnIndex].Value;

                        if (cellValue != null)
                        {
                            sbRow.Append("<TD>" + cellValue.ToString() + "</TD>");
                        }
                        else
                        {
                            sbRow.Append("<TD>" + "" + "</TD>");
                        }
                    }
                }

                /*******************************************************************/
                // Color the whole table row if an alarm word is found
                /*******************************************************************/

                String rowTextColor = NORMAL_TEXT_COLOR;

                if (argRedColorIfContainsAnyOf != null)
                {
                    foreach (String word in argRedColorIfContainsAnyOf)
                    {
                        if (sbRow.ToString().Contains(word))
                        {
                            rowTextColor = ALARM_TEXT_COLOR;
                        }
                    }
                }

                sbHtmlOutput.Append("<TR " + "style=\"color: " + rowTextColor + "; \"" + ">" + sbRow + "</TR>" + Environment.NewLine);
            }

            /*******************************************************************/
            // End of table
            /*******************************************************************/

            sbHtmlOutput.Append("</table>" + Environment.NewLine);

            /*******************************************************************/
            // Added Footer if required
            /*******************************************************************/

            if (argAddHtmlHeaderAndFooter)
            {
                sbHtmlOutput.Append("</body>" + Environment.NewLine);
                sbHtmlOutput.Append("</html>" + Environment.NewLine);
            }

            return sbHtmlOutput.ToString();
        }
    }
}