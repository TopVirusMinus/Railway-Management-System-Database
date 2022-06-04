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
    public partial class InsertForm : Form
    {
        public String table;
        string cols = "";
        List<string> vals;
        string values;

        public InsertForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InsertForm_Load(object sender, EventArgs e)
        {
            String ConnectionString = DBString.name;
            SqlConnection con = new SqlConnection(ConnectionString); ;
            con.Open();

            SqlCommand com = new SqlCommand(); ;
            com.CommandText = "SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + table + "' ORDER BY ORDINAL_POSITION";
            com.Connection = con;
            com.CommandType = CommandType.Text;

            SqlDataReader reader = com.ExecuteReader();
            int ct = 1;
            
            vals = new List<string>();

            while (reader.Read())
            {
                if(reader[0]+"" == "id")
                {
                    continue;
                }
                else
                {
                    cols += reader[0] + ",";
                    //MessageBox.Show(reader[1] + "");
                    if (reader[1] + "" != "int" && reader[1] + "" != "float" && reader[1] + "" != "double")
                    {
                        vals.Add("");
                    }
                    else
                    {
                        vals.Add("''");
                    }
                }

                TextBox t = new TextBox();
                Label l = new Label();
                
                t.Location = new Point(70, 35 * ct + 65);
                t.Size = new Size(150, 30);
                t.Visible = true;
                t.BringToFront();

                l.Text = reader[0]+"";
                l.Location = new Point(70, 35 * ct+50);
                l.Visible = true;
                l.BringToFront();
                
                this.Controls.Add(t);
                this.Controls.Add(l);

                ct++;
            }
            cols = cols.Remove(cols.Length - 1);

            //MessageBox.Show(cols+"");
            Button newButton = new Button();
            newButton.Text = "Insert " + table + " Record";
            newButton.Location = new Point(450, 250);
            newButton.Size = new Size(150, 30);
            newButton.Visible = true;
            newButton.BringToFront();
            newButton.Click += new EventHandler(this.queryButtonClick);
            this.Controls.Add(newButton);
        }

        int i = 0;
        void queryButtonClick(object sender, EventArgs e)
        {
            foreach(Control control in this.Controls)
            {
                if(control.GetType() == typeof(TextBox))
                {
                    //MessageBox.Show(control.Text + "");
                    if(vals[i] == "")
                    {
                        values += "'" + control.Text + "',";
                    }
                    else
                    {   
                        values += control.Text + ",";
                    }
                    //INSERT INTO table_name(column1, column2, column3, ...)
                    //VALUES(value1, value2, value3, ...);
                    i++;
                }
            }
            String ConnectionString = DBString.name;
            SqlConnection con = new SqlConnection(ConnectionString); ;
            con.Open();

            SqlCommand com = new SqlCommand(); ;

            values = values.Remove(values.Length - 1);
            this.Text = "INSERT INTO " + table + "(" + cols + ") " + "VALUES(" + values +");"; 
            com.CommandText = "INSERT INTO " + table + "(" + cols + ") " + "VALUES(" + values +");"; 
            //MessageBox.Show(values);

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
    }
}
