using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class bookListForm : Form
    {
        private string roleId;
        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public bookListForm(string id, string roleId)
        {
            InitializeComponent();

            RoleId = roleId;
            Id = id;

            this.Text = "Library";
            this.BackColor = Color.AliceBlue;
            this.ForeColor = Color.DarkBlue;


            textBox1.BackColor = Color.GhostWhite;
            textBox2.BackColor = Color.GhostWhite;

            textBox1.ForeColor = Color.DarkBlue;
            textBox2.ForeColor = Color.DarkBlue;


            label2.Text = "Book id";
            label3.Text = "Reader phone number";

            button3.Text = "ACCEPT CHANGING";
            button4.Text = "GIVE";
            button5.Text = "RETURN";

            button2.Text = "BOOK";


            dataGridView1.DefaultCellStyle.ForeColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;

            if (roleId == "2")
            {
                button1.Visible = false;
                dataGridView1.ReadOnly = false;
                button2.Visible = false;
                comboBox1.Visible = false;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                label3.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                label1.Visible = false;
            }
            else
            {
                button1.Visible = true;
                dataGridView1.ReadOnly = true;
                button2.Visible = true;
                comboBox1.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                label3.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                label1.Visible = true;


                DataBase dataBase = new DataBase();
                dataBase.openConnection();
                string comDel = "SELECT Book.id FROM Book WHERE Book.status_id = 1";

                SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

                SqlParameter pr1;
                pr1 = new SqlParameter("@reader_id", Id);
                cmd1.Parameters.Add(pr1);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (var item in dr.ItemArray)
                    {
                        comboBox1.Items.Add(item.ToString());
                    }
                }

                if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Items.Add("Нет доступных книг");
                    comboBox1.Enabled = false;
                }

                comboBox1.SelectedIndex = 0;

            }

            label1.Text = "ДОСТУПНЫЕ КНИГИ:";
        }


        SqlDataAdapter dataAdapterWorker;
        DataTable dataTableWorker;

        private void Form4_Load(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Text = "";
            string cmd;
            if(RoleId == "1")
                cmd = "SELECT Book.id, Book.name, Book.year, Book.publisher, " +
                    "\r\n\tcase" +
                    "\r\n\t\tWHEN Availability.status = 1 then 'Доступна'" +
                    "\r\n\t\tWHEN Availability.status = 0 then 'Недоступна'" +
                    "\r\n\t\tELSE NULL" +
                    "\r\n\tend as status" +
                    "\r\nFROM Book" +
                    "\r\nJOIN Availability ON Book.status_id = Availability.id;";
            else
                cmd = "SELECT * From Book";


            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            dataAdapterWorker = new SqlDataAdapter(cmd, dataBase.GetConnection());
            dataTableWorker = new DataTable();
            dataAdapterWorker.Fill(dataTableWorker);
            dataGridView1.DataSource = dataTableWorker;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            accoundDataForm form3 = new accoundDataForm(Id);
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();



            string comDel = "UPDATE Book Set status_id = @status_id WHERE id = @id";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1, pr2;
            pr1 = new SqlParameter("@status_id", "2");
            pr2 = new SqlParameter("@id", comboBox1.SelectedItem.ToString());
            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);

            cmd1.ExecuteNonQuery();




            dataBase.closeConnection();



            string cmd;

            cmd = "SELECT Book.id, Book.name, Book.year, Book.publisher, " +
                "\r\n\tcase" +
                "\r\n\t\tWHEN Availability.status = 1 then 'Доступна'" +
                "\r\n\t\tWHEN Availability.status = 0 then 'Недоступна'" +
                "\r\n\t\tELSE NULL" +
                "\r\n\tend as status" +
                "\r\nFROM Book" +
                "\r\nJOIN Availability ON Book.status_id = Availability.id;";

            dataBase.openConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd, dataBase.GetConnection());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;













            dataBase.openConnection();
            comDel = "SELECT Book.id FROM Book WHERE Book.status_id = 1";

            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());


            pr1 = new SqlParameter("@reader_id", Id);
            cmd1.Parameters.Add(pr1);

            dataAdapter = new SqlDataAdapter(cmd1);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var item in dr.ItemArray)
                {
                    comboBox1.Items.Add(item.ToString());
                }
            }

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Нет доступных книг");
                comboBox1.Enabled = false;
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult okCancel = MessageBox.Show("Все изменения в таблицах будут сохранены", "Сохранение изменений", MessageBoxButtons.OKCancel);
            if (okCancel == DialogResult.OK)
            {
                //dataBase.openConnection();
                using (var cmd = new SqlCommandBuilder(dataAdapterWorker))
                    dataAdapterWorker.Update(dataTableWorker);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            string comDel = "INSERT INTO Cards " +
                "VALUES (@book_id, (SELECT Person.id FROM Person WHERE Person.phone_number = @phone_number), @get_date, @return_date, @worker_id)";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1, pr2, pr3, pr4, pr5;
            pr1 = new SqlParameter("@book_id", textBox1.Text);
            pr2 = new SqlParameter("@phone_number", textBox2.Text);
            pr3 = new SqlParameter("@get_date", DateTime.UtcNow.Date.ToString());
            pr4 = new SqlParameter("@return_date", DateTime.UtcNow.Date.AddMonths(1).ToString());
            pr5 = new SqlParameter("@worker_id", Id.ToString());
            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);
            cmd1.Parameters.Add(pr3);
            cmd1.Parameters.Add(pr4);
            cmd1.Parameters.Add(pr5);

            cmd1.ExecuteNonQuery();
            dataBase.closeConnection();






            dataBase = new DataBase();
            dataBase.openConnection();
            comDel = "UPDATE Book Set status_id = @status_id WHERE id = @book_id";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            pr1 = new SqlParameter("@status_id", "2");
            pr2 = new SqlParameter("@book_id", textBox1.Text);
            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);

            cmd1.ExecuteNonQuery();
            dataBase.closeConnection();


            comDel = "SELECT * From Book";
            dataBase = new DataBase();
            dataBase.openConnection();
            dataAdapterWorker = new SqlDataAdapter(comDel, dataBase.GetConnection());
            dataTableWorker = new DataTable();
            dataAdapterWorker.Fill(dataTableWorker);
            dataGridView1.DataSource = dataTableWorker;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            dataBase.openConnection();
            string comDel = "DELETE FROM Cards WHERE book_id = @book_id AND (SELECT Person.id FROM Person WHERE Person.phone_number = @phone_number) = reader_id";
            SqlCommand cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            SqlParameter pr1, pr2;
            pr1 = new SqlParameter("@book_id", textBox1.Text);
            pr2 = new SqlParameter("@phone_number", textBox2.Text);
            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);

            cmd1.ExecuteNonQuery();
            dataBase.closeConnection();


            dataBase = new DataBase();
            dataBase.openConnection();
            comDel = "UPDATE Book Set status_id = @status_id WHERE id = @book_id";
            cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

            pr1 = new SqlParameter("@status_id", "1");
            pr2 = new SqlParameter("@book_id", textBox1.Text);
            cmd1.Parameters.Add(pr1);
            cmd1.Parameters.Add(pr2);

            cmd1.ExecuteNonQuery();
            dataBase.closeConnection();


            comDel = "SELECT * From Book";
            dataBase = new DataBase();
            dataBase.openConnection();
            dataAdapterWorker = new SqlDataAdapter(comDel, dataBase.GetConnection());
            dataTableWorker = new DataTable();
            dataAdapterWorker.Fill(dataTableWorker);
            dataGridView1.DataSource = dataTableWorker;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
