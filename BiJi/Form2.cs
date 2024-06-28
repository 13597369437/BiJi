using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BiJi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private Hashtable eventTab = new Hashtable();

        public delegate void weituo();
        public event weituo wt;
        private void button11_Click(object sender, EventArgs e)
        {
            eventTab.Add(1, "1");
            eventTab.Add("1", 1);
            wt();
        }
    }
}
