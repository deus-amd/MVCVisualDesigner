namespace MVCVisualDesigner
{
    partial class DeployToolWindowForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvGeneratorList = new System.Windows.Forms.ListView();
            this.colEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDLL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "To be implemented";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Controls.Add(this.lvGeneratorList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnGenerate, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSettings, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(787, 203);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lvGeneratorList
            // 
            this.lvGeneratorList.CheckBoxes = true;
            this.lvGeneratorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEnabled,
            this.colName,
            this.colDLL,
            this.colPath,
            this.colDesc});
            this.tableLayoutPanel1.SetColumnSpan(this.lvGeneratorList, 4);
            this.lvGeneratorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGeneratorList.Location = new System.Drawing.Point(3, 3);
            this.lvGeneratorList.Name = "lvGeneratorList";
            this.lvGeneratorList.Size = new System.Drawing.Size(781, 167);
            this.lvGeneratorList.TabIndex = 8;
            this.lvGeneratorList.UseCompatibleStateImageBehavior = false;
            this.lvGeneratorList.View = System.Windows.Forms.View.Details;
            // 
            // colEnabled
            // 
            this.colEnabled.Text = "";
            this.colEnabled.Width = 22;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 146;
            // 
            // colDLL
            // 
            this.colDLL.Text = "DLL";
            this.colDLL.Width = 121;
            // 
            // colPath
            // 
            this.colPath.Text = "Path";
            this.colPath.Width = 178;
            // 
            // colDesc
            // 
            this.colDesc.Text = "Description";
            this.colDesc.Width = 303;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerate.Location = new System.Drawing.Point(610, 176);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(84, 24);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(700, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSettings.Location = new System.Drawing.Point(3, 176);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(84, 24);
            this.btnSettings.TabIndex = 9;
            this.btnSettings.Text = "Settings...";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // DeployToolWindowForm
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(787, 203);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DeployToolWindowForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Code Generation";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvGeneratorList;
        private System.Windows.Forms.ColumnHeader colEnabled;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDLL;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colDesc;
        private System.Windows.Forms.Button btnSettings;
    }
}