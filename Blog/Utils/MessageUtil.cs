using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.Utils
{
    public class MessageUtil
    {
        
        string messageFilePath;
        XmlDocument xmlDoc;
        

        public MessageUtil()
        {
            this.xmlDoc = new XmlDocument();
            this.messageFilePath = AppDomain.CurrentDomain.BaseDirectory + "Utils\\XMLFile1.xml";
            xmlDoc.Load(messageFilePath);
        }

        public string getMessage(string nodeKey)
        {
            XmlNode xmlNode = xmlDoc.DocumentElement.SelectSingleNode(nodeKey);
            return xmlNode.InnerText;
        }
    }
}