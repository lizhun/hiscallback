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

        private bool IsExsitData(string type, CriticalEntity data)
        {
            bool hasData = false;
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                var queryparams = new SqlParameter[] { new SqlParameter("@CheckNum", data.CheckNum) };
                var datalist = SqlHelper.ExecuteReader(con, CommandType.Text, "select 1 from TB_WJZ where CheckNum=@CheckNum", queryparams);
                if (datalist.Read())
                {
                    hasData = true;
                }
                datalist.Close();
            }
            return hasData;
        }

        private bool IsExsitAckAntCVResult(string type, AckAntCVResult data)
        {
            bool hasData = false;
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                var queryparams = new SqlParameter[] { new SqlParameter("@CheckNum", data.CheckNum) };
                var datalist = SqlHelper.ExecuteReader(con, CommandType.Text, "select 1 from TB_AckAntCVResult where CheckNum=@CheckNum", queryparams);
                if (datalist.Read())
                {
                    hasData = true;
                }
                datalist.Close();
            }
            return hasData;
        }

        public void SaveOrUpdateCritical(string type, CriticalEntity data)
        {
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                if (this.IsExsitData(type, data))
                {

                    var sqlparams = new SqlParameter[] { new SqlParameter("ExecDr", data.ExecDr) ,
                    new SqlParameter("@CriticalID", data.CriticalID) ,
                        new SqlParameter("@CheckNum", data.CheckNum),
                        new SqlParameter("@ExecDate", data.ExecDate)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Update TB_WJZ set ExecDr=@ExecDr,ExecDate=@ExecDate 
                                                  where CriticalID=@CriticalID", sqlparams);
                }
                else
                {
                    var sqlparams = new SqlParameter[] { new SqlParameter("ExecDr", data.ExecDr) ,
                    new SqlParameter("@CriticalID", data.CriticalID) ,
                        new SqlParameter("@CheckNum", data.CheckNum),
                        new SqlParameter("@ExecDate", data.ExecDate)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Insert into TB_WJZ (CriticalID,CheckNum,ExecDr,ExecDate) 
                                         VALUES (@CriticalID,@CheckNum,@ExecDr,@ExecDate)", sqlparams);

                }
                con.Close();
            }

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
                     new SqlParameter("@CheckNum", data.CheckNum)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Update TB_AckAntCVResult 
set ReportType=@ReportType,ExecDocCode=@ExecDocCode,ExecDocName=@ExecDocName,
ExecDate=@ExecDate,ExecTime=@ExecTime,CheckNum=@CheckNum 
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
                     new SqlParameter("@CheckNum", data.CheckNum)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Insert into TB_AckAntCVResult (AntCVResultID,ReportType,ExecDocCode,ExecDocName,ExecDate,ExecTime,CheckNum) 
                                         VALUES (@AntCVResultID,@ReportType,@ExecDocCode,@ExecDocName,@ExecDate,@ExecTime,@CheckNum)", sqlparams);

                }
                con.Close();
            }

        }

        public CriticalEntity loadCriticalXml(string xml)
        {
            var data = new CriticalEntity();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.SelectSingleNode("//ExecInfo");
            if (root != null)
            {
                data.CriticalID = (root.SelectSingleNode("CriticalID")).InnerText;
                data.ExecDate = DateTime.Parse((root.SelectSingleNode("ExecDate")).InnerText);
                data.ExecDr = root.SelectSingleNode("ExecDr").InnerText;
                string checknum = data.CriticalID.Split("_".ToCharArray())[1];
                data.CheckNum = checknum;
            }
            return data;
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
                data.CheckNum = checknum;
            }
            return data;
        }
    }
}
