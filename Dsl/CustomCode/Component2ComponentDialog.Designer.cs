namespace MVCVisualDesigner
{
    partial class Component2ComponentDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tpNewAction = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbClientAction = new System.Windows.Forms.ComboBox();
            this.txtServerAction = new System.Windows.Forms.TextBox();
            this.lblSrc = new System.Windows.Forms.Label();
            this.lblEvent = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblJoint = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.cmbEvent = new System.Windows.Forms.ComboBox();
            this.cmbJoint = new System.Windows.Forms.ComboBox();
            this.chkServer = new System.Windows.Forms.CheckBox();
            this.tpNewTarget = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAction2 = new System.Windows.Forms.Label();
            this.lblJoint2 = new System.Windows.Forms.Label();
            this.lblTarget2 = new System.Windows.Forms.Label();
            this.txtTarget2 = new System.Windows.Forms.TextBox();
            this.cmbJoint2 = new System.Windows.Forms.ComboBox();
            this.lstAction = new System.Windows.Forms.ListBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tpNewAction.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpNewTarget.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.tabCtrl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdCancel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdOK, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 269);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabCtrl
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabCtrl, 2);
            this.tabCtrl.Controls.Add(this.tpNewAction);
            this.tabCtrl.Controls.Add(this.tpNewTarget);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(3, 3);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(406, 234);
            this.tabCtrl.TabIndex = 0;
            // 
            // tpNewAction
            // 
            this.tpNewAction.Controls.Add(this.tableLayoutPanel2);
            this.tpNewAction.Location = new System.Drawing.Point(4, 22);
            this.tpNewAction.Name = "tpNewAction";
            this.tpNewAction.Padding = new System.Windows.Forms.Padding(3);
            this.tpNewAction.Size = new System.Drawing.Size(398, 208);
            this.tpNewAction.TabIndex = 0;
            this.tpNewAction.Text = "New Action";
            this.tpNewAction.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblSrc, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblEvent, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTarget, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblAction, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblJoint, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtSource, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtTarget, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbEvent, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbJoint, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.chkServer, 2, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(392, 202);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbClientAction);
            this.panel1.Controls.Add(this.txtServerAction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(63, 85);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 26);
            this.panel1.TabIndex = 15;
            // 
            // cmbClientAction
            // 
            this.cmbClientAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbClientAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientAction.FormattingEnabled = true;
            this.cmbClientAction.Location = new System.Drawing.Point(0, 0);
            this.cmbClientAction.Name = "cmbClientAction";
            this.cmbClientAction.Size = new System.Drawing.Size(248, 21);
            this.cmbClientAction.TabIndex = 14;
            // 
            // txtServerAction
            // 
            this.txtServerAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerAction.Location = new System.Drawing.Point(0, 0);
            this.txtServerAction.Name = "txtServerAction";
            this.txtServerAction.Size = new System.Drawing.Size(248, 20);
            this.txtServerAction.TabIndex = 15;
            this.txtServerAction.Visible = false;
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrc.Location = new System.Drawing.Point(3, 3);
            this.lblSrc.Margin = new System.Windows.Forms.Padding(3);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(47, 13);
            this.lblSrc.TabIndex = 0;
            this.lblSrc.Text = "Source";
            // 
            // lblEvent
            // 
            this.lblEvent.AutoSize = true;
            this.lblEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvent.Location = new System.Drawing.Point(3, 31);
            this.lblEvent.Margin = new System.Windows.Forms.Padding(3);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(40, 13);
            this.lblEvent.TabIndex = 1;
            this.lblEvent.Text = "Event";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarget.Location = new System.Drawing.Point(3, 59);
            this.lblTarget.Margin = new System.Windows.Forms.Padding(3);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(44, 13);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(3, 87);
            this.lblAction.Margin = new System.Windows.Forms.Padding(3);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(43, 13);
            this.lblAction.TabIndex = 3;
            this.lblAction.Text = "Action";
            // 
            // lblJoint
            // 
            this.lblJoint.AutoSize = true;
            this.lblJoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJoint.Location = new System.Drawing.Point(3, 115);
            this.lblJoint.Margin = new System.Windows.Forms.Padding(3);
            this.lblJoint.Name = "lblJoint";
            this.lblJoint.Size = new System.Drawing.Size(34, 13);
            this.lblJoint.TabIndex = 4;
            this.lblJoint.Text = "Joint";
            // 
            // txtSource
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtSource, 2);
            this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSource.Enabled = false;
            this.txtSource.Location = new System.Drawing.Point(63, 3);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(326, 20);
            this.txtSource.TabIndex = 5;
            // 
            // txtTarget
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtTarget, 2);
            this.txtTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTarget.Enabled = false;
            this.txtTarget.Location = new System.Drawing.Point(63, 59);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(326, 20);
            this.txtTarget.TabIndex = 6;
            // 
            // cmbEvent
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cmbEvent, 2);
            this.cmbEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Location = new System.Drawing.Point(63, 31);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(326, 21);
            this.cmbEvent.TabIndex = 7;
            // 
            // cmbJoint
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cmbJoint, 2);
            this.cmbJoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJoint.FormattingEnabled = true;
            this.cmbJoint.Location = new System.Drawing.Point(63, 115);
            this.cmbJoint.Name = "cmbJoint";
            this.cmbJoint.Size = new System.Drawing.Size(326, 21);
            this.cmbJoint.TabIndex = 9;
            // 
            // chkServer
            // 
            this.chkServer.AutoSize = true;
            this.chkServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkServer.Location = new System.Drawing.Point(315, 87);
            this.chkServer.Name = "chkServer";
            this.chkServer.Size = new System.Drawing.Size(74, 22);
            this.chkServer.TabIndex = 10;
            this.chkServer.Text = "Server";
            this.chkServer.UseVisualStyleBackColor = true;
            // 
            // tpNewTarget
            // 
            this.tpNewTarget.Controls.Add(this.tableLayoutPanel3);
            this.tpNewTarget.Location = new System.Drawing.Point(4, 22);
            this.tpNewTarget.Name = "tpNewTarget";
            this.tpNewTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tpNewTarget.Size = new System.Drawing.Size(398, 208);
            this.tpNewTarget.TabIndex = 1;
            this.tpNewTarget.Text = "New Target";
            this.tpNewTarget.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblAction2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblJoint2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblTarget2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtTarget2, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cmbJoint2, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lstAction, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(392, 202);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblAction2
            // 
            this.lblAction2.AutoSize = true;
            this.lblAction2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction2.Location = new System.Drawing.Point(3, 3);
            this.lblAction2.Margin = new System.Windows.Forms.Padding(3);
            this.lblAction2.Name = "lblAction2";
            this.lblAction2.Size = new System.Drawing.Size(43, 13);
            this.lblAction2.TabIndex = 0;
            this.lblAction2.Text = "Action";
            // 
            // lblJoint2
            // 
            this.lblJoint2.AutoSize = true;
            this.lblJoint2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJoint2.Location = new System.Drawing.Point(3, 149);
            this.lblJoint2.Margin = new System.Windows.Forms.Padding(3);
            this.lblJoint2.Name = "lblJoint2";
            this.lblJoint2.Size = new System.Drawing.Size(34, 13);
            this.lblJoint2.TabIndex = 1;
            this.lblJoint2.Text = "Joint";
            // 
            // lblTarget2
            // 
            this.lblTarget2.AutoSize = true;
            this.lblTarget2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarget2.Location = new System.Drawing.Point(3, 177);
            this.lblTarget2.Margin = new System.Windows.Forms.Padding(3);
            this.lblTarget2.Name = "lblTarget2";
            this.lblTarget2.Size = new System.Drawing.Size(44, 13);
            this.lblTarget2.TabIndex = 2;
            this.lblTarget2.Text = "Target";
            // 
            // txtTarget2
            // 
            this.txtTarget2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTarget2.Enabled = false;
            this.txtTarget2.Location = new System.Drawing.Point(63, 177);
            this.txtTarget2.Name = "txtTarget2";
            this.txtTarget2.ReadOnly = true;
            this.txtTarget2.Size = new System.Drawing.Size(326, 20);
            this.txtTarget2.TabIndex = 3;
            // 
            // cmbJoint2
            // 
            this.cmbJoint2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbJoint2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJoint2.FormattingEnabled = true;
            this.cmbJoint2.Location = new System.Drawing.Point(63, 149);
            this.cmbJoint2.Name = "cmbJoint2";
            this.cmbJoint2.Size = new System.Drawing.Size(326, 21);
            this.cmbJoint2.TabIndex = 4;
            // 
            // lstAction
            // 
            this.lstAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAction.FormattingEnabled = true;
            this.lstAction.Location = new System.Drawing.Point(63, 3);
            this.lstAction.Name = "lstAction";
            this.lstAction.Size = new System.Drawing.Size(326, 140);
            this.lstAction.TabIndex = 5;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdCancel.Location = new System.Drawing.Point(334, 243);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdOK.Location = new System.Drawing.Point(253, 243);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // Component2ComponentDialog
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(412, 269);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Component2ComponentDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Link";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tpNewAction.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tpNewTarget.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tpNewAction;
        private System.Windows.Forms.TabPage tpNewTarget;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblSrc;
        private System.Windows.Forms.Label lblEvent;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblJoint;
        private System.Windows.Forms.Label lblAction2;
        private System.Windows.Forms.Label lblJoint2;
        private System.Windows.Forms.Label lblTarget2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.ComboBox cmbEvent;
        private System.Windows.Forms.ComboBox cmbJoint;
        private System.Windows.Forms.CheckBox chkServer;
        private System.Windows.Forms.TextBox txtTarget2;
        private System.Windows.Forms.ComboBox cmbJoint2;
        private System.Windows.Forms.ListBox lstAction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbClientAction;
        private System.Windows.Forms.TextBox txtServerAction;
    }
}