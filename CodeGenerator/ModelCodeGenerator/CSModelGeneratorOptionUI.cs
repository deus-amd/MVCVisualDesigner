using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner.CodeGenerator.ModelCodeGenerator
{
    public partial class CSModelGeneratorOptionUI : UserControl
    {
        public CSModelGeneratorOptionUI()
        {
            InitializeComponent();
        }

        private string m_rootViewPath;
        private VDView m_rootView;
        internal void initView(VDView rootView, string rootViewPath)
        {
            m_rootViewPath = rootViewPath;
            m_rootView = rootView;

            if (m_rootView != null)
            {
                string path = SettingsHelper.getCSModelPathFromView(rootView, m_rootViewPath);
                if (string.IsNullOrEmpty(path))
                    this.txtPath.Text = string.Empty;
                else
                    this.txtPath.Text = path;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtPath.Text = this.folderBrowserDialog.SelectedPath;
                this.txtPath.Focus();
            }
        }

        private void txtPath_Leave(object sender, EventArgs e)
        {
            if (m_rootView == null) return;

            string curPath = SettingsHelper.getCSModelPathFromView(m_rootView, m_rootViewPath);
            string newPath = this.txtPath.Text;
            // path not changed
            if (string.Compare(curPath, newPath, true) == 0) return;

            if (string.IsNullOrEmpty(newPath))
                SettingsHelper.saveCSModelPathToView(string.Empty, m_rootView, m_rootViewPath);
            else
                SettingsHelper.saveCSModelPathToView(newPath, m_rootView, m_rootViewPath);
        }
    }
}
