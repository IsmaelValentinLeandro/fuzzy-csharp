using Fuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class TesteFuzzy : Form
    {
        public TesteFuzzy()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Double[] universo = new Double[2];
                Double[] suporte = new Double[2];
                Double[] nucleo = new Double[2];
                Double valor;

                universo[0] = Convert.ToDouble(txtUni1.Text);
                universo[1] = Convert.ToDouble(txtUni2.Text);

                suporte[0] = Convert.ToDouble(txtSup1.Text);
                suporte[1] = Convert.ToDouble(txtSup2.Text);

                nucleo[0] = Convert.ToDouble(txtNuc1.Text);
                nucleo[1] = Convert.ToDouble(txtNuc2.Text);

                valor = Convert.ToDouble(txtValue.Text);

                FuzzyAlgorithm fuzzy = new FuzzyAlgorithm(universo, suporte, nucleo, valor);
                lblResultado.Text = "Pertinência: " + fuzzy.getRelevance.ToString("###,##0.0000");

            }
            catch(Exception exc)
            {
                lblResultado.Text = "Erro: " + exc.Message;
            }
        } 
    }
}
