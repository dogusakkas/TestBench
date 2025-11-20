using ScrewTest.DataType;
using ScrewTest.DTO;
using ScrewTest.Screw.Communication.Interface;
using ScrewTest.Screw.Communication.ScrewCommunication;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static ScrewTest.Screw.Communication.ScrewCommunication.EthernetCommunicaton;

namespace ScrewTest.Screw
{
    public partial class PF8000 : UserControl
    {
        private EthernetCommunicaton _communication;
        private LineStructure _lineStructure;

        public PF8000(LineStructure lineStructure)
        {
            InitializeComponent();
            _communication = new EthernetCommunicaton();
            _lineStructure = lineStructure;
            _lineStructure.ScrewMachine.CommunicationStrategy = _communication;

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


        private void PF_8000_Load(object sender, EventArgs e)
        {
            lbl_ethernetclose.Text = "Ethernet portu kapatılır";
            lbl_ethernetopen.Text = "Ethernet portu açılır";

            chkBox_OK_NOK.Text = "OK";

            btn_ethernetClose.Enabled = false;
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

                await _communication.StartListening(); // <- port dinlemeye başlar

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


        private void lbl_TorqueDisplay_TextChanged(object sender, EventArgs e)
        {
            lbl_TorqueDisplay.Text = $"Tork Değeri: {txt_TorqueValue.Text}";
        }

        private void lbl_AngleDisplay_TextChanged(object sender, EventArgs e)
        {
            lbl_AngleDisplay.Text = $"Açı Değeri: {txt_AngleValue.Text}";
        }


        private string ReplaceTorqueValue(string originalData, string newTorque)
        {
            newTorque = newTorque.Replace(',', '.').Trim();

            if (!double.TryParse(newTorque, NumberStyles.Float, CultureInfo.InvariantCulture, out double torqueValue))
            {
                Console.WriteLine("Geçersiz tork değeri.");
                return originalData;
            }

            // 1.32 → 13200 → "0013200"
            int scaledValue = (int)(torqueValue * 10000);
            string formattedTorque = scaledValue.ToString("D7"); // 7 haneli, sıfır dolgu

            // "0077716" geçen yeri bul
            int index = originalData.IndexOf("0077716");
            if (index < 0)
            {
                Console.WriteLine("Orijinal tork değeri bulunamadı.");
                return originalData;
            }

            StringBuilder sb = new StringBuilder(originalData);
            sb.Remove(index, 7);
            sb.Insert(index, formattedTorque);

            return sb.ToString();
        }

        private string ReplaceAngleValue(string originalData, string newAngle)
        {
            newAngle = newAngle.Trim();

            // "1234" değerinin pozisyonunu bul
            int index = originalData.IndexOf("01234");
            if (index < 0)
            {
                Console.WriteLine("Orijinal açı değeri bulunamadı.");
                return originalData;
            }

            // Orijinal açı uzunluğu
            int fixedLength = "01234".Length;

            // Yeni değeri pad ile aynı uzunluğa getir
            string paddedAngle = newAngle.PadLeft(fixedLength, '0');

            // StringBuilder ile değiştir
            StringBuilder sb = new StringBuilder(originalData);
            sb.Remove(index, fixedLength);
            sb.Insert(index, paddedAngle);

            return sb.ToString();
        }

        private string ReplaceStatusValue(string originalData, string newStatus)
        {
            int index = originalData.IndexOf("911");
            if (index < 0 || index + 1 >= originalData.Length)
                return originalData;

            char[] chars = originalData.ToCharArray();
            chars[index + 1] = newStatus[0];

            return new string(chars);
        }



        private async void btn_SendData_Click(object sender, EventArgs e)
        {
            string testData = "02310061    0000    010000020003                         04                         050006003070000080000091101111120000461300007014000050150007771600000179999918000001901234202025-05-08:08:31:46212025-02-27:13:44:38222230000002251\0";

            string userTorque = txt_TorqueValue.Text.Trim();
            lbl_TorqueDisplay.Text = $"Tork Değeri: {userTorque}";

            string userAngle = txt_AngleValue.Text.Trim();

            string statusValue = chkBox_OK_NOK.Checked ? "1" : "0";


            // Önce tork
            string updatedData = ReplaceTorqueValue(testData, userTorque);

            // Sonra açı 
            updatedData = ReplaceAngleValue(updatedData, userAngle);

            // Statü
            updatedData = ReplaceStatusValue(updatedData, statusValue);



            if (_lineStructure.ScrewMachine != null)
            {
                try
                {
                    var result = await _lineStructure.ScrewMachine.CommunicationStrategy.WriteData(true, updatedData);
                    WriteLog(result.IsSuccessful, "OK Data Gönderildi", updatedData);
                    if (!result.IsSuccessful)
                        Debug.WriteLine("Gönderim başarısız: " + result.Content);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Gönderim sırasında hata: " + ex.Message);
                }
            }
            else
            {
                WriteLog(false, "Vida cihazı tanımlı değil", "");
            }
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
