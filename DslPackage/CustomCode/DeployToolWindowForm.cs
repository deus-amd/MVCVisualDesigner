using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class DeployToolWindowForm : Form
    {
        public DeployToolWindowForm()
        {
            InitializeComponent();
        }

        public void AddTabPage(string tabTitle, Control tabContent)
        {
            TabPage page = new TabPage(tabTitle);
            page.Controls.Add(tabContent);
            tabContent.Dock = DockStyle.Fill;
            this.tabCtrl.TabPages.Add(page);
        }
    }
}
