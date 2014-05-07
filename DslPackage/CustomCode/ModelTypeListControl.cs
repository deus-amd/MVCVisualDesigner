using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class ModelTypeListControl : UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public event EventHandler ValueChanged;

        public ModelTypeListControl()
        {
            InitializeComponent();
        }

        public void InitTypeList(string[] typeList, VDModelType modelType)
        {
            this.cmbKeyTypeList.Items.Clear();
            this.cmbKeyTypeList.Items.Add(Utility.Constants.STR_NOT_SPECIFIED);
            this.cmbKeyTypeList.Items.AddRange(typeList);
            this.cmbValueTypeList.Items.Clear();
            this.cmbValueTypeList.Items.Add(Utility.Constants.STR_NOT_SPECIFIED);
            this.cmbValueTypeList.Items.AddRange(typeList);

            SetModelType(modelType);
        }

        public void SetFont(Font font)
        {
            this.cmbCollectonType.Font = font;
            this.cmbKeyTypeList.Font = font;
            this.cmbValueTypeList.Font = font;
            this.lblLeftAngleBracket.Font = font;
            this.lblRightAngleBracket.Font = font;
            this.lblComma.Font = font;
        }

        private const int TYPE_INDEX_NOT_SPECIFIED = 0;
        private const int COLLECTION_INDEX_SINGULAR = 0;
        private const int COLLECTION_INDEX_DICT = 1;
        private const int COLLECTION_INDEX_LIST = 2;
        public void SetModelType(VDModelType modelType)
        {
            if (modelType == null)
            {
                this.cmbCollectonType.SelectedIndex = COLLECTION_INDEX_SINGULAR;
                this.cmbValueTypeList.SelectedIndex = TYPE_INDEX_NOT_SPECIFIED;
            }
            else if (modelType is VDDictionaryType)
            {
                VDDictionaryType dictType = (VDDictionaryType)modelType;
                this.cmbCollectonType.SelectedIndex = COLLECTION_INDEX_DICT;

                if (dictType.KeyInfo != null && dictType.KeyInfo.Type != null)
                    this.cmbKeyTypeList.Text = dictType.KeyInfo.Type.FullName;
                else
                    this.cmbKeyTypeList.SelectedIndex = TYPE_INDEX_NOT_SPECIFIED;

                if (dictType.ValueInfo != null && dictType.ValueInfo.Type != null)
                    this.cmbValueTypeList.Text = dictType.ValueInfo.Type.FullName;
                else
                    this.cmbValueTypeList.SelectedIndex = TYPE_INDEX_NOT_SPECIFIED;
            }
            else if (modelType is VDListType)
            {
                VDListType listType = (VDListType)modelType;
                this.cmbCollectonType.SelectedIndex = COLLECTION_INDEX_LIST;

                if (listType.ValueInfo != null && listType.ValueInfo.Type != null)
                    this.cmbValueTypeList.Text = listType.ValueInfo.Type.FullName;
                else
                    this.cmbValueTypeList.SelectedIndex = TYPE_INDEX_NOT_SPECIFIED;
            }
            else
            {
                this.cmbCollectonType.SelectedIndex = COLLECTION_INDEX_SINGULAR;
                this.cmbValueTypeList.Text = modelType.FullName;
            }
        }

        private void ModelTypeList_Load(object sender, EventArgs e)
        {
            if (this.cmbKeyTypeList.Items.Count > 0 && this.cmbValueTypeList.SelectedIndex < 0)
                this.cmbKeyTypeList.SelectedIndex = 0;
            if (this.cmbValueTypeList.Items.Count > 0 && this.cmbValueTypeList.SelectedIndex < 0)
                this.cmbValueTypeList.SelectedIndex = 0;

            this.cmbCollectonType.Height = this.Height;
            this.lblLeftAngleBracket.Height = this.Height;
            this.cmbKeyTypeList.Height = this.Height;
            this.lblComma.Height = this.Height;
            this.cmbValueTypeList.Height = this.Height;
            this.lblRightAngleBracket.Height = this.Height;
        }

        private void ModelTypeList_SizeChanged(object sender, EventArgs e)
        {
            layoutControls();
        }

        private void cmbCollectonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            layoutControls();

            if (this.cmbKeyTypeList.Visible)
                this.cmbKeyTypeList.Focus();
            else
                this.cmbValueTypeList.Focus();
        }

        private void cmbKeyTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbValueTypeList.Focus();
        }

        private void cmbValueTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null) this.SelectedIndexChanged(sender, e);

            if (this.ValueChanged != null) this.ValueChanged(this, e);
        }

        private const int COLLECTION_TYPE_WIDTH = 40;
        private const int LABEL_WIDTH = 10;
        private const int LOCATION_TOP = 0;
        private void layoutControls()
        {
            if (cmbCollectonType.SelectedIndex == 0) // not collection type
            {
                this.lblLeftAngleBracket.Hide();
                this.cmbKeyTypeList.Hide();
                this.lblComma.Hide();
                this.cmbValueTypeList.Show();
                this.lblRightAngleBracket.Hide();

                this.cmbValueTypeList.Location = new Point(this.cmbCollectonType.Bounds.Right, LOCATION_TOP);
                this.cmbValueTypeList.Width = this.Width - this.cmbCollectonType.Width;

                // set default value
                if (this.cmbValueTypeList.SelectedIndex < 0)
                    this.cmbValueTypeList.Text = Utility.Constants.STR_TYPE_STRING;
            }
            else if (cmbCollectonType.SelectedIndex == 1) // dictionary
            {
                this.lblLeftAngleBracket.Show();
                this.cmbKeyTypeList.Show();
                this.lblComma.Show();
                this.cmbValueTypeList.Show();
                this.lblRightAngleBracket.Show();

                int width = this.Width - COLLECTION_TYPE_WIDTH - 3 * LABEL_WIDTH;
                this.cmbKeyTypeList.Width = Math.Max(width / 2, COLLECTION_TYPE_WIDTH);
                this.cmbValueTypeList.Width = Math.Max(width / 2, COLLECTION_TYPE_WIDTH);
                this.lblComma.Location = new Point(this.cmbKeyTypeList.Bounds.Right, LOCATION_TOP);
                this.cmbValueTypeList.Location = new Point(this.lblComma.Bounds.Right, LOCATION_TOP);
                this.lblRightAngleBracket.Location = new Point(this.cmbValueTypeList.Bounds.Right, LOCATION_TOP);

                // set default value
                if (this.cmbKeyTypeList.SelectedIndex <= 0)
                    this.cmbKeyTypeList.Text = Utility.Constants.STR_TYPE_STRING;
                if (this.cmbValueTypeList.SelectedIndex <= 0)
                    this.cmbValueTypeList.Text = Utility.Constants.STR_TYPE_STRING;
            }
            else // list
            {
                this.lblLeftAngleBracket.Show();
                this.cmbKeyTypeList.Hide();
                this.lblComma.Hide();
                this.cmbValueTypeList.Show();
                this.lblRightAngleBracket.Show();

                this.cmbValueTypeList.Width = Math.Max(COLLECTION_TYPE_WIDTH, this.Width - COLLECTION_TYPE_WIDTH - 2 * LABEL_WIDTH);
                this.cmbValueTypeList.Location = new Point(this.lblLeftAngleBracket.Bounds.Right, LOCATION_TOP);
                this.lblRightAngleBracket.Location = new Point(this.cmbValueTypeList.Bounds.Right, LOCATION_TOP);

                // set default value
                if (this.cmbValueTypeList.SelectedIndex <= 0)
                    this.cmbValueTypeList.Text = Utility.Constants.STR_TYPE_STRING;
            }

        }

        private void cmbKeyTypeList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                cmbValueTypeList.Focus();
                e.Handled = true;
            }
        }

        private void cmbValueTypeList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ValueChanged != null) this.ValueChanged(this, null);
                e.Handled = true;
            }
        }

        private void ModelTypeList_Leave(object sender, EventArgs e)
        {
            if (this.ValueChanged != null) this.ValueChanged(this, e);
        }

        //
        public ModelTypeValue GetValue()
        {
            ModelTypeValue value = null;
            if (this.cmbCollectonType.SelectedIndex == COLLECTION_INDEX_SINGULAR)
            {
                value = new ModelTypeValue() { CollectionType = E_CollectionType.Not_Collection, ValueType = this.cmbValueTypeList.Text };
            }
            else if (this.cmbCollectonType.SelectedIndex == COLLECTION_INDEX_DICT)
            {
                value = new ModelTypeValue() { CollectionType = E_CollectionType.Dictionary, 
                    KeyType = this.cmbKeyTypeList.Text, 
                    ValueType = this.cmbValueTypeList.Text };
            }
            else if (this.cmbCollectonType.SelectedIndex == COLLECTION_INDEX_LIST)
            {
                value = new ModelTypeValue() { CollectionType = E_CollectionType.List, ValueType = this.cmbValueTypeList.Text };
            }
            return value;
        }
    }
}
