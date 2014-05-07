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
            this.tlpViewModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvViewModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColDefault = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvIsJSModel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvChildMemberInstances_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlViewModelType = new MVCVisualDesigner.ModelTypeListControl();
            this.tlpActionModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvActionModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label1 = new System.Windows.Forms.Label();
            this.ctxMenuViewModel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpWidgetModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvWidgetModel = new BrightIdeasSoftware.DataTreeListView();
            this.olvColName_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColType_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValue_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColValidators_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColBindsTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColFormatter_W = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.chkEnableWidgetModel = new System.Windows.Forms.CheckBox();
            this.tlpViewModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).BeginInit();
            this.tlpActionModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvActionModel)).BeginInit();
            this.ctxMenuViewModel.SuspendLayout();
            this.tlpWidgetModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvWidgetModel)).BeginInit();
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
            this.tlpViewModelLayout.Controls.Add(this.ctrlViewModelType, 1, 0);
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
            this.tlvViewModel.AllColumns.Add(this.olvChildMemberInstances_W);
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
            this.tlvViewModel.KeyAspectName = "Id";
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
            this.tlvViewModel.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.tlvViewModel_CellEditFinishing);
            this.tlvViewModel.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.tlvViewModel_CellEditStarting);
            this.tlvViewModel.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.tlvViewModel_CellEditValidating);
            this.tlvViewModel.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.tlvViewModel_CellRightClick);
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
            // olvChildMemberInstances_W
            // 
            this.olvChildMemberInstances_W.AspectName = "ChildMemberInstances";
            this.olvChildMemberInstances_W.CellPadding = null;
            this.olvChildMemberInstances_W.IsVisible = false;
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
            // ctrlViewModelType
            // 
            this.ctrlViewModelType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlViewModelType.Location = new System.Drawing.Point(102, 3);
            this.ctrlViewModelType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.ctrlViewModelType.Name = "ctrlViewModelType";
            this.ctrlViewModelType.Size = new System.Drawing.Size(431, 21);
            this.ctrlViewModelType.TabIndex = 5;
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
            this.tlvActionModel.AllColumns.Add(this.olvColumn11);
            this.tlvActionModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvActionModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvActionModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn11});
            this.tlpActionModelLayout.SetColumnSpan(this.tlvActionModel, 4);
            this.tlvActionModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvActionModel.DataSource = null;
            this.tlvActionModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvActionModel.FullRowSelect = true;
            this.tlvActionModel.GridLines = true;
            this.tlvActionModel.KeyAspectName = "Id";
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
            this.olvColumn3.AspectName = "DefaultValue";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Text = "Default";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "ValidatorNames";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.Text = "Validators";
            // 
            // olvColumn11
            // 
            this.olvColumn11.CellPadding = null;
            this.olvColumn11.Text = "Selectors";
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
            this.ctxMenuViewModel.Size = new System.Drawing.Size(156, 48);
            this.ctxMenuViewModel.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMenuViewModel_ItemClicked);
            // 
            // tsmiAddViewModelMember
            // 
            this.tsmiAddViewModelMember.Name = "tsmiAddViewModelMember";
            this.tsmiAddViewModelMember.Size = new System.Drawing.Size(155, 22);
            this.tsmiAddViewModelMember.Text = "Add Member";
            // 
            // tsmiDeleteViewModelMember
            // 
            this.tsmiDeleteViewModelMember.Name = "tsmiDeleteViewModelMember";
            this.tsmiDeleteViewModelMember.Size = new System.Drawing.Size(155, 22);
            this.tsmiDeleteViewModelMember.Text = "Delete Member";
            // 
            // tlpWidgetModelLayout
            // 
            this.tlpWidgetModelLayout.ColumnCount = 4;
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.Controls.Add(this.tlvWidgetModel, 0, 1);
            this.tlpWidgetModelLayout.Controls.Add(this.chkEnableWidgetModel, 0, 0);
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
            this.tlvWidgetModel.AllColumns.Add(this.olvColValue_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColValidators_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvColBindsTo);
            this.tlvWidgetModel.AllColumns.Add(this.olvColFormatter_W);
            this.tlvWidgetModel.AllColumns.Add(this.olvChildMemberInstances_W);
            this.tlvWidgetModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvWidgetModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvWidgetModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName_W,
            this.olvColType_W,
            this.olvColValue_W,
            this.olvColValidators_W,
            this.olvColBindsTo,
            this.olvColFormatter_W});
            this.tlpWidgetModelLayout.SetColumnSpan(this.tlvWidgetModel, 4);
            this.tlvWidgetModel.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvWidgetModel.DataSource = null;
            this.tlvWidgetModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvWidgetModel.FullRowSelect = true;
            this.tlvWidgetModel.GridLines = true;
            this.tlvWidgetModel.KeyAspectName = "Id";
            this.tlvWidgetModel.Location = new System.Drawing.Point(3, 25);
            this.tlvWidgetModel.Name = "tlvWidgetModel";
            this.tlvWidgetModel.OwnerDraw = true;
            this.tlvWidgetModel.ParentKeyAspectName = "ParentID";
            this.tlvWidgetModel.RootKeyValueString = "";
            this.tlvWidgetModel.SelectAllOnControlA = false;
            this.tlvWidgetModel.ShowGroups = false;
            this.tlvWidgetModel.ShowImagesOnSubItems = true;
            this.tlvWidgetModel.Size = new System.Drawing.Size(732, 94);
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
            // olvColValue_W
            // 
            this.olvColValue_W.AspectName = "DefaultValue";
            this.olvColValue_W.CellPadding = null;
            this.olvColValue_W.Text = "Value";
            this.olvColValue_W.Width = 82;
            // 
            // olvColValidators_W
            // 
            this.olvColValidators_W.AspectName = "ValidatorNames";
            this.olvColValidators_W.CellPadding = null;
            this.olvColValidators_W.Text = "Validators";
            this.olvColValidators_W.Width = 110;
            // 
            // olvColBindsTo
            // 
            this.olvColBindsTo.AspectName = "BindingTargetName";
            this.olvColBindsTo.CellPadding = null;
            this.olvColBindsTo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColBindsTo.Text = "Binds To";
            this.olvColBindsTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColBindsTo.Width = 103;
            // 
            // olvColFormatter_W
            // 
            this.olvColFormatter_W.AspectName = "FormatterNames";
            this.olvColFormatter_W.CellPadding = null;
            this.olvColFormatter_W.Text = "Formatter";
            this.olvColFormatter_W.Width = 109;
            // 
            // chkEnableWidgetModel
            // 
            this.chkEnableWidgetModel.AutoSize = true;
            this.chkEnableWidgetModel.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkEnableWidgetModel.Location = new System.Drawing.Point(3, 3);
            this.chkEnableWidgetModel.Name = "chkEnableWidgetModel";
            this.chkEnableWidgetModel.Size = new System.Drawing.Size(92, 16);
            this.chkEnableWidgetModel.TabIndex = 2;
            this.chkEnableWidgetModel.Text = "Widget Model";
            this.chkEnableWidgetModel.UseVisualStyleBackColor = true;
            this.chkEnableWidgetModel.CheckedChanged += new System.EventHandler(this.chkEnableWidgetModel_CheckedChanged);
            // 
            // ModelToolWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 536);
            this.Controls.Add(this.tlpActionModelLayout);
            this.Controls.Add(this.tlpWidgetModelLayout);
            this.Controls.Add(this.tlpViewModelLayout);
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
            this.tlpWidgetModelLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvWidgetModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.OLVColumn olvColName;
        private BrightIdeasSoftware.OLVColumn olvColType;
        private BrightIdeasSoftware.OLVColumn olvColDefault;
        private BrightIdeasSoftware.OLVColumn olvColValidators;
        private System.Windows.Forms.TableLayoutPanel tlpViewModelLayout;
        private System.Windows.Forms.TableLayoutPanel tlpActionModelLayout;
        private BrightIdeasSoftware.DataTreeListView tlvViewModel;
        private System.Windows.Forms.ContextMenuStrip ctxMenuViewModel;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddViewModelMember;
        private System.Windows.Forms.Label label2;
        private BrightIdeasSoftware.OLVColumn olvIsJSModel;
        private BrightIdeasSoftware.OLVColumn olvChildMemberInstances_W;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteViewModelMember;
        private ModelTypeListControl ctrlViewModelType;
        private BrightIdeasSoftware.DataTreeListView tlvActionModel;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.TableLayoutPanel tlpWidgetModelLayout;
        private BrightIdeasSoftware.DataTreeListView tlvWidgetModel;
        private BrightIdeasSoftware.OLVColumn olvColName_W;
        private BrightIdeasSoftware.OLVColumn olvColType_W;
        private BrightIdeasSoftware.OLVColumn olvColValue_W;
        private BrightIdeasSoftware.OLVColumn olvColValidators_W;
        private BrightIdeasSoftware.OLVColumn olvColBindsTo;
        private BrightIdeasSoftware.OLVColumn olvColFormatter_W;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private System.Windows.Forms.CheckBox chkEnableWidgetModel;

    }
}