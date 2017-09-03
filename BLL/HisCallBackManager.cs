using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;

namespace BLL
{
    public class HisCallBackManager
    {
        private string GetDbCon(string type)
        {
            string result = "";
            string dbname = string.Format("DB_{0}", type);
            result = ConfigurationManager.ConnectionStrings[dbname].ConnectionString;
            return result;

        } 

        private bool IsExsitAckAntCVResult(string type, AckAntCVResult data)
        {
            bool hasData = false;
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                var queryparams = new SqlParameter[] { new SqlParameter("@StudyNo", data.StudyNo) };
                var datalist = SqlHelper.ExecuteReader(con, CommandType.Text, "select 1 from TB_AckAntCVResult where StudyNo=@StudyNo", queryparams);
                if (datalist.Read())
                {
                    hasData = true;
                }
                datalist.Close();
            }
            return hasData;
        }
       
        public void SaveOrUpdateAckAntCVResult(string type, AckAntCVResult data)
        {
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                if (this.IsExsitAckAntCVResult(type, data))
                {

                    var sqlparams = new SqlParameter[] { new SqlParameter("AntCVResultID", data.AntCVResultID) ,
                    new SqlParameter("@ReportType", data.ReportType) ,
                        new SqlParameter("@ExecDocCode", data.ExecDocCode),
                        new SqlParameter("@ExecDocName", data.ExecDocName),
                     new SqlParameter("@ExecDate", data.ExecDate),
                     new SqlParameter("@ExecTime", data.ExecTime),
                     new SqlParameter("@StudyNo", data.StudyNo),
                     new SqlParameter("@LastUpateTime", DateTime.Now),
                    new SqlParameter("@Status", 2)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Update TB_AckAntCVResult 
set ReportType=@ReportType,ExecDocCode=@ExecDocCode,ExecDocName=@ExecDocName,
ExecDate=@ExecDate,ExecTime=@ExecTime,StudyNo=@StudyNo,LastUpateTime=@LastUpateTime,Status=@Status  
where AntCVResultID=@AntCVResultID", sqlparams);
                }
                else
                {
                    var sqlparams = new SqlParameter[] { new SqlParameter("AntCVResultID", data.AntCVResultID) ,
                    new SqlParameter("@ReportType", data.ReportType) ,
                        new SqlParameter("@ExecDocCode", data.ExecDocCode),
                        new SqlParameter("@ExecDocName", data.ExecDocName),
                     new SqlParameter("@ExecDate", data.ExecDate),
                     new SqlParameter("@ExecTime", data.ExecTime),
                     new SqlParameter("@StudyNo", data.StudyNo),
                     new SqlParameter("@Status", 2),
                     new SqlParameter("@LastUpateTime", DateTime.Now)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Insert into TB_AckAntCVResult (AntCVResultID,ReportType,ExecDocCode,ExecDocName,ExecDate,ExecTime,StudyNo,LastUpateTime,Status) 
                                         VALUES (@AntCVResultID,@ReportType,@ExecDocCode,@ExecDocName,@ExecDate,@ExecTime,@StudyNo,@LastUpateTime,@Status)", sqlparams);

                }
                con.Close();
            }

        }

   

        public AckAntCVResult loadAckAntCVResultXml(string xml)
        {
            var data = new AckAntCVResult();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.SelectSingleNode("//AckAntCVResult");
            if (root != null)
            {
                data.AntCVResultID = (root.SelectSingleNode("AntCVResultID")).InnerText;
                data.ReportType = (root.SelectSingleNode("ReportType")).InnerText;
                data.ExecDocCode = (root.SelectSingleNode("ExecDocCode")).InnerText;
                data.ExecDocName = (root.SelectSingleNode("ExecDocName")).InnerText;
                data.ExecDate = (root.SelectSingleNode("ExecDate")).InnerText;
                data.ExecTime = root.SelectSingleNode("ExecTime").InnerText;
                string checknum = data.AntCVResultID.Split("_".ToCharArray())[1];
                data.StudyNo = checknum;
            }
            return data;
        }
    }
}
