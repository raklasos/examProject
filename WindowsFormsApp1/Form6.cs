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
    public partial class Form6 : Form
    {
        SqlDataAdapter dataAdapter;
        BindingSource bindingSource;


        DataTable table;

        DataBase dataBase = new DataBase();
        public Form6()
        {
            InitializeComponent();

            this.Text = "Workers";

            this.BackColor = SystemColors.InactiveCaption;

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.DarkBlue;
            button1.FlatAppearance.BorderSize = 3;

            button1.Text = "Accept";

            dataGridView1.DefaultCellStyle.ForeColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;


            string comDel = " SELECT * FROM Person";
            dataAdapter = new SqlDataAdapter(comDel, dataBase.GetConnection());
            table = new DataTable();
            dataAdapter.Fill(table);
            bindingSource = new BindingSource();
            bindingSource.DataSource = table;

            dataGridView1.DataSource = bindingSource;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DialogResult okCancel = MessageBox.Show("Все изменения в таблицах будут сохранены", "Сохранение изменений", MessageBoxButtons.OKCancel);
            if (okCancel == DialogResult.OK)
            {
                //dataBase.openConnection();
                using (var cmd = new SqlCommandBuilder(dataAdapter))
                    dataAdapter.Update(table);
            }
            
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
