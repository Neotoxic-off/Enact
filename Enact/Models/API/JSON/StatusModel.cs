using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enact.Models.API.JSON
{
    public class StatusModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string[] locales { get; set; }
        public object[] maintenances { get; set; }
        public object[] incidents { get; set; }
    }
}
