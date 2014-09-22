namespace MVCVisualDesigner
{
    partial class ModelToolWindowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelToolWindowForm));
            this.tlpViewModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvViewModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColName_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColIsJSModel_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlpActionModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvActionModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColName_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDataSource_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCustomSelector_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDispName_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColID_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColParentID_Action = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ctxMenuViewModel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpWidgetModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvWidgetModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColName_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColInitValue_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColFormatter_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDispName_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColID_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColParentID_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ctxMenuActionData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddActionDataMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteActionDataMember = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipMsg = new System.Windows.Forms.ToolTip(this.components);
            this.ctxMenuWidgetValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddWidgetValueMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteWidgetValueMember = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.olvParentID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.autocompleteMenu_Type = new AutocompleteMenuNS.AutocompleteMenu();
            this.m_typeEditor = new MVCVisualDesigner.AutoCompleteTextBox();
            this.olvColDispName_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColID_View = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColParentID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlpViewModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).BeginInit();
            this.tlpActionModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvActionModel)).BeginInit();
            this.ctxMenuViewModel.SuspendLayout();
            this.tlpWidgetModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvWidgetModel)).BeginInit();
            this.ctxMenuActionData.SuspendLayout();
            this.ctxMenuWidgetValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpViewModelLayout
            // 
            this.tlpViewModelLayout.ColumnCount = 4;
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpViewModelLayout.Controls.Add(this.tlvViewModel, 0, 0);
            this.tlpViewModelLayout.Location = new System.Drawing.Point(12, 12);
            this.tlpViewModelLayout.Name = "tlpViewModelLayout";
            this.tlpViewModelLayout.RowCount = 3;
            this.tlpViewModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpViewModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpViewModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tlpViewModelLayout.Size = new System.Drawing.Size(735, 179);
            this.tlpViewModelLayout.TabIndex = 0;
            // 
            // tlvViewModel
            // 
            this.tlvViewModel.AllColumns.Add(this.olvColName_View);
            this.tlvViewModel.AllColumns.Add(this.olvColType_View);
            this.tlvViewModel.AllColumns.Add(this.olvColValidators_View);
            this.tlvViewModel.AllColumns.Add(this.olvColIsJSModel_View);
            this.tlvViewModel.AllColumns.Add(this.olvColDispName_View);
            this.tlvViewModel.AllColumns.Add(this.olvColID_View);
            this.tlvViewModel.AllColumns.Add(this.olvColParentID);
            this.tlvViewModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvViewModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName_View,
            this.olvColType_View,
            this.olvColValidators_View,
            this.olvColIsJSModel_View,
            this.olvColDispName_View});
            this.tlpViewModelLayout.SetColumnSpan(this.tlvViewModel, 4);
            this.tlvViewModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvViewModel.DataSource = null;
            this.tlvViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvViewModel.FullRowSelect = true;
            this.tlvViewModel.GridLines = true;
            this.tlvViewModel.KeyAspectName = "ID";
            this.tlvViewModel.Location = new System.Drawing.Point(3, 3);
            this.tlvViewModel.Name = "tlvViewModel";
            this.tlvViewModel.OwnerDraw = true;
            this.tlvViewModel.ParentKeyAspectName = "ParentID";
            this.tlvViewModel.RootKeyValueString = "";
            this.tlpViewModelLayout.SetRowSpan(this.tlvViewModel, 3);
            this.tlvViewModel.SelectAllOnControlA = false;
            this.tlvViewModel.ShowGroups = false;
            this.tlvViewModel.ShowImagesOnSubItems = true;
            this.tlvViewModel.Size = new System.Drawing.Size(729, 173);
            this.tlvViewModel.TabIndex = 0;
            this.tlvViewModel.UseAlternatingBackColors = true;
            this.tlvViewModel.UseCompatibleStateImageBehavior = false;
            this.tlvViewModel.UseHotItem = true;
            this.tlvViewModel.UseSubItemCheckBoxes = true;
            this.tlvViewModel.UseTranslucentHotItem = true;
            this.tlvViewModel.View = System.Windows.Forms.View.Details;
            this.tlvViewModel.VirtualMode = true;
            // 
            // olvColName_View
            // 
            this.olvColName_View.AspectName = "Name";
            this.olvColName_View.CellPadding = null;
            this.olvColName_View.Text = "Name";
            this.olvColName_View.Width = 150;
            // 
            // olvColType_View
            // 
            this.olvColType_View.AspectName = "TypeName";
            this.olvColType_View.CellPadding = null;
            this.olvColType_View.MinimumWidth = 150;
            this.olvColType_View.Text = "Type";
            this.olvColType_View.Width = 149;
            // 
            // olvColValidators_View
            // 
            this.olvColValidators_View.AspectName = "ValidatorNames";
            this.olvColValidators_View.CellPadding = null;
            this.olvColValidators_View.Text = "Validator";
            this.olvColValidators_View.Width = 114;
            // 
            // olvColIsJSModel_View
            // 
            this.olvColIsJSModel_View.AspectName = "IsJavaScriptModel";
            this.olvColIsJSModel_View.CellPadding = null;
            this.olvColIsJSModel_View.CheckBoxes = true;
            this.olvColIsJSModel_View.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColIsJSModel_View.Text = "JS Model";
            this.olvColIsJSModel_View.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColIsJSModel_View.Width = 61;
            // 
            // tlpActionModelLayout
            // 
            this.tlpActionModelLayout.ColumnCount = 4;
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.Controls.Add(this.tlvActionModel, 0, 0);
            this.tlpActionModelLayout.Location = new System.Drawing.Point(12, 375);
            this.tlpActionModelLayout.Name = "tlpActionModelLayout";
            this.tlpActionModelLayout.RowCount = 3;
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpActionModelLayout.Size = new System.Drawing.Size(735, 139);
            this.tlpActionModelLayout.TabIndex = 2;
            // 
            // tlvActionModel
            // 
            this.tlvActionModel.AllColumns.Add(this.olvColName_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColType_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColDataSource_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColCustomSelector_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColValidators_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColDispName_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColID_Action);
            this.tlvActionModel.AllColumns.Add(this.olvColParentID_Action);
            this.tlvActionModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvActionModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvActionModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName_Action,
            this.olvColType_Action,
            this.olvColDataSource_Action,
            this.olvColCustomSelector_Action,
            this.olvColValidators_Action,
            this.olvColDispName_Action});
            this.tlpActionModelLayout.SetColumnSpan(this.tlvActionModel, 4);
            this.tlvActionModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvActionModel.DataSource = null;
            this.tlvActionModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvActionModel.FullRowSelect = true;
            this.tlvActionModel.GridLines = true;
            this.tlvActionModel.KeyAspectName = "ID";
            this.tlvActionModel.Location = new System.Drawing.Point(3, 3);
            this.tlvActionModel.Name = "tlvActionModel";
            this.tlvActionModel.OwnerDraw = true;
            this.tlvActionModel.ParentKeyAspectName = "ParentID";
            this.tlvActionModel.RootKeyValueString = "";
            this.tlpActionModelLayout.SetRowSpan(this.tlvActionModel, 3);
            this.tlvActionModel.SelectAllOnControlA = false;
            this.tlvActionModel.ShowGroups = false;
            this.tlvActionModel.ShowImagesOnSubItems = true;
            this.tlvActionModel.Size = new System.Drawing.Size(729, 133);
            this.tlvActionModel.TabIndex = 2;
            this.tlvActionModel.UseAlternatingBackColors = true;
            this.tlvActionModel.UseCompatibleStateImageBehavior = false;
            this.tlvActionModel.UseHotItem = true;
            this.tlvActionModel.UseSubItemCheckBoxes = true;
            this.tlvActionModel.UseTranslucentHotItem = true;
            this.tlvActionModel.View = System.Windows.Forms.View.Details;
            this.tlvActionModel.VirtualMode = true;
            // 
            // olvColName_Action
            // 
            this.olvColName_Action.AspectName = "Name";
            this.olvColName_Action.CellPadding = null;
            this.olvColName_Action.Text = "Name";
            this.olvColName_Action.Width = 146;
            // 
            // olvColType_Action
            // 
            this.olvColType_Action.AspectName = "TypeName";
            this.olvColType_Action.CellPadding = null;
            this.olvColType_Action.MinimumWidth = 150;
            this.olvColType_Action.Text = "Type";
            this.olvColType_Action.Width = 150;
            // 
            // olvColDataSource_Action
            // 
            this.olvColDataSource_Action.AspectName = "DataSource";
            this.olvColDataSource_Action.CellPadding = null;
            this.olvColDataSource_Action.Text = "Data Source";
            this.olvColDataSource_Action.Width = 115;
            // 
            // olvColCustomSelector_Action
            // 
            this.olvColCustomSelector_Action.AspectName = "CustomSelector";
            this.olvColCustomSelector_Action.CellPadding = null;
            this.olvColCustomSelector_Action.Text = "Custom Selector";
            this.olvColCustomSelector_Action.Width = 125;
            // 
            // olvColValidators_Action
            // 
            this.olvColValidators_Action.AspectName = "ValidatorNames";
            this.olvColValidators_Action.CellPadding = null;
            this.olvColValidators_Action.Text = "Validator";
            this.olvColValidators_Action.Width = 111;
            // 
            // olvColDispName_Action
            // 
            this.olvColDispName_Action.AspectName = "DispName";
            this.olvColDispName_Action.CellPadding = null;
            this.olvColDispName_Action.MinimumWidth = 150;
            this.olvColDispName_Action.Text = "Display Name";
            this.olvColDispName_Action.Width = 150;
            // 
            // olvColID_Action
            // 
            this.olvColID_Action.AspectName = "ID";
            this.olvColID_Action.CellPadding = null;
            this.olvColID_Action.IsVisible = false;
            this.olvColID_Action.Text = "ID";
            // 
            // olvColParentID_Action
            // 
            this.olvColParentID_Action.AspectName = "ParentID";
            this.olvColParentID_Action.CellPadding = null;
            this.olvColParentID_Action.IsVisible = false;
            this.olvColParentID_Action.Text = "Parent ID";
            // 
            // ctxMenuViewModel
            // 
            this.ctxMenuViewModel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddViewModelMember,
            this.tsmiDeleteViewModelMember});
            this.ctxMenuViewModel.Name = "ctxMenuViewModel";
            this.ctxMenuViewModel.Size = new System.Drawing.Size(128, 48);
            // 
            // tsmiAddViewModelMember
            // 
            this.tsmiAddViewModelMember.Name = "tsmiAddViewModelMember";
            this.tsmiAddViewModelMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiAddViewModelMember.Text = "Add Child";
            // 
            // tsmiDeleteViewModelMember
            // 
            this.tsmiDeleteViewModelMember.Name = "tsmiDeleteViewModelMember";
            this.tsmiDeleteViewModelMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiDeleteViewModelMember.Text = "Delete";
            // 
            // tlpWidgetModelLayout
            // 
            this.tlpWidgetModelLayout.ColumnCount = 4;
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.Controls.Add(this.tlvWidgetModel, 0, 0);
            this.tlpWidgetModelLayout.Location = new System.Drawing.Point(9, 209);
            this.tlpWidgetModelLayout.Name = "tlpWidgetModelLayout";
            this.tlpWidgetModelLayout.RowCount = 3;
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWidgetModelLayout.Size = new System.Drawing.Size(738, 142);
            this.tlpWidgetModelLayout.TabIndex = 1;
            // 
            // tlvWidgetModel
            // 
            this.tlvWidgetModel.AllColumns.Add(this.olvColName_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColType_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColInitValue_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColValidators_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColFormatter_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColDispName_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColID_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColParentID_W);
            this.tlvWidgetModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvWidgetModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvWidgetModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName_W,
            this.olvColType_W,
            this.olvColInitValue_W,
            this.olvColValidators_W,
            this.olvColFormatter_W,
            this.olvColDispName_W});
            this.tlpWidgetModelLayout.SetColumnSpan(this.tlvWidgetModel, 4);
            this.tlvWidgetModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvWidgetModel.DataSource = null;
            this.tlvWidgetModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvWidgetModel.FullRowSelect = true;
            this.tlvWidgetModel.GridLines = true;
            this.tlvWidgetModel.KeyAspectName = "ID";
            this.tlvWidgetModel.Location = new System.Drawing.Point(3, 3);
            this.tlvWidgetModel.Name = "tlvWidgetModel";
            this.tlvWidgetModel.OwnerDraw = true;
            this.tlvWidgetModel.ParentKeyAspectName = "ParentID";
            this.tlvWidgetModel.RootKeyValueString = "";
            this.tlpWidgetModelLayout.SetRowSpan(this.tlvWidgetModel, 3);
            this.tlvWidgetModel.SelectAllOnControlA = false;
            this.tlvWidgetModel.ShowGroups = false;
            this.tlvWidgetModel.ShowImagesOnSubItems = true;
            this.tlvWidgetModel.Size = new System.Drawing.Size(732, 136);
            this.tlvWidgetModel.TabIndex = 1;
            this.tlvWidgetModel.UseAlternatingBackColors = true;
            this.tlvWidgetModel.UseCompatibleStateImageBehavior = false;
            this.tlvWidgetModel.UseHotItem = true;
            this.tlvWidgetModel.UseSubItemCheckBoxes = true;
            this.tlvWidgetModel.UseTranslucentHotItem = true;
            this.tlvWidgetModel.View = System.Windows.Forms.View.Details;
            this.tlvWidgetModel.VirtualMode = true;
            // 
            // olvColName_W
            // 
            this.olvColName_W.AspectName = "Name";
            this.olvColName_W.CellPadding = null;
            this.olvColName_W.Text = "Name";
            this.olvColName_W.Width = 150;
            // 
            // olvColType_W
            // 
            this.olvColType_W.AspectName = "TypeName";
            this.olvColType_W.CellPadding = null;
            this.olvColType_W.MinimumWidth = 150;
            this.olvColType_W.Text = "Type";
            this.olvColType_W.Width = 150;
            // 
            // olvColInitValue_W
            // 
            this.olvColInitValue_W.AspectName = "InitValue";
            this.olvColInitValue_W.CellPadding = null;
            this.olvColInitValue_W.Text = "Init Value";
            this.olvColInitValue_W.Width = 82;
            // 
            // olvColValidators_W
            // 
            this.olvColValidators_W.AspectName = "ValidatorNames";
            this.olvColValidators_W.CellPadding = null;
            this.olvColValidators_W.Text = "Validator";
            this.olvColValidators_W.Width = 110;
            // 
            // olvColFormatter_W
            // 
            this.olvColFormatter_W.AspectName = "FormatterNames";
            this.olvColFormatter_W.CellPadding = null;
            this.olvColFormatter_W.Text = "Formatter";
            this.olvColFormatter_W.Width = 109;
            // 
            // olvColDispName_W
            // 
            this.olvColDispName_W.AspectName = "DispName";
            this.olvColDispName_W.CellPadding = null;
            this.olvColDispName_W.Text = "Display Name";
            this.olvColDispName_W.Width = 93;
            // 
            // olvColID_W
            // 
            this.olvColID_W.AspectName = "ID";
            this.olvColID_W.CellPadding = null;
            this.olvColID_W.IsVisible = false;
            this.olvColID_W.Text = "ID";
            // 
            // olvColParentID_W
            // 
            this.olvColParentID_W.AspectName = "ParentID";
            this.olvColParentID_W.CellPadding = null;
            this.olvColParentID_W.IsVisible = false;
            this.olvColParentID_W.Text = "Parent ID";
            // 
            // ctxMenuActionData
            // 
            this.ctxMenuActionData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddActionDataMember,
            this.tsmiDeleteActionDataMember});
            this.ctxMenuActionData.Name = "ctxMenuActionModel";
            this.ctxMenuActionData.Size = new System.Drawing.Size(128, 48);
            // 
            // tsmiAddActionDataMember
            // 
            this.tsmiAddActionDataMember.Name = "tsmiAddActionDataMember";
            this.tsmiAddActionDataMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiAddActionDataMember.Text = "Add Child";
            // 
            // tsmiDeleteActionDataMember
            // 
            this.tsmiDeleteActionDataMember.Name = "tsmiDeleteActionDataMember";
            this.tsmiDeleteActionDataMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiDeleteActionDataMember.Text = "Delete";
            // 
            // ctxMenuWidgetValue
            // 
            this.ctxMenuWidgetValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddWidgetValueMember,
            this.tsmiDeleteWidgetValueMember});
            this.ctxMenuWidgetValue.Name = "ctxMenuWidgetValue";
            this.ctxMenuWidgetValue.Size = new System.Drawing.Size(128, 48);
            // 
            // tsmiAddWidgetValueMember
            // 
            this.tsmiAddWidgetValueMember.Name = "tsmiAddWidgetValueMember";
            this.tsmiAddWidgetValueMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiAddWidgetValueMember.Text = "Add Child";
            // 
            // tsmiDeleteWidgetValueMember
            // 
            this.tsmiDeleteWidgetValueMember.Name = "tsmiDeleteWidgetValueMember";
            this.tsmiDeleteWidgetValueMember.Size = new System.Drawing.Size(127, 22);
            this.tsmiDeleteWidgetValueMember.Text = "Delete";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Class_489.png");
            this.imageList1.Images.SetKeyName(1, "Class-Friend_491.png");
            this.imageList1.Images.SetKeyName(2, "Class-Private_493.png");
            this.imageList1.Images.SetKeyName(3, "Class-Protected_492.png");
            this.imageList1.Images.SetKeyName(4, "Class-Sealed_490.png");
            // 
            // olvParentID
            // 
            this.olvParentID.AspectName = "ParentID";
            this.olvParentID.CellPadding = null;
            this.olvParentID.IsVisible = false;
            this.olvParentID.Text = "Parent ID";
            // 
            // olvID
            // 
            this.olvID.AspectName = "ID";
            this.olvID.CellPadding = null;
            this.olvID.IsVisible = false;
            this.olvID.Text = "ID";
            // 
            // autocompleteMenu_Type
            // 
            this.autocompleteMenu_Type.AllowsTabKey = true;
            this.autocompleteMenu_Type.AppearInterval = 100;
            this.autocompleteMenu_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu_Type.ImageList = this.imageList1;
            this.autocompleteMenu_Type.Items = new string[0];
            this.autocompleteMenu_Type.MinFragmentLength = 1;
            this.autocompleteMenu_Type.TargetControlWrapper = null;
            // 
            // m_typeEditor
            // 
            this.autocompleteMenu_Type.SetAutocompleteMenu(this.m_typeEditor, this.autocompleteMenu_Type);
            this.m_typeEditor.Location = new System.Drawing.Point(75, 534);
            this.m_typeEditor.Name = "m_typeEditor";
            this.m_typeEditor.Size = new System.Drawing.Size(100, 20);
            this.m_typeEditor.TabIndex = 3;
            // 
            // olvColDispName_View
            // 
            this.olvColDispName_View.AspectName = "DispName";
            this.olvColDispName_View.CellPadding = null;
            this.olvColDispName_View.Text = "Display Name";
            this.olvColDispName_View.Width = 127;
            // 
            // olvColID_View
            // 
            this.olvColID_View.AspectName = "ID";
            this.olvColID_View.CellPadding = null;
            this.olvColID_View.IsVisible = false;
            this.olvColID_View.Text = "ID";
            // 
            // olvColParentID
            // 
            this.olvColParentID.AspectName = "ParentID";
            this.olvColParentID.CellPadding = null;
            this.olvColParentID.IsVisible = false;
            this.olvColParentID.Text = "Parent ID";
            // 
            // ModelToolWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 576);
            this.Controls.Add(this.m_typeEditor);
            this.Controls.Add(this.tlpActionModelLayout);
            this.Controls.Add(this.tlpWidgetModelLayout);
            this.Controls.Add(this.tlpViewModelLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModelToolWindowForm";
            this.Text = "ModelToolWindowForm";
            this.tlpViewModelLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).EndInit();
            this.tlpActionModelLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvActionModel)).EndInit();
            this.ctxMenuViewModel.ResumeLayout(false);
            this.tlpWidgetModelLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlvWidgetModel)).EndInit();
            this.ctxMenuActionData.ResumeLayout(false);
            this.ctxMenuWidgetValue.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private BrightIdeasSoftware.OLVColumn olvColName_Action;
        private BrightIdeasSoftware.OLVColumn olvColType_Action;
        private BrightIdeasSoftware.OLVColumn olvColValidators_Action;
        private BrightIdeasSoftware.OLVColumn olvColDispName_Action;
        private BrightIdeasSoftware.OLVColumn olvParentID;
        private BrightIdeasSoftware.OLVColumn olvID;

        private BrightIdeasSoftware.OLVColumn olvColName_View;
        private BrightIdeasSoftware.OLVColumn olvColType_View;
        private BrightIdeasSoftware.OLVColumn olvColValidators_View;
        private BrightIdeasSoftware.OLVColumn olvColIsJSModel_View;

        private BrightIdeasSoftware.OLVColumn olvColName_W;
        private BrightIdeasSoftware.OLVColumn olvColType_W;
        private BrightIdeasSoftware.OLVColumn olvColInitValue_W;
        private BrightIdeasSoftware.OLVColumn olvColValidators_W;
        private BrightIdeasSoftware.OLVColumn olvColFormatter_W;
        private BrightIdeasSoftware.OLVColumn olvColDispName_W;
        private BrightIdeasSoftware.OLVColumn olvColID_W;
        private BrightIdeasSoftware.OLVColumn olvColParentID_W;

        private System.Windows.Forms.TableLayoutPanel tlpViewModelLayout;
        private System.Windows.Forms.TableLayoutPanel tlpWidgetModelLayout;
        private System.Windows.Forms.TableLayoutPanel tlpActionModelLayout;

        private BrightIdeasSoftware.DataTreeListView tlvViewModel;
        private BrightIdeasSoftware.DataTreeListView tlvWidgetModel;
        private BrightIdeasSoftware.DataTreeListView tlvActionModel;

        private System.Windows.Forms.ContextMenuStrip ctxMenuViewModel;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddViewModelMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteViewModelMember;
        private System.Windows.Forms.ToolTip toolTipMsg;
        private System.Windows.Forms.ContextMenuStrip ctxMenuActionData;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddActionDataMember;
        private System.Windows.Forms.ContextMenuStrip ctxMenuWidgetValue;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddWidgetValueMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteWidgetValueMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteActionDataMember;
        private System.Windows.Forms.ImageList imageList1;
        private AutoCompleteTextBox m_typeEditor;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu_Type;
        private BrightIdeasSoftware.OLVColumn olvColDataSource_Action;
        private BrightIdeasSoftware.OLVColumn olvColCustomSelector_Action;
        private BrightIdeasSoftware.OLVColumn olvColID_Action;
        private BrightIdeasSoftware.OLVColumn olvColParentID_Action;
        private BrightIdeasSoftware.OLVColumn olvColDispName_View;
        private BrightIdeasSoftware.OLVColumn olvColID_View;
        private BrightIdeasSoftware.OLVColumn olvColParentID;
        //private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu_Type;
    }
}