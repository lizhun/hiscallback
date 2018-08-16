using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class SendAppBillResult
    {
        public string RegNo { get; set; }//	登记号 
        public string CardNo { get; set; }//	卡号
        public string Name { get; set; }//	姓名 
        public string SexCode { get; set; }//	性别代码
        public string Sex { get; set; }//	性别 
        public string Age { get; set; }//	年龄 
        public string BirthDay { get; set; }//	出生日期 
        public string Marry { get; set; }//	婚姻 
        public string Address { get; set; }//	地址 
        public string Telephone { get; set; }//	电话 
        public string CredentialNo { get; set; }//	证件号 
        public string CredentialType { get; set; }//	证件类型
        public string NationCode { get; set; }//	民族代码
        public string Nation { get; set; }//	民族 
        public string OccupationCode { get; set; }//	职业代码
        public string Occupation { get; set; }//	职业 
        public string DocumentID { get; set; }//	病案号
        public string InsuranceNo { get; set; }//	医保号 
        public string AdmType { get; set; }//	就诊类别
        public string AdmNo { get; set; }//	就诊ID 
        public string AdmSerialNum { get; set; }//	就诊流水号 
        public string FeeType { get; set; }//	费别
        public string WardCode { get; set; }//	病区代码
        public string Ward { get; set; }//	病区 
        public string RoomCode { get; set; }//	房间代码
        public string Room { get; set; }//	房间号
        public string BedNo { get; set; }//	床号 
        public string ClinicDiagnose { get; set; }//	临床诊断 
        public string ClinicDisease { get; set; }//	临床病史 
        public string OperationInfo { get; set; }//	手术资料 
        public string OtherInfo { get; set; }//	其他信息
        public string AdmDocRowID { get; set; }//	就诊医生指针
        public string AdmDocCode { get; set; }//	就诊医生代码
        public string AdmDoc { get; set; }//	就诊医生
        public string OrdRowID { get; set; }//	医嘱号
        public string ArcimCode { get; set; }//	医嘱项目代码
        public string OrdName { get; set; }//	医嘱名称 
        public string OrdPrice { get; set; }//	医嘱价格
        public string OrdBillStatus { get; set; }//	收费状态
        public string OrdPriorityCode { get; set; }//	医嘱类别代码
        public string OrdPriority { get; set; }//	医嘱类别
        public string OrdHospital { get; set; }//	开单医院 
        public string OrdHospitalCode { get; set; }//	开单医院代码
        public string OrdExeHospital { get; set; }//	执行医院
        public string OrdExeHospitalCode { get; set; }//	执行医院代码
        public string ARCItemCat { get; set; }//	医嘱子类
        public string ARCItemCatCode { get; set; }//	医嘱子类代码
        public string OECOrderCategory { get; set; }//	医嘱大类
        public string OECOrderCategoryCode { get; set; }//	医嘱大类代码
        public string OrdLocCode { get; set; }//	开单科室代码
        public string OrdLoc { get; set; }//	开单科室 
        public string OrdDoctorCode { get; set; }//	开单医生代码
        public string OrdDoctor { get; set; }//	开单医生 
        public string OrdDate { get; set; }//	开单日期 
        public string OrdTime { get; set; }//	开单时间 
        public string OrdExeLocCode { get; set; }//	执行科室代码
        public string OrdExeLoc { get; set; }//	执行科室 
        public string SampleCode { get; set; }//	标本代码
        public string SampleName { get; set; }//	标本名称 
        public string SendFlag { get; set; }//	发送标志
        public string NoteInfo { get; set; }//	备注
        public string Position { get; set; }//	部位
        public string Purpose { get; set; }//	检查目的
        public string CurCase { get; set; }//	当前情况
        public string Destination { get; set; }//	发送位置
        public string AutoFlag { get; set; }//	自动预约标志
        public string BookDate { get; set; }//	预约日期
        public string BookTime { get; set; }//	预约时间
        public string PhyAddress { get; set; }//	病人所在地址
        public string SpecialMarket { get; set; }//	特殊标志
        public string SpecialPerson { get; set; }//	期望执行医生
        public string SpecialDate { get; set; }//	期望执行日期
        public string StopDocCode { get; set; }//	停医嘱医生代码
        public string StopDocDesc { get; set; }//	停医嘱医生
        public string Modality { get; set; }//	设备名称

    }
}
