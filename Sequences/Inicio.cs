using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sequences
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_orf f2 = new Form_orf();
            f2.ShowDialog(); // Shows Form2
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conc f3 = new Conc();
            f3.ShowDialog(); // Shows Form3
        }

        private void btn_propriedades_Click(object sender, EventArgs e)
        {
            Form_prop f4 = new Form_prop();
            f4.ShowDialog(); // Shows Form4
        }

        private void btn_protParam_Click(object sender, EventArgs e)
        {
            Form_prot f5 = new Form_prot();
            f5.ShowDialog(); // Shows Form4
        }
    }
}
