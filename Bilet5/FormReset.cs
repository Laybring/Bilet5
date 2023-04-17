using Microsoft.VisualBasic.Logging;
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

namespace Bilet5
{
    public partial class FormReset : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=LAYBRING\MSSQLSERVER02; Initial Catalog=ExamUsers;Integrated Security=True");

        public FormReset()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool presenceNumbers = false;
            bool presenceSymbols = false;

            string password = textBox1.Text;

            int CountSymbols = 0;
            int CountNumbers = 0;

            char[] charPassword = password.ToCharArray();
            char[] charSymbols = password.ToCharArray();

            char[] Symbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string symbols1 = "@#%)(.<";

            static bool charVerification(char symbol, char[] symbolArray)
            {
                bool result = false;
                foreach (var itemSymbolArray in symbolArray)
                {
                    if (symbol == itemSymbolArray)
                    {
                        result = true;
                    }
                }
                return result;
            }

            static bool numVerification(char number, char[] numberArray)
            {
                bool result = false;
                foreach (var itemNumberArray in numberArray)
                {
                    if (number == itemNumberArray)
                    {
                        result = true;
                    }
                }
                return result;
            }

            foreach (var itemCharPassword in charPassword)
            {
                bool num = numVerification(itemCharPassword, numbers);
                if (num == true) presenceNumbers = true;
                CountNumbers++;

            }
            foreach (var itemCharSymbols in charSymbols)
            {
                bool up = charVerification(itemCharSymbols, Symbols);
                if (up == true) presenceSymbols = true;
                CountSymbols++;
            }

            if (textBox1.Text.Contains(symbols1)
                & presenceSymbols
                & presenceNumbers
                & CountSymbols>=5
                & CountNumbers>=3
                )
            {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"UPDATE USERS SET password = '{password}' WHERE login = '{User.Login}' ", connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Пароль изменен");
                    this.Hide();
                    connection.Close();
            }
            else
            {
            MessageBox.Show("Пароль не соответствует требованиям защиты. Испольуйте 5 символов, 3 цифры, а также знаки @#%)(.<");
            }
         
        }
    }
}
