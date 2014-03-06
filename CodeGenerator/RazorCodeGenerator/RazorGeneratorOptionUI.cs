using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner.CodeGenerator.RazorCodeGenerator
{
    public partial class RazorGeneratorOptionUI : UserControl
    {
        public RazorGeneratorOptionUI()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtPath.Text = this.folderBrowserDialog.SelectedPath;
            }
        }
    }
}
