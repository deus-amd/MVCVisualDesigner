using BrightIdeasSoftware;
using MVCVisualDesigner.TypeDescriptor;
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
        private ModelTypeListControl m_ctrlTypeList;
        public ModelToolWindowForm(ModelToolWindow toolWindow)
        {
            InitializeComponent();
            m_toolWindow = toolWindow;

            this.ctrlViewModelType.ValueChanged += ctrlViewModelType_ValueChanged;

            // init tree list view
            this.tlvViewModel.RootKeyValue = Guid.Empty;

            m_ctrlTypeList = new ModelTypeListControl();
            m_ctrlTypeList.ValueChanged += TypeListCellEditor_ValueChanged;
        }

        // cache the predefined type list
        // todo: when to clear it??
        private List<IMVDTypeDescriptor> m_predefinedTypes = null;
        private HashSet<string> m_predefinedTypeNames = null;
        private List<IMVDTypeDescriptor> PredefinedTypes
        {
            get
            {
                getPredefinedTypes();
                return m_predefinedTypes;
            }
        }

        private HashSet<string> PredefinedTypeNames
        {
            get
            {
                if (m_predefinedTypeNames != null && m_predefinedTypeNames.Count > 0)
                    return m_predefinedTypeNames;

                getPredefinedTypes();
                return m_predefinedTypeNames;
            }
        }

        private void getPredefinedTypes()
        {
            if (m_predefinedTypes == null || m_predefinedTypes.Count <= 0)
            {
                var package = this.m_toolWindow.GetPackage();
                if (package != null)
                {
                    m_predefinedTypes = package.GetAllTypeDescriptors();
                    if (m_predefinedTypes != null && m_predefinedTypes.Count > 0)
                    {
                        m_predefinedTypeNames = new HashSet<string>();
                        m_predefinedTypes.ForEach(td =>
                        {
                            if (!m_predefinedTypeNames.Contains(td.FullName))
                                m_predefinedTypeNames.Add(td.FullName);
                        });
                    }
                }
            }
        }

        private string[] getAllTypes()
        {
            HashSet<string> typeList = new HashSet<string>();

            if (this.modelStore != null)
            {
                foreach (var mt in this.modelStore.ModelTypes)
                {
                    typeList.Add(mt.FullName);
                }
            }

            var typeDescriptors = this.PredefinedTypes;
            if (typeDescriptors != null)
            {
                foreach (var td in typeDescriptors)
                {
                    string fullName = td.FullName;
                    if (!typeList.Contains(fullName))
                    {
                        typeList.Add(fullName);
                    }
                }
            }

            List<string> types = typeList.ToList();
            types.Sort();
            return types.ToArray();
        }

#region "Init Model Window"
        private VDWidget m_currentWidget = null;
        public void ShowWidgetModel(VDWidget widget)
        {
            tlpActionModelLayout.Visible = false;
            tlpViewModelLayout.Visible = false;
            tlpWidgetModelLayout.Visible = true;
            tlpWidgetModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = widget.RootView;
            m_currentWidget = widget;

            //todo: init widget view
        }

        public void ShowActionModel()
        {
            tlpActionModelLayout.Visible = true;
            tlpViewModelLayout.Visible = false;
            tlpWidgetModelLayout.Visible = false;
            tlpActionModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = null;
            m_currentWidget = null;
            // todo: 
        }

        private VDView m_currentView = null;
        public void ShowViewModel(VDView view)
        {
            tlpActionModelLayout.Visible = false;
            tlpViewModelLayout.Visible = true;
            tlpWidgetModelLayout.Visible = false;
            tlpViewModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = view;
            m_currentWidget = null;

            // set model type
            this.ctrlViewModelType.InitTypeList(getAllTypes(), view.GetModelType());

            // init tree list view
            refreshAllItemsForViewModel();
        }

        public void ClearWindow()
        {
            tlvViewModel.ClearObjects();
            tlvViewModel.DataSource = null;

            tlvActionModel.ClearObjects();
            tlvActionModel.DataSource = null;
        }

        public void OnLostFocus()
        {
            // hide type list control
            if (m_ctrlTypeList != null && m_ctrlTypeList.Visible)
                m_ctrlTypeList.Hide();
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
                string[] typeList = getAllTypes();
                VDModelMemberInstance instance = (VDViewModelMemberInstance)e.RowObject;
                m_ctrlTypeList.InitTypeList(typeList, instance == null ? null : instance.GetModelType());
                m_ctrlTypeList.Bounds = e.CellBounds;
                m_ctrlTypeList.SetFont(((ObjectListView)sender).Font);
                m_ctrlTypeList.Tag = e.RowObject;
                m_ctrlTypeList.Visible = true;

                e.Control = m_ctrlTypeList;
                e.AutoDispose = false;
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
                ModelTypeListControl ctrlTypeList = e.Control as ModelTypeListControl;
                //cmbTypeList.Leave -= m_cmbTypeList_Leave;
                //cmbTypeList.KeyUp -= m_cmbTypeList_KeyUp;
                //m_cmbTypeList = null;
            }
            else if (columnName == COLUMN_JS_MODEL)
            {

            }

            // Make the list redraw the involved ListViewItem
            ((ObjectListView)sender).RefreshItem(e.ListViewItem);
        }

        void TypeListCellEditor_ValueChanged(object sender, EventArgs e)
        {
            ModelTypeListControl ctrlTypeList = (ModelTypeListControl)sender;

            VDModelMemberInstance instance = (VDViewModelMemberInstance)ctrlTypeList.Tag;

            ModelTypeValue modelTypeValue = ctrlTypeList.GetValue();
            string typeName = modelTypeValue.ToString();

            if (modelTypeValue.HasUnspecifiedValue)
            {
                MessageBox.Show("Please specify a type.");
            }
            else if (!modelTypeValue.IsValidName)
            {
                MessageBox.Show(typeName + " is not a valid C# class name.");
            }
            else if (instance != null && instance.TypeName != typeName)
            {
                using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("Update model member's type"))
                {
                    if (this.isPredefinedType(typeName))
                        this.modelStore.ChangeModelMemberType<VDViewModelMemberInstance>(instance.ModelMemberInfo, modelTypeValue, this.PredefinedTypeNames);
                    else
                        this.modelStore.ChangeModelMemberType<VDViewModelMemberInstance>(instance.ModelMemberInfo, modelTypeValue, this.PredefinedTypeNames);
                    trans.Commit();
                }

                this.refreshAllItemsForViewModel();
            }
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
                        this.modelStore.AddMemberToModelType<VDPropertyInfo, VDViewModelMemberInstance>(
                            modelType, "NewMember", Utility.Constants.STR_TYPE_STRING, this.PredefinedTypeNames);
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

                refreshAllItemsForViewModel();
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
        void ctrlViewModelType_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentView != null);
            if (m_currentView == null) return;
            if (this.modelStore == null) return;

            ModelTypeValue newModelType = ctrlViewModelType.GetValue();
            string newModelTypeName = newModelType.ToString();

            // model type is changed
            if (string.Compare(newModelTypeName, m_currentView.ViewModelType) != 0)
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
                        VDModelType modelType = null;
                        if (newModelType.CollectionType == E_CollectionType.Dictionary)
                        {
                            modelType = this.modelStore.CreateDictionaryModelType(newModelType.KeyType, newModelType.ValueType, this.PredefinedTypeNames);
                        }
                        else if (newModelType.CollectionType == E_CollectionType.List)
                        {
                            modelType = this.modelStore.CreateListModelType(newModelType.ValueType, this.PredefinedTypeNames);
                        }
                        else
                        {
                            modelType = createModelType(newModelType.ValueType);
                        }

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

                refreshAllItemsForViewModel();
            }
        }

        private VDModelType createModelType(string newModelTypeName)
        {
            VDModelType modelType = null;

            // validate if it's a valid class name
            if (!this.isValidClassName(newModelTypeName))
            {
                MessageBox.Show(newModelTypeName + " is not a valid C# class name.");
                return null;
            }

            // if model type exists in ModelStore, create a new model instance of that model type;
            // else create both model type and model instance
            modelType = this.modelStore.CreateModelType(newModelTypeName, this.PredefinedTypeNames);

            return modelType;
        }
#endregion

        private bool isPredefinedType(string fullName)
        {
            if (this.PredefinedTypeNames != null && this.PredefinedTypeNames.Contains(fullName))
                return true;
            else
                return false;
        }

        private void refreshAllItemsForViewModel()
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

        private void refreshAllItemsForWidgetModel()
        {
            // update tree list view
            if (m_currentWidget != null && m_currentWidget.ModelInstance != null)
            {
                tlvWidgetModel.DataSource = m_currentWidget.ModelInstance.GetAllSubMemberInstances();
            }
            else
            {
                tlvWidgetModel.ClearObjects();
                tlvWidgetModel.DataSource = null;
            }
        }

        // to check if a string is a valid identifier
        private CodeDomProvider m_provider = CodeDomProvider.CreateProvider("C#");
        private bool isValidClassName(string clsName)
        {
            if (isPredefinedType(clsName)) return true;

            string nameSpace = null;
            string typeName = null;
            Utility.Helper.SplitFullName(clsName, out nameSpace, out typeName);

            // todo: regular express
            // todo: valid nameSpace
            // todo: allow Dictionary<T, T> or List<T>
            return m_provider.IsValidIdentifier(typeName);
        }

        private bool isValidMemberName(string memberName)
        {
            // todo: regular express
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
