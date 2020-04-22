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
    public partial class AddCinema : UserControl
    {
        string connString = "Data source=orcl;User Id=hr; Password=hr;";
        OracleConnection conn;
        OracleDataAdapter adapter;
        List<int> cinemaIDs , HallIDs;
        DataSet ds;
        int newC_id = 0 , newH_id = 0 , newS_id=0, hallCountL ,seatCountLabel , selectedEditCinId=1;
        public AddCinema()
        {
            InitializeComponent();
            panel9.Visible = false;
            conn = new OracleConnection(connString);
            conn.Open();
            cinemaIDs = new List<int>();
            HallIDs = new List<int>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AddCinema_Load(object sender, EventArgs e)
        {
            getAllCinemaId_Name();
            getAllHallId_Name();
        }
        //************************** add new cinema *******************************************************
        private void button1_Click(object sender, EventArgs e)
        {
            string caption = "cinema confirmation";
            getCinemaNewId();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into cinema values (:id,:name,:location ,:hallCount)";
            cmd.Parameters.Add("id", newC_id);
            cmd.Parameters.Add("name", c_name.Text);
            cmd.Parameters.Add("location", c_location.Text);
            cmd.Parameters.Add("hallCount", Convert.ToInt32(hall_count.Text.ToString()));
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                comboBoxC_id.Items.Add(newC_id);
                MessageBox.Show("Cinema is added and it's ID is ( " + newC_id + " ) Please add the Hall detailes", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                c_name.Text = "";
                c_location.Text = "";
                hall_count.Text = "";
            }

            comboBoxCINName.Items.Clear();
            comboBoxC_id.Items.Clear();
            comboBoxC_ID2.Items.Clear();
            getAllCinemaId_Name();
        }
        //************************** to get the new id *******************************************************
        public void getCinemaNewId()
        {
            int max_id = 0;
            try
            {
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select max(CINID) from cinema ";
                c.CommandType = CommandType.Text;
                max_id=Convert.ToInt32(c.ExecuteScalar());
                newC_id = max_id + 1;
            }
            catch
            {
                newC_id = 1;
            }
        }

        public void getHallNewId()
        {
            int max_id = 0;
            try
            {
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select max(hallid) from Hall ";
                c.CommandType = CommandType.Text;
                max_id = Convert.ToInt32(c.ExecuteScalar());
                newH_id = max_id + 1;
            }
            catch
            {
                newH_id = 1;
            }
        }

        public void getSeatNewId()
        {
            int max_id = 0;
            try
            {
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select max(seatid) from seat ";
                c.CommandType = CommandType.Text;
                max_id = Convert.ToInt32(c.ExecuteScalar());
                newS_id = max_id + 1;
            }
            catch
            {
                newS_id = 1;
            }
        }

        //function to get cinema id and name into comboboxes
        public void getAllCinemaId_Name()
        {
            cinemaIDs.Clear();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select CINID , cinname from cinema ";
            c.CommandType = CommandType.Text;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                cinemaIDs.Add(int.Parse(dr[0].ToString()));
                comboBox1.Items.Add(dr[1]);
                comboBoxC_id.Items.Add(dr[0]);
                comboBoxC_ID2.Items.Add(dr[0]);
                comboBoxCINName.Items.Add(dr[1]);
                comboBox2.Items.Add(dr[1]);
                
            }
            dr.Close();
        }
        //function to get hall id and name into comboboxes
        public void getAllHallId_Name()
        {
            HallIDs.Clear();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select HALLID , hallname from hall ";
            c.CommandType = CommandType.Text;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                HallIDs.Add(int.Parse(dr[0].ToString()));
                comboBoxH_id.Items.Add(dr[0]);
                comboBox3.Items.Add(dr[1]);
            }
            dr.Close();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (conn.State==ConnectionState.Closed)
                 conn.Open();
            panel9.Visible = false;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            panel6.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            panel9.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }
        //************************************************* add new seats ***********************************************************
        private void button3_Click(object sender, EventArgs e)
        {
            string caption = "Seats confirmation";
            getSeatNewId();
            int currentLefted = Convert.ToInt32(count_seat.Text.ToString());
            if (seat_count.Text != "" && seat_price.Text != "" && currentLefted >=Convert.ToInt32(seat_count.Text.ToString()) && Convert.ToInt32( seat_count.Text.ToString()) >0 && currentLefted>=0)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into seat values (:id,:seattype,:price ,:seatcount ,:hallid)";
                cmd.Parameters.Add("id", newS_id);
                cmd.Parameters.Add("seattype", SeatTypeCombo.SelectedItem.ToString());
                cmd.Parameters.Add("price", seat_price.Text);
                cmd.Parameters.Add("seatcount", seat_count.Text);
                cmd.Parameters.Add("hallid", Convert.ToInt32(comboBoxH_id.SelectedItem.ToString()));
                int r = cmd.ExecuteNonQuery();
                if (r != -1)
                {
                    MessageBox.Show("seats are added and their ID is ( " + newS_id + " )", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
                SeatTypeCombo.Text = "";
                comboBoxH_id.Text = "";
                comboBoxC_id.Text = "";
                seat_price.Text = "";
                seat_count.Text = "";
                calculateCurrentSeatsInHall();
            }
            else 
                MessageBox.Show("Seat count is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         
        }
        //updating combo box values  of hall ID 
        private void updateHallIDCombo(object sender, EventArgs e)
        {
                comboBoxH_id.Items.Clear();
                OracleCommand cmd1 = new OracleCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "select hallid , hallname from hall where cinid =:id";
                cmd1.Parameters.Add("id", comboBoxC_ID2.SelectedItem.ToString());
                OracleDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    comboBoxH_id.Items.Add(dr[0]);
                }
                dr.Close();
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
        //get cinema data according to the cinName selected from the combo box
        private void getSelectedCinIDEdit(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select cinid ,cinname ,cinlocation,hallcount from cinema where cinname=:name";
            cmd.Parameters.Add("name", comboBoxCINName.SelectedItem.ToString());
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                selectedEditCinId = Convert.ToInt32(dr[0]);
                editCinName.Text = dr[1].ToString();
                editCinLocation.Text = dr[2].ToString();
                editCinHallNum.Text = dr[3].ToString();

            }
            dr.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //************************************************* delete cinema function connected mode*********************************************
        private void button7_Click(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "Delete from cinema where cinid=:id";
            c.Parameters.Add("id", selectedEditCinId);
            int r = c.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Cinema deleted successfully");
                comboBoxCINName.Text = "";
                editCinName.Text = "";
                editCinLocation.Text = "";
                editCinHallNum.Text = "";
                comboBoxCINName.Items.Clear();
                comboBoxC_id.Items.Clear();
                comboBoxC_ID2.Items.Clear();
                comboBox1.Items.Clear();
                getAllCinemaId_Name();
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        //************************************************* updating data in hall table ***********************************************************

        private void button8_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(conn.State == ConnectionState.Closed)
                    conn.Open();
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                OracleCommand cmd = new OracleCommand(@"update cinema set Hallcount = :count", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("count", dataGridView2.RowCount - 1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // display data in datagridview after updating the selected item in combobox
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string cmd = @"select * from hall where CINID = :id";
            adapter = new OracleDataAdapter(cmd, connString);
            adapter.SelectCommand.Parameters.Add("id", cinemaIDs.ElementAt(comboBox1.SelectedIndex));
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //************************************************* updating data in datagridview ***********************************************************
        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.FormattedValue.ToString() != "" && e.FormattedValue.ToString() != cinemaIDs.ElementAt(comboBox1.SelectedIndex).ToString())
            {
                dataGridView2[e.RowIndex, e.ColumnIndex].Value = cinemaIDs.ElementAt(comboBox1.SelectedIndex).ToString();
            }
        }
        //updating combo box values of hall name
        private void getHallName_fromSelectedCinema(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select h.hallname from hall h , cinema c where h.cinid = c.cinid and c.cinname =:n";
            cmd1.Parameters.Add("n", comboBox2.SelectedItem.ToString());
            OracleDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader[0]);
            }
            reader.Close();
        }
        // ************************update and edit seats data *******************************************************************
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                }
                conn.Open();
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                OracleCommand cmd = new OracleCommand(@"update hall set Hallcapacity = :count", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("count", sum);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmd = @"select * from seat where hallid = :id";
            adapter = new OracleDataAdapter(cmd, connString);
            adapter.SelectCommand.Parameters.Add("id", HallIDs.ElementAt(comboBox2.SelectedIndex));
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        //*****************************************cinema update function******************************************************************************
        private void button6_Click(object sender, EventArgs e)
        {
            if  (editCinName.Text != ""&& editCinLocation.Text != "" && editCinHallNum.Text != "") {
                string caption = "cinema update confirmation";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update cinema set cinname =:name, cinlocation=:location ,hallCount=:hallcount where cinid=:id";
                cmd.Parameters.Add("name", editCinName.Text);
                cmd.Parameters.Add("location", editCinLocation.Text);
                cmd.Parameters.Add("hallCount", Convert.ToInt32(editCinHallNum.Text.ToString()));
                cmd.Parameters.Add("id", selectedEditCinId);
                int r = cmd.ExecuteNonQuery();
                if (r != -1)
                {
                    MessageBox.Show("Cinema is updated successfully ", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    editCinName.Text = "";
                    editCinLocation.Text = "";
                    editCinHallNum.Text = "";
                    comboBoxCINName.Items.Clear();
                    comboBoxC_id.Items.Clear();
                    comboBoxC_ID2.Items.Clear();
                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();
                    getAllCinemaId_Name();
                }
              
            }
            else
            {
                MessageBox.Show("Unvalid data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        // function to get count of seats after action happen in  combobox selection
        public void getCountOfSeats(object sender, EventArgs e)
        {
            calculateCurrentSeatsInHall();

        }
        //calculate current seats in hall after adding them
        public void calculateCurrentSeatsInHall()
        {
            int total_seats = 0;
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select h.hallcapacity,h.hallname,c.cinname from hall h , cinema c where h.cinid = c.cinid and hallid =:id";
            cmd1.Parameters.Add("id", comboBoxH_id.SelectedItem.ToString());
            OracleDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                total_seats = Convert.ToInt32(dr[0]);
                selectedHallName.Text = dr[1].ToString() + " in " + dr[2].ToString();

            }
            dr.Close();
            try
            {
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "select sum(seatcount) from seat where hallid =:id";
                cmd2.Parameters.Add("id", comboBoxH_id.SelectedItem.ToString());
                seatCountLabel = Convert.ToInt32(cmd2.ExecuteScalar());
            }
            catch
            {
                seatCountLabel = 0;
            }

            count_seat.Text = (total_seats - seatCountLabel).ToString();
            
        }
        //get amount of lefted hall that hasn't been addded to the cinema
        private void calculate_leftedHall(object sender, EventArgs e)
        {
            int total=0;
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select hallcount,cinname from cinema where cinid =:id";
            cmd1.Parameters.Add("id",comboBoxC_id.SelectedItem.ToString());
            OracleDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                total = Convert.ToInt32(dr[0]);
                Cemina_Lname.Text = dr[1].ToString();
            }
            dr.Close();
            try
            {
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "select count(*) from Hall where cinid =:id";
                cmd2.Parameters.Add("id", comboBoxC_id.SelectedItem.ToString());
                hallCountL = Convert.ToInt32(cmd2.ExecuteScalar());
            }
            catch
            {
                hallCountL = 0;
            }
            
          count_hall_lefted.Text = (total - hallCountL).ToString();
        }
        //*************************************add hall function ***********************************************
        private void button2_Click(object sender, EventArgs e)
        {
            string caption = "Hall confirmation";
            getHallNewId();
           int  currentLefted = Convert.ToInt32(count_hall_lefted.Text.ToString());
           if (hall_name.Text != "" && hall_capacity.Text != "" && currentLefted!=0)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into Hall values (:id,:name,:capacity ,:cinemaId)";
                    cmd.Parameters.Add("id", newH_id);
                    cmd.Parameters.Add("name", hall_name.Text.ToString());
                    cmd.Parameters.Add("capacity", hall_capacity.Text);
                    cmd.Parameters.Add("cinemaId", Convert.ToInt32(comboBoxC_id.SelectedItem.ToString()));
                    int r = cmd.ExecuteNonQuery();
                    if (r != -1)
                    {
                        MessageBox.Show("Hall is added and it's ID is ( " + newH_id + " ) Please don't forget to add the Seats detailes"
                            , caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    hall_name.Text = "";
                    hall_capacity.Text = "";
                    currentLefted--;
                    count_hall_lefted.Text = currentLefted.ToString();
                }
            }      
    }
}
