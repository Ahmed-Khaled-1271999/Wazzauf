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

namespace WuzzufProject
{
    public partial class EmployerMenu : Form
    {
        public EmployerMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddJobOffer ajo = new AddJobOffer();
            ajo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditMyOffers emo = new EditMyOffers();
            emo.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Applications app = new Applications();
            app.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login L = new Login();
            L.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Login L = new Login();
            L.Show();
        }
    }
}
