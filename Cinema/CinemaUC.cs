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
    public partial class CinemaUC : UserControl
    {
        string connString = "Data source=orcl;User Id=scott; Password=tiger;";
        OracleDataAdapter adapter;
        OracleConnection conn;
        List<int> cinemaIDs;
        DataSet ds;
        public CinemaUC()
        {
            InitializeComponent();
            conn = new OracleConnection(connString);
            conn.Open();
            cinemaIDs = new List<int>();
        }

        private void CinemaUC_Load(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("Select CINID,CINNAME from cinema", conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cinemaIDs.Add(int.Parse(dr[0].ToString()));
                comboBox1.Items.Add(dr[1]);
            }
            conn.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cmd = @"select * from hall where CINID = :id";
            adapter = new OracleDataAdapter(cmd, connString);
            adapter.SelectCommand.Parameters.Add("id",cinemaIDs.ElementAt(comboBox1.SelectedIndex));
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                OracleCommand cmd = new OracleCommand(@"update cinema set Hallcount = :count", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("count", dataGridView2.RowCount-1);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.FormattedValue.ToString() != "" && e.FormattedValue.ToString() != cinemaIDs.ElementAt(comboBox1.SelectedIndex).ToString())
            {
                dataGridView2[e.RowIndex, e.ColumnIndex].Value = cinemaIDs.ElementAt(comboBox1.SelectedIndex).ToString();
            }
        }

        private void dataGridView2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
