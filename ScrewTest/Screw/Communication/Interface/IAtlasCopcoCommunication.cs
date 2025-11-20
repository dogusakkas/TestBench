using ScrewTest.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTest.Screw.Communication.Interface
{
    public interface IAtlasCopcoCommunication
    {
        Task<StandardData> ConnectMachine();
        Task<StandardData> DisconnectMachine();
    }
}
