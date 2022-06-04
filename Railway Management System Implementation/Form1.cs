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
        String ConnectionString = DBString.name;
        SqlConnection con;
        SqlCommand com;
        SqlDataReader reader;
        Form tableForm;
        List<String> tableNames = new List<String>();

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            con = new SqlConnection(ConnectionString);
            con.Open();
            com = new SqlCommand();
            KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode){
                case Keys.Enter:
                    break;
            }
        }

        void writeSqlQuery(String query)
        {
            if (con.State != ConnectionState.Open)
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
            }
            com.CommandText = query;
            com.Connection = con;
            com.CommandType = CommandType.Text;
            int affectedRows = com.ExecuteNonQuery();
            if(affectedRows > 0)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Fail");
            }
            con.Close();
        }

        String executeQuery()
        {
            if (con.State != ConnectionState.Open)
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
            }
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
            // TODO: This line of code loads data into the 'railwaySystemManualDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.railwaySystemManualDataSet.Employee);
            //Get All Tables Name
            if (con.State != ConnectionState.Open)
            {
                con = new SqlConnection(ConnectionString);
                con.Open();
            }
            com.CommandText = "SELECT name FROM sys.sysobjects  WHERE XTYPE = 'U'";
            com.Connection = con;
            com.CommandType = CommandType.Text;

            String view = executeQuery();
            String[] tableNames = view.Split('~');

            //Create Button For Each Table Name
            for (int i = 0; i < tableNames.Length - 1; i++)
            {
                Button newButton = new Button();
                newButton.Text = tableNames[i];
                newButton.Location = new Point(70, 35 * i + 65);
                newButton.Size = new Size(150, 30);
                newButton.Visible = true;
                newButton.BringToFront();
                newButton.Name = tableNames[i];
                newButton.Click += new EventHandler(this.button_Click);

                this.Controls.Add(newButton);
            }

            con.Close();
        }


        private void button_Click(object sender, EventArgs e)
        {


            Button btn = sender as Button;
            //MessageBox.Show(btn.Name);
            Form pnn = new Form();
            tableForm = pnn;
            
            pnn.Text = btn.Text + " Table";
            pnn.Width = 1000;
            pnn.Height = 600;
            pnn.StartPosition = FormStartPosition.CenterScreen;


            using (con)
            {
                if (con.State != ConnectionState.Open)
                {
                    con = new SqlConnection(ConnectionString);
                    con.Open();
                }
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM " + btn.Name, con);
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);

                DataGridView dgv = new DataGridView();
                dgv.DataSource = dtb;
                dgv.Width = 800;
                dgv.Location = new Point(100, 0);
                pnn.Controls.Add(dgv);
            }

            Button newButton = new Button();
            newButton.Text = "Update " + btn.Name;
            newButton.Location = new Point(450, 170);
            newButton.Size = new Size(150, 30);
            newButton.Visible = true;
            newButton.BringToFront();
            newButton.Name = "Update~" + btn.Name;
            newButton.Click += new EventHandler(this.queryButtonClick);

            pnn.Controls.Add(newButton);


            newButton = new Button();
            newButton.Text = "Insert " + btn.Name;
            newButton.Location = new Point(450, 210);
            newButton.Size = new Size(150, 30);
            newButton.Visible = true;
            newButton.BringToFront();
            newButton.Name = "Insert~" + btn.Name;
            newButton.Click += new EventHandler(this.queryButtonClick);

            pnn.Controls.Add(newButton);


            newButton = new Button();
            newButton.Text = "Delete " + btn.Name;
            newButton.Location = new Point(450, 250);
            newButton.Size = new Size(150, 30);
            newButton.Visible = true;
            newButton.BringToFront();
            newButton.Name = "Delete~"+btn.Name;
            newButton.Click += new EventHandler(this.queryButtonClick);

            pnn.Controls.Add(newButton);

            pnn.Show();
            con.Close();
        }

        void queryButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String btnName = btn.Name;
            String query = btnName.Split('~')[0];
            String tableName = btnName.Split('~')[1];

            if (query == "Delete")
            {
                DeleteForm df = new DeleteForm();
                df.table = tableName;
                df.Show();
            }
            else if(query == "Update")
            {
                UpdateForm uf = new UpdateForm();
                uf.table = tableName;
                uf.Show();
            }
            else if(query == "Insert")
            {
                InsertForm iF = new InsertForm();
                iF.table = tableName;
                iF.Show();
            }
        }

    }
}
