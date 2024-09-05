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

namespace guardadoDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Add event handlers for text changed
            /*tbName.TextChanged += checkName();
            tbSurname.TextChanged += checkSurname();
            tbAge.TextChanged += checkAge();
            tbHeight.TextChanged += checkHeight();
            tbPhone.TextChanged += checkPhone();*/
        }

        private bool isValidInt(string str)
        {
            int result;
            return int.TryParse(str, out result);
        }
        private bool isValidFloat(string  str)
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


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get data from text boxes
            string names, lastnames;
            int age, height;
            long phone;
            names = tbName.Text;
            lastnames = tbSurname.Text;
            age = int.Parse(tbAge.Text);
            height = int.Parse(tbHeight.Text);
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
            string fileRoute = "C:\\Users\\BSTW\\Documents\\out.txt";
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
