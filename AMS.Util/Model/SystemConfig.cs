using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Util.Model
{
    public class SystemConfig
    {
        public string WorkPlace { get; set; }
        public string SqlServerConnection_home { get; set; }
        public string SqlServerConnection_work { get; set; }
        public int DBCommandTimeout { get; set; }
    }
}
