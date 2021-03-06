﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            button5.MouseEnter += new EventHandler(button5_move);
            button5.MouseLeave += new EventHandler(button5_leave);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            HomeUC home = new HomeUC(); 
            panel1.Controls.Add(home);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            SearchUC UC = new SearchUC();
            panel1.Controls.Add(UC);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Profile UC = new Profile();
            panel1.Controls.Add(UC);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
   
        private void button5_move(object sender, EventArgs e)
        {
            button5.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.exit1));
        }
        private void button5_leave(object sender, EventArgs e)
        {
            button5.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.exit));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            AddCinema Cinema = new AddCinema();
            panel1.Controls.Add(Cinema);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Movie movie = new Movie();
            movie.TopLevel = false;
            panel1.Controls.Add(movie);
            movie.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            movie.Dock = DockStyle.Fill;
            movie.Show();
        }
    }
}
