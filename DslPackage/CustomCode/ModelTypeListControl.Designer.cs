namespace MVCVisualDesigner
{
    //partial class ModelTypeListControl
    //{
    //    /// <summary> 
    //    /// Required designer variable.
    //    /// </summary>
    //    private System.ComponentModel.IContainer components = null;

    //    /// <summary> 
    //    /// Clean up any resources being used.
    //    /// </summary>
    //    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing && (components != null))
    //        {
    //            components.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }

    //    #region Component Designer generated code

    //    /// <summary> 
    //    /// Required method for Designer support - do not modify 
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent()
    //    {
    //        this.cmbCollectonType = new System.Windows.Forms.ComboBox();
    //        this.lblLeftAngleBracket = new System.Windows.Forms.Label();
    //        this.cmbKeyTypeList = new System.Windows.Forms.ComboBox();
    //        this.cmbValueTypeList = new System.Windows.Forms.ComboBox();
    //        this.lblRightAngleBracket = new System.Windows.Forms.Label();
    //        this.lblComma = new System.Windows.Forms.Label();
    //        this.SuspendLayout();
    //        // 
    //        // cmbCollectonType
    //        // 
    //        this.cmbCollectonType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    //        this.cmbCollectonType.FormattingEnabled = true;
    //        this.cmbCollectonType.Items.AddRange(new object[] {
    //        "",
    //        "Dict",
    //        "List"});
    //        this.cmbCollectonType.Location = new System.Drawing.Point(0, 0);
    //        this.cmbCollectonType.Margin = new System.Windows.Forms.Padding(2);
    //        this.cmbCollectonType.Name = "cmbCollectonType";
    //        this.cmbCollectonType.Size = new System.Drawing.Size(40, 21);
    //        this.cmbCollectonType.TabIndex = 8;
    //        this.cmbCollectonType.SelectedIndexChanged += new System.EventHandler(this.cmbCollectonType_SelectedIndexChanged);
    //        // 
    //        // lblLeftAngleBracket
    //        // 
    //        this.lblLeftAngleBracket.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    //        this.lblLeftAngleBracket.Location = new System.Drawing.Point(41, 0);
    //        this.lblLeftAngleBracket.Margin = new System.Windows.Forms.Padding(0);
    //        this.lblLeftAngleBracket.Name = "lblLeftAngleBracket";
    //        this.lblLeftAngleBracket.Size = new System.Drawing.Size(10, 23);
    //        this.lblLeftAngleBracket.TabIndex = 9;
    //        this.lblLeftAngleBracket.Text = "<";
    //        this.lblLeftAngleBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        this.lblLeftAngleBracket.Visible = false;
    //        // 
    //        // cmbKeyTypeList
    //        // 
    //        this.cmbKeyTypeList.FormattingEnabled = true;
    //        this.cmbKeyTypeList.Location = new System.Drawing.Point(50, 0);
    //        this.cmbKeyTypeList.Margin = new System.Windows.Forms.Padding(2);
    //        this.cmbKeyTypeList.Name = "cmbKeyTypeList";
    //        this.cmbKeyTypeList.Size = new System.Drawing.Size(70, 21);
    //        this.cmbKeyTypeList.TabIndex = 10;
    //        this.cmbKeyTypeList.Visible = false;
    //        this.cmbKeyTypeList.SelectedIndexChanged += new System.EventHandler(this.cmbKeyTypeList_SelectedIndexChanged);
    //        this.cmbKeyTypeList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbKeyTypeList_KeyPress);
    //        // 
    //        // cmbValueTypeList
    //        // 
    //        this.cmbValueTypeList.FormattingEnabled = true;
    //        this.cmbValueTypeList.Location = new System.Drawing.Point(130, 0);
    //        this.cmbValueTypeList.Margin = new System.Windows.Forms.Padding(2);
    //        this.cmbValueTypeList.Name = "cmbValueTypeList";
    //        this.cmbValueTypeList.Size = new System.Drawing.Size(70, 21);
    //        this.cmbValueTypeList.TabIndex = 12;
    //        this.cmbValueTypeList.SelectedIndexChanged += new System.EventHandler(this.cmbValueTypeList_SelectedIndexChanged);
    //        this.cmbValueTypeList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbValueTypeList_KeyPress);
    //        // 
    //        // lblRightAngleBracket
    //        // 
    //        this.lblRightAngleBracket.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    //        this.lblRightAngleBracket.Location = new System.Drawing.Point(200, 0);
    //        this.lblRightAngleBracket.Margin = new System.Windows.Forms.Padding(0);
    //        this.lblRightAngleBracket.Name = "lblRightAngleBracket";
    //        this.lblRightAngleBracket.Size = new System.Drawing.Size(10, 23);
    //        this.lblRightAngleBracket.TabIndex = 13;
    //        this.lblRightAngleBracket.Text = ">";
    //        this.lblRightAngleBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        this.lblRightAngleBracket.Visible = false;
    //        // 
    //        // lblComma
    //        // 
    //        this.lblComma.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    //        this.lblComma.Location = new System.Drawing.Point(120, 0);
    //        this.lblComma.Margin = new System.Windows.Forms.Padding(0);
    //        this.lblComma.Name = "lblComma";
    //        this.lblComma.Size = new System.Drawing.Size(10, 23);
    //        this.lblComma.TabIndex = 11;
    //        this.lblComma.Text = ",";
    //        this.lblComma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    //        this.lblComma.Visible = false;
    //        // 
    //        // ModelTypeList
    //        // 
    //        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //        this.Controls.Add(this.cmbCollectonType);
    //        this.Controls.Add(this.lblLeftAngleBracket);
    //        this.Controls.Add(this.cmbKeyTypeList);
    //        this.Controls.Add(this.cmbValueTypeList);
    //        this.Controls.Add(this.lblRightAngleBracket);
    //        this.Controls.Add(this.lblComma);
    //        this.Name = "ModelTypeList";
    //        this.Size = new System.Drawing.Size(220, 20);
    //        this.Load += new System.EventHandler(this.ModelTypeList_Load);
    //        this.SizeChanged += new System.EventHandler(this.ModelTypeList_SizeChanged);
    //        this.Leave += new System.EventHandler(this.ModelTypeList_Leave);
    //        this.ResumeLayout(false);

    //    }

    //    #endregion

    //    private System.Windows.Forms.ComboBox cmbCollectonType;
    //    private System.Windows.Forms.Label lblLeftAngleBracket;
    //    private System.Windows.Forms.ComboBox cmbKeyTypeList;
    //    private System.Windows.Forms.ComboBox cmbValueTypeList;
    //    private System.Windows.Forms.Label lblRightAngleBracket;
    //    private System.Windows.Forms.Label lblComma;

    //}
}
