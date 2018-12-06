using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Bells2
{
    public partial class TimeDialog : Form
    {
        Regex r;
        public string res;
        public bool succes;

        public TimeDialog(int lessonNumber, string start, string end)
        {
            InitializeComponent();
            r = new Regex(@"^\d{1,2}\:\d{2}$");
            lessonLabel.Text = "Урок " + lessonNumber.ToString();
            startBox.Text = start;
            endBox.Text = end;
            succes = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if(!r.Match(startBox.Text).Success || !r.Match(endBox.Text).Success)
            {
                MessageBox.Show("Входное время имело неверный формат! Формат ввода \"HH:MM\"");
                return;
            }
            res = startBox.Text + "-" + endBox.Text;
            succes = true;
            Close();
        }
    }
}
