using System;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class CodeGeneratorOptionPageControl : UserControl
    {
        public CodeGeneratorOptionPageControl()
        {
            InitializeComponent();
        }

        internal CodeGeneratorOptionPage OptionPage { get; set; }
        
        internal void Initialize()
        {
            // init the assembly list box
            if (this.OptionPage.CodeGeneratorAssemblyList != null)
            {
                foreach (string ass in this.OptionPage.CodeGeneratorAssemblyList)
                {
                    this.lstAssemblyList.Items.Add(PackageUtility.GetAbsolutePath(ass));
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach(string fn in this.openFileDialog.FileNames)
                {
                    if (!isItemInList(fn))
                    {
                        this.lstAssemblyList.Items.Add(fn);
                        this.OptionPage.CodeGeneratorAssemblyList.Add(PackageUtility.GetRelativePath(fn));
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

        // delete select item from assembly list
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.lstAssemblyList.SelectedIndex >= 0)
            {
                int prevIdx = this.lstAssemblyList.SelectedIndex;

                // remove item from list
                string selectedAssembly = this.lstAssemblyList.SelectedItem as string;
                selectedAssembly = PackageUtility.GetRelativePath(selectedAssembly);
                this.OptionPage.CodeGeneratorAssemblyList.Remove(selectedAssembly);
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

        // enable/disable delete button
        private void lstAssemblyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstAssemblyList.SelectedIndex >= 0)
            {
                if (!this.btnDelete.Enabled) this.btnDelete.Enabled = true;
            }
            else
            {
                if (this.btnDelete.Enabled) this.btnDelete.Enabled = false;
            }
        }
    }
}
