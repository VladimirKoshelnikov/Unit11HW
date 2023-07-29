using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Concurrent;

using Unit11HW.Models;

namespace Unit11HW.Services
{
    public class MemoryStorage:IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;
        
        public MemoryStorage(){
            _sessions = new ConcurrentDictionary<long, Session>();
        }
        
        public Session GetSession(long chatId){
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];
            var newSession = new Session(){
                Action = "Count"
            };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}