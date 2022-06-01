using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.DataGridViewUtilities
{
    public static class BindDataGridViewToDataSource
    {
        public static void bind(DataGridView argDataGridView, BindingSource argBindingSource)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                argDataGridView.Invoke(new Action(() =>
                {
                    argDataGridView.DataSource = argBindingSource;
                }));
            }
            else
            {
                argDataGridView.DataSource = argBindingSource;
            }
        }
    }
}