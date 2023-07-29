using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unit11HW.Models;

namespace Unit11HW.Services
{
    public interface IStorage
    {
        public Session GetSession(long chatId);
    }
}