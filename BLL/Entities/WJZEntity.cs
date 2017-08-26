using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CriticalEntity
    {
        public int id { get; set; }
        public string CriticalID { get; set; }
        public string CheckNum { get; set; }
        public string ExecDr { get; set; }
        public DateTime ExecDate { get; set; }
    }
}
