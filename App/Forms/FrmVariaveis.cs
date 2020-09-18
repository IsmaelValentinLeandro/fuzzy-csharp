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

namespace App
{
    public partial class FrmVariaveis : Form
    {
        private int QTDVARIAVEIS;
        private List<Variable> list = new List<Variable>();
        private Variable selectedVariable;

        public FrmVariaveis(int qtdVar)
        {
            this.QTDVARIAVEIS = qtdVar;
            InitializeComponent();
            verificaArquivoConfiguracao();
            list = Variable.listXml();
            loadGrid();
            txtVariavel.Focus();
        }

        private void verificaArquivoConfiguracao()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FrmPrincipal.PATHVARIABLEXML)) 
            {
                doc.LoadXml("<?xml version='1.0'?>" +
                            "<configuracao>" +
                            "</configuracao>");
                doc.Save(FrmPrincipal.PATHVARIABLEXML);

                MessageBox.Show("Arquivo de configuração adicionado com sucesso!");
            }
            else
            {
                doc.Load(FrmPrincipal.PATHVARIABLEXML);

                Variable.listXml();

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (verifyForm() == -1)
                return;

            if (dgvVariaveis.RowCount > QTDVARIAVEIS)
            {
                MessageBox.Show("Quantidade de variáveis ultrapassa o limite parametrizado. Limite = " + QTDVARIAVEIS);
                return;
            }

            try{
                Variable var = new Variable();
                var.name = txtVariavel.Text;
                var.universe[0] = Convert.ToInt32(txtInicio.Text); ;
                var.universe[1] = Convert.ToInt32(txtFim.Text); ;
                var.isObjective = chbObjetivo.Checked;

                foreach (Variable v in list)
                {
                    if (v.name == var.name)
                    {
                        MessageBox.Show("A descrição " + v.name + " já foi inserida!", "Atenção");
                        txtVariavel.Focus();
                        return;
                    }
                }

                newItemXml(var);

                list = Variable.listXml();
                loadGrid();

                controlButtons(false);
                txtVariavel.Focus();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Erro: "+exc.Message);
            }
        }

        private int verifyForm()
        {
            string value;
            int inicio, fim;
            try
            {
                if (txtVariavel.Text.Trim().Length == 0)
                {
                    MessageBox.Show("O nome da variável deve ser inserido");
                    txtVariavel.Focus();
                    return -1;
                }
                value = txtVariavel.Text;

                try
                {
                    if (txtInicio.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("O valor inicial do universo deve ser inserido");
                        txtInicio.Focus();
                        return -1;
                    }
                    inicio = Convert.ToInt32(txtInicio.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("O início deve ser um valor numérico");
                    txtInicio.Focus();
                    return -1;
                }

                try
                {
                    if (txtFim.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("O valor final do universo deve ser inserido");
                        txtFim.Focus();
                        return -1;
                    }
                    fim = Convert.ToInt32(txtFim.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("O fim deve ser um valor numérico");
                    txtFim.Focus();
                    return -1;
                }

                if (inicio > fim)
                {
                    MessageBox.Show("O inicio deve ser menor que o fim do universo");
                    txtInicio.Focus();
                    return -1;
                }

                if ( chbObjetivo.Checked )
                {
                    foreach(Variable v in list)
                    {
                        if (v.isObjective)
                        {
                            MessageBox.Show("Erro somente deve ser selecionado uma variável objetivo.");
                            chbObjetivo.Focus();
                            return -1;
                        }
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return -1;
            }

            return 0;
        }

        private void newItemXml(Variable var)
        {

            XmlElement rootNode;
            XmlElement root;
            XmlDocument doc = new XmlDocument();

            doc.Load(FrmPrincipal.PATHVARIABLEXML);

            root = doc.CreateElement("variavel");
            doc.DocumentElement.AppendChild(root);
            
            // inicio universo
            rootNode = doc.CreateElement("inicio");
            rootNode.InnerText = var.universe[0].ToString();
            root.AppendChild(rootNode);

            // fim universo
            rootNode = doc.CreateElement("fim");
            rootNode.InnerText = var.universe[1].ToString();
            root.AppendChild(rootNode);

            /*
            // id
            rootNode = doc.CreateElement("id");
            rootNode.InnerText = var.id.ToString();
            root.AppendChild(rootNode);
             */
            
            // name
            rootNode = doc.CreateElement("nome");
            rootNode.InnerText = var.name;
            root.AppendChild(rootNode);
            
            // variable objective
            rootNode = doc.CreateElement("objetivo");
            rootNode.InnerText = (var.isObjective) ? "1" : "0"; 
            root.AppendChild(rootNode);

            rootNode = doc.CreateElement("termos");
            root.AppendChild(rootNode);

            doc.Save(FrmPrincipal.PATHVARIABLEXML);

            clearForm();

        }

        private void clearForm()
        {
            txtVariavel.ResetText();
            txtInicio.ResetText();
            txtFim.ResetText();
        }

        public void loadGrid()
        {
            dgvVariaveis.Rows.Clear();

            foreach (Variable v in list)
            {
                dgvVariaveis.Rows.Add(v.name,
                                      v.universe[0],
                                      v.universe[1],
                                      (v.isObjective)? "S" : "N");
            }
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Você deseja apagar a informação selecionada?", "Atenção", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FrmPrincipal.PATHVARIABLEXML);

                foreach (XmlNode root in doc.SelectNodes("configuracao"))
                {
                    foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
                    {
                        if (node["nome"].InnerText == selectedVariable.name)
                        {
                            root.RemoveChild(node);
                        }
                    }
                }
                
                doc.Save(FrmPrincipal.PATHVARIABLEXML);

                controlButtons(false);

                list = Variable.listXml();
                loadGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica validações do formulário
            if (verifyForm() == -1)
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(FrmPrincipal.PATHVARIABLEXML);

            foreach (XmlNode node in doc.SelectNodes("configuracao/variavel"))
            {
                if (node["nome"].InnerText == selectedVariable.name)
                {
                    node["nome"].InnerText = txtVariavel.Text.ToUpper();
                    node["inicio"].InnerText = txtInicio.Text;
                    node["fim"].InnerText = txtFim.Text;
                    node["objetivo"].InnerText = (chbObjetivo.Checked) ? "1" : "0";
                }
            }


            doc.Save(FrmPrincipal.PATHVARIABLEXML);

            controlButtons(false);

            list = Variable.listXml();
            loadGrid();
        }

        private void dgvVariaveis_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string name = dgvVariaveis.Rows[dgvVariaveis.CurrentRow.Index].Cells[0].Value.ToString();

                if (list.Count > 0)
                {
                    foreach (Variable v in list)
                    {
                        if (v.name == name)
                        {
                            selectedVariable = v;
                            break;
                        }
                    }
                }
                controlButtons(true);

                txtVariavel.Text = selectedVariable.name;
                txtInicio.Text = selectedVariable.universe[0].ToString();
                txtFim.Text = selectedVariable.universe[1].ToString();
                chbObjetivo.Checked = selectedVariable.isObjective;
            }
            catch (NullReferenceException) { }
        }
    
        private void controlButtons(bool control)
        {
            if (control)
            {
                btnTrash.Enabled = true;
                btnUpdate.Enabled = true;
                btnAdd.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = false;
                btnTrash.Enabled = false;
                btnUpdate.Enabled = false;
                btnAdd.Enabled = true;
                txtVariavel.ResetText();
                txtInicio.ResetText();
                txtFim.ResetText();
                chbObjetivo.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            selectedVariable = new Variable();
            controlButtons(false);
        }
    }
}
