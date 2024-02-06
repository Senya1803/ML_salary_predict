using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML_salary_predict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Проверка на заполненность полей для подключения (27-40 стр)
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "")
            {
                labelErrorConnect.Visible = true;
            }
        }

        private void textBoxIP_Enter(object sender, EventArgs e)
        {
            labelErrorConnect.Visible = false;
        }

        private void textBoxPort_Enter(object sender, EventArgs e)
        {
            labelErrorConnect.Visible = false;
        }

        // Проверка на заполненность полей для отправки данных (46-89 стр)
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (textBoxLastName.Text == "" || textBoxFirstName.Text == "" || textBoxSurname.Text == "" || (!radioBtnMale.Checked && !radioBtnFemale.Checked) || comboBoxRegion.Text == "" || comboBoxEducation.Text == "" || textBoxDesiredSalary.Text == "")
            {
                labelErrorSend.Visible = true;
            }
        }

        private void textBoxLastName_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void textBoxFirstName_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void textBoxSurname_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void radioBtnMale_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void radioBtnFemale_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void comboBoxRegion_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void comboBoxEducation_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }

        private void textBoxDesiredSalary_Enter(object sender, EventArgs e)
        {
            labelErrorSend.Visible = false;
        }
    }
}
