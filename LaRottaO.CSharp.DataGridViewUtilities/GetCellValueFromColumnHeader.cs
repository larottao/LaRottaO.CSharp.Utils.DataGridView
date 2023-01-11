using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    //Based on the code by Patrick Quirk
    //https://stackoverflow.com/questions/13436553/how-to-get-cell-value-of-datagridview-by-column-name
    /*

    USAGE:

    */

    public static class GetCellValueFromColumnHeader
    {
        public static object getByColHeaderText(this DataGridViewCellCollection CellCollection, string HeaderText)
        {
            return CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
        }
    }
}