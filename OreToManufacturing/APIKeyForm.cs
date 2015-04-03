using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OreToManufacturing
{
    public partial class APIKeyForm : Form
    {
        public APIKeyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txbCharID.Text = "";
            txbKeyID.Text = "";
            txbVCode.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string charID = txbCharID.Text;
            string keyID = txbKeyID.Text;
            string vcode = txbVCode.Text;
            string text = charID + "\n" + keyID + "\n" + vcode;
            StreamWriter writer = new StreamWriter("settings.cfg");
            writer.Write(text);
            writer.Close();

            //string scrambler = "ghijklmnopqrstuvwxyzabcdefGHIJKLMNOPQRSTUVWXYZABCDEF";
            //foreach (char c in text)
            //{
            //    bool match = false;
            //
            //}

            this.Close();
        }
    }
}
