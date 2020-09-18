using App.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace App.Forms
{
    public partial class FrmTermos : Form
    {
        private Variable selectedVariable;
        private Term selectedTerm;
        private List<Term> terms;
        public int QTDTERMOS;
        List<Variable> listVariables;

        public FrmTermos(int qtdTermos)
        {
            this.QTDTERMOS = qtdTermos;
            InitializeComponent();
            loadGridVariables();
        }

        private int verifyForm()
        {

            if (txtDescricao.Text.Trim().Length == 0)
            {
                MessageBox.Show("A descrição deve ser informada");
                txtDescricao.Focus();
                return -1;
            }

            if (txtSinicio.Text.Trim().Length == 0)
            {
                MessageBox.Show("O início do suporte deve ser informado");
                txtSinicio.Focus();
                return -1;
            }

            if (txtSfim.Text.Trim().Length == 0)
            {
                MessageBox.Show("O fim do suporte deve ser informado");
                txtSinicio.Focus();
                return -1;
            }

            if (txtNinicio.Text.Trim().Length == 0)
            {
                MessageBox.Show("O início do núcleo deve ser informado");
                txtNinicio.Focus();
                return -1;
            }

            if (txtNfim.Text.Trim().Length == 0)
            {
                MessageBox.Show("O fim do núcleo deve ser informado");
                txtNfim.Focus();
                return -1;
            }

            if (verifyInteger(txtSinicio, "O campo inicial do suporte deve ser um número válido") == -1) return -1;
            if (verifyInteger(txtSfim, "O campo final do suporte deve ser um número válido") == -1) return -1;
            if (verifyInteger(txtNinicio, "O campo inicial do núcleo deve ser um número válido") == -1) return -1;
            if (verifyInteger(txtNfim, "O campo inicial do núvcleo deve ser um número válido") == -1) return -1;

            if (Convert.ToInt32(txtSinicio.Text) > Convert.ToInt32(txtSfim.Text))
            {
                MessageBox.Show("O valor inicial deve ser menor ou igual ao valor final do suporte");
            }

            if (Convert.ToInt32(txtNinicio.Text) > Convert.ToInt32(txtNfim.Text))
            {
                MessageBox.Show("O valor inicial deve ser menor ou igual ao valor final do núcleo");
            }

            return 0;
        }

        private void loadGridTerms()
        {
            dgvTermos.Rows.Clear();
            foreach(Term term in terms)
            {
                dgvTermos.Rows.Add(term.name, term.support[0], term.support[1],term.core[0],term.core[1]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (selectedVariable == null)
            {
                MessageBox.Show("Selecione uma variável válida");
                return;
            }

            if (QTDTERMOS == terms.Count())
            {
                MessageBox.Show("Quantidade de termos excede o limite parametrizado. Limite: "+ QTDTERMOS);
                formClear();
                txtDescricao.Focus();
                return;
            }

            if (verifyForm() == -1)
                return;

            Term termo = new Term();
            termo.name = txtDescricao.Text;
            termo.support[0] = Convert.ToInt32(txtSinicio.Text);
            termo.support[1] = Convert.ToInt32(txtSfim.Text);
            termo.core[0] = Convert.ToInt32(txtNinicio.Text);
            termo.core[1] = Convert.ToInt32(txtNfim.Text);

            xmlNewItem(termo);

            formClear();

            terms = Term.listXml(selectedVariable);

            loadGridTerms();

            txtDescricao.Focus();
        }

        private void xmlNewItem(Term termo)
        {
            XmlElement child;
            XmlElement root;
            XmlDocument doc = new XmlDocument();

            doc.Load(FrmPrincipal.PATHVARIABLEXML);

            foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
            {
                if(node["nome"].InnerText == selectedVariable.name)
                {
                    foreach (XmlNode nodeT in node.SelectNodes("termos"))
                    {
                        child = doc.CreateElement("termo");
                        nodeT.AppendChild(child);

                        root = child;
                        
                        child = doc.CreateElement("descricao");
                        child.InnerText = termo.name;
                        root.AppendChild(child);

                        child = doc.CreateElement("suporteinicio");
                        child.InnerText = termo.support[0].ToString();
                        root.AppendChild(child);
                        
                        child = doc.CreateElement("suportefim");
                        child.InnerText = termo.support[1].ToString();
                        root.AppendChild(child);

                        child = doc.CreateElement("nucleoinicio");
                        child.InnerText = termo.core[0].ToString();
                        root.AppendChild(child);

                        child = doc.CreateElement("nucleofim");
                        child.InnerText = termo.core[1].ToString();
                        root.AppendChild(child);
                    }
                }
            }

            doc.Save(FrmPrincipal.PATHVARIABLEXML);
        }
        
        // Seleciona a variável
        private void dgvVariaveis_MouseClick(object sender, MouseEventArgs e)
        {
            if (listVariables.Count == 0)
            {
                MessageBox.Show("Cadastre variáveis para inserir os termos");
                this.Close();
                return;
            }


            if (dgvVariaveis.CurrentRow.Index < listVariables.Count)
            {
                string name = dgvVariaveis.Rows[dgvVariaveis.CurrentRow.Index].Cells[0].Value.ToString();

                if (listVariables.Count > 0)
                {
                    foreach (Variable v in listVariables)
                    {
                        if (v.name == name)
                        {
                            selectedVariable = v;
                            break;
                        }
                    }
                    lblVariavel.Text = selectedVariable.name;
                }
                terms = Term.listXml(selectedVariable);
                loadGridTerms();

                txtDescricao.Enabled = true;
                txtSinicio.Enabled = true;
                txtSfim.Enabled = true;
                txtNinicio.Enabled = true;
                txtNfim.Enabled = true;
                selectedTerm = null;
                formClear();
                stateButtons(true);
            }
            else
            {
                txtDescricao.Enabled = true;
                txtSinicio.Enabled = true;
                txtSfim.Enabled = true;
                txtNinicio.Enabled = true;
                txtNfim.Enabled = true;
                selectedTerm = null;
                formClear();
                stateButtons(true);
            }
        }

        // Atualiza Grid de variáveis
        private void loadGridVariables()
        {
            listVariables = Variable.listXml();

            if (listVariables==null)
            {
                MessageBox.Show("Não foram encontradas variáveis cadastradas, entre em configuração>variáveis para cadastrar uma nova variável.");
                this.Dispose();
            }

            dgvVariaveis.Rows.Clear();

            foreach (Variable v in listVariables)
            {
                dgvVariaveis.Rows.Add(v.name,
                                      v.universe[0],
                                      v.universe[1],
                                      (v.isObjective) ? "S" : "N");
            }
        }

        // Limpa dados do formulário
        private void formClear()
        {
            txtDescricao.ResetText();
            txtNfim.ResetText();
            txtNinicio.ResetText();
            txtSfim.ResetText();
            txtSinicio.ResetText();
        }

        private void stateButtons(bool state)
        {
            if (state)
            {
                btnAdd.Enabled = true;
                btnCancel.Enabled = false;
                btnTrash.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = false;
                btnCancel.Enabled = true;
                btnTrash.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }
    
        private int verifyInteger(TextBox txt, string messageError)
        {
            try
            {
                int valor = Convert.ToInt32(txt.Text);
                return 0;
            }
            catch (Exception)
            {
                MessageBox.Show(messageError);
                txt.Text = "";
                txt.Focus();
                return -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formClear();
            txtDescricao.Focus();
            selectedTerm = null;
            stateButtons(true);

        }

        private void dgvTermos_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (selectedVariable == null)
                {
                    MessageBox.Show("Selecione uma variável válida");
                }
                else
                {
                    terms = Term.listXml(selectedVariable);

                    string description = dgvTermos.Rows[dgvTermos.CurrentRow.Index].Cells[0].Value.ToString();

                    if (listVariables.Count > 0)
                    {
                        foreach (Term t in terms)
                        {
                            if (t.name == description)
                            {
                                selectedTerm = t;
                                break;
                            }
                        }
                    }

                    txtDescricao.Text = selectedTerm.name;
                    txtSinicio.Text = selectedTerm.support[0].ToString();
                    txtSfim.Text = selectedTerm.support[1].ToString();
                    txtNinicio.Text = selectedTerm.core[0].ToString();
                    txtNfim.Text = selectedTerm.core[1].ToString();

                    stateButtons(false);
                }
            }
            catch (Exception) { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(selectedTerm == null)
            {
                MessageBox.Show("Selecione uma variável válida");
            }
            else
            {
                if (verifyForm() == -1)
                    return;

                XmlDocument doc = new XmlDocument();
                doc.Load(FrmPrincipal.PATHVARIABLEXML);
                foreach (XmlNode nodeV in doc.SelectNodes("configuracao/variavel"))
                {
                    if (nodeV["nome"].InnerText == selectedVariable.name)
                    {
                        foreach (XmlNode nodeT in nodeV.ChildNodes)
                        {
                            if (nodeT.LocalName == "termos")
                            {
                                foreach (XmlNode node in nodeT.ChildNodes)
                                {
                                    if (node.LocalName == "termo")
                                    {
                                        if (node["descricao"].InnerText == selectedTerm.name)
                                        {
                                            node["descricao"].InnerText = txtDescricao.Text.ToUpper();
                                            node["suporteinicio"].InnerText = txtSinicio.Text;
                                            node["suportefim"].InnerText = txtSfim.Text;
                                            node["nucleoinicio"].InnerText = txtNinicio.Text;
                                            node["nucleofim"].InnerText = txtNfim.Text;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                doc.Save(FrmPrincipal.PATHVARIABLEXML);

                stateButtons(true);

                terms = Term.listXml(selectedVariable);
                loadGridTerms();

                formClear();
            }
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            if (selectedTerm != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FrmPrincipal.PATHVARIABLEXML);
                foreach (XmlNode root in doc.SelectNodes("configuracao"))
                {
                    foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
                    {
                        if (node["nome"].InnerText == selectedVariable.name)
                        {
                            foreach (XmlNode nodeTerms in node.ChildNodes)
                            {
                                if (nodeTerms.LocalName == "termos")
                                {
                                    foreach (XmlNode nodeTerm in nodeTerms.ChildNodes)
                                    {
                                        if (nodeTerm.LocalName == "termo")
                                        {
                                            if (nodeTerm["descricao"].InnerText == selectedTerm.name)
                                            {
                                                nodeTerms.RemoveChild(nodeTerm);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                doc.Save(FrmPrincipal.PATHVARIABLEXML);

                terms = Term.listXml(selectedVariable);

                loadGridTerms();

                formClear();

                stateButtons(true);
            }
            else
            {
                stateButtons(true);
            }
        }
    }
}
