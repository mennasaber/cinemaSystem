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
    public partial class SearchUC : UserControl
    {
        string connString = "Data source=orcl;User Id=scott; Password=tiger;";
        public SearchUC()
        {
            InitializeComponent();
        }

        private void profile1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text.ToString() != "")
            {
                string cmd = @"select * from movie where MNAME Like :name";
                OracleDataAdapter adapter = new OracleDataAdapter(cmd, connString);
                adapter.SelectCommand.Parameters.Add("name", '%'+textBox1.Text.ToString()+'%');
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
