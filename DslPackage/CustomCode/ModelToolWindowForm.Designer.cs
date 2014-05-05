﻿namespace MVCVisualDesigner
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
            this.ctrlViewModelType = new MVCVisualDesigner.ModelTypeListControl();
            this.tlpWidgetModelLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ctrlWidgetModelType = new MVCVisualDesigner.ModelTypeListControl();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWidgetModelName = new System.Windows.Forms.TextBox();
            this.txtWidgetModelValue = new System.Windows.Forms.TextBox();
            this.cmbWidgetModelBindTo = new System.Windows.Forms.ComboBox();
            this.cmbWidgetModelValidator = new System.Windows.Forms.ComboBox();
            this.cmbWidgetModelFormatter = new System.Windows.Forms.ComboBox();
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
            this.tlpViewModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvViewModel)).BeginInit();
            this.tlpWidgetModelLayout.SuspendLayout();
            this.tlpActionModelLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvActionModel)).BeginInit();
            this.ctxMenuViewModel.SuspendLayout();
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
            // tlpWidgetModelLayout
            // 
            this.tlpWidgetModelLayout.ColumnCount = 4;
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpWidgetModelLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpWidgetModelLayout.Controls.Add(this.label11, 0, 3);
            this.tlpWidgetModelLayout.Controls.Add(this.label6, 0, 2);
            this.tlpWidgetModelLayout.Controls.Add(this.label7, 2, 0);
            this.tlpWidgetModelLayout.Controls.Add(this.label5, 2, 1);
            this.tlpWidgetModelLayout.Controls.Add(this.label4, 0, 1);
            this.tlpWidgetModelLayout.Controls.Add(this.ctrlWidgetModelType, 1, 1);
            this.tlpWidgetModelLayout.Controls.Add(this.label3, 0, 0);
            this.tlpWidgetModelLayout.Controls.Add(this.txtWidgetModelName, 1, 0);
            this.tlpWidgetModelLayout.Controls.Add(this.txtWidgetModelValue, 3, 1);
            this.tlpWidgetModelLayout.Controls.Add(this.cmbWidgetModelBindTo, 3, 0);
            this.tlpWidgetModelLayout.Controls.Add(this.cmbWidgetModelValidator, 1, 2);
            this.tlpWidgetModelLayout.Controls.Add(this.cmbWidgetModelFormatter, 1, 3);
            this.tlpWidgetModelLayout.Location = new System.Drawing.Point(9, 197);
            this.tlpWidgetModelLayout.Name = "tlpWidgetModelLayout";
            this.tlpWidgetModelLayout.RowCount = 5;
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpWidgetModelLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWidgetModelLayout.Size = new System.Drawing.Size(738, 116);
            this.tlpWidgetModelLayout.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 26);
            this.label11.TabIndex = 18;
            this.label11.Text = "Formatter";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 26);
            this.label6.TabIndex = 14;
            this.label6.Text = "Validator";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(392, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 26);
            this.label7.TabIndex = 11;
            this.label7.Text = "Bind To";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(392, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "Value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Model Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctrlWidgetModelType
            // 
            this.ctrlWidgetModelType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlWidgetModelType.Location = new System.Drawing.Point(112, 29);
            this.ctrlWidgetModelType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.ctrlWidgetModelType.Name = "ctrlWidgetModelType";
            this.ctrlWidgetModelType.Size = new System.Drawing.Size(275, 21);
            this.ctrlWidgetModelType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Widget Model Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWidgetModelName
            // 
            this.txtWidgetModelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWidgetModelName.Location = new System.Drawing.Point(113, 3);
            this.txtWidgetModelName.Name = "txtWidgetModelName";
            this.txtWidgetModelName.Size = new System.Drawing.Size(273, 20);
            this.txtWidgetModelName.TabIndex = 8;
            // 
            // txtWidgetModelValue
            // 
            this.txtWidgetModelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWidgetModelValue.Location = new System.Drawing.Point(462, 29);
            this.txtWidgetModelValue.Name = "txtWidgetModelValue";
            this.txtWidgetModelValue.Size = new System.Drawing.Size(273, 20);
            this.txtWidgetModelValue.TabIndex = 12;
            // 
            // cmbWidgetModelBindTo
            // 
            this.cmbWidgetModelBindTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWidgetModelBindTo.FormattingEnabled = true;
            this.cmbWidgetModelBindTo.Location = new System.Drawing.Point(462, 3);
            this.cmbWidgetModelBindTo.Name = "cmbWidgetModelBindTo";
            this.cmbWidgetModelBindTo.Size = new System.Drawing.Size(273, 21);
            this.cmbWidgetModelBindTo.TabIndex = 13;
            // 
            // cmbWidgetModelValidator
            // 
            this.cmbWidgetModelValidator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWidgetModelValidator.FormattingEnabled = true;
            this.cmbWidgetModelValidator.Location = new System.Drawing.Point(113, 55);
            this.cmbWidgetModelValidator.Name = "cmbWidgetModelValidator";
            this.cmbWidgetModelValidator.Size = new System.Drawing.Size(273, 21);
            this.cmbWidgetModelValidator.TabIndex = 19;
            // 
            // cmbWidgetModelFormatter
            // 
            this.cmbWidgetModelFormatter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWidgetModelFormatter.FormattingEnabled = true;
            this.cmbWidgetModelFormatter.Location = new System.Drawing.Point(113, 81);
            this.cmbWidgetModelFormatter.Name = "cmbWidgetModelFormatter";
            this.cmbWidgetModelFormatter.Size = new System.Drawing.Size(273, 21);
            this.cmbWidgetModelFormatter.TabIndex = 20;
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
            this.tlpActionModelLayout.Location = new System.Drawing.Point(12, 328);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 26);
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
            this.tlpWidgetModelLayout.ResumeLayout(false);
            this.tlpWidgetModelLayout.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip ctxMenuViewModel;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddViewModelMember;
        private System.Windows.Forms.Label label2;
        private BrightIdeasSoftware.OLVColumn olvIsJSModel;
        private BrightIdeasSoftware.OLVColumn olvChildMemberInstances;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteViewModelMember;
        private ModelTypeListControl ctrlViewModelType;
        private BrightIdeasSoftware.DataTreeListView tlvActionModel;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private ModelTypeListControl ctrlWidgetModelType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWidgetModelName;
        private System.Windows.Forms.TextBox txtWidgetModelValue;
        private System.Windows.Forms.ComboBox cmbWidgetModelBindTo;
        private System.Windows.Forms.ComboBox cmbWidgetModelValidator;
        private System.Windows.Forms.ComboBox cmbWidgetModelFormatter;

    }
}