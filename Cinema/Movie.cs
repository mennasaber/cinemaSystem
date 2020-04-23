using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Cinema
{
    public partial class Movie : Form
    {
        string connString = "Data source=orcl;User Id=hr; Password=hr;";
        public Movie()
        {
            InitializeComponent();
            panel2.Show();
            panel3.Show();
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel4.Hide();
            panel5.Hide();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select CATNAME from CATEGORY", conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CAT_CMB.Items.Add(dr[0]);
            }

            OracleCommand cmd1 = new OracleCommand("Select CINNAME from CINEMA", conn);
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                CIN_CMB.Items.Add(dr1[0]);
            }
            conn.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Show();
        }

        private void Done_BTN_Click(object sender, EventArgs e)
        {
            
            int id, newid, catNum, hallid;
            string status;
            string name, discrip;
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "GETMOVIEID";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("MovID",OracleDbType.Int32,ParameterDirection.Output);
            cmd1.ExecuteNonQuery();
           try
            {
                id = Convert.ToInt32(cmd1.Parameters["MovID"].Value.ToString());
                newid = id +1;
            }

            catch
            {
                newid = 1;
            }

            OracleCommand cmd4 = new OracleCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "SELECTCAT";
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("CatName", CAT_CMB.Text);
            cmd4.Parameters.Add("CatID", OracleDbType.Int32, ParameterDirection.Output);
            cmd4.ExecuteNonQuery();
            catNum = Convert.ToInt32(cmd4.Parameters["CatID"].Value.ToString());

            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "RETHALLID";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("HName", HALL_CMB.Text);
            cmd3.Parameters.Add("HID", OracleDbType.Int32, ParameterDirection.Output);
            cmd3.ExecuteNonQuery();
            hallid = Convert.ToInt32(cmd3.Parameters["HID"].Value.ToString());

            if (CS_RDB.Checked)
            {
                status = "Coming Soon";
            }

            else
            {
                status = "In Cinema";
            }

            name = name_txt.Text;
            discrip = sum_txt.Text;
            string SD = SD_txt.Text;
            string ED = ED_txt.Text;
            int time = Convert.ToInt32(time_txt.Text);

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "ADDMOVIE";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("ID", newid);
            cmd2.Parameters.Add("NAME", name);
            cmd2.Parameters.Add("Descrip", discrip);
            cmd2.Parameters.Add("UPCOM", status);
            cmd2.Parameters.Add("SD", SD);
            cmd2.Parameters.Add("ED",ED);
            cmd2.Parameters.Add("Tim", time);
            cmd2.Parameters.Add("HID", hallid);
            cmd2.Parameters.Add("Cat", catNum);
            cmd2.ExecuteNonQuery();

            MessageBox.Show("The Movie has been Added Successfuly");
            name_txt.Text  = sum_txt.Text = time_txt.Text = " ";
            SD_txt.Text = ED_txt.Text = "dd-MMM-yyyy";
            HALL_CMB.Text =CIN_CMB.Text= "";
            CAT_CMB.Text = "";
           
        }

        private void CIN_CMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            HALL_CMB.Items.Clear();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECTHALL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CName", CIN_CMB.Text);
            cmd.Parameters.Add("MovID", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                HALL_CMB.Items.Add(dr[0]);
            }
            dr.Close();
            conn.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel5.Hide();
            panel4.Show();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand("Select CATNAME from CATEGORY", conn);
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CAT_CMB2.Items.Add(dr[0]);
            }

            OracleCommand cmd1 = new OracleCommand("Select CINNAME from CINEMA", conn);
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                CIN_CMB2.Items.Add(dr1[0]);
            }

            OracleCommand cmd2 = new OracleCommand("Select MNAME from MOVIE", conn);
            cmd2.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                MN_CMB.Items.Add(dr2[0]);
            }
            conn.Close();
        }

        private void CIN_CMB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HALL_CMB2.Items.Clear();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECTHALL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CName", CIN_CMB2.Text);
            cmd.Parameters.Add("MovID", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                HALL_CMB2.Items.Add(dr[0]);
            }
            dr.Close();
            conn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int id=0, catNum, hallid;
            string status;
            string name, discrip;
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "Select MID from MOVIE where MNAME = '"+MN_CMB.Text+"'";
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            
            OracleCommand cmd4 = new OracleCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "SELECTCAT";
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("CatName", CAT_CMB2.Text);
            cmd4.Parameters.Add("CatID", OracleDbType.Int32, ParameterDirection.Output);
            cmd4.ExecuteNonQuery();
            catNum = Convert.ToInt32(cmd4.Parameters["CatID"].Value.ToString());

            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "RETHALLID";
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("HName", HALL_CMB2.Text);
            cmd3.Parameters.Add("HID", OracleDbType.Int32, ParameterDirection.Output);
            cmd3.ExecuteNonQuery();
            hallid = Convert.ToInt32(cmd3.Parameters["HID"].Value.ToString());

            if (CS_RDB2.Checked)
            {
                status = "Coming Soon";
            }

            else
            {
                status = "In Cinema";
            }

            discrip = plot_txt.Text;
            string SD = SD_txt2.Text;
            string ED = ED_txt2.Text;
            int time = Convert.ToInt32(time_txt2.Text);

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "UPDATEMOVIE";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("ID", id);
            cmd2.Parameters.Add("Descrip", discrip);
            cmd2.Parameters.Add("UPCOM", status);
            cmd2.Parameters.Add("SD", SD);
            cmd2.Parameters.Add("ED", ED);
            cmd2.Parameters.Add("Tim", time);
            cmd2.Parameters.Add("HID", hallid);
            cmd2.Parameters.Add("Cat", catNum);
            cmd2.ExecuteNonQuery();

            MessageBox.Show("The Movie has been Updated Successfuly");
            M_txt.Text = plot_txt.Text = time_txt2.Text = " ";
            SD_txt2.Text = ED_txt2.Text = "dd-MMM-yyyy";
            HALL_CMB2.Text = CIN_CMB2.Text=CAT_CMB2.Text="";
            IC_RDB2.Checked = false;
            CS_RDB2.Checked = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MN_CMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ED_txt2.Text = SD_txt2.Text = "dd-MMM-yyyy";
            M_txt.Text = plot_txt.Text = time_txt2.Text = "";
            CAT_CMB2.Text = CIN_CMB2.Text = HALL_CMB2.Text = "";
            string status=null;
            int hall=0,id=0;
            M_txt.Text = MN_CMB.SelectedItem.ToString();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select MID,DESCPRIPTION,UPCOMING,STARTDATE,ENDDATE,TIME,HALLID from MOVIE where MNAME = '" + MN_CMB.SelectedItem.ToString()+"'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0].ToString());
                plot_txt.Text = dr[1].ToString();
                status = dr[2].ToString();
                SD_txt2.Text = dr[3].ToString();
                ED_txt2.Text = dr[4].ToString();
                time_txt2.Text = dr[5].ToString();
                hall = Convert.ToInt32(dr[6].ToString());
            }
            dr.Close();

            int cinID=0;
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "Select HALLNAME,CINID from HALL where HALLID = " + hall;
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                HALL_CMB2.Text = dr1[0].ToString();
                cinID = Convert.ToInt32(dr1[1].ToString());
            }
            dr1.Close();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "Select CINNAME from CINEMA where CINID = "+cinID;
            cmd2.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                CIN_CMB2.Text = dr2[0].ToString();
            }
            dr2.Close();

            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "Select CATNAME from CATEGORY where CATID = (Select CATID from MOVIE_CAT where MID = "+id+")";
            cmd3.CommandType = CommandType.Text;
            OracleDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                CAT_CMB2.Text = dr3[0].ToString();
            }
            dr3.Close();

            if (status == "In Cinema")
                IC_RDB2.Checked = true;
            else
                CS_RDB2.Checked = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ED_txt3.Text = SD_txt3.Text = "dd-MMM-yyyy";
            MN_txt.Text = plot_txt2.Text = Time_txt3.Text = "";
            CAT_txt.Text = CIN_txt.Text = HALL_txt.Text = "";
            CS_RDB3.Checked = false;
            IC_RDB3.Checked = false;
            string status = null;
            int hall = 0, id = 0;
            MN_txt.Text = M_CMB.SelectedItem.ToString();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select MID,DESCPRIPTION,UPCOMING,STARTDATE,ENDDATE,TIME,HALLID from MOVIE where MNAME = '" + M_CMB.SelectedItem.ToString() + "'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0].ToString());
                plot_txt2.Text = dr[1].ToString();
                status = dr[2].ToString();
                SD_txt3.Text = dr[3].ToString();
                ED_txt3.Text = dr[4].ToString();
                Time_txt3.Text = dr[5].ToString();
                hall = Convert.ToInt32(dr[6].ToString());
            }
            dr.Close();

            int cinID = 0;
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "Select HALLNAME,CINID from HALL where HALLID = " + hall;
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                HALL_txt.Text = dr1[0].ToString();
                cinID = Convert.ToInt32(dr1[1].ToString());
            }
            dr1.Close();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "Select CINNAME from CINEMA where CINID = " + cinID;
            cmd2.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                CIN_txt.Text = dr2[0].ToString();
            }
            dr2.Close();

            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "Select CATNAME from CATEGORY where CATID = (Select CATID from MOVIE_CAT where MID = " + id + ")";
            cmd3.CommandType = CommandType.Text;
            OracleDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                CAT_txt.Text = dr3[0].ToString();
            }
            dr3.Close();

            if (status == "In Cinema")
                IC_RDB3.Checked = true;
            else
                CS_RDB3.Checked = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel5.Show();
            panel3.Hide();
            panel4.Hide();
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            
            OracleCommand cmd2 = new OracleCommand("Select MNAME from MOVIE", conn);
            cmd2.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                M_CMB.Items.Add(dr2[0]);
            }
            conn.Close();
        }

        private void Del_BTN_Click(object sender, EventArgs e)
        {
            int id = 0;
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "Select MID from MOVIE where MNAME = '" + M_CMB.Text + "'";
            cmd1.CommandType = CommandType.Text;
            OracleDataReader dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0]);
            }
            dr.Close();

            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "DELETEMOVIE";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("ID", id);
            
            cmd2.ExecuteNonQuery();

            M_CMB.Items.RemoveAt(M_CMB.SelectedIndex);
            MessageBox.Show("The Movie has been Deleted Successfuly");
            MN_txt.Text = plot_txt2.Text = Time_txt3.Text = " ";
            SD_txt3.Text = ED_txt3.Text = "dd-MMM-yyyy";
            HALL_txt.Text = CIN_txt.Text =CAT_txt.Text= "";
            IC_RDB3.Checked = false;
            CS_RDB3.Checked = false;
            M_CMB.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
