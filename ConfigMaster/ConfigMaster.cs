using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigMaster
{
    public static class Configer
    {
        public static string GetValue(string FilePath, string Route)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FilePath);
            return xmlDoc.SelectSingleNode(Route).InnerText.Trim();
        }
    }
}
