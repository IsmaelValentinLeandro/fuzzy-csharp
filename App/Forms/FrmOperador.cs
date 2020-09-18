using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Forms
{
    public partial class FrmOperador : Form
    {
        public FrmOperador()
        {
            InitializeComponent();
            loadOperations();
        }

        private void loadOperations()
        {
            string[] operadores = new string[] { "E", "OU" };
            cbOperacao.DataSource = operadores;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
