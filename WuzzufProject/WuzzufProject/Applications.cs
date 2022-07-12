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
    public partial class Applications : Form
    {
        public Applications()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kindly, confirm your login information.");
            }
            else
            {
                dataGridView1.Rows.Clear();
                string conString = "Data source = orcl; User Id = hr; password = hr;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetEmpID";
                cmd.Parameters.Add("tempID", OracleDbType.Int32, ParameterDirection.Output);
                cmd.Parameters.Add("Uname", textBox1.Text);
                cmd.Parameters.Add("Pword", textBox2.Text);
                cmd.ExecuteNonQuery();
                int id = Convert.ToInt32(cmd.Parameters["tempID"].Value.ToString());
                OracleCommand CursorCommand = new OracleCommand();
                CursorCommand.Connection = con;
                CursorCommand.CommandType = CommandType.StoredProcedure;
                CursorCommand.CommandText = "GetApps";
                CursorCommand.Parameters.Add("tempID", id);
                CursorCommand.Parameters.Add("cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                OracleDataReader odr = CursorCommand.ExecuteReader();
                while(odr.Read())
                {
                    dataGridView1.Rows.Add(odr[0],odr[1],odr[2],odr[3],odr[4],odr[5]);
                }
                con.Close();
            }
        }

        private void Applications_Load(object sender, EventArgs e)
        {
        }
    }
}
