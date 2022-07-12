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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            this.Hide();
            m.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string myConnection = "Data source = orcl; User Id=hr; password=hr;";
            OracleConnection con = new OracleConnection(myConnection);
            con.Open();

            string myQuery1 = "select * from developers where username ='" + textBox1.Text.Trim() + "' and password = '" + textBox2.Text.Trim() + "'";
            string myQuery2 = "select * from employers where username ='" + textBox1.Text.Trim() + "' and password = '" + textBox2.Text.Trim() + "'";
            string myQuery3 = "select * from applicants where username ='" + textBox1.Text.Trim() + "' and password = '" + textBox2.Text.Trim() + "'";
            OracleDataAdapter odp1 = new OracleDataAdapter(myQuery1, myConnection);
            OracleDataAdapter odp2 = new OracleDataAdapter(myQuery2, myConnection);
            OracleDataAdapter odp3 = new OracleDataAdapter(myQuery3, myConnection);

            DataTable dt1 = new DataTable();
            odp1.Fill(dt1);
            DataTable dt2 = new DataTable();
            odp2.Fill(dt2);
            DataTable dt3 = new DataTable();
            odp3.Fill(dt3);

            if (dt1.Rows.Count == 1)
            {
                this.Hide();
                DeveloperMenu dm = new DeveloperMenu();
                dm.Show();
            }
            else if (dt2.Rows.Count == 1)
            {
                this.Hide();
                EmployerMenu em = new EmployerMenu();
                em.Show();
            }
            else if (dt3.Rows.Count == 1)
            {
                this.Hide();
                ApplicantMenu am = new ApplicantMenu();
                am.Show();
            }
            else if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kindly enter your login information.");
            }
            else
            {
                MessageBox.Show("ERROR! Invalid username or password.");
            }
            con.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
