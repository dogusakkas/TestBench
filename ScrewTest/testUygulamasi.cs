using ScrewTest.DTO;
using ScrewTest.Screw;
using ScrewTest.Screw.Communication.Interface;
using ScrewTest.Screw.Communication.ScrewCommunication;
using System.Net.Sockets;

namespace ScrewTest
{
    public partial class testUygulamasi : Form
    {

        LineStructure lineStructure;
        private EthernetCommunicaton _communication;
        private TcpListener _listener;
        public testUygulamasi()
        {
            lineStructure = new LineStructure();
            InitializeComponent();
        }


        private void btn_pf8000_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();


            var communication = new EthernetCommunicaton();
            var machine = new EthernetScrewMachineBase(communication);

            lineStructure.ScrewMachine = machine;

            PF8000 pfControl = new PF8000(lineStructure);
            pfControl.Dock = DockStyle.Fill;

            panel_main.Controls.Add(pfControl);

            HighlightActiveButton(btn_pf8000);
        }

        private void btn_mt6000_Click(object sender, EventArgs e)
        {
            panel_main.Controls.Clear();

            var communication = new EthernetCommunicaton();
            var machine = new EthernetScrewMachineBase(communication);

            lineStructure.ScrewMachine = machine;

            MT6000 pfControl = new MT6000(lineStructure);
            pfControl.Dock = DockStyle.Fill;

            panel_main.Controls.Add(pfControl);

            HighlightActiveButton(btn_mt6000);
        }

        private async void testUygulamasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_communication != null)
                {
                    await _communication.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form kapanýrken hata: " + ex.Message);
            }
        }

        private void HighlightActiveButton(Button activeButton)
        {
            btn_pf8000.FlatAppearance.BorderSize = 0;
            btn_mt6000.FlatAppearance.BorderSize = 0;

            activeButton.FlatStyle = FlatStyle.Flat;
            activeButton.FlatAppearance.BorderSize = 2;
            activeButton.FlatAppearance.BorderColor = Color.DarkGray;
        }
    }
}
