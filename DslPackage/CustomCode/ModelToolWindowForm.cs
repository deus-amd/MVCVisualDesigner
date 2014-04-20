using BrightIdeasSoftware;
using System;
using System.CodeDom.Compiler;
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
    public partial class ModelToolWindowForm : Form
    {
        private ModelToolWindow m_toolWindow;
        private ComboBox m_cmbTypeList;
        public ModelToolWindowForm(ModelToolWindow toolWindow)
        {
            InitializeComponent();
            m_toolWindow = toolWindow;

            this.tlvViewModel.RootKeyValue = Guid.Empty;

            this.cmbViewModelType.LostFocus += cmbViewModelType_LostFocus;
        }

#region "Init Model Window"
        public void ShowWidgetModel()
        {
            tlpActionModelLayout.Visible = false;
            tlpViewModelLayout.Visible = false;
            tlpWidgetModelLayout.Visible = true;
            tlpWidgetModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = null;
        }

        public void ShowActionModel()
        {
            tlpActionModelLayout.Visible = true;
            tlpViewModelLayout.Visible = false;
            tlpWidgetModelLayout.Visible = false;
            tlpActionModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = null;
        }

        private VDView m_currentView = null;
        public void ShowViewModel(VDView view)
        {
            tlpActionModelLayout.Visible = false;
            tlpViewModelLayout.Visible = true;
            tlpWidgetModelLayout.Visible = false;
            tlpViewModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            // set model type
            cmbViewModelType.Text = !string.IsNullOrEmpty(view.ModelType) ? view.ModelType : Utility.Constants.STR_NOT_SPECIFIED;

            m_currentView = view;

            // init tree list view
            RefreshAllItemsForViewModel();
        }

        public void ClearWindow()
        {
            tlvViewModel.ClearObjects();
            tlvViewModel.DataSource = null;

            tlvActionModel.ClearObjects();
            tlvActionModel.DataSource = null;
        }

        public void HideEditingControls()
        {
            if (m_cmbTypeList != null && m_cmbTypeList.Visible)
                m_cmbTypeList.Hide();
        }
#endregion

#region "Edit Cells"
        private const string COLUMN_NAME = "Name";
        private const string COLUMN_TYPE = "Type";
        private const string COLUMN_DEFAULT = "Default";
        private const string COLUMN_JS_MODEL = "JS Model";

        private void tlvViewModel_CellEditStarting(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_TYPE)
            {
                ComboBox cmbTypeList = new ComboBox();
                cmbTypeList.DropDownStyle = ComboBoxStyle.DropDown;
                cmbTypeList.Bounds = e.CellBounds;
                cmbTypeList.Font = ((ObjectListView)sender).Font;
                // todo: init list from options
                List<string> typeList = new List<string> { "int", "string", "long" };
                cmbTypeList.Items.AddRange(typeList.ToArray());
                cmbTypeList.Text = (string)e.Value;
                cmbTypeList.Tag = e.RowObject; 
                cmbTypeList.VisibleChanged += m_cmbTypeList_Leave;
                cmbTypeList.KeyUp += m_cmbTypeList_KeyUp;
                e.Control = cmbTypeList;

                m_cmbTypeList = cmbTypeList;
            }
        }

        void m_cmbTypeList_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cb.Hide();                
            }
        }

        void m_cmbTypeList_Leave(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Visible) return;

            VDModelMemberInstance instance = (VDViewModelMemberInstance)cb.Tag;

            string typeName = cb.Text;
            if (string.IsNullOrEmpty(typeName))
            {
                MessageBox.Show("Please specify a type.");
            }
            else if (!this.isValidClassName(typeName))
            {
                MessageBox.Show(typeName + " is not a valid C# class name.");
            }
            else if (instance != null && instance.TypeName != typeName)
            {
                using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("Update model member's type"))
                {
                    this.modelStore.ChangeModelMemberType<VDCustomType, VDViewModelMemberInstance>(instance.ModelMemberInfo, typeName);
                    trans.Commit();
                }

                this.RefreshAllItemsForViewModel();
            }
        }

        private void tlvViewModel_CellEditValidating(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_NAME)
            {
                if (!this.isValidMemberName((string)e.NewValue))
                {
                    MessageBox.Show(((string)e.NewValue) + " is not a valid C# identifier.");
                    e.Cancel = true;
                }
            }
            else if (columnName == COLUMN_TYPE)
            {
            }
            else if (columnName == COLUMN_DEFAULT)
            {
                //todo: validate according to type
            }
        }

        private void tlvViewModel_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            // We have updated the model object, so we cancel the auto update
            e.Cancel = true;

            System.Diagnostics.Debug.Assert(m_currentView != null);
            if (m_currentView == null) return;

            VDViewModelMemberInstance instance = e.RowObject as VDViewModelMemberInstance;
            if (instance == null) return;

            string columnName = e.Column.Text;
            if (e.Column.Text == COLUMN_NAME)
            {
                using(var trans = m_currentView.Store.TransactionManager.BeginTransaction("Update model member name"))
                {
                    instance.ModelMemberInfo.Name = e.NewValue as string;
                    trans.Commit();
                }
            }
            else if (e.Column.Text == COLUMN_DEFAULT)
            {
                using(var trans = m_currentView.Store.TransactionManager.BeginTransaction("Update model member default"))
                {
                    instance.DefaultValue = e.NewValue as string;
                    trans.Commit();
                }
            }
            else if (columnName == COLUMN_TYPE)
            {
                // Stop listening for change events
                ComboBox cmbTypeList = e.Control as ComboBox;
                cmbTypeList.Leave -= m_cmbTypeList_Leave;
                cmbTypeList.KeyUp -= m_cmbTypeList_KeyUp;
                m_cmbTypeList = null;
            }
            else if (columnName == COLUMN_JS_MODEL)
            {

            }

            // Make the list redraw the involved ListViewItem
            ((ObjectListView)sender).RefreshItem(e.ListViewItem);
        }
#endregion

#region "Context Menu"
        // context menu
        private void tlvViewModel_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.ctxMenuViewModel;

            //todo: if the model instance or parent member has no ModelType specified ???
            VDModelType modelType = null;
            VDViewModelMemberInstance selectedMember = (VDViewModelMemberInstance)e.Model;
            if (selectedMember == null) // add member to model
            {
                if (m_currentView.ModelInstance != null)
                    modelType = m_currentView.ModelInstance.GetModelType();
            }
            else // add member to parent member
            {
                modelType = selectedMember.GetModelType();
            }

            // if the type is not able to modify (external type, or primitive type ) ???
            this.tsmiAddViewModelMember.Enabled = modelType != null && !(modelType.IsReadOnly);
            this.tsmiDeleteViewModelMember.Enabled = modelType != null && !(modelType.IsReadOnly) && (selectedMember != null);

            // Add Member for View Model
            this.ctxMenuViewModel.Tag = modelType;
            this.tsmiDeleteViewModelMember.Tag = selectedMember;
        }

        private void ctxMenuViewModel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentView != null);
            if (m_currentView == null) return;
            if (this.modelStore == null) return;

            VDModelType modelType = (VDModelType)this.ctxMenuViewModel.Tag;
            if (modelType != null)
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
                if (mi == this.tsmiAddViewModelMember) // add new member
                {
                    using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("add model member"))
                    {
                        // todo: select Property or Method from UI
                        this.modelStore.AddMemberToModelType<VDPropertyInfo, VDCustomType, VDViewModelMemberInstance>(
                            modelType, "NewMember", Utility.Constants.STR_TYPE_STRING);
                        trans.Commit();
                    }
                }
                else if (mi == this.tsmiDeleteViewModelMember) // delete member
                {
                    VDViewModelMemberInstance selectedMember = (VDViewModelMemberInstance)mi.Tag;
                    if (selectedMember == null || selectedMember.ModelMemberInfo == null) return;

                    using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("delete model member"))
                    {
                        this.modelStore.RemoveMemberFromModelType(modelType, selectedMember.ModelMemberInfo);
                        trans.Commit();
                    }
                }

                RefreshAllItemsForViewModel();
            }
        }
#endregion

#region "Change View Model Type"
        //
        private void cmbViewModelType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\t')
            {
                tlvViewModel.Focus();
                e.Handled = true;
            }
        }

        // check View's ModelType property, and create new model if needed (delete old model object if exists)
        void cmbViewModelType_LostFocus(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentView != null);
            if (m_currentView == null) return;
            if (this.modelStore == null) return;

            string newModelTypeName = cmbViewModelType.Text;

            // model type is changed
            if (string.Compare(newModelTypeName, m_currentView.ModelType) != 0)
            {
                using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("update view model type"))
                {
                    if (m_currentView.ModelInstance != null)
                    {
                        // todo: delete old model type if it's not referenced by any model instances (and members defined in other types) ??
                        // (or keep types and ask user to manually clear up??)
                        //VDModelType type = m_currentView.ModelInstance.ModelType;

                        this.modelStore.DeleteModelInstance(m_currentView.ModelInstance);
                    }

                    // no new model is specified
                    if (string.IsNullOrEmpty(newModelTypeName)
                        || Utility.Constants.STR_NOT_SPECIFIED.Equals(newModelTypeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // do nothing...
                    }
                    else
                    {
                        // validate if it's a valid class name
                        if (!this.isValidClassName(newModelTypeName))
                        {
                            MessageBox.Show(newModelTypeName + " is not a valid C# class name.");
                            return;
                        }

                        // if model type exists in ModelStore, create a new model instance of that model type;
                        // else create both model type and model instance
                        VDModelType modelType = this.modelStore.CreateModelType<VDCustomType>(newModelTypeName);
                        if (modelType != null)
                        {
                            VDModelInstance modelInstance = this.modelStore
                                .CreateModelInstance<VDViewModelMemberInstance>(modelType, "Model");
                            m_currentView.ModelInstance = modelInstance;
                        }
                    }

                    //
                    trans.Commit();
                }

                RefreshAllItemsForViewModel();
            }
        }
#endregion

        private void RefreshAllItemsForViewModel()
        {
            // update tree list view
            if (m_currentView != null && m_currentView.ModelInstance != null)
            {
                tlvViewModel.DataSource = m_currentView.ModelInstance.GetAllSubMemberInstances();
                //tlvViewModel.SetObjects(m_currentView.ModelInstance.GetAllSubMemberInstances());
            }
            else
            {
                tlvViewModel.ClearObjects();
                tlvViewModel.DataSource = null;
            }
        }

        // to check if a string is a valid identifier
        private CodeDomProvider m_provider = CodeDomProvider.CreateProvider("C#");
        private bool isValidClassName(string clsName)
        {
            //todo:
            if (clsName == Utility.Constants.STR_TYPE_INT
                || clsName == Utility.Constants.STR_TYPE_STRING)
                return true;

            return m_provider.IsValidIdentifier(clsName);
        }

        private bool isValidMemberName(string memberName)
        {
            // todo:
            return m_provider.IsValidIdentifier(memberName);
        }

        private VDModelStore modelStore
        {
            get
            {
                if (m_currentView != null)
                {
                    return m_currentView.GetModelStore();
                }
                //else

                return null;
            }
        }
    }
}
