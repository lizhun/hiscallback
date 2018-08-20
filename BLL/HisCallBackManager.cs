using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Linq;

namespace BLL
{
    public class HisCallBackManager
    {
        public string GetDbConByOrdName(string OrdName)
        {
            var conname = ConfigurationManager.AppSettings[OrdName];
            return ConfigurationManager.ConnectionStrings[conname].ConnectionString;
        }


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

        public SendAppBillResult LoadSendAppBillResult(string xml)
        {
            var data = new SendAppBillResult();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var root = doc.SelectSingleNode("//SendAppBill");
            if (root != null)
            {
                data.RegNo = (root.SelectSingleNode("RegNo")).InnerText;
                data.CardNo = (root.SelectSingleNode("CardNo")).InnerText;
                data.Name = (root.SelectSingleNode("Name")).InnerText;
                data.SexCode = (root.SelectSingleNode("SexCode")).InnerText;
                data.Sex = (root.SelectSingleNode("Sex")).InnerText;
                data.Age = (root.SelectSingleNode("Age")).InnerText;
                data.BirthDay = (root.SelectSingleNode("BirthDay")).InnerText;
                data.Marry = (root.SelectSingleNode("Marry")).InnerText;
                data.Address = (root.SelectSingleNode("Address")).InnerText;
                data.Telephone = (root.SelectSingleNode("Telephone")).InnerText;
                data.CredentialNo = (root.SelectSingleNode("CredentialNo")).InnerText;
                data.CredentialType = (root.SelectSingleNode("CredentialType")).InnerText;
                data.NationCode = (root.SelectSingleNode("NationCode")).InnerText;
                data.Nation = (root.SelectSingleNode("Nation")).InnerText;
                data.OccupationCode = (root.SelectSingleNode("OccupationCode")).InnerText;
                data.Occupation = (root.SelectSingleNode("Occupation")).InnerText;
                data.DocumentID = (root.SelectSingleNode("DocumentID")).InnerText;
                data.InsuranceNo = (root.SelectSingleNode("InsuranceNo")).InnerText;
                data.AdmType = (root.SelectSingleNode("AdmType")).InnerText;
                data.AdmNo = (root.SelectSingleNode("AdmNo")).InnerText;
                data.AdmSerialNum = (root.SelectSingleNode("AdmSerialNum")).InnerText;
                data.FeeType = (root.SelectSingleNode("FeeType")).InnerText;
                data.WardCode = (root.SelectSingleNode("WardCode")).InnerText;
                data.Ward = (root.SelectSingleNode("Ward")).InnerText;
                data.RoomCode = (root.SelectSingleNode("RoomCode")).InnerText;
                data.Room = (root.SelectSingleNode("Room")).InnerText;
                data.BedNo = (root.SelectSingleNode("BedNo")).InnerText;
                data.ClinicDiagnose = (root.SelectSingleNode("ClinicDiagnose")).InnerText;
                data.ClinicDisease = (root.SelectSingleNode("ClinicDisease")).InnerText;
                data.OperationInfo = (root.SelectSingleNode("OperationInfo")).InnerText;
                data.OtherInfo = (root.SelectSingleNode("OtherInfo")).InnerText;
                data.AdmDocRowID = (root.SelectSingleNode("AdmDocRowID")).InnerText;
                data.AdmDocCode = (root.SelectSingleNode("AdmDocCode")).InnerText;
                data.AdmDoc = (root.SelectSingleNode("AdmDoc")).InnerText;
                data.OrdRowID = (root.SelectSingleNode("OrdRowID")).InnerText;
                data.ArcimCode = (root.SelectSingleNode("ArcimCode")).InnerText;
                data.OrdName = (root.SelectSingleNode("OrdName")).InnerText;
                data.OrdPrice = (root.SelectSingleNode("OrdPrice")).InnerText;
                data.OrdBillStatus = (root.SelectSingleNode("OrdBillStatus")).InnerText;
                data.OrdPriorityCode = (root.SelectSingleNode("OrdPriorityCode")).InnerText;
                data.OrdPriority = (root.SelectSingleNode("OrdPriority")).InnerText;
                data.OrdHospital = (root.SelectSingleNode("OrdHospital")).InnerText;
                data.OrdHospitalCode = (root.SelectSingleNode("OrdHospitalCode")).InnerText;
                data.OrdExeHospital = (root.SelectSingleNode("OrdExeHospital ")).InnerText;
                data.OrdExeHospitalCode = (root.SelectSingleNode("OrdExeHospitalCode")).InnerText;
                data.ARCItemCat = (root.SelectSingleNode("ARCItemCat")).InnerText;
                data.ARCItemCatCode = (root.SelectSingleNode("ARCItemCatCode")).InnerText;
                data.OECOrderCategory = (root.SelectSingleNode("OECOrderCategory")).InnerText;
                data.OECOrderCategoryCode = (root.SelectSingleNode("OECOrderCategoryCode")).InnerText;
                data.OrdLocCode = (root.SelectSingleNode("OrdLocCode")).InnerText;
                data.OrdLoc = (root.SelectSingleNode("OrdLoc")).InnerText;
                data.OrdDoctorCode = (root.SelectSingleNode("OrdDoctorCode")).InnerText;
                data.OrdDoctor = (root.SelectSingleNode("OrdDoctor")).InnerText;
                data.OrdDate = (root.SelectSingleNode("OrdDate")).InnerText;
                data.OrdTime = (root.SelectSingleNode("OrdTime")).InnerText;
                data.OrdExeLocCode = (root.SelectSingleNode("OrdExeLocCode")).InnerText;
                data.OrdExeLoc = (root.SelectSingleNode("OrdExeLoc")).InnerText;
                data.SampleCode = (root.SelectSingleNode("SampleCode")).InnerText;
                data.SampleName = (root.SelectSingleNode("SampleName")).InnerText;
                data.SendFlag = (root.SelectSingleNode("SendFlag")).InnerText;
                data.NoteInfo = (root.SelectSingleNode("NoteInfo")).InnerText;
                data.Position = (root.SelectSingleNode("Position")).InnerText;
                data.Purpose = (root.SelectSingleNode("Purpose")).InnerText;
                data.CurCase = (root.SelectSingleNode("CurCase")).InnerText;
                data.Destination = (root.SelectSingleNode("Destination")).InnerText;
                data.AutoFlag = (root.SelectSingleNode("AutoFlag")).InnerText;
                data.BookDate = (root.SelectSingleNode("BookDate")).InnerText;
                data.BookTime = (root.SelectSingleNode("BookTime")).InnerText;
                data.PhyAddress = (root.SelectSingleNode("PhyAddress")).InnerText;
                data.SpecialMarket = (root.SelectSingleNode("SpecialMarket")).InnerText;
                data.SpecialPerson = (root.SelectSingleNode("SpecialPerson")).InnerText;
                data.SpecialDate = (root.SelectSingleNode("SpecialDate")).InnerText;
                data.StopDocCode = (root.SelectSingleNode("StopDocCode")).InnerText;
                data.StopDocDesc = (root.SelectSingleNode("StopDocDesc")).InnerText;
                data.Modality = (root.SelectSingleNode("Modality")).InnerText;
            }
            return data;
        }
        public void SaveSendAppBillResult(SendAppBillResult data)
        {
            using (var con = new SqlConnection(this.GetDbConByOrdName(data.OrdName)))
            {
                var sqlps = new SqlParameter[] { new SqlParameter("@strlen", "000"),
                    new SqlParameter("@Save", 0) };
                var patid = SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "GetPatID", sqlps).ToString();
                SqlParameter[] sqlparams = Getyydjparas(data, patid);
                var fieldstr = string.Join(",", sqlparams.Select(x => x.ParameterName.Replace("@", "")));
                var valuestr = string.Join(",", sqlparams.Select(x => x.ParameterName));
                SqlHelper.ExecuteNonQuery(con, CommandType.Text,
                    $"Insert into yydj ({fieldstr}) VALUES ({valuestr});", sqlparams);
                var maxcid = SqlHelper.ExecuteScalar(con, CommandType.Text, "select isnull(max(cid),0) maxcid from communion").ToString();
                var cid = int.Parse(maxcid) + 1;
                SqlParameter[] sqlparams1 = Getcommonparas(data, patid, cid);
                var fieldstr1 = string.Join(",", sqlparams1.Select(x => x.ParameterName.Replace("@", "")));
                var valuestr1 = string.Join(",", sqlparams1.Select(x => x.ParameterName));
                SqlHelper.ExecuteNonQuery(con, CommandType.Text,
                    $"Insert into communion ({fieldstr1}) VALUES ({valuestr1});", sqlparams1);
                con.Close();
            }

        }

        private SqlParameter[] Getcommonparas(SendAppBillResult data, string patid, int cid)
        {
            var sexcode = 0;
            switch (data.SexCode)
            {
                case "F":
                    {
                        sexcode = 2;
                        break;
                    }
                case "M":
                    {
                        sexcode = 1;
                        break;
                    }
            }
            var Chztypecode = 9;
            switch (data.AdmType?.ToUpper())
            {
                case "O":
                    {
                        Chztypecode = 1;
                        break;
                    }
                case "E":
                    {
                        Chztypecode = 2;
                        break;
                    }
                case "I":
                    {
                        Chztypecode = 3;
                        break;
                    }
            }
            var sqlparams1 = new SqlParameter[] {
                new SqlParameter("@Cid", cid),
                new SqlParameter("@Cpatid", patid),
                new SqlParameter("@Chztype", data.AdmType),
                new SqlParameter("@Cpatname", data.Name),
                new SqlParameter("@Croom", data.Room),
                new SqlParameter("@Croomcode", data.RoomCode),
                new SqlParameter("@Cward", data.Ward),
                new SqlParameter("@Cwardcode", data.WardCode),
                new SqlParameter("@Csendcode", data.OrdLocCode),
                new SqlParameter("@Cjccode", data.ArcimCode),
                new SqlParameter("@Cjcxm", data.OrdName),
                new SqlParameter("@Csexcode", sexcode),
                new SqlParameter("@Chztypecode", Chztypecode),
                new SqlParameter("@cyzh", data.OrdRowID),
                new SqlParameter("@cjzh", data.AdmNo),
                new SqlParameter("@cdjh", data.RegNo)};
            return sqlparams1;
        }

        private SqlParameter[] Getyydjparas(SendAppBillResult data, string patid)
        {
            var agestr = data.Age?.Split("岁".ToCharArray());
            var age = agestr == null ? 0 : int.Parse(agestr[0]);
            return new SqlParameter[] {
                    new SqlParameter("@Bzyno", data.DocumentID) ,
                    new SqlParameter("@yzh", data.OrdRowID),
                    new SqlParameter("@patientid", data.CredentialNo),
                    new SqlParameter("@jzh", data.AdmNo),
                    new SqlParameter("@bpatage", age),
                    new SqlParameter("@pattel", data.Telephone),
                    new SqlParameter("@patsex", data.Sex),
                    new SqlParameter("@patname", data.Name),
                    new SqlParameter("@bcwno", data.BedNo),
                    new SqlParameter("@sendunit", data.OrdLoc),
                    new SqlParameter("@patid", patid),
                    new SqlParameter("@Bdate", data.BookDate),
                    new SqlParameter("@Booktime", data.BookTime),
                    new SqlParameter("@Pataddr", data.Address),
                    new SqlParameter("@Pnative", data.Nation),
                    new SqlParameter("@Bfeature", data.ClinicDiagnose),
                    new SqlParameter("@Senddr", data.OrdDoctor),
                    new SqlParameter("@DJZT", 0),
                    new SqlParameter("@yc", data.RegNo)
                };
        }
    }
}
