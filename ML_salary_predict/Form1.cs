using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ML_salary_predict
{
    public partial class Form1 : Form
    {
        string ipaddress = "";
        TcpClient tcpClient;
        int port = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // По умолчанию указываем поля по дефолту
        {
            comboBoxWork.SelectedIndex = 0;
            comboBoxRegion.SelectedIndex = 0;
            comboBoxEducation.SelectedIndex = 0;
        }

        // Проверка на заполненность полей для подключения (27-40 стр)
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ipaddress = textBoxIP.Text;
            port = (int)nUDPort.Value;
            if (!IPAddress.TryParse(textBoxIP.Text, out _) || port > 65535)
            {
                
                labelErrorConnect.Visible = true;
                btnSend.Visible = false;
                return;
            }

            labelErrorConnect.Visible = false;
            btnSend.Visible = true;
        }

        // Проверка на заполненность полей для отправки данных (46-89 стр)
        private async void BtnSend_ClickAsync(object sender, EventArgs e)
        {
            string last_name = textBoxLastName.Text;
            string first_name = textBoxFirstName.Text;
            string patronymic = textBoxSurname.Text;
            bool gender = radioBtnMale.Checked;
            DateTime date_of_birth = dateTimePicker.Value;
            string education_name = comboBoxEducation.Text;
            int place_of_work = comboBoxRegion.SelectedIndex + 1;
            int work = comboBoxWork.SelectedIndex + 1;
            int the_offered_price = (int)nUDDesiredSalary.Value;

            if (string.IsNullOrEmpty(last_name) || string.IsNullOrEmpty(first_name) || string.IsNullOrEmpty(patronymic))
            {
                labelErrorSend.Visible = true;
                return;
            }

            // Отправка данных на сервер
            string queryToServer = $"{last_name};{first_name};{patronymic};{gender};{date_of_birth.ToShortDateString()};{education_name};{place_of_work};{work};{the_offered_price};)";
            string result = await SendMesageAsync(queryToServer);
            if(!string.IsNullOrEmpty(result)) lbPredict.Text = result;
        }

        private void ErrorOnEntry(object sender, EventArgs e) => labelErrorSend.Visible = false;    // Обработка ошибки

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) => System.Diagnostics.Process.Start("https://github.com/Senya1803/ML_salary_predict/"); // Ссылка на GitHub

        private async Task<string> SendMesageAsync(string message)  // Отправка сообщения на сервер
        {
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ipaddress, port);
                var stream = tcpClient.GetStream();

                // отправляем маркер завершения подключения - END
                byte[] messageBytes = Encoding.UTF8.GetBytes($"{message}\0");
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);


                // буфер для входящих данных
                var response = new List<byte>();
                int bytesRead;
                // считываем данные до конечного символа
                while ((bytesRead = stream.ReadByte()) != '\0')
                {
                    // добавляем в буфер
                    response.Add((byte)bytesRead);
                }
                var translation = Encoding.UTF8.GetString(response.ToArray());
                response.Clear();
                return translation;
            } 
            catch(Exception ex)
            {
                lbPredict.Text = ex.Message;
            }
            finally
            {
                tcpClient.Close();
            }
            return "";
        }
    }
}
