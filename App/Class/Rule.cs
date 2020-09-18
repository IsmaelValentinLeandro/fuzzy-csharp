using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Class
{
    class Rule
    {
        public long id { get; set; }
        public List<String> listString = new List<String>();

        public string toString()
        {
            string txt = "";
            foreach (String str in this.listString)
                txt += str + " ";  
            return txt;
        }

        public static long getNextId()
        {
            long nextId = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHRULESXML);
            XmlNode node = doc.SelectSingleNode("configuracao");
            nextId = Convert.ToInt64(node["sequence"].InnerText);
            nextId++;
            node["sequence"].InnerText = nextId.ToString();
            doc.Save(FrmPrincipal.PATHRULESXML);
            return nextId;
        }

        public static List<Rule> listXml()
        {
            Rule rule;
            List<Rule> list = new List<Rule>();
            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHRULESXML);
            foreach (XmlNode nodeR in doc.SelectNodes("configuracao/regra"))
            {
                rule = new Rule();
                foreach(XmlNode node in nodeR.ChildNodes)
                {
                    if (node.LocalName == "ID")
                    {
                        rule.id = Convert.ToInt64(node.InnerText);
                    }
                    else
                    {
                        rule.listString.Add(node.InnerText);
                    }
                }
                list.Add(rule);
            }
            return list;
        }
       
    }
}
