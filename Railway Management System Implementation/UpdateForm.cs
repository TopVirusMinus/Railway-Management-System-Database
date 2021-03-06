using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Railway_Management_System_Project
{
    public partial class UpdateForm : Form
    {
        public String table = "";
        public UpdateForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Load += UpdateForm_Load;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            String ConnectionString = DBString.name;
            SqlConnection con = new SqlConnection(ConnectionString); ;
            con.Open();

            SqlCommand com = new SqlCommand(); ;
            com.CommandText = "SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + table + "' ORDER BY ORDINAL_POSITION";
            com.Connection = con;
            com.CommandType = CommandType.Text;

            SqlDataReader reader = com.ExecuteReader();
        
            while (reader.Read())
            {
                if ((string)reader[0] != "id")
                {
                    comboBox1.Items.Add(reader[0]+"-"+reader[1]);
                }
                comboBox2.Items.Add(reader[0]+"-"+reader[1]);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ConnectionString = DBString.name;
            SqlConnection con = new SqlConnection(ConnectionString); ;
            con.Open();

            SqlCommand com = new SqlCommand(); ;
            
            String l1 = comboBox1.SelectedItem.ToString();
            String []l1Arr = l1.Split('-');            
            String l2 = comboBox2.SelectedItem.ToString();
            String []l2Arr = l2.Split('-');

            if ((l1Arr[1] == "int" || l1Arr[1] == "float" || l1Arr[1] == "double") && (l2Arr[1] == "int" || l2Arr[1] == "float" || l2Arr[1] == "double"))
            {
                com.CommandText = "UPDATE " + table + " SET " + l1Arr[0] + " = " + textBox2.Text + " WHERE " + l2Arr[0] + " = " + textBox1.Text;
                this.Text = "UPDATE " + table + " SET " + l1Arr[0] + " = " + textBox2.Text + " WHERE " + l2Arr[0] + " = " + textBox1.Text;
            }
            else if((l1Arr[1] != "int" && l1Arr[1] != "float" && l1Arr[1] != "double") && (l2Arr[1] != "int" && l2Arr[1] != "float" && l2Arr[1] != "double"))
            {
                com.CommandText = "UPDATE " + table + " SET " + l1Arr[0] + " = " + "'" + textBox2.Text + "'" + " WHERE " + l2Arr[0] + " = " + "'" + textBox1.Text + "'";
                this.Text = "UPDATE " + table + " SET " + l1Arr[0] + " = " + "'" + textBox2.Text + "'" + " WHERE "+     l2Arr[0] + " = " + "'" + textBox1.Text + "'";
            }
            else if((l1Arr[1] != "int" && l1Arr[1] != "float" && l1Arr[1] != "double") && (l2Arr[1] == "int" || l2Arr[1] == "float" || l2Arr[1] == "double"))
            {
                com.CommandText = "UPDATE " + table + " SET " + l1Arr[0] + " = " + "'" + textBox2.Text + "'" + " WHERE " + l2Arr[0] + " = " + textBox1.Text;
                this.Text = "UPDATE " + table + " SET " + l1Arr[0] + " = " + "'" + textBox2.Text + "'" + " WHERE "+ l2Arr[0] + " = " + textBox1.Text;
            }
            else if((l1Arr[1] == "int" || l1Arr[1] == "float" || l1Arr[1] == "double") && (l2Arr[1] != "int" && l2Arr[1] != "float" && l2Arr[1] != "double"))
            {
                 com.CommandText = "UPDATE " + table + " SET " + l1Arr[0] + " = " + textBox2.Text + " WHERE " + l2Arr[0] + " = " + "'" + textBox1.Text + "'";
                this.Text = "UPDATE " + table + " SET " + l1Arr[0] + " = " + textBox2.Text + " WHERE " + l2Arr[0] + " = " + "'" + textBox1.Text + "'";
            }
            com.Connection = con;
            com.CommandType = CommandType.Text;

            int affectedRows = com.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
