using App.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace App.Forms
{
    public partial class FrmGrafico : Form
    {
        private List<StrRule> listRules;
        struct StrRule
        {
            public long id;
            public Variable variable;
            public Term term;
            public Double valor;
        };

        public FrmGrafico()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void loadVariables()
        {
            listRules = new List<StrRule>();
            Double value = 0;
            bool exists = false;
            StrRule index;
            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHRULESXML);

            foreach(XmlNode root in doc.SelectNodes("configuracao/regra"))
            {
                StrRule strRule = new StrRule();
                foreach(XmlNode node in root.ChildNodes)
                {
                    if ( node.LocalName == "ID" )
                    {
                       strRule.id  = Convert.ToInt32(node.InnerText);
                    }
                    else if ( node.LocalName == "RESULTADO")
                    {
                        strRule.variable = getVariable(node.InnerText.Split('=')[0]);
                        strRule.term = getTerm(strRule.variable, node.InnerText.Split('=')[1]);
                    }
                    else if (node.LocalName == "VALOR")
                    {
                        strRule.valor = Convert.ToDouble(node.InnerText);
                    }
                }

                foreach(StrRule rule in listRules)
                {
                    if (rule.term.name == strRule.term.name)
                    {
                        exists = true;

                        if (rule.valor < strRule.valor)
                        {
                            value = strRule.valor;
                            index = rule;
                        }
                    }
                }

                if (!exists)
                    listRules.Add(strRule);
                else
                    index.valor = strRule.valor;

                exists = false;

            }
        }

        private  Variable getVariable(string variable)
        {
            foreach (Variable v in Variable.listXml())
                if (v.name == variable)
                    return v;
            return null;
        }

        private Term getTerm(Variable variable, string term)
        {
            foreach(Term t in Term.listXml(variable))
                if (t.name == term)
                    return t;
            return null;
        }

        private void loadGraph()
        {
            try
            {
                //---CENTROIDE--
                List<double> listY = new List<double>();
                List<List<double>> listX = new List<List<double>>();
                double lastValue = 0;
                //--------------

                this.chart.Series.Clear();
                this.chart1.Series.Clear();

                Series series;
                int count = 0;

                listRules = listRules.OrderBy(x => x.term.name).Reverse().ToList();

                lblObjetivo.Text = listRules[0].variable.name;

                foreach (StrRule rule in listRules)
                {
                    series = this.chart.Series.Add(rule.term.name);
                    series.Color = (count == 0) ? Color.Red : (count == 1) ? Color.Blue : Color.Green;
                    count++;
                    series.BorderWidth = 2;
                    series.ChartType = SeriesChartType.Line;
                    series.Points.AddXY(rule.term.support[0], 0);
                    series.Points.AddXY(rule.term.core[0], 1);
                    series.Points.AddXY(rule.term.core[1], 1);
                    series.Points.AddXY(rule.term.support[1], 0);

                    series = this.chart1.Series.Add(rule.term.name);
                    series.Color = Color.GreenYellow;
                    series.BorderWidth = 2;
                    series.ChartType = SeriesChartType.Area;
                    series.Points.AddXY(rule.term.support[0], 0);
                    series.Points.AddXY(rule.term.core[0], rule.valor);
                    series.Points.AddXY(rule.term.core[1], rule.valor);
                    series.Points.AddXY(rule.term.support[1], 0);


                    List<double> valuesX = new List<double>();

                    if (rule.term.support[0] >= lastValue)
                        lastValue = rule.term.support[0];

                    for (double i = lastValue; i < rule.term.support[1]; i = i + 2)
                    {
                        valuesX.Add(i);
                    }

                    lastValue = rule.term.support[1];

                    listX.Add(valuesX);
                    listY.Add(rule.valor);
                }

                Double sum = 0;
                Double sup = 0;
                Double div = 0;

                for (int i = 0; i < listY.Count; i++)
                {
                    foreach (double vlr in listX[i])
                    {
                        sum += vlr;
                    }

                    sup += sum * listY[i];
                    div += listX[i].Count * listY[i];
                }

                lblCentroide.Text = (sup / div).ToString();
            }
            catch (Exception) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadVariables();
            loadGraph();
        }
    }
}
