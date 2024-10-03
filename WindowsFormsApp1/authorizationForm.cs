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

namespace WindowsFormsApp1
{
    public partial class authorizationForm : Form
    {
        public authorizationForm()
        {
            InitializeComponent();

            button1.Enabled = false;
            button1.Text = "REGISTER";
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.BorderColor = Color.DarkBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Times New Roman", 20);
            button1.ForeColor = Color.DarkBlue;
            button1.BackColor = Color.AliceBlue;

            button2.Text = "LOG IN";
            button2.ForeColor = Color.DarkBlue;
            button2.BackColor = Color.AliceBlue;
            button2.Font = new Font("Times New Roman", 20);
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 3;
            button2.FlatAppearance.BorderColor = Color.DarkBlue;

            textBox1.Visible = false;
            textBox1.BackColor = Color.GhostWhite;
            textBox1.Text = "ENTER NAME";
            textBox1.ForeColor = Color.LightSlateGray;

            textBox2.BackColor = Color.GhostWhite;
            textBox2.Text = "ENTER PHONE NUMBER";
            textBox2.ForeColor = Color.LightSlateGray;

            checkBox1.Text = "First time here?";
            checkBox1.Font = new Font("Times New Roman", 16);
            checkBox1.ForeColor = Color.DarkBlue;

            textBox3.BackColor = Color.GhostWhite;
            textBox3.Text = "ENTER PASSWORD";
            textBox3.ForeColor = Color.LightSlateGray;

            this.Text = "Enter";
            this.ActiveControl = button2;
            this.BackColor = SystemColors.InactiveCaption;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.ForeColor != Color.LightSlateGray && textBox2.ForeColor != Color.LightSlateGray && textBox3.ForeColor != Color.LightSlateGray)
            {
                DataBase dataBase = new DataBase();
                dataBase.openConnection();
                try
                {
                    string comDel = " INSERT INTO Person([name], [phone_number], [password], [role_id]) Values(@name, @phone_number, @password, 1)";
                    SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());
                    SqlParameter pr1, pr2, pr3;

                    pr1 = new SqlParameter("@name", textBox1.Text);
                    pr2 = new SqlParameter("@phone_number", textBox2.Text);
                    pr3 = new SqlParameter("@password", textBox3.Text);

                    cmd1.Parameters.Add(pr1);
                    cmd1.Parameters.Add(pr2);
                    cmd1.Parameters.Add(pr3);
                    cmd1.ExecuteNonQuery();
                }
                catch{ MessageBox.Show("Пользователь уже зарегистрирован"); }

                dataBase.closeConnection();
            }
            else
                MessageBox.Show("Вы заполнили не все поля");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();

            dataBase.openConnection();
            try
            {
                List<string> list = new List<string>();
                dataBase.openConnection();
                string comDel = " SELECT id, role_id FROM Person WHERE phone_number = @phone_number and password = @password";
                SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

                SqlParameter pr1, pr2;
                pr1 = new SqlParameter("@phone_number", textBox2.Text);
                pr2 = new SqlParameter("@password", textBox3.Text);
                cmd1.Parameters.Add(pr1);
                cmd1.Parameters.Add(pr2);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                    foreach (var item in dr.ItemArray) 
                        list.Add(item.ToString());

                dataBase.closeConnection();


                if (list[1] == "1" || list[1] == "2")
                {
                    bookListForm form4 = new bookListForm(list[0], list[1]);
                    form4.Show();
                    this.Hide();
                }
                if (list[1] == "3")
                {
                    peopleForm form6 = new peopleForm();
                    form6.Show();
                    this.Hide();
                }
            }
            catch { MessageBox.Show("Пользователя не существует"); }

            dataBase.closeConnection();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = Color.LightSlateGray;
                textBox1.Text = "ENTER NAME";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = Color.LightSlateGray;
                textBox2.Text = "ENTER PHONE NUMBER";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.ForeColor = Color.LightSlateGray;
                textBox3.Text = "ENTER PASSWORD";
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "ENTER PASSWORD" && textBox3.ForeColor == Color.LightSlateGray)
            {
                textBox3.ForeColor = Color.DarkBlue;
                textBox3.Text = "";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "ENTER NAME" && textBox1.ForeColor == Color.LightSlateGray)
            {
                textBox1.ForeColor = Color.DarkBlue;
                textBox1.Text = "";
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "ENTER PHONE NUMBER" && textBox2.ForeColor == Color.LightSlateGray)
            {
                textBox2.ForeColor = Color.DarkBlue;
                textBox2.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                textBox1.Visible = false;
                button1.Enabled = false;
            }
            else
            {
                textBox1.Visible = true;
                button1.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
