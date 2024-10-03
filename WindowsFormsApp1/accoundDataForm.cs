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
    public partial class accoundDataForm : Form
    {

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public accoundDataForm(string id)
        {
            Id = id;
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Text = "";
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Text = "";
            label3.Text = "BOOK IN HAND";

            this.Text = "Account";

            this.BackColor = SystemColors.InactiveCaption;
            this.ForeColor = Color.DarkBlue;

            label1.Font = new Font("Times New Roman", 20);
            label2.Font = new Font("Times New Roman", 20);

            label3.Font = new Font("Times New Roman", 18);
            
            label4.Font = new Font("Times New Roman", 16);
            label5.Font = new Font("Times New Roman", 16);
            label6.Font = new Font("Times New Roman", 16);
            label7.Font = new Font("Times New Roman", 12);
            label8.Font = new Font("Times New Roman", 16);
            label9.Font = new Font("Times New Roman", 16);
            label10.Font = new Font("Times New Roman", 12);
            label11.Font = new Font("Times New Roman", 12);
            label12.Font = new Font("Times New Roman", 12);


            label10.Text = "NAME:";
            label11.Text = "YEAR:";
            label12.Text = "PUBLISHING HOUSE:";


            List<string> list = new List<string>();
            DataBase bookInfoDataBase = new DataBase();
            bookInfoDataBase.openConnection();
            string comDel = "SELECT DISTINCT Book.name, Book.year, Book.publisher, Cards.get_date , Cards.return_date" +
                "\r\nFROM Cards" +
                "\r\nJOIN Book ON Book.id = Cards.book_id" +
                "\r\nJOIN Person ON @reader_id = Cards.reader_id;";

            SqlCommand cmd1 = new SqlCommand(comDel, bookInfoDataBase.GetConnection());

            SqlParameter pr1;
            pr1 = new SqlParameter("@reader_id", Id);
            cmd1.Parameters.Add(pr1);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow dr in dataTable.Rows)
                foreach (var item in dr.ItemArray)
                    list.Add(item.ToString());

            label4.Text = list[0];
            label5.Text = list[1];
            label6.Text = list[2];
            label7.Text = "DATE:";
            DateTime myDate = DateTime.Parse(list[3]);
            label8.Text = myDate.ToString("dd/MM/yyyy");
            myDate = DateTime.Parse(list[4]);
            label9.Text = myDate.ToString("dd/MM/yyyy");

            bookInfoDataBase.closeConnection();
            list.Clear();

            DataBase dataBase = new DataBase();

            dataBase.openConnection();
            try
            {
                dataBase.openConnection();
                comDel = " SELECT name, phone_number FROM Person WHERE id = @id";
                cmd1 = new SqlCommand(comDel, dataBase.GetConnection());

                SqlParameter pr2;
                pr2 = new SqlParameter("@id", id);

                cmd1.Parameters.Add(pr2);

                dataAdapter = new SqlDataAdapter(cmd1);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                    foreach (var item in dr.ItemArray)
                        list.Add(item.ToString());

                dataBase.closeConnection();

                label1.Text = list[0];
                label2.Text = list[1];
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bookListForm form4 = new bookListForm(Id, "1");
            form4.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changeNameForm form2 = new changeNameForm(Id);
            form2.Show();
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            bookListForm form4 = new bookListForm(Id, "1");
            form4.Show();
            this.Close();
        }
    }
}
