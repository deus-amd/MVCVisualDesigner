using AutocompleteMenuNS;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class ModelToolWindowForm : Form
    {
        private ModelToolWindow m_toolWindow;
        public ModelToolWindowForm(ModelToolWindow toolWindow)
        {
            InitializeComponent();
            m_toolWindow = toolWindow;

            m_widgetValueHandler = new WidgetValueHandler(
                this, tlvWidgetModel, toolTipMsg, ctxMenuWidgetValue, tsmiAddWidgetValueMember, tsmiDeleteWidgetValueMember, m_typeEditor);
            m_viewModelHandler = new ViewModelHandler(this, tlvViewModel, toolTipMsg, ctxMenuViewModel, tsmiAddViewModelMember, tsmiDeleteViewModelMember);
            m_actionDataHandler = new ActionDataHandler(this, tlvActionModel, toolTipMsg, ctxMenuActionData, tsmiAddActionDataMember, tsmiDeleteActionDataMember);

            // init tree list view
            this.tlvViewModel.RootKeyValue = Guid.Empty;
            this.tlvWidgetModel.RootKeyValue = Guid.Empty;
            this.tlvActionModel.RootKeyValue = Guid.Empty;

            // init type editor
            m_typeEditor.Init(autocompleteMenu_Type);
        }

        internal void ShowWidgetValuePanel()
        {
            this.tlpActionModelLayout.Hide();
            this.tlpViewModelLayout.Hide();
            this.tlpWidgetModelLayout.Show();
            this.tlpWidgetModelLayout.Dock = DockStyle.Fill;
        }

        internal void ShowViewModelPanel()
        {
            this.tlpActionModelLayout.Hide();
            this.tlpViewModelLayout.Show();
            this.tlpWidgetModelLayout.Hide();
            this.tlpViewModelLayout.Dock = DockStyle.Fill;
        }

        internal void ShowActionDataPanel()
        {
            this.tlpActionModelLayout.Show();
            this.tlpViewModelLayout.Hide();
            this.tlpWidgetModelLayout.Hide();
            this.tlpActionModelLayout.Dock = DockStyle.Fill;
        }

        private WidgetValueHandler m_widgetValueHandler;
        public WidgetValueHandler WidgetValueHandler { get { return m_widgetValueHandler; } }

        private ViewModelHandler m_viewModelHandler;
        public ViewModelHandler ViewModelHandler { get { return m_viewModelHandler; } }

        private ActionDataHandler m_actionDataHandler;
        public ActionDataHandler ActionDataHandler { get { return m_actionDataHandler; } }

        public void OnLostFocus()
        {
            WidgetValueHandler.OnLostFocus();
            ViewModelHandler.OnLostFocus();
            ActionDataHandler.OnLostFocus();
        }

        public void OnHideWindow()
        {
            WidgetValueHandler.OnHideWindow();
            ViewModelHandler.OnHideWindow();
            ActionDataHandler.OnHideWindow();
        }

#if todel
        private ModelTypeListControl m_ctrlTypeListForViewModel;
        private ModelTypeListControl m_ctrlTypeListForActionModel;
        private SelectorControl m_ctrlSelector;

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

#region "Edit Cells For View Model"
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
                m_ctrlTypeListForViewModel.InitTypeList(typeList, instance == null ? null : instance.ModelType);
                m_ctrlTypeListForViewModel.Bounds = e.CellBounds;
                m_ctrlTypeListForViewModel.SetFont(((ObjectListView)sender).Font);
                m_ctrlTypeListForViewModel.Tag = e.RowObject;
                m_ctrlTypeListForViewModel.Visible = true;

                e.Control = m_ctrlTypeListForViewModel;
                e.AutoDispose = false;
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

        void ViewModel_TypeListCellEditor_ValueChanged(object sender, EventArgs e)
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
                    this.modelStore.ChangeModelMemberType<VDViewModelMemberInstance>(instance.ModelMemberInfo, modelTypeValue, this.PredefinedTypeNames);
                    trans.Commit();
                }

                this.refreshAllItemsForViewModel();
            }
        }
#endregion

#region "Edit Cells For Action Model"
        private const string COLUMN_SELECTOR = "Selector";

        private void tlvActionModel_CellEditStarting(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_TYPE)
            {
                string[] typeList = getAllTypes();
                VDModelMemberInstance instance = (VDModelMemberInstance)e.RowObject;
                m_ctrlTypeListForActionModel.InitTypeList(typeList, instance == null ? null : instance.ModelType);
                m_ctrlTypeListForActionModel.Bounds = e.CellBounds;
                m_ctrlTypeListForActionModel.SetFont(((ObjectListView)sender).Font);
                m_ctrlTypeListForActionModel.Tag = e.RowObject;
                m_ctrlTypeListForActionModel.Visible = true;

                e.Control = m_ctrlTypeListForActionModel;
                e.AutoDispose = false;
            }
            else if (columnName == COLUMN_SELECTOR)
            {
                VDActionModelMemberInstance instance = (VDActionModelMemberInstance)e.RowObject;
                m_ctrlSelector.Init(instance.SelectorAnchor, instance.SelectorFunction, instance.ResolvedTargetWidget);
                m_ctrlSelector.Bounds = e.CellBounds;
                //m_ctrlSelector.SetFont(((ObjectListView)sender).Font);
                m_ctrlSelector.Tag = e.RowObject;
                m_ctrlSelector.Visible = true;
                e.Control = m_ctrlSelector;
                e.AutoDispose = false;
            }
        }

        void ActionModel_TypeListCellEditor_ValueChanged(object sender, EventArgs e)
        {
            ModelTypeListControl ctrlTypeList = (ModelTypeListControl)sender;

            VDModelMemberInstance instance = (VDModelMemberInstance)ctrlTypeList.Tag;

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
                    if (instance.ModelMemberInfo == null)
                    {
                        // change type of the action model itself
                        string name = instance.Name;
                        this.modelStore.DeleteModelInstance(instance);
                        VDModelType2 newModelType = this.modelStore.CreateModelType(modelTypeValue, this.PredefinedTypeNames);
                        this.m_currentWidget.ModelInstance = this.modelStore.CreateModelInstance<VDActionModelMemberInstance>(newModelType, name);
                    }
                    else
                    {
                        // change type of a member
                        this.modelStore.ChangeModelMemberType<VDActionModelMemberInstance>(instance.ModelMemberInfo, modelTypeValue, this.PredefinedTypeNames);
                    }
                    trans.Commit();
                }

                this.refreshAllItemsForActionModel();
            }
        }
#endregion

#region "Context Menu For View Model"
        // context menu
        private void tlvViewModel_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.ctxMenuViewModel;

            //todo: if the model instance or parent member has no ModelType specified ???
            VDModelType2 modelType = null;
            VDViewModelMemberInstance selectedMember = (VDViewModelMemberInstance)e.Model;
            if (selectedMember == null) // add member to model
            {
                if (m_currentView.ModelInstance != null)
                    modelType = m_currentView.ModelInstance.ModelType;

                // disable delete menu item
                this.tsmiDeleteViewModelMember.Enabled = false;
            }
            else // add member to parent member
            {
                modelType = selectedMember.ModelType;

                // enable/disable delete menu item
                this.tsmiDeleteViewModelMember.Enabled =
                    selectedMember.ModelMemberInfo != null &&
                    selectedMember.ModelMemberInfo.HostModelType != null &&
                    !selectedMember.ModelMemberInfo.HostModelType.IsReadOnly;
            }

            // if the type is not able to modify (external type, or primitive type ) ???
            this.tsmiAddViewModelMember.Enabled = modelType != null && !(modelType.IsReadOnly);

            // Add Member for View Model
            this.ctxMenuViewModel.Tag = modelType;
            this.tsmiDeleteViewModelMember.Tag = selectedMember;
        }

        private void ctxMenuViewModel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentView != null);
            if (m_currentView == null) return;
            if (this.modelStore == null) return;

            VDModelType2 modelType = (VDModelType2)this.ctxMenuViewModel.Tag;
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
                    if (selectedMember == null || selectedMember.ModelMemberInfo == null
                        || selectedMember.ModelMemberInfo.HostModelType == null) return;

                    using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("delete model member"))
                    {
                        this.modelStore.RemoveMemberFromModelType(selectedMember.ModelMemberInfo.HostModelType, selectedMember.ModelMemberInfo);
                        trans.Commit();
                    }
                }

                refreshAllItemsForViewModel();
            }
        }
#endregion

#region "Context Menu for Action Model"
        private void tlvActionModel_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.ctxMenuActionModel;

            VDModelType2 modelType = null;
            VDActionModelMemberInstance selectedMember = (VDActionModelMemberInstance)e.Model;
            if (selectedMember == null) // add model instance for current selected Action object
            {
                if (m_currentWidget.ModelInstance == null)
                {
                    // not model instance for current action
                    this.tsmiAddActionModelMember.Enabled = true;
                }
                else
                {
                    this.tsmiAddActionModelMember.Enabled = false;
                }

                // disable delete menu item
                this.tsmiDeleteActionModelMember.Enabled = false;
            }
            else // add member to parent member
            {
                // enable/disable delete menu item
                this.tsmiDeleteActionModelMember.Enabled =
                    (selectedMember.ModelMemberInfo == null) // delete the model instance itself
                    ||
                    (selectedMember.ModelMemberInfo != null &&      // delete a memeber of model instance
                    selectedMember.ModelMemberInfo.HostModelType != null &&
                    !selectedMember.ModelMemberInfo.HostModelType.IsReadOnly);

                // if the type is not able to modify (external type, or primitive type ) ???
                modelType = selectedMember.ModelType;
                this.tsmiAddActionModelMember.Enabled = modelType != null && !(modelType.IsReadOnly);
            }

            // Add Member for View Model
            this.ctxMenuViewModel.Tag = modelType;
            this.tsmiDeleteActionModelMember.Tag = selectedMember;
        }

        private void ctxMenuActionModel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentWidget != null);
            if (m_currentWidget == null) return;
            if (this.modelStore == null) return;

            VDModelType2 modelType = (VDModelType2)this.ctxMenuViewModel.Tag;
            if (modelType == null) // not model for action yet
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
                if (mi == this.tsmiAddActionModelMember) // add new action model
                {
                    using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("add model member"))
                    {
                        string newModelTypeName = getFreeModelTypeName("ActionModel_" + m_currentWidget.WidgetName);
                        VDModelType2 newModelType = createModelType(newModelTypeName);
                        VDActionModelMemberInstance modelInstance = this.modelStore.CreateModelInstance<VDActionModelMemberInstance>(newModelType, "_ActionModel");
                        m_currentWidget.ModelInstance = modelInstance;

                        trans.Commit();
                    }

                    refreshAllItemsForActionModel();
                }
                else
                {
                    // shouldn't happen
                    throw new InvalidOperationException("Invalid operation in current situation.");
                }
            }
            else 
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)e.ClickedItem;
                if (mi == this.tsmiAddActionModelMember) // add new member
                {
                    using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("add model member"))
                    {
                        // todo: select Property or Method from UI
                        this.modelStore.AddMemberToModelType<VDPropertyInfo, VDActionModelMemberInstance>(
                            modelType, "NewMember", Utility.Constants.STR_TYPE_STRING, this.PredefinedTypeNames);
                        trans.Commit();
                    }
                }
                else if (mi == this.tsmiDeleteActionModelMember) // delete member
                {
                    VDActionModelMemberInstance selectedMember = (VDActionModelMemberInstance)mi.Tag;
                    if (selectedMember == null) return;

                    if (selectedMember.ModelMemberInfo == null) // delete the model instance itself
                    {
                        using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("delete model instance"))
                        {
                            selectedMember.Delete();
                            trans.Commit();
                        }
                    }
                    else if( selectedMember.ModelMemberInfo != null && selectedMember.ModelMemberInfo.HostModelType != null) // delete a member
                    {
                        using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("delete model member"))
                        {
                            this.modelStore.RemoveMemberFromModelType(selectedMember.ModelMemberInfo.HostModelType, selectedMember.ModelMemberInfo);
                            trans.Commit();
                        }
                    }
                }

                refreshAllItemsForActionModel();
            }
        }
#endregion

#region "Change View Model Type"
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
                        //VDModelType2 type = m_currentView.ModelInstance.ModelType;

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
                        VDModelType2 modelType = null;
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
                            VDViewModelMemberInstance modelInstance = this.modelStore
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

        private VDModelType2 createModelType(string newModelTypeName)
        {
            VDModelType2 modelType = null;

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

        private string getFreeModelTypeName(string baseName)
        {
            if (string.IsNullOrEmpty(baseName)) baseName = "_NewType";
            int idx = 1;
            while (true)
            {
                string modelTypeName = baseName + idx++;
                if (this.modelStore.GetModelType(modelTypeName) == null)
                {
                    return modelTypeName;
                }
            }
            //return baseName;
        }
#endregion

        private bool isPredefinedType(string fullName)
        {
            if (this.PredefinedTypeNames != null && this.PredefinedTypeNames.Contains(fullName))
                return true;
            else
                return false;
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

        // 
        private void chkEnableWidgetModel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.modelStore==null || m_currentWidget == null) return;

            using (var trans = m_currentView.Store.TransactionManager.BeginTransaction("enable/disable widget model instance"))
            {
                // delete old instance
                if (m_currentWidget.ModelInstance != null)
                {
                    this.modelStore.DeleteModelInstance(m_currentWidget.ModelInstance);
                }

                // create new instance
                if (this.chkEnableWidgetModel.Checked)
                {
                    string newModelTypeName = string.IsNullOrEmpty(m_currentWidget.IntrinsicModelType) ? Utility.Constants.STR_TYPE_STRING : m_currentWidget.IntrinsicModelType;
                    ModelTypeValue newModelType = new ModelTypeValue(newModelTypeName);

                    VDModelType2 modelType = null;
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
                        VDWidgetModelMemberInstance modelInstance = this.modelStore
                            .CreateModelInstance<VDWidgetModelMemberInstance>(modelType, this.m_currentWidget.WidgetName);
                        m_currentWidget.ModelInstance = modelInstance;
                    }
                }

                trans.Commit();
            }

            refreshAllItemsForWidgetModel();
        }

        private void ModelToolWindowForm_Load(object sender, EventArgs e)
        {

        }
#endif
    }

    public abstract class ModelToolWindowHandler
    {
        protected ModelToolWindowForm m_form;
        protected DataTreeListView m_treeListView;
        protected ToolTip m_toolTip;
        protected ContextMenuStrip m_contextMenu;
        protected ToolStripMenuItem m_miAddChild;
        protected ToolStripMenuItem m_miDelete;

        public ModelToolWindowHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip, 
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete)
        {
            m_form = form;
            m_treeListView = treeListView;
            m_toolTip = toolTip;
            m_contextMenu = contextMenu;
            m_miAddChild = miAddChild;
            m_miDelete = miDelete;
        }

        protected VDView RootView { get; set; }

        public virtual void OnLostFocus() 
        { 
        }

        public virtual void OnHideWindow()
        { 
            m_treeListView.ClearObjects();
            m_treeListView.DataSource = null;
        }
    }

    public class WidgetValueHandler : ModelToolWindowHandler
    {
        private const string COLUMN_NAME = "Name";
        private const string COLUMN_TYPE = "Type";
        private const string COLUMN_INITVAL = "Init Value";
        private const string COLUMN_VALIDATOR = "Validator";
        private const string COLUMN_FORMATTER = "Formatter";
        private const string COLUMN_DISPNAME = "Display Name";

        private AutoCompleteTextBox m_typeEditor = null;

        public WidgetValueHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip,
                                    ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete, AutoCompleteTextBox txtTypeEditor)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete)
        {
            // set up event handlers
            m_treeListView.CellEditStarting += OnCellEditStarting;
            m_treeListView.CellEditValidating += OnCellEditValidating;
            m_treeListView.CellEditFinishing += OnCellEditFinishing;
            m_treeListView.CellRightClick += OnCellRightClick;

            //
            m_miAddChild.Click += OnAddMemberMenuClick;
            m_miDelete.Click += OnDelMemberMenuClick;

            //
            m_typeEditor = txtTypeEditor;
        }

        void OnCellRightClick(object sender, CellRightClickEventArgs e)
        {
            // no widget selected
            if (m_currentWidget == null) return;

            e.MenuStrip = this.m_contextMenu;

            NodeBase selectedNode = (NodeBase)e.Model;
            m_miAddChild.Enabled = selectedNode.CanAddChildMembers;
            m_miAddChild.Tag = selectedNode;

            m_miDelete.Enabled = selectedNode.CanBeDeleted;
            m_miDelete.Tag = selectedNode;
        }

        void OnAddMemberMenuClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentWidget != null);
            if (m_currentWidget == null) return;

            NodeBase selectedNode = m_miAddChild.Tag as NodeBase;
            using(var trans = m_currentWidget.Store.TransactionManager.BeginTransaction("addd widget value member"))
            {
                selectedNode.AddChildNode();
                trans.Commit();
            }

            refreshTreeListView();
        }

        void OnDelMemberMenuClick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(m_currentWidget != null);
            if (m_currentWidget == null) return;

            NodeBase selectedNode = m_miDelete.Tag as NodeBase;
            using(var trans = m_currentWidget.Store.TransactionManager.BeginTransaction("delete widget value member"))
            {
                selectedNode.DeleteNode();
                trans.Commit();
            }

            refreshTreeListView();
        }

        void OnCellEditStarting(object sender, CellEditEventArgs e)
        {
            NodeBase node = e.RowObject as NodeBase;
            string columnName = e.Column.Text;

            if (columnName == COLUMN_NAME || columnName == COLUMN_DISPNAME)
            {
                if (node == null || !node.CanChangeName)
                {
                    showTooltipMsg(columnName + " can not be changed.", e);
                    e.Cancel = true;
                }
            }
            else if (columnName == COLUMN_TYPE)
            {
                if (node == null || !node.CanChangeType)
                {
                    showTooltipMsg("Type can not be changed.", e);
                    e.Cancel = true;
                }
                else
                {
                    m_typeEditor.Text = node.TypeName;
                    m_typeEditor.Bounds = e.CellBounds;
                    m_typeEditor.Font = (((ObjectListView)sender).Font);
                    m_typeEditor.Tag = e.RowObject;
                    m_typeEditor.Visible = true;
                    e.Control = m_typeEditor;
                    e.AutoDispose = false;
                }
            }
            else // init value, formatter, validator
            {
                if (node == null || node is TypeNode)
                {
                    showTooltipMsg(columnName + " can not be changed.", e);
                    e.Cancel = true;
                }
            }
        }

        void OnCellEditValidating(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_NAME)
            {
                //if (!this.isValidMemberName((string)e.NewValue))
                //{
                //    MessageBox.Show(((string)e.NewValue) + " is not a valid C# identifier.");
                //    e.Cancel = true;
                //}
            }
            else if (columnName == COLUMN_TYPE)
            {
            }
            else if (columnName == COLUMN_INITVAL)
            {
                //todo: validate according to type
            } 
        }

        void OnCellEditFinishing(object sender, CellEditEventArgs e)
        {
            // the editing is canceled (press ESC)
            if (e.Cancel) return;

            // We have updated the model object, so we cancel the auto update
            e.Cancel = true;

            System.Diagnostics.Debug.Assert(m_currentWidget != null);
            if (m_currentWidget == null) return;

            NodeBase node = e.RowObject as NodeBase;
            if (node == null) return;

            string columnName = e.Column.Text;
            using (var trans = m_currentWidget.Store.TransactionManager.BeginTransaction("Update model member " + columnName))
            {
                string oldValue = (string)(e.Value);
                string newValue = (string)(e.NewValue);
                if (columnName == COLUMN_NAME)
                    node.OnNameChanged(oldValue, newValue);
                else if (columnName == COLUMN_DISPNAME)
                    node.OnDispNameChanged(oldValue, newValue);
                else if (columnName == COLUMN_TYPE)
                {
                    node.OnTypeNameChanged(oldValue, newValue);
                }
                else if (columnName == COLUMN_INITVAL)
                    node.OnInitValueChanged(oldValue, newValue);
                else if (columnName == COLUMN_VALIDATOR)
                    node.OnValidatorChanged(oldValue, newValue);
                else if (columnName == COLUMN_FORMATTER)
                    node.OnFormatterChanged(oldValue, newValue);

                trans.Commit();
            }

            refreshTreeListView();
        }

        private VDWidget m_currentWidget;
        public void Show(VDWidget widget) 
        { 
            m_form.ShowWidgetValuePanel();

            this.RootView = widget.RootView;
            this.m_currentWidget = widget;

            m_typeEditor.SetModelStore(widget != null ? widget.GetModelStore() : null);
            refreshTreeListView();
        }

        public override void OnLostFocus()
        {
            // hide type editor
            if (m_typeEditor != null && m_typeEditor.Visible)
            {
                m_typeEditor.Hide();
            }
        }

        private void refreshTreeListView()
        {
            // reclaim the old nodes
            if (m_treeListView.DataSource != null && m_treeListView.DataSource is List<NodeBase>)
            {
                reclaimNodes((List<NodeBase>)m_treeListView.DataSource);
            }

            // update tree list view
            if (m_currentWidget != null)
            {
                List<NodeBase> allNodes = new List<NodeBase>();
                getNodesForWidget(m_currentWidget, null, allNodes);
                m_treeListView.DataSource = allNodes;
            }
            else
            {
                m_treeListView.ClearObjects();
                m_treeListView.DataSource = null;
            }
        }

        private void showTooltipMsg(string msg, CellEditEventArgs e)
        {
            m_toolTip.Show(msg, m_treeListView, e.CellBounds.Left, e.CellBounds.Bottom, 800); 
        }

        #region Tree List View Nodes
        private void getNodesForWidget(VDWidget widget, NodeBase parentNode, List<NodeBase> allNodes)
        {
            NodeBase node = getTypeNode(widget, parentNode);

            int count = allNodes.Count;
            foreach(VDWidget child in widget.Children)
            {
                getNodesForWidget(child, node, allNodes);
            }

            if (count < allNodes.Count // new child nodes added 
                || widget.WidgetValue != null) 
            {
                if (widget.WidgetValue != null)
                {
                    getNodeForWidgetValue(widget, widget.WidgetValue, node, allNodes);
                }
                else
                {
                    node.Name = string.Format("{0} [{1}]", widget.WidgetName, widget.WidgetType.ToString());
                    node.TypeName = Utility.Constants.STR_TYPE_JS_OBJECT;
                }
                allNodes.Add(node);
            }
        }

        private void getNodeForWidgetValue(VDWidget widget, VDWidgetValue widgetValue, NodeBase parentNode, List<NodeBase> allNodes)
        {
            if (widgetValue == null) return;

            foreach (VDModelMember mem in widgetValue.Members)
            {
                VDWidgetValueMember  widgetValueMember = mem as VDWidgetValueMember;
                MemberNode memberNode = getMemberNode(widget, widgetValueMember, parentNode);
                allNodes.Add(memberNode);
                if (widgetValueMember.Meta != null && !(widgetValueMember.Meta.Type is VDPrimitiveType)) // the type of this member is not Primitive Type
                {
                    getNodeForWidgetValue(widget, widgetValueMember.Type as VDWidgetValue, memberNode, allNodes);
                }
            }
        }

        private TypeNode getTypeNode(VDWidget widget, NodeBase parentNode)
        {
            TypeNode node = null;
            if (m_typeNodeCache.Count > 0)
                node = m_typeNodeCache.Pop();
            else
                node = new TypeNode();
            node.InitTypeNode(widget, parentNode);
            return node;
        }

        private MemberNode getMemberNode(VDWidget widget, VDWidgetValueMember widgetValueMember, NodeBase parentNode)
        {
            MemberNode node = null;
            if (m_memberNodeCache.Count > 0)
                node = m_memberNodeCache.Pop();
            else
                node = new MemberNode();
            node.InitMemberNode(widget, widgetValueMember, parentNode);
            return node;
        }

        private void reclaimNodes(List<NodeBase> nodes)
        {
            foreach(NodeBase node in nodes)
            {
                if (node is TypeNode)
                    m_typeNodeCache.Push((TypeNode)node);
                else
                    m_memberNodeCache.Push((MemberNode)node);
            }
        }
        
        // cache the nodes
        private Stack<TypeNode> m_typeNodeCache = new Stack<TypeNode>();
        private Stack<MemberNode> m_memberNodeCache = new Stack<MemberNode>();

        abstract class NodeBase
        {
            protected Guid m_id;
            protected VDWidget m_widget;
            protected NodeBase m_parentNode;

            protected void Init(Guid id, VDWidget widget, NodeBase parent)
            {
                m_id = id;
                m_widget = widget;
                m_parentNode = parent;
            }

            public Guid ID { get { return m_id; } }
            public Guid ParentID { get { return m_parentNode == null ? Guid.Empty : m_parentNode.ID; } }

            public string Name { get; set; }
            public string DispName { get; set; }
            public string TypeName { get; set; }
            public string InitValue { get; set; }
            public string ValidatorNames { get; set; }
            public string FormatterNames { get; set; }

            /// <summary> If be able to Add child nodes of this node </summary>
            abstract internal bool CanAddChildMembers { get; }

            abstract internal bool CanBeDeleted { get; }

            /// <summary> If be able to change Name column of this node </summary>
            abstract internal bool CanChangeName { get; }

            /// <summary> If be able to change Type column of this node </summary>
            abstract internal bool CanChangeType { get; }

            virtual internal void AddChildNode() { }
            virtual internal void DeleteNode() { }

            virtual internal void OnNameChanged(string oldValue, string newValue) { }
            virtual internal void OnDispNameChanged(string oldValue, string newValue) { }
            virtual internal void OnTypeNameChanged(string oldValue, string newValue) { }
            virtual internal void OnInitValueChanged(string oldValue, string newValue) { }
            virtual internal void OnValidatorChanged(string oldValue, string newValue) { }
            virtual internal void OnFormatterChanged(string oldValue, string newValue) { }
        }

        // the node to represent the WidgetValue of a widget, only one node of this kind for a widget
        class TypeNode : NodeBase
        {
            private VDWidgetValue m_widgetValue;
            internal void InitTypeNode(VDWidget widget, NodeBase parent)
            {
                base.Init(widget.Id, widget, parent);

                m_widgetValue = widget.WidgetValue;
                if (m_widgetValue != null)
                {
                    Name = widget.WidgetName;
                    TypeName = m_widgetValue.Meta != null ? m_widgetValue.Meta.FullName : string.Empty;
                    DispName = m_widgetValue.DispName;
                    InitValue = string.Empty;
                    ValidatorNames = string.Empty;
                    FormatterNames = string.Empty;
                }
                else
                {
                    Name = string.Empty;
                    TypeName = string.Empty;
                    DispName = string.Empty;
                    InitValue = string.Empty;
                    ValidatorNames = string.Empty;
                    FormatterNames = string.Empty;
                }
            }

            internal override bool CanAddChildMembers
            {
                get 
                {
                    if (m_widgetValue == null || m_widgetValue.Meta == null) 
                        return false;

                    VDMetaType metaType = m_widgetValue.Meta;
                    return !(metaType is VDDictionaryType || metaType is VDListType
                        || metaType is VDPredefinedType || metaType is VDPrimitiveType);
                }
            }

            internal override bool CanBeDeleted { get { return false; } }

            internal override bool CanChangeName { get { return false; } }

            internal override bool CanChangeType 
            { 
                get 
                { 
                    return m_widgetValue != null && m_widgetValue.Meta != null;
                }
            }

            internal override void AddChildNode()
            {
                if (m_widget == null || m_widgetValue == null) return;
                VDModelStore modelStore = m_widgetValue.ModelStore;
                int idx = 0;
                while (m_widgetValue.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                m_widgetValue.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override void OnTypeNameChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return;
                if (m_widget == null || m_widgetValue == null) return;

                VDModelStore modelStore = m_widgetValue.ModelStore;

                // delete old widgets
                if (!m_widgetValue.HasExternalReference)
                {
                    m_widgetValue.Delete();
                    m_widgetValue = null;
                }

                m_widgetValue = modelStore.CreateWidgetValue(newValue);
                if (!modelStore.IsPrimitiveType(newValue) && !modelStore.IsPredefinedType(newValue))
                {
                    // add member "Value"
                    m_widgetValue.AddMember<VDBuiltInProperty>(Utility.Constants.STR_TYPE_STRING, Utility.Constants.STR_VALUE_MEMBER);
                }
                m_widget.WidgetValue = m_widgetValue;
            }
        }

        class MemberNode : NodeBase
        {
            private VDWidgetValueMember m_member;
            internal void InitMemberNode(VDWidget widget, VDWidgetValueMember member, NodeBase parent)
            {
                base.Init(member.Id, widget, parent);

                m_member = member;
                if (member != null)
                {
                    Name = member.Name;
                    DispName = member.DispName;
                    TypeName = member.Meta != null && member.Meta.Type != null ? member.Meta.Type.FullName : string.Empty;
                    InitValue = member.InitValue;
                    ValidatorNames = member.ValidatorNames;
                    FormatterNames = member.FormatterNames;
                }
                else
                {
                    Name = string.Empty;
                    DispName = string.Empty;
                    TypeName = string.Empty;
                    InitValue = string.Empty;
                    ValidatorNames = string.Empty;
                    FormatterNames = string.Empty;
                }
            }

            internal override bool CanAddChildMembers
            {
                get
                {
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Type == null)
                        return false;

                    if (m_member.Meta is VDBuiltInProperty) 
                        return false;

                    VDMetaType metaType = m_member.Meta.Type as VDMetaType;
                    return !(metaType is VDDictionaryType || metaType is VDListType
                        || metaType is VDPredefinedType || metaType is VDPrimitiveType);
                }
            }

            internal override bool CanBeDeleted
            {
                get 
                {
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Host == null)
                        return false;

                    if (m_member.Meta is VDBuiltInProperty)
                        return false;

                    VDMetaType metaType = m_member.Meta.Host as VDMetaType;
                    return !(metaType is VDDictionaryType || metaType is VDListType
                        || metaType is VDPredefinedType || metaType is VDPrimitiveType);
                }
            }

            internal override bool CanChangeName
            {
                get 
                {
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Host == null)
                        return false;

                    if (m_member.Meta is VDBuiltInProperty)
                        return false;

                    VDMetaType metaType = m_member.Meta.Host as VDMetaType;
                    return !(metaType is VDDictionaryType || metaType is VDListType
                        || metaType is VDPredefinedType || metaType is VDPrimitiveType);
                }
            }

            internal override bool CanChangeType
            {
                get 
                { 
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Host == null)
                        return false;

                    VDMetaType metaType = m_member.Meta.Host as VDMetaType;
                    return !(metaType is VDPredefinedType || metaType is VDPrimitiveType);
                }
            }

            internal override void AddChildNode()
            {
                if (!CanAddChildMembers) return;
                
                VDConcreteType host = (VDConcreteType)m_member.Host;
                if (host == null) return;

                VDModelStore modelStore = host.ModelStore;
                int idx = 0;
                while (host.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                host.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override void DeleteNode()
            {
                if (!CanBeDeleted) return;

                if (m_member.Host != null)
                {
                    ((VDConcreteType)m_member.Host).DeleteMember(m_member.Name);
                }
            }

            internal override void OnNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return;
                if (oldValue == newValue) return;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");
                
                m_member.ChangeName(newValue);
            }

            internal override void OnDispNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return;
                if (oldValue == newValue) return;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");
                
                m_member.ChangeDispName(newValue);
            }

            internal override void OnTypeNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeType) return;
                if (oldValue == newValue) return;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeType(newValue);
            }

            internal override void OnInitValueChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return;
                m_member.InitValue = newValue;
            }

            internal override void OnValidatorChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return;
                m_member.ValidatorNames = newValue;
            }

            internal override void OnFormatterChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return;
                m_member.FormatterNames = newValue;
            }
        }
        #endregion
    }

    public class ViewModelHandler : ModelToolWindowHandler
    {
        public ViewModelHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip, 
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete)
        {
        }

        public void Show(VDView view) 
        { 
            m_form.ShowViewModelPanel(); 
        }

#if false
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
#endif
    }

    public class ActionDataHandler : ModelToolWindowHandler
    {
        public ActionDataHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip,
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete)
        {
        }

        public void Show(VDActionBase action) 
        { 
            m_form.ShowActionDataPanel();  
        }
#if false
        public void ShowActionModel(VDClientAction clientAction)
        {
            tlpActionModelLayout.Visible = true;
            tlpViewModelLayout.Visible = false;
            tlpWidgetModelLayout.Visible = false;
            tlpActionModelLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            m_currentView = clientAction.RootView;
            m_currentWidget = clientAction;

            // todo: 

            refreshAllItemsForActionModel();
        }
        
        private void refreshAllItemsForActionModel()
        {
            // update tree list view
            if (m_currentWidget != null && m_currentWidget.ModelInstance != null)
            {
                tlvActionModel.DataSource = m_currentWidget.ModelInstance.GetAllMemberInstances();
            }
            else
            {
                tlvActionModel.ClearObjects();
                tlvActionModel.DataSource = null;
            }
        }
#endif
    }

    public class AutoCompleteTextBox : TextBox
    {
        private AutocompleteMenu m_autoCompleteMenu;
        private ModelTypeDynamicCollection m_modelTypeCollection;

        // 
        public void Init(AutocompleteMenu autoCompleteMenu)
        {
            m_autoCompleteMenu = autoCompleteMenu;
            m_modelTypeCollection = new ModelTypeDynamicCollection(this);
            m_autoCompleteMenu.SetAutocompleteItems(m_modelTypeCollection);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (m_autoCompleteMenu != null)
            {
                if (keyData == Keys.Enter || keyData == Keys.Tab)
                {
                    m_autoCompleteMenu.ProcessKey((char)keyData, Keys.None);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //forcibly shows menu
            if (e.Control && e.KeyCode == Keys.Space)
                m_autoCompleteMenu.Show(this, true);

            base.OnKeyDown(e);
        }

        public void SetModelStore(VDModelStore modelStore)
        {
            this.m_modelTypeCollection.ModelStore = modelStore;
        }

        // collection
        class ModelTypeDynamicCollection : IEnumerable<AutocompleteItem>
        {
            private TextBoxBase tb;

            public ModelTypeDynamicCollection(TextBoxBase tb)
            {
                this.tb = tb;
            }

            public IEnumerator<AutocompleteItem> GetEnumerator()
            {
                return BuildList().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private IEnumerable<AutocompleteItem> BuildList()
            {
                var types = new Dictionary<string, VDMetaType>();

                if (ModelStore != null)
                {
                    foreach (VDMetaType metaType in ModelStore.MetaTypes)
                    {
                        types.Add(metaType.FullName, metaType);
                    }
                }

                //return autocomplete items
                foreach (var typeName in types.Keys)
                    yield return new AutocompleteItem(typeName, 0);
            }

            public VDModelStore ModelStore { get; set; }
        }
    }
}

