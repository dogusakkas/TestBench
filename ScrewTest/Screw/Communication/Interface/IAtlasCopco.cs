using ScrewTest.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Screw.Communication.Interface
{
    public interface IAtlasCopco
    {
        Task<StandardData> GetMidAsync();
    }
}
