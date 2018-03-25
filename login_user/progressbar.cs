using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_user
{
    public partial class progressbar : Form
    {
        public progressbar()
        {
            InitializeComponent();
        
            
        }

        private void progressbar_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            int succs = 100;
            label2.Text = progressBar1.Value.ToString() + "%";
            if (progressBar1.Value.ToString() == succs.ToString())
            {
               
                this.Close();              
            }
         
           
        }

        private void progressbar_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}
