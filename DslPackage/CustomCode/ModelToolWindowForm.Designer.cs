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
            this.olvChildMemberInstances = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label2 = new System.Windows.Forms.Label();
            this.cmbViewModelType = new System.Windows.Forms.ComboBox();
            this.tlpWidgetModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlpActionModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlvActionModel = new BrightIdeasSoftware.DataTreeListView();
            this.label1 = new System.Windows.Forms.Label();
            this.ctxMenuViewModel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteViewModelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpViewModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).BeginInit();
            this.tlpActionModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvActionModel)).BeginInit();
            this.ctxMenuViewModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpViewModelLayout
            // 
            this.tlpViewModelLayout.ColumnCount = 4;
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpViewModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpViewModelLayout.Controls.Add(this.tlvViewModel, 0, 1);
            this.tlpViewModelLayout.Controls.Add(this.label2, 0, 0);
            this.tlpViewModelLayout.Controls.Add(this.cmbViewModelType, 1, 0);
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
            this.tlvViewModel.AllColumns.Add(this.olvChildMemberInstances);
            this.tlvViewModel.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.tlvViewModel.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.tlvViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColName,
            this.olvColType,
            this.olvColDefault,
            this.olvColValidators,
            this.olvIsJSModel});
            this.tlpViewModelLayout.SetColumnSpan(this.tlvViewModel, 4);
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
            this.olvColType.Text = "Type";
            this.olvColType.Width = 100;
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
            // olvChildMemberInstances
            // 
            this.olvChildMemberInstances.AspectName = "ChildMemberInstances";
            this.olvChildMemberInstances.CellPadding = null;
            this.olvChildMemberInstances.IsVisible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Model Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbViewModelType
            // 
            this.cmbViewModelType.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbViewModelType.FormattingEnabled = true;
            this.cmbViewModelType.Items.AddRange(new object[] {
            "aa",
            "bb",
            "(no)"});
            this.cmbViewModelType.Location = new System.Drawing.Point(75, 3);
            this.cmbViewModelType.Name = "cmbViewModelType";
            this.cmbViewModelType.Size = new System.Drawing.Size(441, 21);
            this.cmbViewModelType.TabIndex = 2;
            this.cmbViewModelType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbViewModelType_KeyPress);
            // 
            // tlpWidgetModelLayout
            // 
            this.tlpWidgetModelLayout.ColumnCount = 4;
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpWidgetModelLayout.Location = new System.Drawing.Point(9, 197);
            this.tlpWidgetModelLayout.Name = "tlpWidgetModelLayout";
            this.tlpWidgetModelLayout.RowCount = 3;
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWidgetModelLayout.Size = new System.Drawing.Size(738, 153);
            this.tlpWidgetModelLayout.TabIndex = 1;
            // 
            // tlpActionModelLayout
            // 
            this.tlpActionModelLayout.ColumnCount = 4;
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpActionModelLayout.Controls.Add(this.tlvActionModel, 0, 1);
            this.tlpActionModelLayout.Controls.Add(this.label1, 0, 0);
            this.tlpActionModelLayout.Location = new System.Drawing.Point(12, 385);
            this.tlpActionModelLayout.Name = "tlpActionModelLayout";
            this.tlpActionModelLayout.RowCount = 3;
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpActionModelLayout.Size = new System.Drawing.Size(735, 139);
            this.tlpActionModelLayout.TabIndex = 2;
            // 
            // tlvActionModel
            // 
            this.tlpActionModelLayout.SetColumnSpan(this.tlvActionModel, 4);
            this.tlvActionModel.DataSource = null;
            this.tlvActionModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlvActionModel.FullRowSelect = true;
            this.tlvActionModel.GridLines = true;
            this.tlvActionModel.Location = new System.Drawing.Point(3, 23);
            this.tlvActionModel.Name = "tlvActionModel";
            this.tlvActionModel.OwnerDraw = true;
            this.tlvActionModel.RootKeyValueString = "";
            this.tlvActionModel.ShowGroups = false;
            this.tlvActionModel.Size = new System.Drawing.Size(729, 93);
            this.tlvActionModel.TabIndex = 0;
            this.tlvActionModel.UseCompatibleStateImageBehavior = false;
            this.tlvActionModel.View = System.Windows.Forms.View.Details;
            this.tlvActionModel.VirtualMode = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.OLVColumn olvColName;
        private BrightIdeasSoftware.OLVColumn olvColType;
        private BrightIdeasSoftware.OLVColumn olvColDefault;
        private BrightIdeasSoftware.OLVColumn olvColValidators;
        private System.Windows.Forms.TableLayoutPanel tlpViewModelLayout;
        private System.Windows.Forms.TableLayoutPanel tlpWidgetModelLayout;
        private System.Windows.Forms.TableLayoutPanel tlpActionModelLayout;
        private BrightIdeasSoftware.DataTreeListView tlvViewModel;
        private BrightIdeasSoftware.DataTreeListView tlvActionModel;
        private System.Windows.Forms.ContextMenuStrip ctxMenuViewModel;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddViewModelMember;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbViewModelType;
        private BrightIdeasSoftware.OLVColumn olvIsJSModel;
        private BrightIdeasSoftware.OLVColumn olvChildMemberInstances;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteViewModelMember;

    }
}