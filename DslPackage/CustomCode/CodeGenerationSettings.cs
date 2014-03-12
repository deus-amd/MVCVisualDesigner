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
    public partial class CodeGenerationSettings : Form
    {
        private VDView m_rootView;
        private string m_rootViewPath;
        public CodeGenerationSettings(VDView rootView, string rootViewPath)
        {
            InitializeComponent();
            m_rootView = rootView;
            m_rootViewPath = rootViewPath;
        }

        public void AddTabPage(string tabTitle, ICodeGeneratorController controller)
        {
            controller.OnLoadSettings(m_rootView, m_rootViewPath);
            controller.SettingControl.Dock = DockStyle.Fill;

            TabPage page = new TabPage(tabTitle);
            page.Controls.Add(controller.SettingControl);
            saveGeneratorControllerToPageTag(page, controller);
            this.tabGenerators.TabPages.Add(page);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (TabPage page in this.tabGenerators.TabPages)
            {
                try
                {
                    ICodeGeneratorController controller = getGeneratorControllerFromPageTag(page);
                    controller.OnSaveSettings(m_rootView, m_rootViewPath);
                }
                catch (SettingUIValidationException ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabGenerators.SelectedTab = page;
                    return;
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void saveGeneratorControllerToPageTag(TabPage page, ICodeGeneratorController controller)
        {
            page.Tag = controller;
        }

        private ICodeGeneratorController getGeneratorControllerFromPageTag(TabPage page)
        {
            return (ICodeGeneratorController)page.Tag;
        }
    }
}
