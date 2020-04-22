using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace Cinema
{
    public partial class HomeUC : UserControl
    {
        string connString = "Data source=orcl;User Id=hr; Password=hr;";
        public HomeUC()
        {
            InitializeComponent();
           
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HomeUC_Load(object sender, EventArgs e)
        {
            string cmd = "select * from movie";
            OracleDataAdapter adapter = new OracleDataAdapter(cmd, connString);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
