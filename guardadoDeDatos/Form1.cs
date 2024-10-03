using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace guardadoDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Add event handlers for leaving the textboxes
            tbName.Leave += checkNames;
            tbSurname.Leave += checkNames;
            tbAge.Leave += checkAge;
            tbHeight.Leave += checkHeight;
            //tbPhone.TextChanged += checkPhone;
            tbPhone.Leave += checkPhone;

        }

        string sqlConnection = "server=localhost; port=3306; database=michaelbay; uid=root; pwd=;";

        void insertRegistry(string name, string lastname, int age, float height,long telephone, string gender)
        {
            using (MySqlConnection connection = new MySqlConnection(sqlConnection))
            {
                connection.Open();

                string insertQuery = "insert into tabla (nameVal, lastName, telephone, height, age, gender) " +
                    "values (@name, @lastname, @telephone, @height, @age, @gender)";
                
                using (MySqlCommand command = new MySqlCommand(insertQuery,connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@telephone", telephone);
                    command.Parameters.AddWithValue("@height", height);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@gender", gender);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }


        private bool isValidInt(string str)
        {
            int result;
            return int.TryParse(str, out result);
        }
        private bool isValidFloat(string str)
        {
            decimal result;
            return decimal.TryParse(str, out result);
        }
        private bool isValidTenDigitNum(string str)
        {
            long result;
            return long.TryParse(str, out result) && str.Length == 10;
        }
        private bool isValidText(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z\s]+$");
        }

        private void checkAge(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidInt(textBox.Text))
                {
                    MessageBox.Show("Please, enter a valid age", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkHeight(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidFloat(textBox.Text))
                {
                    MessageBox.Show("Please, enter a valid decimal metre value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkNames(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidText(textBox.Text))
                {
                    MessageBox.Show("Please, check the name and surname fields and write a valid text values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkPhone(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidTenDigitNum(textBox.Text))
                {
                    MessageBox.Show("Please, enter a valid 10 digit phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get data from text boxes
            string names, lastnames;
            int age;
            float height;
            long phone;
            names = tbName.Text;
            lastnames = tbSurname.Text;
            age = int.Parse(tbAge.Text);
            height = float.Parse(tbHeight.Text);
            phone = long.Parse(tbPhone.Text);

            // Get gender
            string gender = "";
            if (rbMale.Checked)
            {
                gender = "Male";
            }
            else if (rbFemale.Checked)
            {
                gender = "Female";
            }
            else if (rbUndefined.Checked)
            {
                gender = "Undefined";
            }
            else if (rbIDontGiveAFuck.Checked)
            {
                gender = "Apache Helicopter";
            }

            // Data out variable
            string data = $"Name: {names}\r\nLast Name: {lastnames}\r\nPhone: {phone}\r\nHeight: {height}\r\nAge: {age}\r\nGender: {gender}";

            // Save text file
            string fileRoute = "C:\\Users\\Public\\out.txt";
            bool fileExists = File.Exists(fileRoute);

            using (StreamWriter wrt = new StreamWriter(fileRoute))
            {
                if (fileExists)
                {
                    wrt.WriteLine();
                }
                else
                {
                    wrt.WriteLine(data);
                }
            }
            // Show cached data

            insertRegistry(names, lastnames, age, height, phone, gender);

            MessageBox.Show("Data saved succesfully:\n\n" + data, "Information");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear everything up
            tbAge.Clear();
            tbPhone.Clear();
            tbName.Clear();
            tbSurname.Clear();
            tbHeight.Clear();
            rbFemale.Checked = false;
            rbUndefined.Checked = false;
            rbMale.Checked = false;
            rbIDontGiveAFuck.Checked = false;
        }
    }
}
