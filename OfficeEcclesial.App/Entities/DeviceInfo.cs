using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeEcclesial.App.Entities
{
    public class DeviceInfo
    {
        public string SystemModel { get; internal set; }
        public string Manufacturer { get; internal set; }
        public string SystemFamily { get; internal set; }
        public string Processor { get; internal set; }
    }
}
