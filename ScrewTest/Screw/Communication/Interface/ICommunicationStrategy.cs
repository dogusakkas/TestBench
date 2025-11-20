using ScrewTest.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTest.Screw.Communication.Interface
{
    public interface ICommunicationStrategy
    {
        //bool IsConnected { get; set; }
        Task StartListening();
        CancellationTokenSource _cts { get; set; }
        PictureBox ConnectionStatusImage { get; set; }
        Label ConnectionTypeLabel { get; set; }
        Task<StandardData> Connect();
        Task<StandardData> Disconnect();
        Task<StandardData> ReadData();
        Task<StandardData> OneReadData();
        Task<StandardData> WriteData(bool result, string data = "");
        void UpdateConnectionStatus();
        void UpdateConnectionType();
    }
}
