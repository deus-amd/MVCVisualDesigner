using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using MVCVisualDesigner.TypeDescriptor;

namespace MVCVisualDesigner
{
    public partial class PredefinedTypeOptionPageControl : MVDOptionPageControlBase
    {
        public PredefinedTypeOptionPageControl()
        {
            InitializeComponent();
        }

        private PredefinedTypeOptionPage PTOptionPage 
        {
            get { return (PredefinedTypeOptionPage)OptionPage; } 
        }

        internal override void Initialize(DialogPage optionPage)
        {
            base.Initialize(optionPage);

            // init the assembly list box
            if (this.PTOptionPage.TypeDescriptorAssemblyList != null)
            {
                foreach (string ass in this.PTOptionPage.TypeDescriptorAssemblyList)
                {
                    this.lstAssemblyList.Items.Add(Utility.PathHelper.GetAbsolutePath(ass));
                }
            }
        }

        private void btnAddAssembly_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string fn in this.openFileDialog.FileNames)
                {
                    if (!isItemInList(fn))
                    {
                        this.lstAssemblyList.Items.Add(fn);
                        this.PTOptionPage.TypeDescriptorAssemblyList.Add(Utility.PathHelper.GetRelativePath(fn));
                    }
                }
            }

            if (this.lstAssemblyList.SelectedIndex < 0) this.lstAssemblyList.SelectedIndex = 0;
        }

        private bool isItemInList(string item)
        {
            foreach (var i in this.lstAssemblyList.Items)
            {
                if (string.Compare(item, (string)i, true) == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void btnDeleteAssembly_Click(object sender, EventArgs e)
        {
            if (this.lstAssemblyList.SelectedIndex >= 0)
            {
                int prevIdx = this.lstAssemblyList.SelectedIndex;

                // remove item from list
                string selectedAssembly = this.lstAssemblyList.SelectedItem as string;
                selectedAssembly = Utility.PathHelper.GetRelativePath(selectedAssembly);
                this.PTOptionPage.TypeDescriptorAssemblyList.Remove(selectedAssembly);
                this.lstAssemblyList.Items.RemoveAt(this.lstAssemblyList.SelectedIndex);

                // set selected item after deleting
                if (this.lstAssemblyList.Items.Count > 0)
                {
                    if (prevIdx > this.lstAssemblyList.Items.Count)
                        // select previous index
                        this.lstAssemblyList.SelectedIndex = prevIdx;
                    else
                        // select last item
                        this.lstAssemblyList.SelectedIndex = this.lstAssemblyList.Items.Count - 1;
                }
            }
        }

        private void lstAssemblyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lvTypes.Items.Clear();

            if (this.lstAssemblyList.SelectedIndex >= 0)
            {
                if (!this.btnDeleteAssembly.Enabled) this.btnDeleteAssembly.Enabled = true;

                // 
                string selectedPath = this.lstAssemblyList.Text;
                if (!string.IsNullOrWhiteSpace(selectedPath) && this.PTOptionPage.Package != null)
                {
                    List<IMVDTypeDescriptor> tds = this.PTOptionPage.Package.GetTypeDescriptors(selectedPath);
                    foreach(IMVDTypeDescriptor td in tds)
                    {
                        this.lvTypes.Items.Add(new ListViewItem(new string[] { td.Name, td.NameSpace ?? string.Empty }));
                    }
                }
            }
            else
            {
                if (this.btnDeleteAssembly.Enabled) this.btnDeleteAssembly.Enabled = false;
            }
        }
    }
}
