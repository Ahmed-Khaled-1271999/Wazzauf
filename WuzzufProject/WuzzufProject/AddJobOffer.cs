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
    public partial class AddJobOffer : Form
    {
        public AddJobOffer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddJobOffer_Load(object sender, EventArgs e)
        {
            string conString = "Data source=orcl; User Id =hr; password=hr;";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select Title from categories";
            OracleDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
                comboBox1.Items.Add(odr[0]);
            odr.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("ERROR! Kindly, complete the form.");
            }
            else
            {
                int latestID, newID, r;
                string conString = "Data source=orcl; User Id =hr; password=hr;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                OracleCommand cmd = new OracleCommand();
                OracleCommand qmd = new OracleCommand();
                OracleCommand cmd1 = new OracleCommand();
                OracleCommand cmd2 = new OracleCommand();
                cmd.Connection = con;
                qmd.Connection = con;
                cmd1.Connection = con;
                cmd2.Connection = con;

                string myQuery = "select * from employers where username ='" + textBox1.Text.Trim() + "' and password = '" + textBox2.Text.Trim() + "'";
                OracleDataAdapter odp = new OracleDataAdapter(myQuery, con);

                DataTable dt = new DataTable();
                odp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    qmd.CommandType = CommandType.StoredProcedure;
                    qmd.CommandText = "JobsOffersLastID";
                    qmd.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
                    qmd.ExecuteNonQuery();
                    try
                    {
                        latestID = Convert.ToInt32(qmd.Parameters["id"].Value.ToString());
                        newID = latestID + 1;
                    }
                    catch
                    {
                        newID = 1;
                    }
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "GetEmpID";
                    cmd1.Parameters.Add("tempID", OracleDbType.Int32, ParameterDirection.Output);
                    cmd1.Parameters.Add("Uname", textBox1.Text);
                    cmd1.Parameters.Add("Pword", textBox2.Text);
                    cmd1.ExecuteNonQuery();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "GetCategoryNumber";
                    cmd2.Parameters.Add("tempCID", OracleDbType.Int32, ParameterDirection.Output);
                    cmd2.Parameters.Add("ctitle", comboBox1.SelectedItem.ToString());
                    cmd2.ExecuteNonQuery();
                    int empid = Convert.ToInt32(cmd1.Parameters["tempID"].Value.ToString());
                    int categoryNum = Convert.ToInt32(cmd2.Parameters["tempCID"].Value.ToString());
                    cmd.CommandText = "insert into JobsOffers values(:NM, :PT, :CN, :EMPID, :SL, :STATUS)";
                    cmd.Parameters.Add("NM", newID);
                    cmd.Parameters.Add("PT", textBox3.Text);
                    cmd.Parameters.Add("CN", categoryNum);
                    cmd.Parameters.Add("EMPID", empid);
                    cmd.Parameters.Add("SL", textBox4.Text);
                    cmd.Parameters.Add("STATUS", "AV");
                    r = cmd.ExecuteNonQuery();
                    if (r != -1)
                    {
                        MessageBox.Show("A new job offer is added successfully!");
                    }
                }
                else { MessageBox.Show("ERROR! Invalid username or password."); }
                con.Close();
            }
        }
    }
}
