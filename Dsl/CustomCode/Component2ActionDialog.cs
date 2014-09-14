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
    public partial class Component2ActionDialog : Form
    {
        public Component2ActionDialog()
        {
            InitializeComponent();
        }

        public void SetComponentAndAction(VDViewComponent srcComponent, VDActionBase targetAction)
        {
            // set source component
            cmbEvent.Items.Clear();
            if (srcComponent != null)
            {
                txtSource.Text = string.Format("[{1}] {0}", srcComponent.WidgetName, srcComponent.WidgetType.ToString());
                if (srcComponent.SupportedEvents != null)
                {
                    cmbEvent.Items.AddRange(srcComponent.SupportedEvents.ToArray());
                    if (cmbEvent.Items.Count > 0) cmbEvent.SelectedIndex = 0;
                }
            }
            else
            {
                txtSource.Text = string.Empty;
            }

            // set target action
            if (targetAction != null)
            {
                txtAction.Text = targetAction.Name;
                chkServer.Checked = targetAction is VDServerAction;
            }
            else
            {
                txtAction.Text = string.Empty;
                chkServer.Checked = false;
            }
        }

        public IEventInfo SelectedEvent
        {
            get { return cmbEvent.SelectedItem as IEventInfo; }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (SelectedEvent == null)
            {
                MessageBox.Show("Please select an event.");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
