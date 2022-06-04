using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Railway_Management_System_Project
{
    public partial class Form1 : Form
    {
        String ConnectionString = "Data Source=DESKTOP-23ET8U1\\SQL2019;Initial Catalog=RailwaySystemManual;Integrated Security=True";
        SqlConnection con;
        SqlCommand com;
        SqlDataReader reader;

        List<String> tableNames = new List<String>();

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            con = new SqlConnection(ConnectionString);
            com = new SqlCommand();
        }

        void openConnection()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Close();
                con.Open();
            }
        }

        void writeSqlQuery(String query)
        {
            openConnection();
            com.CommandText = query;
            com.Connection = con;
            com.CommandType = CommandType.Text;
            con.Close();
        }

        String executeQuery()
        {
            openConnection();
            String view = "";
            reader = com.ExecuteReader();

            while (reader.Read())
            {
                view += reader[0];
                view += "~";
            }

            reader.Close();
            con.Close();

            //MessageBox.Show(view);
            return view;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get All Tables Name
            openConnection();
            writeSqlQuery("SELECT name FROM sys.sysobjects  WHERE XTYPE = 'U'");
            String view = executeQuery();
            String[] tableNames = view.Split('~');

            for (int i = 0; i < tableNames.Length - 1; i++)
            {
                Button newButton = new Button();
                newButton.Text = tableNames[i];
                newButton.Location = new Point(70, 35 * i + 65);
                newButton.Size = new Size(150, 30);
                newButton.Visible = true;
                newButton.BringToFront();
                this.Controls.Add(newButton);
            }

            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openConnection();
        }
    }
}
