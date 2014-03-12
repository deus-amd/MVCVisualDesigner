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
            this.tvAllViews.AfterSelect += tvAllViews_AfterSelect;
            this.txtPath.Leave += txtPath_Leave;
        }

        void txtPath_Leave(object sender, EventArgs e)
        {
            TreeNode curNode = tvAllViews.SelectedNode;
            if (curNode == null) return;

            VDView view = getViewFromNode(curNode);
            if (view == null) return;

            string curPath = SettingsHelper.getViewPathFromView(view, m_rootViewPath);
            string newPath = this.txtPath.Text;
            // path not changed
            if (string.Compare(curPath, newPath, true) == 0) return;

            if (string.IsNullOrEmpty(newPath))
                SettingsHelper.saveViewPathToView(string.Empty, view, m_rootViewPath);
            else
                SettingsHelper.saveViewPathToView(newPath, view, m_rootViewPath);
        }

        void tvAllViews_AfterSelect(object sender, TreeViewEventArgs e)
        {
            VDView view = getViewFromNode(e.Node);
            if (view != null)
            {
                string path = SettingsHelper.getViewPathFromView(view, m_rootViewPath);
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
            }
        }

        private string m_rootViewPath;
        internal void initTreeView(VDView rootView, string rootViewPath)
        {
            m_rootViewPath = rootViewPath;

            tvAllViews.Nodes.Clear();
            TreeNode rootNode = new TreeNode(rootView.WidgetName);
            saveViewToNode(rootNode, rootView);
            tvAllViews.Nodes.Add(rootNode);
            createTreeNode(rootView, rootNode);
        }

        static private void createTreeNode(VDView parentView, TreeNode parentNode)
        {
            List<VDView> childViews = parentView.GetChildren<VDView>();
            foreach (VDView cv in childViews)
            {
                TreeNode cn = new TreeNode(cv.WidgetName);
                saveViewToNode(cn, cv);
                parentNode.Nodes.Add(cn);
                createTreeNode(cv, cn);
            }
        }

        static private VDView getViewFromNode(TreeNode node) 
        { 
            return (VDView)node.Tag; 
        }

        static private void saveViewToNode(TreeNode node, VDView view) 
        { 
            node.Tag = view; 
        }
    }
}
