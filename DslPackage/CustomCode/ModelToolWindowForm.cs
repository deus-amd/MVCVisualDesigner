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

            m_widgetValueHandler = new WidgetValueHandler(this, tlvWidgetModel, toolTipMsg, ctxMenuWidgetValue, 
                        tsmiAddWidgetValueMember, tsmiDeleteWidgetValueMember, m_typeEditor);
            m_viewModelHandler = new ViewModelHandler(this, tlvViewModel, toolTipMsg, ctxMenuViewModel, 
                        tsmiAddViewModelMember, tsmiDeleteViewModelMember, tsmiCreateViewModel, tsmiRemoveViewModel, m_typeEditor);
            m_actionDataHandler = new ActionDataHandler(this, tlvActionModel, toolTipMsg, ctxMenuActionData, 
                        tsmiAddActionDataMember, tsmiDeleteActionDataMember, m_typeEditor);

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
    }

    public abstract class ModelToolWindowHandler
    {
        protected const string COLUMN_NAME = "Name";
        protected const string COLUMN_TYPE = "Type";
        protected const string COLUMN_VALIDATOR = "Validator";
        protected const string COLUMN_DISPNAME = "Display Name";

        protected ModelToolWindowForm m_form;
        protected DataTreeListView m_treeListView;
        protected ToolTip m_toolTip;
        protected ContextMenuStrip m_contextMenu;
        protected ToolStripMenuItem m_miAddChild;
        protected ToolStripMenuItem m_miDelete;
        protected AutoCompleteTextBox m_typeEditor = null;

        public ModelToolWindowHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip, 
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete, AutoCompleteTextBox typeEditor)
        {
            m_form = form;
            m_treeListView = treeListView;
            m_toolTip = toolTip;
            m_contextMenu = contextMenu;
            m_miAddChild = miAddChild;
            m_miDelete = miDelete;
            m_typeEditor = typeEditor;

            // set up event handlers
            // - cell editing
            m_treeListView.CellEditStarting += OnCellEditStarting;
            m_treeListView.CellEditValidating += OnCellEditValidating;
            m_treeListView.CellEditFinishing += OnCellEditFinishing;
            // - menu handling
            m_treeListView.CellRightClick += OnCellRightClick;
            m_miAddChild.Click += OnAddMemberMenuClick;
            m_miDelete.Click += OnDelMemberMenuClick;
        }

        // show/hide/lost focus
        protected VDView RootView { get; set; }
        protected VDWidget m_currentWidget;

        virtual public void Show(VDWidget widget)  // widget could be action, view and widgets
        {
            if (widget == null)
            {
                m_currentWidget = null;
                this.RootView = null;
            }
            else
            {
                this.RootView = widget.RootView;
                this.m_currentWidget = widget;
                m_typeEditor.SetModelStore(widget != null ? widget.GetModelStore() : null);
            }
            refreshTreeListView();
        }

        public virtual void OnLostFocus()
        {
            // hide type editor
            if (m_typeEditor != null && m_typeEditor.Visible)
            {
                m_typeEditor.Hide();
            }
        }

        public virtual void OnHideWindow()
        { 
            m_treeListView.ClearObjects();
            m_treeListView.DataSource = null;
        }

        // menu handling
        virtual protected void OnCellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.m_contextMenu;
            m_miAddChild.Enabled = false;
            m_miDelete.Enabled = false;

            NodeBase selectedNode = (NodeBase)e.Model;
            if (m_currentWidget != null && selectedNode != null)
            {
                m_miAddChild.Enabled = selectedNode.CanAddChildMembers;
                m_miAddChild.Tag = selectedNode;

                m_miDelete.Enabled = selectedNode.CanBeDeleted;
                m_miDelete.Tag = selectedNode;
            }
        }

        virtual protected void OnAddMemberMenuClick(object sender, EventArgs e) 
        {
            System.Diagnostics.Debug.Assert(m_currentWidget != null);
            if (m_currentWidget == null) return;

            NodeBase selectedNode = m_miAddChild.Tag as NodeBase;
            using (var trans = m_currentWidget.Store.TransactionManager.BeginTransaction("addd widget value member"))
            {
                selectedNode.AddChildNode();
                trans.Commit();
            }

            refreshTreeListView();
        }

        virtual protected void OnDelMemberMenuClick(object sender, EventArgs e) 
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

        // cell editing
        virtual protected void OnCellEditStarting(object sender, CellEditEventArgs e) 
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
        }

        virtual protected void OnCellEditValidating(object sender, CellEditEventArgs e) 
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
        }

        virtual protected void OnCellEditFinishing(object sender, CellEditEventArgs e) 
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
                string oldValue = e.Value != null ? e.Value.ToString() : "";
                string newValue = e.NewValue != null ? e.NewValue.ToString() : "";
                bool ret = false;
                if (columnName == COLUMN_NAME)
                    ret = node.OnNameChanged(oldValue, newValue);
                else if (columnName == COLUMN_DISPNAME)
                    ret = node.OnDispNameChanged(oldValue, newValue);
                else if (columnName == COLUMN_TYPE)
                    ret = node.OnTypeNameChanged(oldValue, newValue);
                else if (columnName == COLUMN_VALIDATOR)
                    ret = node.OnValidatorChanged(oldValue, newValue);
                else
                    ret = onCellEditingFinishedOtherColumns(sender, e, node, columnName, oldValue, newValue);

                if (ret) trans.Commit();
            }

            refreshTreeListView();
        }

        virtual protected bool onCellEditingFinishedOtherColumns(object sender, CellEditEventArgs e, 
            NodeBase node, string columnName, string oldValue, string newValue)
        {
            return false;
        }

        // tree list view handling
        protected virtual void refreshTreeListView()
        {
            // reclaim the old nodes
            if (m_treeListView.DataSource != null && m_treeListView.DataSource is List<NodeBase>)
            {
                List<NodeBase> nodes = (List<NodeBase>)m_treeListView.DataSource;
                foreach(NodeBase node in nodes)
                {
                    reclaimNode(node);
                }
            }

            // update tree list view
            if (m_currentWidget != null)
            {
                List<NodeBase> allNodes = getAllNodes();
                if (allNodes != null)
                {
                    m_treeListView.DataSource = allNodes;
                }
            }
            else
            {
                m_treeListView.ClearObjects();
                m_treeListView.DataSource = null;
            }
        }

        virtual protected List<NodeBase> getAllNodes() { return null; }

        protected void reclaimNode(NodeBase node) 
        {
            if (!m_nodeCache.ContainsKey(node.GetType()))
                m_nodeCache.Add(node.GetType(), new Stack<NodeBase>());
            m_nodeCache[node.GetType()].Push(node);
        }

        protected T getCachedNode<T>() where T : NodeBase, new()
        {
            if (!m_nodeCache.ContainsKey(typeof(T)) || m_nodeCache[typeof(T)].Count <= 0) 
                return new T();
            else
                return m_nodeCache[typeof(T)].Pop() as T;
        }

        protected Dictionary<Type, Stack<NodeBase>> m_nodeCache = new Dictionary<Type, Stack<NodeBase>>();

        // Utility
        protected void showTooltipMsg(string msg, CellEditEventArgs e)
        {
            m_toolTip.Show(msg, m_treeListView, e.CellBounds.Left, e.CellBounds.Bottom, 1000); 
        }

        // Node class
        protected abstract class NodeBase
        {
            protected Guid m_id;
            protected NodeBase m_parentNode;

            protected void Init(Guid id, NodeBase parent)
            {
                m_id = id;
                m_parentNode = parent;
            }

            public Guid ID { get { return m_id; } }
            public Guid ParentID { get { return m_parentNode == null ? Guid.Empty : m_parentNode.ID; } }

            public string Name { get; set; }
            public string DispName { get; set; }
            public string TypeName { get; set; }
            public string ValidatorNames { get; set; }

            /// <summary> If be able to Add child nodes of this node </summary>
            abstract internal bool CanAddChildMembers { get; }

            abstract internal bool CanBeDeleted { get; }

            /// <summary> If be able to change Name column of this node </summary>
            abstract internal bool CanChangeName { get; }

            /// <summary> If be able to change Type column of this node </summary>
            abstract internal bool CanChangeType { get; }

            virtual internal void AddChildNode() { }
            virtual internal void DeleteNode() { }

            virtual internal bool OnNameChanged(string oldValue, string newValue) { return false; }
            virtual internal bool OnDispNameChanged(string oldValue, string newValue) { return false; }
            virtual internal bool OnTypeNameChanged(string oldValue, string newValue) { return false; }
            virtual internal bool OnValidatorChanged(string oldValue, string newValue) { return false; }
        }
    }

    public class WidgetValueHandler : ModelToolWindowHandler
    {
        private const string COLUMN_INITVAL = "Init Value";
        private const string COLUMN_FORMATTER = "Formatter";

        public WidgetValueHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip,
                                    ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete, AutoCompleteTextBox typeEditor)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete, typeEditor)
        {
        }

        override protected void OnCellEditStarting(object sender, CellEditEventArgs e)
        {
            NodeBase node = e.RowObject as NodeBase;
            string columnName = e.Column.Text;

            if (columnName == COLUMN_INITVAL || columnName == COLUMN_FORMATTER || columnName == COLUMN_VALIDATOR)
            {
                if (node == null || node is TypeNode)
                {
                    showTooltipMsg(columnName + " can not be changed.", e);
                    e.Cancel = true;
                }
            }
            else
                base.OnCellEditStarting(sender, e);
        }

        override protected void OnCellEditValidating(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_INITVAL)
            {
                //todo: validate according to type
            } 
            else
            {
                base.OnCellEditValidating(sender, e);
            }
        }

        protected override bool onCellEditingFinishedOtherColumns(object sender, CellEditEventArgs e, 
            NodeBase node, string columnName, string oldValue, string newValue)
        {
            WidgetValueNode curNode = node as WidgetValueNode;
            if (curNode == null) return false;

            if (columnName == COLUMN_INITVAL)
                return curNode.OnInitValueChanged(oldValue, newValue);
            else if (columnName == COLUMN_FORMATTER)
                return curNode.OnFormatterChanged(oldValue, newValue);

            return false;
        }

        override public void Show(VDWidget widget) 
        {
            m_form.ShowWidgetValuePanel();
            base.Show(widget);
        }

        #region Tree List View Nodes
        protected override List<NodeBase> getAllNodes()
        {
            List<NodeBase> nodes = new List<NodeBase>();
            getNodesForWidget(m_currentWidget, null, nodes);
            return nodes;
        }

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
            TypeNode node = getCachedNode<TypeNode>();
            node.InitTypeNode(widget, parentNode);
            return node;
        }

        private MemberNode getMemberNode(VDWidget widget, VDWidgetValueMember widgetValueMember, NodeBase parentNode)
        {
            MemberNode node = getCachedNode<MemberNode>();
            node.InitMemberNode(widget, widgetValueMember, parentNode);
            return node;
        }

        abstract class WidgetValueNode : NodeBase
        {
            protected VDWidget m_widget;
            protected void Init(Guid id, VDWidget widget, NodeBase parent)
            {
                m_widget = widget;
                base.Init(id, parent);
            }

            public string InitValue { get; set; }
            public string FormatterNames { get; set; }

            virtual internal bool OnInitValueChanged(string oldValue, string newValue) { return false; }
            virtual internal bool OnFormatterChanged(string oldValue, string newValue) { return false; }
        }

        // the node to represent the WidgetValue of a widget, only one node of this kind for a widget
        class TypeNode : WidgetValueNode
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

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                if (m_widget == null || m_widgetValue == null) return false;

                VDModelStore modelStore = m_widgetValue.ModelStore;

                // delete old widgets
                m_widgetValue.DeleteIfNoReference();
                m_widgetValue = null;

                m_widgetValue = modelStore.CreateConcreteType<VDWidgetValue>(newValue);
                if (!modelStore.IsPrimitiveType(newValue) && !modelStore.IsPredefinedType(newValue))
                {
                    // add member "Value"
                    m_widgetValue.AddMember<VDBuiltInProperty>(Utility.Constants.STR_TYPE_STRING, Utility.Constants.STR_VALUE_MEMBER);
                }
                m_widget.WidgetValue = m_widgetValue;
                return true;
            }
        }

        class MemberNode : WidgetValueNode
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

                    //if (m_member.Meta is VDBuiltInProperty) 
                    //    return false;

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
                
                VDConcreteType memberType = (VDConcreteType)m_member.Type;
                if (memberType == null) return;

                VDModelStore modelStore = memberType.ModelStore;
                int idx = 0;
                while (memberType.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                memberType.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override void DeleteNode()
            {
                if (!CanBeDeleted) return;

                if (m_member.Host != null)
                {
                    ((VDConcreteType)m_member.Host).DeleteMember(m_member.Name);
                }
            }

            internal override bool OnNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");
                
                m_member.ChangeName(newValue);
                return true;
            }

            internal override bool OnDispNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");
                
                m_member.ChangeDispName(newValue);
                return true;
            }

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeType) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeType(newValue);
                return true;
            }

            internal override bool OnInitValueChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.InitValue = newValue;
                return true;
            }

            internal override bool OnValidatorChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.ValidatorNames = newValue;
                return true;
            }

            internal override bool OnFormatterChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.FormatterNames = newValue;
                return true;
            }
        }
        #endregion
    }

    public class ViewModelHandler : ModelToolWindowHandler
    {
        private const string COLUMN_IS_JS_MODEL = "JS Model";

        private ToolStripMenuItem m_miCreateModel;
        private ToolStripMenuItem m_miRemoveModel;

        public ViewModelHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip, 
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete,
            ToolStripMenuItem miCreateModel, ToolStripMenuItem miRemoveModel, AutoCompleteTextBox typeEditor)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete, typeEditor)
        {
            m_miCreateModel = miCreateModel;
            m_miRemoveModel = miRemoveModel;

            m_miCreateModel.Click += onCreateModelClick;
            m_miRemoveModel.Click += onRemoveModelClick;
        }

        private VDView m_currentView = null;
        public override void Show(VDWidget widget)
        {
            m_form.ShowViewModelPanel();
            m_currentView = widget as VDView;
            base.Show(widget);
        }

        protected override List<NodeBase> getAllNodes()
        {
            List<NodeBase> allNodes = new List<NodeBase>();
            if (m_currentView != null && m_currentView.Model != null)
            {
                RootNode rootNode = getCachedNode<RootNode>();
                rootNode.InitRootNode(m_currentView);
                allNodes.Add(rootNode);
                getMemberNode(m_currentView.Model, rootNode, allNodes);
            }
            return allNodes;
        }

        private void getMemberNode(VDViewModel model, NodeBase parent, List<NodeBase> allNodes)
        {
            if (model == null) return;

            foreach (VDModelMember m in model.Members)
            {
                VDViewModelMember member = m as VDViewModelMember;
                MemberNode node = getCachedNode<MemberNode>();
                node.InitMemberNode(member, parent);
                allNodes.Add(node);

                if (member.Meta != null && !(member.Meta.Type is VDPrimitiveType)) // the type of this member is not Primitive Type
                {
                    getMemberNode(member.Type as VDViewModel, node, allNodes);
                }
            }
        }

        // Add Model / Remove Model menu
        protected override void OnCellRightClick(object sender, CellRightClickEventArgs e)
        {
            base.OnCellRightClick(sender, e);

            m_miCreateModel.Enabled = m_currentView != null && m_currentView.Model == null;
            m_miRemoveModel.Enabled = m_currentView != null && m_currentView.Model != null;
        }

        void onCreateModelClick(object sender, EventArgs e)
        {
            if (m_currentView == null || m_currentView.Model != null)
                return;

            using (var trans = m_currentView.Store.TransactionManager.BeginTransaction(
                string.Format("Create view model for {0} [{1}]", m_currentView.WidgetName ?? string.Empty, m_currentView.WidgetType.ToString())))
            {
                VDModelStore modelStore = m_currentView.GetModelStore();
                if (modelStore != null)
                {
                    m_currentView.Model = modelStore.CreateConcreteType<VDViewModel>(Utility.Constants.STR_TYPE_STRING);
                }
                trans.Commit();
            }

            refreshTreeListView();
        }

        void onRemoveModelClick(object sender, EventArgs e)
        {
            if (m_currentView == null || m_currentView.Model == null)
                return;

            using (var trans = m_currentView.Store.TransactionManager.BeginTransaction(
                string.Format("Remove view model for {0} [{1}]", m_currentView.WidgetName ?? string.Empty, m_currentView.WidgetType.ToString())))
            {
                VDModelStore modelStore = m_currentView.GetModelStore();
                if (modelStore != null)
                {
                    VDViewModel viewModel = m_currentView.Model;
                    m_currentView.Model = null;
                    viewModel.DeleteIfNoReference();
                }
                trans.Commit();
            }

            refreshTreeListView();
        }

        protected override void OnCellEditStarting(object sender, CellEditEventArgs e)
        {
            NodeBase node = e.RowObject as NodeBase;
            string columnName = e.Column.Text;

            if (columnName == COLUMN_IS_JS_MODEL || columnName == COLUMN_VALIDATOR)
            {
                if (node == null || node is RootNode)
                {
                    showTooltipMsg(columnName + " can not be changed.", e);
                    e.Cancel = true;
                }
            }
            else
                base.OnCellEditStarting(sender, e);
        }

        protected override void OnCellEditValidating(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            base.OnCellEditValidating(sender, e);
        }

        abstract class ViewModelNode : NodeBase
        {
            abstract public bool IsJSModel { get; set; }
        }

        class RootNode : ViewModelNode
        {
            private VDView m_view;
            internal void InitRootNode(VDView view)
            {
                m_view = view;
                base.Init(m_view.Id, null);
                if (m_view.Model != null)
                {
                    this.Name = m_view != null ? "[" + view.WidgetType.ToString() + "]" : string.Empty;
                    TypeName = m_view.Model.Meta != null ? m_view.Model.Meta.FullName : string.Empty;
                    DispName = m_view.Model.DispName;
                }
                else
                {
                    Name = string.Empty;
                    TypeName = string.Empty;
                    DispName = string.Empty;
                }

                ValidatorNames = string.Empty;
                IsJSModel = false;
            }

            public override bool IsJSModel 
            { 
                get { return false; }
                set { } 
            }

            internal override bool CanAddChildMembers
            {
                get
                {
                    if (m_view.Model == null || m_view.Model.Meta == null)
                        return false;

                    VDMetaType metaType = m_view.Model.Meta;
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
                    return m_view.Model != null && m_view.Model.Meta != null;
                }
            }

            internal override void AddChildNode()
            {
                if (m_view.Model == null || m_view.Model == null) return;
                VDModelStore modelStore = m_view.Model.ModelStore;
                int idx = 0;
                while (m_view.Model.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                m_view.Model.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;

                if (m_view.Model == null || m_view.Model == null) return false;

                VDModelStore modelStore = m_view.Model.ModelStore;

                // delete old widgets
                m_view.Model.DeleteIfNoReference();

                m_view.Model = modelStore.CreateConcreteType<VDViewModel>(newValue);
                return true;
            }
        }

        class MemberNode : ViewModelNode
        {
            private VDViewModelMember m_member;
            internal void InitMemberNode(VDViewModelMember member, NodeBase parent)
            {
                m_member = member;
                base.Init(member.Id, parent);

                if (member != null)
                {
                    Name = member.Name;
                    DispName = member.DispName;
                    TypeName = member.Meta != null && member.Meta.Type != null ? member.Meta.Type.FullName : string.Empty;
                    ValidatorNames = member.ValidatorNames;
                    IsJSModel = member.IsJSModel;
                }
                else
                {
                    Name = string.Empty;
                    DispName = string.Empty;
                    TypeName = string.Empty;
                    ValidatorNames = string.Empty;
                    IsJSModel = false;
                }
            }

            public override bool IsJSModel 
            { 
                get 
                {
                    return m_member.IsJSModel;
                }
                set
                { 
                    using(var trans = this.m_member.Store.TransactionManager.BeginTransaction("Set IsJSModel value"))
                    {
                        m_member.IsJSModel = value;
                        trans.Commit();
                    }
                } 
            }

            internal override bool CanAddChildMembers
            {
                get
                {
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Type == null)
                        return false;

                    //if (m_member.Meta is VDBuiltInProperty)
                    //    return false;

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

                VDConcreteType memberType = (VDConcreteType)m_member.Type;
                if (memberType == null) return;

                VDModelStore modelStore = memberType.ModelStore;
                int idx = 0;
                while (memberType.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                memberType.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override void DeleteNode()
            {
                if (!CanBeDeleted) return;

                if (m_member.Host != null)
                {
                    ((VDConcreteType)m_member.Host).DeleteMember(m_member.Name);
                }
            }

            internal override bool OnNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeName(newValue);
                return true;
            }

            internal override bool OnDispNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeDispName(newValue);
                return true;
            }

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeType) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeType(newValue);
                return true;
            }

            internal override bool OnValidatorChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.ValidatorNames = newValue;
                return true;
            }
        }
    }

    public class ActionDataHandler : ModelToolWindowHandler
    {
        private const string COLUMN_DATA_SOURCE = "Data Source";
        private const string COLUMN_CUSTOM_SELECTOR = "Custom Selector";

        public ActionDataHandler(ModelToolWindowForm form, DataTreeListView treeListView, ToolTip toolTip,
            ContextMenuStrip contextMenu, ToolStripMenuItem miAddChild, ToolStripMenuItem miDelete, AutoCompleteTextBox typeEditor)
            : base(form, treeListView, toolTip, contextMenu, miAddChild, miDelete, typeEditor)
        {
        }

        private VDActionBase m_currentAction;
        public override void Show(VDWidget widget)
        {
            m_form.ShowActionDataPanel();
            m_currentAction = widget as VDActionBase;
            base.Show(widget);
        }

        protected override List<NodeBase> getAllNodes()
        {
            List<NodeBase> allNodes = new List<NodeBase>();
            if (m_currentAction != null && m_currentAction.ActionData != null)
            {
                RootNode rootNode = getCachedNode<RootNode>();
                rootNode.InitRootNode(m_currentAction);
                allNodes.Add(rootNode);
                getMemberNode(m_currentAction.ActionData, rootNode, allNodes);
            }
            return allNodes;
        }
        
        private void getMemberNode(VDActionData ad, NodeBase parent, List<NodeBase> allNodes)
        {
            if (ad == null) return;

            foreach(VDModelMember m in ad.Members)
            {
                VDActionDataMember member = m as VDActionDataMember;
                MemberNode node = getCachedNode<MemberNode>();
                node.InitMemberNode(member, parent);
                allNodes.Add(node);

                if (member.Meta != null && !(member.Meta.Type is VDPrimitiveType)) // the type of this member is not Primitive Type
                {
                    getMemberNode(member.Type as VDActionData, node, allNodes);
                }
            }
        }

        protected override void OnCellEditStarting(object sender, CellEditEventArgs e)
        {
            NodeBase node = e.RowObject as NodeBase;
            string columnName = e.Column.Text;

            if (columnName == COLUMN_DATA_SOURCE || columnName == COLUMN_CUSTOM_SELECTOR || columnName == COLUMN_VALIDATOR)
            {
                if (node == null || node is RootNode)
                {
                    showTooltipMsg(columnName + " can not be changed.", e);
                    e.Cancel = true;
                }
            }
            else
                base.OnCellEditStarting(sender, e);
        }

        protected override void OnCellEditValidating(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text;
            if (columnName == COLUMN_TYPE)
            {
                string newValue = (string)e.NewValue;
                if (m_currentAction == null || m_currentAction.ActionData == null ||
                    (e.RowObject is RootNode && m_currentAction.ActionData.ModelStore.IsPrimitiveType(newValue)))
                {
                    showTooltipMsg("Type can not be primitive type", e);
                    e.Cancel = true;
                }
            }
            else
            {
                base.OnCellEditValidating(sender, e);
            }
        }

        protected override bool onCellEditingFinishedOtherColumns(object sender, CellEditEventArgs e, 
            NodeBase node, string columnName, string oldValue, string newValue)
        {
            ActionDataNode curNode = node as ActionDataNode;
            if (curNode == null) return false;

            if (columnName == COLUMN_DATA_SOURCE)
                return curNode.OnDataSourceChanged(oldValue, newValue);
            else if (columnName == COLUMN_CUSTOM_SELECTOR)
                return curNode.OnCustomSelectorChanged(oldValue, newValue);

            return base.onCellEditingFinishedOtherColumns(sender, e, node, columnName, oldValue, newValue);
        }

        abstract class ActionDataNode : NodeBase
        {
            public string DataSource { get; set; }
            public string CustomSelector { get; set; }

            internal virtual bool OnDataSourceChanged(string oldValue, string newValue) { return false; }
            internal virtual bool OnCustomSelectorChanged(string oldValue, string newValue) { return false; }
        }

        class RootNode : ActionDataNode
        {
            private VDActionBase m_action;
            internal void InitRootNode(VDActionBase action)
            {
                m_action = action;
                base.Init(action.Id, null);
                if (action.ActionData != null)
                {
                    this.Name = m_action != null ? string.Format("[{0}]", m_action.Name ?? string.Empty) : string.Empty;
                    TypeName = m_action.ActionData.Meta != null ? action.ActionData.Meta.FullName : string.Empty;
                    DispName = action.ActionData.DispName;
                }
                else
                {
                    Name = string.Empty;
                    TypeName = string.Empty;
                    DispName = string.Empty;
                }

                ValidatorNames = string.Empty;
                DataSource = string.Empty;
                CustomSelector = string.Empty;
            }

            internal override bool CanAddChildMembers
            {
                get
                {
                    if (m_action.ActionData == null || m_action.ActionData.Meta == null)
                        return false;

                    VDMetaType metaType = m_action.ActionData.Meta;
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
                    return m_action.ActionData != null && m_action.ActionData.Meta != null;
                }
            }

            internal override void AddChildNode()
            {
                if (m_action.ActionData == null || m_action.ActionData == null) return;
                VDModelStore modelStore = m_action.ActionData.ModelStore;
                int idx = 0;
                while (m_action.ActionData.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                m_action.ActionData.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                if (m_action.ActionData == null || m_action.ActionData == null) return false;

                VDModelStore modelStore = m_action.ActionData.ModelStore;

                // delete old widgets
                m_action.ActionData.DeleteIfNoReference();

                m_action.ActionData = modelStore.CreateConcreteType<VDActionData>(newValue);
                return true;
            }
        }

        class MemberNode : ActionDataNode
        {
            private VDActionDataMember m_member;
            internal void InitMemberNode(VDActionDataMember member, NodeBase parent)
            {
                m_member = member;
                base.Init(member.Id, parent);

                if (member != null)
                {
                    Name = member.Name;
                    DispName = member.DispName;
                    TypeName = member.Meta != null && member.Meta.Type != null ? member.Meta.Type.FullName : string.Empty;
                    ValidatorNames = member.ValidatorNames;
                    DataSource = member.DataSource;
                    CustomSelector = member.CustomSelector;
                }
                else
                {
                    Name = string.Empty;
                    DispName = string.Empty;
                    TypeName = string.Empty;
                    ValidatorNames = string.Empty;
                    DataSource = string.Empty;
                    CustomSelector = string.Empty;
                }
            }

            internal override bool CanAddChildMembers
            {
                get
                {
                    if (m_member == null || m_member.Meta == null || m_member.Meta.Type == null)
                        return false;

                    //if (m_member.Meta is VDBuiltInProperty)
                    //    return false;

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

                VDConcreteType memberType = (VDConcreteType)m_member.Type;
                if (memberType == null) return;

                VDModelStore modelStore = memberType.ModelStore;
                int idx = 0;
                while (memberType.Members.Find(m => m.Name == Utility.Constants.STR_NEW_MEMBER + ++idx) != null) ;
                memberType.AddMember<VDProperty>(Utility.Constants.STR_TYPE_STRING, "NewMember" + idx);
            }

            internal override void DeleteNode()
            {
                if (!CanBeDeleted) return;

                if (m_member.Host != null)
                {
                    ((VDConcreteType)m_member.Host).DeleteMember(m_member.Name);
                }
            }

            internal override bool OnNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeName(newValue);
                return true;
            }

            internal override bool OnDispNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeName) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeDispName(newValue);
                return true;
            }

            internal override bool OnTypeNameChanged(string oldValue, string newValue)
            {
                if (!CanChangeType) return false;
                if (oldValue == newValue) return false;
                if (string.IsNullOrWhiteSpace(newValue)) throw new ArgumentNullException("newValue");

                m_member.ChangeType(newValue);
                return true;
            }

            internal override bool OnValidatorChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.ValidatorNames = newValue;
                return true;
            }

            internal override bool OnDataSourceChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.DataSource = newValue;
                return true;
            }

            internal override bool OnCustomSelectorChanged(string oldValue, string newValue)
            {
                if (oldValue == newValue) return false;
                m_member.CustomSelector = newValue;
                return true;
            }
        }
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
            if (m_autoCompleteMenu != null && m_autoCompleteMenu.Visible)
            {
                if (keyData == Keys.Enter || keyData == Keys.Tab)
                {
                    m_autoCompleteMenu.ProcessKey((char)keyData, Keys.None);
                    return true;
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
                Dictionary<string, int> allTypes = new Dictionary<string,int>();

                if (ModelStore != null) 
                {
                    foreach (VDMetaType metaType in ModelStore.MetaTypes)
                    {
                        if (!allTypes.ContainsKey(metaType.FullName))
                        {
                            if (metaType is VDPrimitiveType)
                                allTypes.Add(metaType.FullName, 2);
                            else if (metaType is VDPredefinedType)
                                allTypes.Add(metaType.FullName, 3);
                            else
                                allTypes.Add(metaType.FullName, 0);
                        }
                    }

                    // get primitive and predefined types
                    foreach(string type in ModelStore.PrimitiveTypes)
                    {
                        if (!allTypes.ContainsKey(type))
                        {
                            allTypes.Add(type, 2);
                        }
                    }

                    // todo: 
                    //foreach(string type in ModelStore.PredefinedTypes)
                    //{
                    //    if (!allTypes.ContainsKey(type))
                    //    {
                    //        allTypes.Add(type, 3);
                    //    }
                    //}
                    if (!allTypes.ContainsKey(Utility.Constants.STR_TYPE_DICTIONARY))
                        allTypes.Add(Utility.Constants.STR_TYPE_DICTIONARY, 3);
                    if (!allTypes.ContainsKey(Utility.Constants.STR_TYPE_LIST))
                        allTypes.Add(Utility.Constants.STR_TYPE_LIST, 3);
                }

                //return autocomplete items
                List<ModelTypeItem> items = new List<ModelTypeItem>(); 
                foreach (var kv in allTypes)
                {
                    items.Add(new ModelTypeItem(kv.Key, kv.Value));
                }
                items.Sort((x, y) => string.Compare(x.Text, y.Text));
                return items;
            }

            public VDModelStore ModelStore { get; set; }

            class ModelTypeItem : AutocompleteItem
            {
                public ModelTypeItem() { }

                public ModelTypeItem(string text) : base(text) { }

                public ModelTypeItem(string text, int imageIndex) : base(text, imageIndex) { }

                public ModelTypeItem(string text, int imageIndex, string menuText) : base(text, imageIndex, menuText) { }

                public ModelTypeItem(string text, int imageIndex, string menuText, string toolTipTitle, string toolTipText)
                    : base(text, imageIndex, menuText, toolTipTitle, toolTipText)
                {
                }

                public override CompareResult Compare(string fragmentText)
                {
                    if (Text.StartsWith(fragmentText, StringComparison.InvariantCultureIgnoreCase))
                        return CompareResult.VisibleAndSelected;

                    return CompareResult.Hidden;
                }
            }
        }
    }
}

