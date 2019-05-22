using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F28Module
{
    public partial class Form1 : Form
    {
        F28ModuleAPI f28API;
        public Form1()
        {
            InitializeComponent();
            f28API = F28ModuleAPI.instance();
            this.panel1.Controls.Add(f28API.GetMainView());
            f28API.startRealTimeUpdate();
        }

        private void f28ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(f28API.GetMainView());
        }

        private void parameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            f28API.ConfigViewFeeder.Refresh();
            this.panel1.Controls.Add(f28API.GetParamView());
        }
    }
}
