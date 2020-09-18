using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Class
{
    class Variable
    {
        public Variable()
        {
            terms = new List<Term>();
            universe = new Double[2];
        }

        public string name { get; set; }
        public Double value { get; set; }
        public Double insertValue { get; set; }
        public Double rulesResult { get; set; }
        public List<Term> terms { get; set; }
        public bool isObjective { get; set; }
        public Double[] universe { get; set; }

        public static List<Variable> listXml()
        {
            List<Variable> listVariables = new List<Variable>();
            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHVARIABLEXML);
            foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
            {
                Variable var = new Variable();
                var.name = node["nome"].InnerText;
                var.universe[0] = Convert.ToInt32(node["inicio"].InnerText);
                var.universe[1] = Convert.ToInt32(node["fim"].InnerText);
                var.isObjective = (node["objetivo"].InnerText == "1") ? true : false;
                listVariables.Add(var);
            }
            return listVariables;
        }
    }
}
