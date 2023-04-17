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

namespace Bilet5
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=LAYBRING\MSSQLSERVER02; Initial Catalog=ExamUsers;Integrated Security=True");
        DataTable dt = new DataTable();

        int count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            connection.Open();
            SqlCommand command = new SqlCommand($"select * from users where login = '{login}' and password = '{password}'", connection);
            dt.Load(command.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    FormMain Main = new FormMain();
                    Main.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль! Проверьте корректность введенных данных.");
                    count++;
                }
                if (count >= 3)
                {
                    MessageBox.Show("Будет открыто окно восстановления пароля.");
                    FormReset reset = new FormReset();
                    reset.Show();
                    User.Login = login;
                }

            connection.Close();
        }
    }
}
