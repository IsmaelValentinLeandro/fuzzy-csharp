using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Class
{
    class Term
    {
        public Term()
        {
            support = new Double[2];
            core = new Double[2];
        }

        public string name { get; set; }
        public Double[] support { get; set; }
        public Double[] core { get; set; }
        public Double relevance { get; set; }   
        
        public static List<Term> listXml(Variable variable)
        {
            List<Term> listTerms = new List<Term>();
            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHVARIABLEXML);
            foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
            {
                if (node["nome"].InnerText == variable.name)
                {
                    foreach (XmlNode nodeTerms in node.ChildNodes)
                    {
                        if (nodeTerms.LocalName == "termos")
                        {
                            foreach (XmlNode nodeTerm in nodeTerms.ChildNodes)
                            {
                                if (nodeTerm.LocalName == "termo")
                                {
                                    Term term = new Term();
                                    //term.id = Convert.ToInt32(nodeTerm["id"].InnerText);
                                    term.name = nodeTerm["descricao"].InnerText;
                                    term.support[0] = Convert.ToDouble(nodeTerm["suporteinicio"].InnerText);
                                    term.support[1] = Convert.ToDouble(nodeTerm["suportefim"].InnerText);
                                    term.core[0] = Convert.ToDouble(nodeTerm["nucleoinicio"].InnerText);
                                    term.core[1] = Convert.ToDouble(nodeTerm["nucleofim"].InnerText);
                                    listTerms.Add(term);
                                }
                            }
                        }
                    }
                }
            }
            return listTerms;
        }
    }
}
