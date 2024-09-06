﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace guardadoDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get data from text boxes
            string names, lastnames, phone;
            int age, height;
            names = tbName.Text;
            lastnames = tbSurname.Text;
            age = int.Parse(tbAge.Text);
            height = int.Parse(tbHeight.Text);
            phone = tbPhone.Text;

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
