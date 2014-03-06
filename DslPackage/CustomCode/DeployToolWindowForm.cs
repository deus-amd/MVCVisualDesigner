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
        }

        internal DeployToolWindow ToolWindow { get; set; }

        public void AddTabPage(string tabTitle, Control tabContent)
        {
            TabPage page = new TabPage(tabTitle);
            page.Controls.Add(tabContent);
            tabContent.Dock = DockStyle.Fill;
            this.tabGeneratorSettings.TabPages.Add(page);
        }

        public void InitializeForm(List<string> generatorAssemblyList)
        {
            this.lvGeneratorList.Items.Clear();

            TabPage allGeneratorPage = this.tabGeneratorSettings.TabPages[0];
            this.tabGeneratorSettings.TabPages.Clear();
            this.tabGeneratorSettings.TabPages.Add(allGeneratorPage);

            foreach (string assemPath in generatorAssemblyList)
            {
                Assembly assGenerator = Assembly.LoadFrom(assemPath);
                if (assGenerator != null)
                {
                    foreach (Type t in assGenerator.GetTypes())
                    {
                        if (t.GetInterface("ICodeGeneratorProvider") == null) continue;

                        ICodeGeneratorProvider gp = Activator.CreateInstance(t) as ICodeGeneratorProvider;
                        if (gp != null)
                        {
                            List<ICodeGeneratorFactory> factoryList = gp.GetGeneratorList();
                            foreach (ICodeGeneratorFactory factory in factoryList)
                            {
                                // add an item in the All Generators list view
                                ListViewItem item = new ListViewItem(new string[] { "", factory.Name, Path.GetFileName(assemPath), Path.GetDirectoryName(assemPath), factory.Description });
                                item.Tag = factory;
                                this.lvGeneratorList.Items.Add(item);

                                // add a new tab page for the setting UI
                                Control settingUI = gp.GetGeneratorOptionsUI(factory);
                                if (settingUI != null)
                                {
                                    settingUI.Dock = DockStyle.Fill;
                                    TabPage newPage = new TabPage(factory.Name);
                                    newPage.Controls.Add(settingUI);
                                    this.tabGeneratorSettings.TabPages.Add(newPage);
                                }
                            }
                        }

                    }
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.ToolWindow.InitializeDeployWindowForm();
        }
    }
}
