using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class EventSettingDialog : Form
    {
        public EventSettingDialog()
        {
            InitializeComponent();
        }

        public void SetEventList(List<string> list)
        {
            this.cmbEventList.Items.Clear();
            list.ForEach(item => this.cmbEventList.Items.Add(item));
        }

        public string Event { get { return this.cmbEventList.Text; } }
        public string Action { get { return this.cmbActionList.Text; } }
    }
}
