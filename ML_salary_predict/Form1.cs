using System;
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
            comboBoxWork.SelectedIndex = 0;
            comboBoxRegion.SelectedIndex = 0;
            comboBoxEducation.SelectedIndex = 0;
        }

        // Проверка на заполненность полей для подключения (27-40 стр)
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ipAddress = textBoxIP.Text;
            int port = (int)nUDPort.Value;

            if (string.IsNullOrEmpty(ipAddress))
            {
                labelErrorConnect.Visible = true;
                btnSend.Visible = false;
                return;
            }
            // connect to server
            labelErrorConnect.Visible = false;
            btnSend.Visible = true;

        }

        // Проверка на заполненность полей для отправки данных (46-89 стр)
        private void btnSend_Click(object sender, EventArgs e)
        {
            string last_name = textBoxLastName.Text;
            string first_name = textBoxFirstName.Text;
            string patronymic = textBoxSurname.Text;
            bool gender = radioBtnMale.Checked ? true : false;
            DateTime date_of_birth = dateTimePicker.Value; // год - месяц - день
            string education_name = comboBoxEducation.Text;
            int place_of_work = comboBoxRegion.SelectedIndex + 1;
            int work = comboBoxWork.SelectedIndex + 1;
            int the_offered_price = (int)nUDDesiredSalary.Value;

            if (string.IsNullOrEmpty(last_name) || string.IsNullOrEmpty(first_name) || string.IsNullOrEmpty(patronymic))
            {
                labelErrorSend.Visible = true;
                return;
            }

            // SQL Query send to server
            string queryToServer = $"INSERT INTO Users (last_name,first_name,patronymic,gender,date_of_birth,education_name,place_of_work,work,the_offered_price) VALUES (\'{last_name}\', \'{first_name}\', \'{patronymic}\', {gender}, {date_of_birth.ToShortDateString()}, \'{education_name}\', {place_of_work}, {work}, {the_offered_price})";
            MessageBox.Show("QUERY = " + queryToServer);
        }

        private void ErrorOnEntry(object sender, EventArgs e) => labelErrorSend.Visible = false;
    }
}
