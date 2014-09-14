namespace MVCVisualDesigner
{
    partial class SelectorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbAnchorWidget = new System.Windows.Forms.ComboBox();
            this.txtSelectorFunction = new System.Windows.Forms.TextBox();
            this.lblResolvedWidget = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.cmbAnchorWidget, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSelectorFunction, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblResolvedWidget, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(562, 20);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmbAnchorWidget
            // 
            this.cmbAnchorWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAnchorWidget.FormattingEnabled = true;
            this.cmbAnchorWidget.Location = new System.Drawing.Point(3, 0);
            this.cmbAnchorWidget.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cmbAnchorWidget.Name = "cmbAnchorWidget";
            this.cmbAnchorWidget.Size = new System.Drawing.Size(162, 21);
            this.cmbAnchorWidget.TabIndex = 0;
            // 
            // txtSelectorFunction
            // 
            this.txtSelectorFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSelectorFunction.Location = new System.Drawing.Point(171, 0);
            this.txtSelectorFunction.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtSelectorFunction.Name = "txtSelectorFunction";
            this.txtSelectorFunction.Size = new System.Drawing.Size(218, 20);
            this.txtSelectorFunction.TabIndex = 1;
            // 
            // lblResolvedWidget
            // 
            this.lblResolvedWidget.AutoSize = true;
            this.lblResolvedWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResolvedWidget.Location = new System.Drawing.Point(395, 0);
            this.lblResolvedWidget.Name = "lblResolvedWidget";
            this.lblResolvedWidget.Size = new System.Drawing.Size(164, 20);
            this.lblResolvedWidget.TabIndex = 2;
            // 
            // SelectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SelectorControl";
            this.Size = new System.Drawing.Size(562, 20);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbAnchorWidget;
        private System.Windows.Forms.TextBox txtSelectorFunction;
        private System.Windows.Forms.Label lblResolvedWidget;
    }
}
