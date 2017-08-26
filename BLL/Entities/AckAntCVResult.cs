using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AckAntCVResult
    {
        public string AntCVResultID { get; set; }
        public string ReportType { get; set; }
        public string ExecDocCode { get; set; }
        public string ExecDocName { get; set; }
        public string ExecDate { get; set; }
        public string ExecTime { get; set; }
        public string CheckNum { get; set; }

        public string ExecDateTime
        {
            get
            {
                if ((!string.IsNullOrEmpty(ExecDate)) && (!string.IsNullOrEmpty(ExecTime)))
                {
                    return this.ExecDate.Trim() + " " + this.ExecTime;
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
