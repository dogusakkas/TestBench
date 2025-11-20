using ScrewTest.DataType;
using ScrewTest.DTO;
using ScrewTest.Screw.Communication.ScrewCommunication;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static ScrewTest.Screw.Communication.ScrewCommunication.EthernetCommunicaton;

namespace ScrewTest.Screw
{
    public partial class MT6000 : UserControl
    {
        private EthernetCommunicaton _communication;
        private LineStructure _lineStructure;

        public MT6000(LineStructure lineStructure)
        {
            InitializeComponent();
            _communication = new EthernetCommunicaton();
            _lineStructure = lineStructure;
            _lineStructure.ScrewMachine.CommunicationStrategy = _communication;

        }
        private void MT6000_Load(object sender, EventArgs e)
        {
            lbl_ethernetclose.Text = "Ethernet portu kapatılır";
            lbl_ethernetopen.Text = "Ethernet portu açılır";

            chkBox_OK_NOK.Text = "OK";

            btn_ethernetClose.Enabled = false;
        }

        private void WriteLog(bool isSuccess, string operationMessage, string content)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string prefix = isSuccess ? "✔" : "✖";
            Color color = isSuccess ? Color.Green : Color.Red;

            richTextBox_Log.SelectionColor = color;
            richTextBox_Log.AppendText($"{timestamp} | {prefix} {operationMessage} {content} \n");
            richTextBox_Log.ScrollToCaret();

            richTextBox_Log.SelectionColor = Color.Black;
        }

        private async void btn_ethernetOpen_Click(object sender, EventArgs e)
        {
            if (_communication != null)
            {
                var result = new StandardData(true, "Dinleme başladı");
                result.OperationMessage = "Ethernet portu açıldı ";
                WriteLog(result.IsSuccessful, result.OperationMessage, result.Content);

                btn_ethernetOpen.Enabled = false;
                btn_ethernetClose.Enabled = true;


                await _communication.StartListening(); // <- burada port dinlemeye başlasın



            }
            else
            {
                WriteLog(false, "Vida cihazı tanımlı değil", "");
            }
        }


        private async void btn_ethernetClose_Click(object sender, EventArgs e)
        {
            try
            {

                WriteLog(true, "Ethernet portu ve bağlantı başarıyla kapatıldı.", "");

                btn_ethernetOpen.Enabled = true;
                btn_ethernetClose.Enabled = false;

                await _communication.Disconnect();

            }
            catch (Exception ex)
            {
                WriteLog(false, "Bağlantı kesilirken hata oluştu.", ex.Message);
            }
        }

        private void btn_LogClear_Click(object sender, EventArgs e)
        {
            richTextBox_Log.Clear();
        }

        private void btn_PsetEkle_Click(object sender, EventArgs e)
        {
            var psetValue = txt_PsetEkle.Text.Trim();


            if (!string.IsNullOrEmpty(psetValue))
            {
                listBox_Pset.Items.Add(psetValue);
                PsetData.PsetList.Add(psetValue);
                txt_PsetEkle.Clear();
            }

        }

        private void btn_PsetSil_Click(object sender, EventArgs e)
        {
            if (listBox_Pset.SelectedItem != null)
            {
                string selectedPset = listBox_Pset.SelectedItem.ToString();

                listBox_Pset.Items.Remove(selectedPset);
                PsetData.PsetList.Remove(selectedPset);

                Console.WriteLine($"Pset silindi: {selectedPset}");
            }
        }

        private void txt_TorqueValue_TextChanged(object sender, EventArgs e)
        {
            lbl_TorqueDisplay.Text = $"Tork Değeri: {txt_TorqueValue.Text}";
        }

        private async void btn_SendData_Click(object sender, EventArgs e)
        {
            string rawData = "182812020010    00  002002000000108000000783020001102000000000000001080302010010200000001302020010200000001302030190500000002014-11-12:07:32:083020400102000000003020500202000000001302060020200000000130207008040000000E644053530208000040000000302090010200000000302100000400000003021100102000000003021200004000000030213001020000000030214008040000000B655181630215016040000000ETD MT41-250-I0630216003020000000007302170070400000002.40 Nm30218009020000000000000001302190190500000002014-08-21:05:36:47302200190500000002014-08-21:05:36:4730221005020000000000003022200004000000030223009020000000000000000302260050200000000000030227005020000000000003022800302000000000030229003020000000000302540030200000000003027400302000000000030275003020000000001302300129000100000001.800e+00302310129005000000001.770e+03302700129005000000001.770e+03302320129020000000006.790e-01302330129025100000002.181e+01302340010200000001302710010200000001302350010200000001302360010200000001302370129000100000009.990e+00302380129005000000008.888e+0330239001020...\0";


            string userTorque = txt_TorqueValue.Text.Trim();
            string userAngle = txt_AngleValue.Text.Trim();

            lbl_TorqueDisplay.Text = $"Tork Değeri: {userTorque}";

            string updatedData = ReplaceTorqueValue(rawData, userTorque);
            updatedData = ReplaceAngleValue(updatedData, userAngle);

            string statusValue = chkBox_OK_NOK.Checked ? "1" : "0";
            updatedData = ReplaceStatusValue(updatedData, statusValue);

            if (_lineStructure.ScrewMachine != null)
            {
                var result = await _lineStructure.ScrewMachine.CommunicationStrategy.WriteData(true, updatedData);
                WriteLog(result.IsSuccessful, "OK Data Gönderildi", updatedData);
            }
            else
            {
                WriteLog(false, "Vida cihazı tanımlı değil", "");
            }
        }

        private string ReplaceTorqueValue(string originalData, string newTorque)
        {
            if (!newTorque.Contains(",") && !newTorque.Contains("."))
            {
                newTorque = $"{newTorque}.00";
            }
            newTorque = newTorque.Replace(',', '.');

            if (!double.TryParse(newTorque, NumberStyles.Float, CultureInfo.InvariantCulture, out double torqueValue))
            {
                Console.WriteLine("Geçersiz tork değeri girildi.");
                return originalData;
            }

            string newFormatted = torqueValue.ToString("0.000e+00", CultureInfo.InvariantCulture);
            var regex = new Regex(@"9\.990e\+00");

            if (regex.IsMatch(originalData))
            {
                return regex.Replace(originalData, newFormatted, 1);
            }

            return originalData;
        }

        private string ReplaceAngleValue(string originalData, string newAngle)
        {
            string TformattedAngle = "";
            if (string.IsNullOrWhiteSpace(newAngle))
                newAngle = "0";

            if (!double.TryParse(newAngle, out double angleValue))
            {
                Console.WriteLine("Geçersiz açı değeri girildi.");
                return originalData;
            }
            StringBuilder sb = new StringBuilder(originalData);

            if (newAngle.Length >= 5)
            {
                TformattedAngle = angleValue.ToString("00.000e+0", System.Globalization.CultureInfo.InvariantCulture);

                string marker = "8.888";
                int index = originalData.IndexOf(marker);
                if (index < 0)
                {
                    Console.WriteLine("8.888 değeri bulunamadı.");
                    return originalData;
                }

                for (int i = 0; i < TformattedAngle.Length; i++)
                {
                    sb[index + i] = TformattedAngle[i];
                }
            }

            else
            {
                // Bilimsel format (örn: 15000 → 1.500e+04)
                string formattedAngle = angleValue.ToString("0.000e+00", System.Globalization.CultureInfo.InvariantCulture);

                string marker = "8.888";
                int index = originalData.IndexOf(marker);
                if (index < 0)
                {
                    Console.WriteLine("8.888 değeri bulunamadı.");
                    return originalData;
                }

                for (int i = 0; i < formattedAngle.Length; i++)
                {
                    sb[index + i] = formattedAngle[i];
                }
            }



            return sb.ToString();
        }

        private string ReplaceStatusValue(string originalData, string statusValue)
        {
            string marker = "30202";
            int index = originalData.IndexOf(marker);
            if (index < 0)
            {
                Console.WriteLine("30202 bulunamadı.");
                return originalData;
            }

            int valueIndex = index + 5 + 3 + 2 + 3 + 4;

            StringBuilder sb = new StringBuilder(originalData);

            for (int i = 0; i < statusValue.Length; i++)
            {
                sb[valueIndex + i] = statusValue[i];
            }

            return sb.ToString();
        }

        private void chkBox_OK_NOK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox_OK_NOK.Checked)
                chkBox_OK_NOK.Text = "OK";
            else
                chkBox_OK_NOK.Text = "NOK";
        }

        private void txt_AngleValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
