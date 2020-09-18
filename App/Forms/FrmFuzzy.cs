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
using System.Xml;

namespace App.Forms
{
    public partial class FrmFuzzy : Form
    {
        private Variable old { get; set; }
        List<Variable> listVariablesObjective = new List<Variable>();
        List<Variable> listVariables = new List<Variable>();

        public FrmFuzzy()
        {
            InitializeComponent();
            loadComboboxVariables();
        }

        private void loadVariables()
        {
            listVariables = Variable.listXml();

            foreach(Variable v in listVariables)
                v.terms = Term.listXml(v);
        }

        private void loadComboboxVariables()
        {
            loadVariables();
            cbVariavel.DataSource = listVariables.Where(x => x.isObjective == false).ToList();
            cbVariavel.DisplayMember = "name";
            cbVariavel.ValueMember = "name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Variable variable = listVariables[cbVariavel.SelectedIndex];
                variable.insertValue = Convert.ToDouble(txtValue.Text);
                dgvFuzzy.Rows.Add(variable.insertValue, variable.name);
            }
            catch (Exception) { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            fuzzyfication();
        }

        private void fuzzyfication()
        {

            double percent = 0;

            FuzzyAlgorithm  fuzzy = new FuzzyAlgorithm();
	        double value = 0;
	        string operador = "";
	
	        XmlDocument doc = new XmlDocument();
	        doc.Load(FrmPrincipal.PATHRULESXML);

            XmlNodeList listNode = doc.SelectNodes("configuracao/regra");

            pBar.Value = Convert.ToInt32(percent);
            
            foreach (XmlNode nodeR in listNode)
	        {
		        foreach (XmlNode node in nodeR.ChildNodes)
		        {
			        if (node.LocalName == "INICIA")
			        {
                        old = new Variable();
				        operador = "";
			        }
			        else if (node.LocalName == "CLAUSULA")
			        {
                        value = getPertinence(fuzzy, listVariables, node.InnerText, operador);				        
			        }
			        else if (node.LocalName == "OPERADOR")
			        {
				        operador = node.InnerText;
			        }
                    else if (node.LocalName == "RESULTADO")
			        {
				        operador = "";
			        }
                    else if (node.LocalName == "VALOR")
                    {
                        node.InnerText = value.ToString();
                    }
                }
                percent = (percent * 100) / listNode.Count;
                pBar.Value = Convert.ToInt32(percent);
	        }
	        doc.Save(FrmPrincipal.PATHRULESXML);
            pBar.Value = 100;
            MessageBox.Show("Calculo efetuado com sucesso!");
        }

        private Double getPertinence(FuzzyAlgorithm fuzzy, List<Variable> variables, string cmd, string op)
        {
            Variable variable = null;
            Term term = null;
            Double value;

            foreach (Variable v in variables)
            {
                if (v.name == cmd.Split('=')[0])
                {
                    variable = v;
                    foreach (Term t in v.terms)
                    {
                        if (t.name == cmd.Split('=')[1])
                        {
                            term = t;
                            break;
                        }
                    }
                }
            }


            fuzzy.parameters(variable.universe, term.support, term.core, variable.insertValue);
            term.relevance = fuzzy.getRelevance;

            variable.rulesResult = term.relevance;

            if (old!=null)
            {
                if (op == "E")
                    if( old.rulesResult>variable.rulesResult)
                        value = variable.rulesResult;
                    else
                        value = old.rulesResult;
                else if (op == "OU")
                    if (old.rulesResult > variable.rulesResult)
                        value = old.rulesResult;
                    else
                        value = variable.rulesResult;
                else
                    value = variable.rulesResult;
            }
            else
            {
                value = variable.rulesResult;
            }
                
            old = variable;
            return value;
        }

    }
}
