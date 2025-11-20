using ScrewTest.DataType;
using ScrewTest.DTO;
using ScrewTest.Screw.Communication.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTest.Screw.Communication.ScrewCommunication
{
    public class EthernetScrewMachineBase : IAtlasCopcoCommunication, IMachine
    {
        public string Name => throw new NotImplementedException();

        public virtual ICommunicationStrategy CommunicationStrategy { get; set; }

        public MachineType MachineType => throw new NotImplementedException();

        public EthernetScrewMachineBase(ICommunicationStrategy communicationStrategy)
        {
            CommunicationStrategy = communicationStrategy ?? throw new ArgumentNullException(nameof(communicationStrategy));
        }

        public async Task<StandardData> ConnectMachine()
        {
            try
            {
                Console.WriteLine("\nMID0001 Gönderiliyor...\n");
                string MID = "00200001            \0";

                await CommunicationStrategy.WriteData(true, MID);

                return new StandardData(true, "MID0001 gönderildi, cevap Traceability tarafından işlenecek.");
            }
            catch (Exception ex)
            {
                return new StandardData(false, $"Cihaza bağlantı sırasında beklenmeyen bir hata oluştu: {ex.Message}");
            }
        }


        public async Task<StandardData> DisconnectMachine()
        {
            var data = "";
            try
            {

                Console.WriteLine("\nMID0003 Gönderiliyor...\n");
                string MID = "00200003            \0";

                var responseWrite = await CommunicationStrategy.WriteData(true, MID);


                Console.WriteLine("MID0003 Gönderildi.");

            }
            catch (Exception ex)
            {
                return new StandardData(false, $"MID03 komutu gönderilemedi. Bilinmeyen bir hata oluştu. Hata: {ex.Message}");
            }

            return new StandardData(data);
        }
    }
}
