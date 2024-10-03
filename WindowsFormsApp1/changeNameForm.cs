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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class changeNameForm : Form
    {

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public changeNameForm(string id)
        {
            this.id = id;
            InitializeComponent();

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Text = "";

            button1.ForeColor = Color.DarkBlue;
            button1.BackColor = Color.AliceBlue;
            button1.Font = new Font("Times New Roman", 20);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Text = "ACCEPT";
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.BorderColor = Color.DarkBlue;

            textBox1.BackColor = Color.GhostWhite;
            textBox1.Font = new Font("Times New Roman", 14);
            textBox1.Text = "ENTER PHONE NUMBER";
            textBox1.ForeColor = Color.LightSlateGray;

            textBox2.Font = new Font("Times New Roman", 14);
            textBox2.BackColor = Color.GhostWhite;
            textBox2.Text = "ENTER NEW NAME";
            textBox2.ForeColor = Color.LightSlateGray;

            this.BackColor = Color.AliceBlue;
            this.Text = "Change name";
            this.ActiveControl = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataBase dataBase = new DataBase();
                dataBase.openConnection();

                string comDel = "UPDATE Person Set name = @name WHERE phone_number = @phone_number";
                SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

                SqlParameter pr1, pr2;
                pr1 = new SqlParameter("@name", textBox2.Text);
                pr2 = new SqlParameter("@phone_number", textBox1.Text);
                cmd1.Parameters.Add(pr1);
                cmd1.Parameters.Add(pr2);

                cmd1.ExecuteNonQuery();

                dataBase.closeConnection();
            }
            catch { MessageBox.Show("Неверный номер телефона"); }

            accoundDataForm form3 = new accoundDataForm(Id);
            form3.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            accoundDataForm form3 = new accoundDataForm(Id);
            form3.Show();
            this.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = Color.LightSlateGray;
                textBox1.Text = "ENTER PHONE NUMBER";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = Color.LightSlateGray;
                textBox2.Text = "ENTER NEW NAME";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "ENTER PHONE NUMBER" && textBox1.ForeColor == Color.LightSlateGray)
            {
                textBox1.ForeColor = Color.DarkBlue;
                textBox1.Text = "";
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "ENTER NEW NAME" && textBox2.ForeColor == Color.LightSlateGray)
            {
                textBox2.ForeColor = Color.DarkBlue;
                textBox2.Text = "";
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            accoundDataForm form3 = new accoundDataForm(Id);
            form3.Show();
            this.Close();
        }
    }
}
