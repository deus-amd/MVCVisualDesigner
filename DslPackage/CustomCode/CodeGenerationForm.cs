using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            this.lvGeneratorList.FullRowSelect = true;
            this.lvGeneratorList.ItemChecked += lvGeneratorList_ItemChecked;
            this.lvGeneratorList.ColumnClick += lvGeneratorList_ColumnClick;
        }

        void lvGeneratorList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // toggle all items check state
            if (e.Column == 0 && this.lvGeneratorList.Items.Count > 0)
            {
                bool checkOrNot = !this.lvGeneratorList.Items[0].Checked;
                foreach (ListViewItem item in this.lvGeneratorList.Items)
                {
                    item.Checked = checkOrNot;
                }
            }
        }

        void lvGeneratorList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                //todo: save settings??
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (m_rootView == null) throw new Exception("Please open a MVD file.");

            string curAMVDFilePath = string.Empty;

            foreach(ListViewItem item in this.lvGeneratorList.Items)
            {
                ICodeGeneratorController controller = getGeneratorControllerFromItemTag(item);
                controller.OnGenerateCode(m_rootView, m_rootViewFilePath);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            CodeGenerationSettings settingForm = new CodeGenerationSettings(m_rootView);
            // init tabs
            foreach (ListViewItem lvi in this.lvGeneratorList.Items)
            {
                ICodeGeneratorController controller = lvi.Tag as ICodeGeneratorController;
                if (controller != null && controller.SettingControl != null)
                {
                    settingForm.AddTabPage(controller.Name, controller);
                }
            }

            DialogResult result = settingForm.ShowDialog(this);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
            }
        }

        private VDView m_rootView = null;
        private string m_rootViewFilePath = null; //file path of current *.amvd file
        public void InitializeForm(List<string> generatorAssemblyList, VDView rootView, string rootViweFilePath)
        {
            m_rootView = rootView;
            m_rootViewFilePath = rootViweFilePath;

            this.lvGeneratorList.Items.Clear();

            foreach (string assemPath in generatorAssemblyList)
            {
                Assembly assGenerator = Assembly.LoadFrom(assemPath);
                if (assGenerator == null) continue;

                foreach (Type t in assGenerator.GetTypes())
                {
                    if (t.GetInterface("ICodeGeneratorProvider") == null) continue;

                    ICodeGeneratorProvider gp = Activator.CreateInstance(t) as ICodeGeneratorProvider;
                    if (gp == null) continue;

                    List<ICodeGeneratorController> generatorList = gp.GetGeneratorList();
                    foreach (ICodeGeneratorController ctrl in generatorList)
                    {
                        // add an item in the All Generators list view
                        ListViewItem item = new ListViewItem(new string[] {"", ctrl.Name, Path.GetFileName(assemPath), Path.GetDirectoryName(assemPath), ctrl.Description });
                        item.Checked = true;
                        saveGeneratorControllerToItemTag(item, ctrl);
                        this.lvGeneratorList.Items.Add(item);
                    }
                }
            }
        }

        private void saveGeneratorControllerToItemTag(ListViewItem item, ICodeGeneratorController controller)
        {
            item.Tag = controller;
        }

        private ICodeGeneratorController getGeneratorControllerFromItemTag(ListViewItem item)
        {
            return (ICodeGeneratorController)item.Tag;
        }
    }
}
