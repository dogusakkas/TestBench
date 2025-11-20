using ScrewTest.DataType;
using ScrewTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewTest.Screw.Communication.Interface
{
    public interface IMachine
    {
        string Name { get; }
        MachineType MachineType { get; }
        ICommunicationStrategy CommunicationStrategy { get; set; }
    }
}
