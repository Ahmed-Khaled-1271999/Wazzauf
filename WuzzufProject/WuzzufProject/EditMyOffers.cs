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
    public partial class EditMyOffers : Form
    {
        DataSet ds;
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        public EditMyOffers()
        {
            InitializeComponent();
        }

        // Cancel Btn
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // 
        private void EditMyOffers_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }


        // Update Btn
        private void button2_Click(object sender, EventArgs e)
        {
            // B:2 == update using OracleCommandBuilder
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
            MessageBox.Show("Your offer was updated successfully!");
        }

        // UI>> Show Btn
        // load The Table locally on a Dataset Object Table
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kindly, confirm your login information.");
            }
            else
            {
                // Connection
                string conString = "Data source = orcl; User Id = hr; password = hr;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();

                // Commands
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetEmpID";

                // feed procedure parameters
                // procdure head -> create or replace PROCEDURE GETEMPID (tempID out number, Uname in varchar2, Pword in varchar2)
                /**
                select id
                into tempID
                from employers
                where username = uname
                and password = pword;
                 */
                cmd.Parameters.Add("tempID", OracleDbType.Int32, ParameterDirection.Output);
                cmd.Parameters.Add("Uname", textBox1.Text);
                cmd.Parameters.Add("Pword", textBox2.Text);

                // execute
                cmd.ExecuteNonQuery();

                // obtain outputs
                int id = Convert.ToInt32(cmd.Parameters["tempID"].Value.ToString());


                // B:1 == certain rows selection based on a user value ( username and password)  
                string comString = "select numb, position, salary, status from jobsoffers where employerid = :id";
              

                adapter = new OracleDataAdapter(comString, conString);
                adapter.SelectCommand.Parameters.Add("id", id);

                ds = new DataSet();

                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

                // Connection end
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
