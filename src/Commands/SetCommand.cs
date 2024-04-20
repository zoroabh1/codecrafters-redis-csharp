using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codecrafters_redis.src.Commands
{
    public class SetCommand
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public int Expiry { get; set; }

    }
}
