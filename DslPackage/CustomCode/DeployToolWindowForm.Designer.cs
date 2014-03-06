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
            this.tabGeneratorSettings = new System.Windows.Forms.TabControl();
            this.tpAll = new System.Windows.Forms.TabPage();
            this.lvGeneratorList = new System.Windows.Forms.ListView();
            this.colEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDLL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReload = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabGeneratorSettings.SuspendLayout();
            this.tpAll.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.tabGeneratorSettings, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReload, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnGenerate, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(779, 262);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabGeneratorSettings
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabGeneratorSettings, 4);
            this.tabGeneratorSettings.Controls.Add(this.tpAll);
            this.tabGeneratorSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGeneratorSettings.Location = new System.Drawing.Point(3, 3);
            this.tabGeneratorSettings.Name = "tabGeneratorSettings";
            this.tabGeneratorSettings.SelectedIndex = 0;
            this.tabGeneratorSettings.Size = new System.Drawing.Size(773, 226);
            this.tabGeneratorSettings.TabIndex = 4;
            // 
            // tpAll
            // 
            this.tpAll.Controls.Add(this.lvGeneratorList);
            this.tpAll.Location = new System.Drawing.Point(4, 22);
            this.tpAll.Name = "tpAll";
            this.tpAll.Padding = new System.Windows.Forms.Padding(3);
            this.tpAll.Size = new System.Drawing.Size(765, 200);
            this.tpAll.TabIndex = 0;
            this.tpAll.Text = "All Generators";
            this.tpAll.UseVisualStyleBackColor = true;
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
            this.lvGeneratorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGeneratorList.Location = new System.Drawing.Point(3, 3);
            this.lvGeneratorList.Name = "lvGeneratorList";
            this.lvGeneratorList.Size = new System.Drawing.Size(759, 194);
            this.lvGeneratorList.TabIndex = 2;
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
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(3, 235);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 5;
            this.btnReload.Text = "&Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(692, 235);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(84, 24);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // DeployToolWindowForm
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeployToolWindowForm";
            this.Text = "Deployment";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabGeneratorSettings.ResumeLayout(false);
            this.tpAll.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabGeneratorSettings;
        private System.Windows.Forms.TabPage tpAll;
        private System.Windows.Forms.ListView lvGeneratorList;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDLL;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colDesc;
        private System.Windows.Forms.ColumnHeader colEnabled;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnGenerate;
    }
}