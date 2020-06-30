using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RAP.Control;

namespace RAP.View
{
    /// <summary>
    /// CumulativeCount.xaml 
    /// </summary>
    public partial class CumulativeCount : Window
    {
        private Controller controller = new Controller();
        public int id = 0;
        public CumulativeCount()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DataTable dataTable = Database.Database.GetPaperStatistics(id);

            //if (dataTable == null || dataTable.Rows.Count <= 0) return;
            long startyear = dataTable.Rows[0].Field<System.Int64>("year");
            // int startyear = dataTable.Rows[0].Field<System.Int64>("year"); 
            long endyear = DateTime.Now.Year;
            long yeardiff = endyear - startyear;

            DataTable dt2 = new DataTable();

            // Create Title Role
            DataColumn dctitle = new DataColumn("year");
            dt2.Columns.Add(dctitle);

            for (long i = startyear; i <= endyear; i++)
            {
                DataColumn dc = new DataColumn(i.ToString(), typeof(System.Int32));
                dt2.Columns.Add(dc);

            }

            DataRow dr = dt2.NewRow();
            for (long i = startyear; i <= endyear + 1; i++)
            {
                int cindex = (int)(i - startyear);
                if (cindex == 0)
                {
                    dr[0] = "cumulative";
                }
                else
                {
                    dr[cindex] = 0;
                }
            }
            dt2.Rows.Add(dr);

            // DataTable.Rows.Count might be 0
            if (dataTable.Rows.Count > 0)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i][0].ToString().Equals(dt2.Columns[j].ColumnName)) 
                        {
                            dt2.Rows[0][j] = dataTable.Rows[i][1];

                        }
                    }
                }

            }

            // Stastics
            int total = 0;
            for (int i = 1; i < dt2.Columns.Count; i++)
            {
                total = total + Int32.Parse(dt2.Rows[0][i].ToString());
                dt2.Rows[0][i] = total;
            } 
            // Connect Data to UI 
            this.dataGrid1.ItemsSource = dt2.DefaultView;




        }
    }
}
