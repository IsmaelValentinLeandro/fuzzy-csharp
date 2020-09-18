using App.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace App.Forms
{
    public partial class FrmRegras : Form
    {
        private bool inicioClausula = true;
        private Variable selectedVariable;
        private Rule selectedRule;

        private List<Variable> listVariables;
        private List<Term> listTerms;
        private List<Rule> listRules;
        
        public FrmRegras()
        {
            InitializeComponent();
            loadVariables();
            loadComboboxVariables();
            stateButtons(true);
            verifyArchiveXml();
            loadGrid();
        }

        private void verifyArchiveXml()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FrmPrincipal.PATHRULESXML))
            {
                doc.LoadXml("<?xml version='1.0'?>" +
                            "<configuracao>" +
                            "   <sequence>0</sequence>"+
                            "</configuracao>");
                doc.Save(FrmPrincipal.PATHRULESXML);

                MessageBox.Show("Arquivo de regras adicionado com sucesso!");
            }
            else
            {
                doc.Load(FrmPrincipal.PATHRULESXML);
            }
        }

        private void loadVariables()
        {
            listVariables = Variable.listXml();
        }

        private void loadComboboxVariables()
        {
            try
            {
                cbVariavel.DataSource = listVariables.Where(x => x.isObjective == false).ToList();
                cbVariavel.DisplayMember = "name";
                cbVariavel.ValueMember = "name";

                Variable variableObjective = listVariables.Where(x => x.isObjective == true).ToList()[0];
                txtVariavelObjetivo.Text = variableObjective.name;

                List<Term> termsObjectives = Term.listXml(variableObjective);

                cbTermosObjetivo.DataSource = termsObjectives;
                cbTermosObjetivo.DisplayMember = "name";
                cbTermosObjetivo.ValueMember = "name";

                cbVariavel.Select();
            }
            catch (Exception) { }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string operador, termo, variavel;

            selectedRule = null;
            
            TreeNode node;

            try
            {
                variavel = cbVariavel.SelectedValue.ToString();
                termo = cbTermo.SelectedValue.ToString();
            }
            catch (Exception) 
            {
                MessageBox.Show("Erro ao obter parâmetros");
                return; 
            }

            if (inicioClausula)
            {
                operador = "SE";
                inicioClausula = false;
            }
            else
            {
                FrmOperador frm = new FrmOperador();
                frm.ShowDialog(this);
                operador = frm.cbOperacao.SelectedValue.ToString();
            }

            node = new TreeNode(operador);
            tvClausula.Nodes.Add(node);

            node = new TreeNode(variavel + "=" + termo);
            tvClausula.Nodes.Add(node);
            
            cbTermo.ResetText();
        }
        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string termoObjetivo;
            try
            {
                termoObjetivo = cbTermosObjetivo.SelectedValue.ToString();
            }
            catch (Exception) { return; }

            TreeNode node;

            node = new TreeNode("ENTAO");
            tvClausula.Nodes.Add(node);


            node = new TreeNode(txtVariavelObjetivo.Text+ "=" + termoObjetivo);
            tvClausula.Nodes.Add(node);

            stateButtons(false);
            inicioClausula = true;

        }

        private void stateButtons(bool state)
        {
            if(state)
            {
                cbTermo.Enabled = true;
                cbTermosObjetivo.Enabled = true;
                cbVariavel.Enabled = true;
                btnAdd.Enabled = true;
                btnConfirm.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnTrash.Enabled = false;
                tvClausula.ResetText();
                tvClausula.Nodes.Clear();
            }
            else
            {
                cbTermo.Enabled = false;
                cbTermosObjetivo.Enabled = false;
                cbVariavel.Enabled = false;
                btnAdd.Enabled = false;
                btnConfirm.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnTrash.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool resultado = false;
            XmlElement child;
            XmlElement root;

            if (tvClausula.Nodes.Count==0)
            {
                return;
            }

            string id = Rule.getNextId().ToString();

            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHRULESXML);

            foreach (XmlNode node in doc.SelectNodes("configuracao"))
            {
                child = doc.CreateElement("regra");
                node.AppendChild(child);

                root = child;

                child = doc.CreateElement("ID");
                child.InnerText = id;
                root.AppendChild(child);

                foreach (TreeNode n in tvClausula.Nodes)
                {
                    if(n.Text == "SE")
                    {
                        child = doc.CreateElement("INICIA");
                        child.InnerText = n.Text;
                        root.AppendChild(child);
                    }
                    else if (n.Text == "ENTAO")
                    {
                        child = doc.CreateElement("FINALIZA");
                        child.InnerText = n.Text;
                        root.AppendChild(child);
                        
                        resultado = true;
                    }
                    else if (n.Text == "E" || n.Text == "OU")
                    {
                        child = doc.CreateElement("OPERADOR");
                        child.InnerText = n.Text;
                        root.AppendChild(child);
                    }
                    else
                    {
                        child = (resultado) ? doc.CreateElement("RESULTADO")  : doc.CreateElement("CLAUSULA");
                        child.InnerText = n.Text;
                        root.AppendChild(child);

                        if (resultado)
                        {
                            child = doc.CreateElement("VALOR");
                            child.InnerText = "";
                            root.AppendChild(child);
                        }
                    }
                }
            }

            doc.Save(FrmPrincipal.PATHRULESXML);

            stateButtons(true);

            tvClausula.ResetText();
            tvClausula.Nodes.Clear();

            loadGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateButtons(true);
        }
        
        private void loadGrid()
        {
            dgvRules.Rows.Clear();
            listRules = App.Class.Rule.listXml();
            foreach (Rule rule in listRules)
            {
                dgvRules.Rows.Add(rule.id, rule.toString());
            }
            lblQtdRules.Text = "Registros: "+ listRules.Count.ToString();


        }

        private void dgvRules_MouseClick(object sender, MouseEventArgs e)
        {
            stateButtons(false);
            tvClausula.ResetText();
            tvClausula.Nodes.Clear();

            TreeNode node;

            long id = Convert.ToInt64(dgvRules.Rows[dgvRules.CurrentRow.Index].Cells[0].Value.ToString());

            foreach(Rule rule in listRules)
            {
                if (id == rule.id)
                {
                    selectedRule = rule;
                }
            }

            foreach (string txt in selectedRule.listString)
            {
                node = new TreeNode(txt);
                tvClausula.Nodes.Add(node);
            }
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            if (selectedRule!=null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FrmPrincipal.PATHRULESXML);
                foreach (XmlNode root in doc.SelectNodes("configuracao"))
                {
                    foreach (XmlNode node in doc.SelectNodes("configuracao/regra"))
                    {
                        if (node["ID"].InnerText == selectedRule.id.ToString())
                        {
                            root.RemoveChild(node);
                        }
                    }
                }
                doc.Save(FrmPrincipal.PATHRULESXML);

                loadGrid();

                stateButtons(true);
                tvClausula.Nodes.Clear();
                tvClausula.ResetText();
            }
            else
            {
                tvClausula.Nodes.Clear();
                tvClausula.ResetText();
                stateButtons(true);
            }
        }

        private void cbVariavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadComboboxTerms();
        }
    
        private void loadComboboxTerms()
        {
            if (listVariables.Count != null)
            {
                cbTermo.ResetText();
                string value = cbVariavel.SelectedValue.ToString();
                foreach (Variable variable in listVariables)
                {
                    if (variable.name == value)
                    {
                        selectedVariable = variable;
                    }
                }
                if (selectedVariable != null)
                {
                    listTerms = Term.listXml(selectedVariable);
                    cbTermo.DataSource = listTerms;
                    cbTermo.DisplayMember = "name";
                    cbTermo.ValueMember = "name";
                }
            }
        }

        private void FrmRegras_Activated(object sender, EventArgs e)
        {
            loadComboboxTerms();
        }
    }
}
