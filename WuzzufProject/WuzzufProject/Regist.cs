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
    public partial class Regist : Form
    {
        public Regist()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            this.Hide();
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int newID, latestID, r;
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Please complete the registration form!");
            }
            else if (textBox7.Text != textBox2.Text)
            {
                MessageBox.Show("ERROR! Password mismatch.");
            }
            else
            {
                string conString = "Data source=orcl; User Id =hr; password=hr;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                OracleCommand qmd = new OracleCommand();
                qmd.Connection = con;

                string myQuery1 = "select * from employers where username ='" + textBox1.Text.Trim() + "'";
                string myQuery2 = "select * from applicants where username ='" + textBox1.Text.Trim() + "'";
                string myQuery3 = "select * from developers where username ='" + textBox1.Text.Trim() + "'";
                OracleDataAdapter odp1 = new OracleDataAdapter(myQuery1, conString);
                OracleDataAdapter odp2 = new OracleDataAdapter(myQuery2, conString);
                OracleDataAdapter odp3 = new OracleDataAdapter(myQuery3, conString);
                DataTable dt1 = new DataTable();
                odp1.Fill(dt1);
                DataTable dt2 = new DataTable();
                odp2.Fill(dt2);
                DataTable dt3 = new DataTable();
                odp3.Fill(dt3);

                if (dt1.Rows.Count == 1 || dt2.Rows.Count == 1 || dt3.Rows.Count == 1)
                    MessageBox.Show("ERROR! Username does exist, please change.");
                else if (comboBox1.SelectedItem.ToString() == "Employer")
                {
                    qmd.CommandType = CommandType.StoredProcedure;
                    qmd.CommandText = "GetLastID1";

                    if (textBox9.Text == "")
                        MessageBox.Show("Kindly, enter company name.");
                    else
                    {
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

                        cmd.CommandText = "insert into Employers values(:EID, :FN, :CM, :EM, :PH, :UN, :PW, :GN, :BD, :LN)";
                        cmd.Parameters.Add("EID", newID);
                        cmd.Parameters.Add("FN", textBox3.Text);
                        cmd.Parameters.Add("CM", textBox9.Text);
                        cmd.Parameters.Add("EM", textBox5.Text);
                        cmd.Parameters.Add("PH", textBox6.Text);
                        cmd.Parameters.Add("UN", textBox1.Text);
                        cmd.Parameters.Add("PW", textBox7.Text);
                        if (radioButton1.Checked)
                            cmd.Parameters.Add("GN", radioButton1.Text);
                        else if (radioButton2.Checked)
                            cmd.Parameters.Add("GN", radioButton2.Text);
                        cmd.Parameters.Add("BD", Convert.ToDateTime(textBox8.Text));
                        cmd.Parameters.Add("LN", textBox4.Text);
                        r = cmd.ExecuteNonQuery();
                        if (r != -1)
                        {
                            MessageBox.Show("You registered as an Employer successfully!");
                        }
                    }
                }
                else if (comboBox1.SelectedItem.ToString() == "Applicant")
                {
                    qmd.CommandType = CommandType.StoredProcedure;
                    qmd.CommandText = "GetLastID";
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
                    cmd.CommandText = "insert into Applicants values(:AID, :FN, :LN, :BD, :EM, :PH, :UN, :PW, :GN)";
                    cmd.Parameters.Add("AID", newID);
                    cmd.Parameters.Add("FN", textBox3.Text);
                    cmd.Parameters.Add("LN", textBox4.Text);
                    cmd.Parameters.Add("BD", Convert.ToDateTime(textBox8.Text));
                    cmd.Parameters.Add("EM", textBox5.Text);
                    cmd.Parameters.Add("PH", textBox6.Text);
                    cmd.Parameters.Add("UN", textBox1.Text);
                    cmd.Parameters.Add("PW", textBox7.Text);
                    if (radioButton1.Checked)
                        cmd.Parameters.Add("GN", radioButton1.Text);
                    else if (radioButton2.Checked)
                        cmd.Parameters.Add("GN", radioButton2.Text);
                    r = cmd.ExecuteNonQuery();
                    if (r != -1)
                    {
                        MessageBox.Show("You registered as an Applicant successfully!");
                    }
                }
                con.Close();
            }
        }
    }
}
