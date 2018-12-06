using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bells2
{
    public partial class ClassDialog : Form
    {

        public string res;
        public bool succes;

        public ClassDialog(string head)
        {
            InitializeComponent();
            headLabel.Text = head;
            succes = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            res = classBox.Text;
            succes = true;
            Close();
        }
    }
}
