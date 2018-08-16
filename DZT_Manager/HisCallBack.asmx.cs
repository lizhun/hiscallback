using BLL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace DZT_Manager
{
    /// <summary>
    /// HisCallBack 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class HisCallBack : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string str)
        {
            return str;
        }

        [WebMethod]
        public XmlDocument TestXmlInputOutput(XmlDocument xml)
        {
            return xml;
        }



        [WebMethod]
        public XmlDocument AckAntCVResult(string data)
        {
            // try
            // {
            var manager = new BLL.HisCallBackManager();
            var wjzdata = manager.loadAckAntCVResultXml(data);
            string type = wjzdata.AntCVResultID.Split("_".ToCharArray())[0];
            manager.SaveOrUpdateAckAntCVResult(type, wjzdata);
            var xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><code>0</code><msg>成功</msg></Response>");
            return xml;
            // }
            //catch (Exception e)
            //{
            //    var xml = new XmlDocument();
            //    xml.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><code>500</code><msg>失败</msg></Response>");
            //    return xml;
            //}
        }

        [WebMethod]
        public XmlDocument SendAppBillResult(string data)
        {
            // try
            // {
            var manager = new BLL.HisCallBackManager();
            var resdata = manager.LoadSendAppBillResult(data);
           // manager.SaveSendAppBillResult(resdata);
            var xml = new XmlDocument();
            //xml.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><code>0</code><msg>成功</msg></Response>");
            xml.LoadXml($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><code>0</code><msg>{resdata.OrdName}</msg></Response>");

            return xml;
            // }
            //catch (Exception e)
            //{
            //    var xml = new XmlDocument();
            //    xml.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><code>500</code><msg>失败</msg></Response>");
            //    return xml;
            //}
        }

    }
}
