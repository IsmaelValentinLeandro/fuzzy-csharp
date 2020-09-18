using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class FrmInitial : Form
    {
        public FrmInitial()
        {
            InitializeComponent();
            load();
        }
        
        private void load()
        {
            Thread.Sleep(2000);
            FrmPrincipal frm = new FrmPrincipal();
            frm.Show();
        }
    }
}
