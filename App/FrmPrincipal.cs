using App.Class;
using App.Forms;
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
    public partial class FrmPrincipal : Form
    {
        /// <summary>
        /// Variáveis
        /// </summary>
        private List<Variable> variaveis = new List<Variable>();
        public static string PATHVARIABLEXML = @"xml/base.xml";
        public static string PATHRULESXML = @"xml/rules.xml";
        protected const int QTDVARIAVEIS = 10;
        protected const int QTDTERMOS = 10;
        
        public FrmPrincipal()
        {
            InitializeComponent();

            verifyDirectoryXml();

        }

        private void verifyDirectoryXml()
        {
            if (!Directory.Exists(@"xml"))
            {
                Directory.CreateDirectory("xml");
                MessageBox.Show("Diretório de configuração criado com sucesso!");
            }
        }

        private void fuzzyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TesteFuzzy frm = new TesteFuzzy();
            // Set the parent form of the child window.
            frm.MdiParent = this;
            // Display the new form.
            frm.Show();
        }

        private void variáveisToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            openFrmVariable();            
        }

        private void openFrmVariable()
        {
            FrmVariaveis frm = new FrmVariaveis(QTDVARIAVEIS);
            // Set the parent form of the child window.
            frm.MdiParent = this;
            // Display the new form.
            frm.Show();
        }

        private void termosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFrmTerms();
        }

        private void openFrmTerms()
        {
            if (verifyArchive(FrmPrincipal.PATHRULESXML, "Variáveis não encontradas, favor efetue o cadastro das variáveis"))
            {
                FrmTermos frm = new FrmTermos(QTDTERMOS);
                // Set the parent form of the child window.
                frm.MdiParent = this;
                // Display the new form.
                frm.Show();
            }
            else
            {
                openFrmVariable();
            }
        }

        private void regrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FrmPrincipal.PATHVARIABLEXML))
            {
                MessageBox.Show("Cadastre variáveis para efetuar o cadastro de termos");
                openFrmVariable();
                return;
            }

            openRules();
        }

        private void openRules()
        {
            FrmRegras frm = new FrmRegras();
            // Set the parent form of the child window.
            frm.MdiParent = this;
            // Display the new form.
            frm.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void valoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFuzzy();
        }

        private void openFuzzy()
        {
            if (verifyArchive(FrmPrincipal.PATHRULESXML, "O arquivo de regras não encontrado, favor cadastrar as regras necessárias"))
            {
                FrmFuzzy frm = new FrmFuzzy();
                // Set the parent form of the child window.
                frm.MdiParent = this;
                // Display the new form.
                frm.Show();
            }
            else
            {
                openRules();
            }
        }

        private void graficosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChart();
        }

        private void openChart()
        {
            if (verifyArchive(FrmPrincipal.PATHRULESXML, "O arquivo de regras não encontrado, favor cadastrar as regras necessárias"))
            {
                FrmGrafico frm = new FrmGrafico();
                // Set the parent form of the child window.
                frm.MdiParent = this;
                // Display the new form.
                frm.Show();
            }
            else
            {
                openRules();
            }
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            // Set the parent form of the child window.
            frm.MdiParent = this;
            // Display the new form.
            frm.Show();
        }

        private bool verifyArchive(string path, string errorMessage)
        {
            if (File.Exists(path))
                return true;
            MessageBox.Show(errorMessage, "Ateñção");
            return false;
        }

    }
}
