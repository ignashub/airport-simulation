using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCP_App
{
    public partial class inputForm : Form
    {
        Form1 f;
        public inputForm(Form1 f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.f.activateForm(Convert.ToInt32(this.numLuggages.Value), Convert.ToInt32(this.numUpDownCapacity.Value));
        }
    }
}
