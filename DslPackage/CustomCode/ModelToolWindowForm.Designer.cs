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
            this.olvColName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDefault = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvIsJSModel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label2 = new System.Windows.Forms.Label();
            this.tlpActionModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvActionModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label1 = new System.Windows.Forms.Label();
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
            this.olvParentID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ctxMenuWidgetValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddWidgetValueMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteWidgetValueMember = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTypeEditor = new System.Windows.Forms.TextBox();
            this.autocompleteMenu_Type = new AutocompleteMenuNS.AutocompleteMenu();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.tlpViewModelLayout.Controls.Add(this.tlvViewModel, 0, 1);
            this.tlpViewModelLayout.Controls.Add(this.label2, 0, 0);
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
            this.tlvViewModel.AllColumns.Add(this.olvColName);
            this.tlvViewModel.AllColumns.Add(this.olvColType);
            this.tlvViewModel.AllColumns.Add(this.olvColDefault);
            this.tlvViewModel.AllColumns.Add(this.olvColValidators);
            this.tlvViewModel.AllColumns.Add(this.olvIsJSModel);
            this.tlvViewModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvViewModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName,
            this.olvColType,
            this.olvColDefault,
            this.olvColValidators,
            this.olvIsJSModel});
            this.tlpViewModelLayout.SetColumnSpan(this.tlvViewModel, 4);
            this.tlvViewModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvViewModel.DataSource = null;
            this.tlvViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvViewModel.FullRowSelect = true;
            this.tlvViewModel.GridLines = true;
            this.tlvViewModel.KeyAspectName = "ID";
            this.tlvViewModel.Location = new System.Drawing.Point(3, 29);
            this.tlvViewModel.Name = "tlvViewModel";
            this.tlvViewModel.OwnerDraw = true;
            this.tlvViewModel.ParentKeyAspectName = "ParentID";
            this.tlvViewModel.RootKeyValueString = "";
            this.tlvViewModel.SelectAllOnControlA = false;
            this.tlvViewModel.ShowGroups = false;
            this.tlvViewModel.ShowImagesOnSubItems = true;
            this.tlvViewModel.Size = new System.Drawing.Size(729, 126);
            this.tlvViewModel.TabIndex = 0;
            this.tlvViewModel.UseAlternatingBackColors = true;
            this.tlvViewModel.UseCompatibleStateImageBehavior = false;
            this.tlvViewModel.UseHotItem = true;
            this.tlvViewModel.UseSubItemCheckBoxes = true;
            this.tlvViewModel.UseTranslucentHotItem = true;
            this.tlvViewModel.View = System.Windows.Forms.View.Details;
            this.tlvViewModel.VirtualMode = true;
            // 
            // olvColName
            // 
            this.olvColName.AspectName = "Name";
            this.olvColName.CellPadding = null;
            this.olvColName.Text = "Name";
            this.olvColName.Width = 150;
            // 
            // olvColType
            // 
            this.olvColType.AspectName = "TypeName";
            this.olvColType.CellPadding = null;
            this.olvColType.MinimumWidth = 150;
            this.olvColType.Text = "Type";
            this.olvColType.Width = 150;
            // 
            // olvColDefault
            // 
            this.olvColDefault.AspectName = "DefaultValue";
            this.olvColDefault.CellPadding = null;
            this.olvColDefault.Text = "Default";
            // 
            // olvColValidators
            // 
            this.olvColValidators.AspectName = "ValidatorNames";
            this.olvColValidators.CellPadding = null;
            this.olvColValidators.Text = "Validators";
            // 
            // olvIsJSModel
            // 
            this.olvIsJSModel.AspectName = "IsJavaScriptModel_OLV";
            this.olvIsJSModel.CellPadding = null;
            this.olvIsJSModel.CheckBoxes = true;
            this.olvIsJSModel.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvIsJSModel.Text = "JS Model";
            this.olvIsJSModel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "View Model Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpActionModelLayout
            // 
            this.tlpActionModelLayout.ColumnCount = 4;
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.Controls.Add(this.tlvActionModel, 0, 1);
            this.tlpActionModelLayout.Controls.Add(this.label1, 0, 0);
            this.tlpActionModelLayout.Location = new System.Drawing.Point(12, 361);
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
            this.tlvActionModel.AllColumns.Add(this.olvColumn1);
            this.tlvActionModel.AllColumns.Add(this.olvColumn2);
            this.tlvActionModel.AllColumns.Add(this.olvColumn3);
            this.tlvActionModel.AllColumns.Add(this.olvColumn4);
            this.tlvActionModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvActionModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvActionModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.tlpActionModelLayout.SetColumnSpan(this.tlvActionModel, 4);
            this.tlvActionModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvActionModel.DataSource = null;
            this.tlvActionModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvActionModel.FullRowSelect = true;
            this.tlvActionModel.GridLines = true;
            this.tlvActionModel.KeyAspectName = "ID";
            this.tlvActionModel.Location = new System.Drawing.Point(3, 29);
            this.tlvActionModel.Name = "tlvActionModel";
            this.tlvActionModel.OwnerDraw = true;
            this.tlvActionModel.ParentKeyAspectName = "ParentID";
            this.tlvActionModel.RootKeyValueString = "";
            this.tlvActionModel.SelectAllOnControlA = false;
            this.tlvActionModel.ShowGroups = false;
            this.tlvActionModel.ShowImagesOnSubItems = true;
            this.tlvActionModel.Size = new System.Drawing.Size(729, 87);
            this.tlvActionModel.TabIndex = 2;
            this.tlvActionModel.UseAlternatingBackColors = true;
            this.tlvActionModel.UseCompatibleStateImageBehavior = false;
            this.tlvActionModel.UseHotItem = true;
            this.tlvActionModel.UseSubItemCheckBoxes = true;
            this.tlvActionModel.UseTranslucentHotItem = true;
            this.tlvActionModel.View = System.Windows.Forms.View.Details;
            this.tlvActionModel.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 150;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "TypeName";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.MinimumWidth = 150;
            this.olvColumn2.Text = "Type";
            this.olvColumn2.Width = 150;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "ValidatorNames";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Text = "Validators";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Selector";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.MinimumWidth = 150;
            this.olvColumn4.Text = "Selector";
            this.olvColumn4.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Event Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctxMenuViewModel
            // 
            this.ctxMenuViewModel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddViewModelMember,
            this.tsmiDeleteViewModelMember});
            this.ctxMenuViewModel.Name = "ctxMenuViewModel";
            this.ctxMenuViewModel.Size = new System.Drawing.Size(134, 48);
            // 
            // tsmiAddViewModelMember
            // 
            this.tsmiAddViewModelMember.Name = "tsmiAddViewModelMember";
            this.tsmiAddViewModelMember.Size = new System.Drawing.Size(133, 22);
            this.tsmiAddViewModelMember.Text = "Add Child";
            // 
            // tsmiDeleteViewModelMember
            // 
            this.tsmiDeleteViewModelMember.Name = "tsmiDeleteViewModelMember";
            this.tsmiDeleteViewModelMember.Size = new System.Drawing.Size(133, 22);
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
            this.tlpWidgetModelLayout.Location = new System.Drawing.Point(9, 197);
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
            this.ctxMenuActionData.Size = new System.Drawing.Size(134, 48);
            // 
            // tsmiAddActionDataMember
            // 
            this.tsmiAddActionDataMember.Name = "tsmiAddActionDataMember";
            this.tsmiAddActionDataMember.Size = new System.Drawing.Size(133, 22);
            this.tsmiAddActionDataMember.Text = "Add Child";
            // 
            // tsmiDeleteActionDataMember
            // 
            this.tsmiDeleteActionDataMember.Name = "tsmiDeleteActionDataMember";
            this.tsmiDeleteActionDataMember.Size = new System.Drawing.Size(133, 22);
            this.tsmiDeleteActionDataMember.Text = "Delete";
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
            // ctxMenuWidgetValue
            // 
            this.ctxMenuWidgetValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddWidgetValueMember,
            this.tsmiDeleteWidgetValueMember});
            this.ctxMenuWidgetValue.Name = "ctxMenuWidgetValue";
            this.ctxMenuWidgetValue.Size = new System.Drawing.Size(134, 48);
            // 
            // tsmiAddWidgetValueMember
            // 
            this.tsmiAddWidgetValueMember.Name = "tsmiAddWidgetValueMember";
            this.tsmiAddWidgetValueMember.Size = new System.Drawing.Size(133, 22);
            this.tsmiAddWidgetValueMember.Text = "Add Child";
            // 
            // tsmiDeleteWidgetValueMember
            // 
            this.tsmiDeleteWidgetValueMember.Name = "tsmiDeleteWidgetValueMember";
            this.tsmiDeleteWidgetValueMember.Size = new System.Drawing.Size(133, 22);
            this.tsmiDeleteWidgetValueMember.Text = "Delete";
            // 
            // txtTypeEditor
            // 
            this.txtTypeEditor.AutoCompleteCustomSource.AddRange(new string[] {
            "aa",
            "bb",
            "cc",
            "dd",
            "ee",
            "ff",
            "gg"});
            this.autocompleteMenu_Type.SetAutocompleteMenu(this.txtTypeEditor, this.autocompleteMenu_Type);
            this.txtTypeEditor.Location = new System.Drawing.Point(38, 506);
            this.txtTypeEditor.Name = "txtTypeEditor";
            this.txtTypeEditor.Size = new System.Drawing.Size(100, 20);
            this.txtTypeEditor.TabIndex = 3;
            // 
            // autocompleteMenu_Type
            // 
            this.autocompleteMenu_Type.AllowsTabKey = true;
            this.autocompleteMenu_Type.AppearInterval = 100;
            this.autocompleteMenu_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu_Type.ImageList = this.imageList1;
            this.autocompleteMenu_Type.Items = new string[0];
            this.autocompleteMenu_Type.MinFragmentLength = 0;
            this.autocompleteMenu_Type.TargetControlWrapper = null;
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
            // ModelToolWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 576);
            this.Controls.Add(this.tlpActionModelLayout);
            this.Controls.Add(this.tlpWidgetModelLayout);
            this.Controls.Add(this.tlpViewModelLayout);
            this.Controls.Add(this.txtTypeEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModelToolWindowForm";
            this.Text = "ModelToolWindowForm";
            this.tlpViewModelLayout.ResumeLayout(false);
            this.tlpViewModelLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).EndInit();
            this.tlpActionModelLayout.ResumeLayout(false);
            this.tlpActionModelLayout.PerformLayout();
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
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvParentID;
        private BrightIdeasSoftware.OLVColumn olvID;
        //private BrightIdeasSoftware.OLVColumn olvColumn4;
        //private BrightIdeasSoftware.OLVColumn olvColumn5;

        private BrightIdeasSoftware.OLVColumn olvColName;
        private BrightIdeasSoftware.OLVColumn olvColType;
        private BrightIdeasSoftware.OLVColumn olvColDefault;
        private BrightIdeasSoftware.OLVColumn olvColValidators;
        private BrightIdeasSoftware.OLVColumn olvIsJSModel;

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
        //private ModelTypeListControl ctrlViewModelType;
        private System.Windows.Forms.ToolTip toolTipMsg;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip ctxMenuActionData;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddActionDataMember;
        private System.Windows.Forms.ContextMenuStrip ctxMenuWidgetValue;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddWidgetValueMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteWidgetValueMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteActionDataMember;
        private System.Windows.Forms.TextBox txtTypeEditor;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu_Type;
        private System.Windows.Forms.ImageList imageList1;
    }
}