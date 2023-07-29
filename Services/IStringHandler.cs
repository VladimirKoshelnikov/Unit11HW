using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unit11HW.Services
{
    public interface IStringHandler
    {
        public string Process (string inputData, string action);
    }
}