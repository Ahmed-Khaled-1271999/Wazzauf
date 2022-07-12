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
    public partial class DeveloperMenu : Form
    {
        public DeveloperMenu()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection("Data source = orcl; User Id=hr; password=hr;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "S1R";
            int job_num = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            cmd.Parameters.Add("n1", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("n2", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("JID", job_num);
            cmd.ExecuteNonQuery();
            textBox1.Text = cmd.Parameters["n1"].Value.ToString();
            textBox2.Text = cmd.Parameters["n2"].Value.ToString();
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void DeveloperMenu_Load(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection("Data source = orcl; User Id=hr; password=hr;");
            con.Open();
            OracleCommand emp_cmd = new OracleCommand();
            emp_cmd.Connection = con;
            emp_cmd.CommandText = "select numb from jobsoffers";

            OracleDataReader dr1 = emp_cmd.ExecuteReader();
            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1[0]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrystalForm cf = new CrystalForm();
            cf.Show();
        }
    }
}
