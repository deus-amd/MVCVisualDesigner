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
    public partial class Component2ComponentDialog : Form
    {
        private NewActionHandler m_newActionHandler;
        private NewTargetHandler m_newTargetHandler;

        public Component2ComponentDialog(bool showNewTargetTab = true)
        {
            InitializeComponent();

            if (!showNewTargetTab) tabCtrl.TabPages.RemoveAt(1);

            m_newActionHandler = new NewActionHandler(txtSource, txtTarget, cmbEvent, cmbClientAction, txtServerAction, cmbJoint, chkServer);
            this.chkServer.CheckedChanged += m_newActionHandler.OnServerCheckboxChanged;
            this.cmbClientAction.SelectedIndexChanged += m_newActionHandler.OnSelectedActionChanged;

            m_newTargetHandler = new NewTargetHandler(lstAction, cmbJoint2, txtTarget2);
        }

        public NewActionHandler NewAction { get { return m_newActionHandler; } }
        public NewTargetHandler NewTarget { get { return m_newTargetHandler; } }

        public bool IfCreateNewAction { get { return this.tabCtrl.SelectedIndex == 0; } }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (IfCreateNewAction)
            {
                if(NewAction.OnOK(sender, e))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                if (NewTarget.OnOK(sender, e))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        public class NewActionHandler
        {
            private TextBox m_txtSource;
            private TextBox m_txtTarget;
            private ComboBox m_cmbEvent;
            private ComboBox m_cmbClientAction;
            private TextBox m_txtServerAction;
            private ComboBox m_cmbJoint;
            private CheckBox m_chkServer;

            public NewActionHandler(TextBox src, TextBox target, ComboBox cmbEvent,
                ComboBox cmbClientAction, TextBox serverAction, ComboBox cmbJoint, CheckBox chkServer)
            {
                m_txtSource = src;
                m_txtTarget = target;
                m_cmbEvent = cmbEvent;
                m_cmbClientAction = cmbClientAction;
                m_txtServerAction = serverAction;
                m_cmbJoint = cmbJoint;
                m_chkServer = chkServer;
            }

            public NewActionHandler SetSource(VDWidget source)
            {
                m_cmbEvent.Items.Clear();

                if (source != null)
                {
                    m_txtSource.Text = string.Format("[{1}] {0}", source.WidgetName, source.WidgetType.ToString());
                    if (source.SupportedEvents != null)
                    {
                        m_cmbEvent.Items.AddRange(source.SupportedEvents.ToArray());
                        if (m_cmbEvent.Items.Count > 0) m_cmbEvent.SelectedIndex = 0;
                    }
                }
                else
                {
                    m_txtSource.Text = string.Empty;
                }

                m_cmbEvent.Enabled = true;
                return this;
            }

            public NewActionHandler SetSourceEvent(VDEventBase evt)
            {
                if (evt != null)
                {
                    VDWidget source = evt.Widget;
                    SetSource(source);
                }
                else
                {
                    SetSource(null);
                }

                m_cmbEvent.Enabled = false;
                return this;
            }

            private List<IActionJointInfo> m_serverActionJoints = null;
            public NewActionHandler SetTarget(VDViewComponent target)
            {
                m_txtTarget.Text = string.Empty;
                m_cmbClientAction.Items.Clear();

                if (target != null)
                {
                    m_txtTarget.Text = string.Format("{0} [{1}]", target.WidgetName, target.WidgetType.ToString());
                    if (target.SupportedActions != null)
                    {
                        m_cmbClientAction.Items.AddRange(target.SupportedActions.ToArray());
                        if (m_cmbClientAction.Items.Count > 0) m_cmbClientAction.SelectedIndex = 0;
                    }
                }

                m_chkServer.Checked = false;
                m_chkServer.Enabled = target.CanBeTargetOfServerAction;

                VDModelStore ms = target.GetModelStore();
                if (ms != null)
                    m_serverActionJoints = ms.GetSupportedServerActionJoints();
                else
                    m_serverActionJoints = null;
                
                return this;
            }

            public void OnServerCheckboxChanged(object sender, EventArgs e)
            {
                m_cmbClientAction.Visible = !m_chkServer.Checked;
                m_txtServerAction.Visible = m_chkServer.Checked;

                m_cmbJoint.Items.Clear();
                if (m_chkServer.Checked)
                {
                    // set joints for server action
                    if (m_serverActionJoints != null)
                        m_cmbJoint.Items.AddRange(m_serverActionJoints.ToArray());

                    if (m_cmbJoint.Items.Count > 0)
                        m_cmbJoint.SelectedIndex = 0;
                }
                else
                {
                    OnSelectedActionChanged(sender, e); 
                }
            }

            public void OnSelectedActionChanged(object sender, EventArgs e)
            {
                m_cmbJoint.Items.Clear();
                m_cmbJoint.Items.Add(DefaultActionJontInfo.Instance);

                IClientActionInfo selectedAction = m_cmbClientAction.SelectedItem as IClientActionInfo;
                if (selectedAction != null && selectedAction.Joints != null)
                {
                    m_cmbJoint.Items.AddRange(selectedAction.Joints.ToArray());
                }

                m_cmbJoint.SelectedIndex = 0;
            }

            public bool OnOK(object sender, EventArgs e)
            {
                if (SelectedEvent == null)
                {
                    m_cmbEvent.Focus();
                    MessageBox.Show("Please select an event.");
                    return false;
                }
                else
                {
                    if (m_chkServer.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(m_txtServerAction.Text))
                        {
                            m_txtServerAction.Focus();
                            MessageBox.Show("Server action name can not be empty.");
                            return false;
                        }
                    }
                    else
                    {
                        if (m_cmbClientAction.SelectedItem == null)
                        {
                            m_cmbClientAction.Focus();
                            MessageBox.Show("Please select a client action.");
                            return false;
                        }
                    }
                }

                return true;
            }

            public IEventInfo SelectedEvent
            {
                get { return m_cmbEvent.SelectedItem as IEventInfo; }
            }

            public IActionInfo SelectedAction
            {
                get
                {
                    if (!m_chkServer.Checked)
                        return m_cmbClientAction.SelectedItem as IClientActionInfo;
                    else
                        return new ServerActionInfo(m_txtServerAction.Text);
                }
            }

            public IActionJointInfo SelectedJoint
            {
                get 
                {
                    if (m_cmbJoint.SelectedItem != null)
                        return m_cmbJoint.SelectedItem as IActionJointInfo;
                    else
                        return DefaultActionJontInfo.Instance;                       
                }
            }
        }

        public class NewTargetHandler
        {
            private ListBox m_lstAction;
            private ComboBox m_cmbJoint;
            private TextBox m_txtTarget;

            public NewTargetHandler(ListBox lstAction, ComboBox cmbJoint, TextBox txtTarget)
            {
                m_lstAction = lstAction;
                m_cmbJoint = cmbJoint;
                m_txtTarget = txtTarget;
            }

            public NewTargetHandler SetSource(VDViewComponent source)
            {
                if (source is VDActionBase)
                {
                    SetSourceAction((VDActionBase)source);
                }
                else if (source is VDViewComponent)
                {
                    List<VDActionBase> actionList = new List<VDActionBase>();
                    findSourceActionList(source, actionList);
                    List<IActionJointInfo> joints = getCommonActionJoint(actionList);

                    m_lstAction.Items.Clear();
                    m_cmbJoint.Items.Clear();
                    m_cmbJoint.Items.Add(DefaultActionJontInfo.Instance);
                    actionList.ForEach(act => { if (act.ActionInfo != null) m_lstAction.Items.Add(act.ActionInfo); });
                    m_cmbJoint.Items.AddRange(joints.ToArray());
                    if (m_cmbJoint.Items.Count > 0) m_cmbJoint.SelectedIndex = 0;
                }

                return this;
            }
            
            private NewTargetHandler SetSourceAction(VDActionBase action)
            {
                m_lstAction.Items.Clear();
                m_cmbJoint.Items.Clear();
                m_cmbJoint.Items.Add(DefaultActionJontInfo.Instance);
                
                if (action != null)
                {
                    m_lstAction.Items.Add(action.ActionInfo);
                    if (action.ActionInfo.Joints != null)
                    {
                        m_cmbJoint.Items.AddRange(action.ActionInfo.Joints.ToArray());
                    }
                }

                m_cmbJoint.SelectedIndex = 0;

                return this;
            }

            public NewTargetHandler SetTarget(VDViewComponent target)
            {
                if (target != null)
                {
                    m_txtTarget.Text = string.Format("{0} [{1}]", target.WidgetName, target.WidgetType.ToString());
                }
                else
                {
                    m_txtTarget.Text = string.Empty;
                }
                return this;
            }

            private static void findSourceActionList(VDViewComponent target, List<VDActionBase> actionList)
            {
                foreach(VDActionJoint joint in target.SourceActionJoints)
                {
                    if (joint.Widget is VDActionBase)
                    {
                        actionList.Add((VDActionBase)joint.Widget);
                    }
                    else
                    {
                        findSourceActionList(joint.Widget, actionList);
                    }
                }
            }

            private static List<IActionJointInfo> getCommonActionJoint(List<VDActionBase> actionList)
            {
                Dictionary<IActionJointInfo, int> count = new Dictionary<IActionJointInfo, int>();
                foreach(VDActionBase action in actionList)
                {
                    if (action.ActionInfo == null || action.ActionInfo.Joints == null) continue;

                    foreach (IActionJointInfo joint in action.ActionInfo.Joints)
                    {
                        if (!count.ContainsKey(joint)) count.Add(joint, 0);

                        count[joint] = count[joint] + 1;
                    }
                }

                List<IActionJointInfo> commJoints = new List<IActionJointInfo>();
                foreach(var kv in count)
                {
                    if (kv.Value == actionList.Count) commJoints.Add(kv.Key);
                }

                return commJoints;
            }

            public bool OnOK(object sender, EventArgs e)
            {
                if (m_lstAction.Items.Count <= 0)
                {
                    MessageBox.Show("No action available");
                    return false;
                }
                return true;
            }

            public IActionJointInfo SelectedJoint 
            { 
                get
                {
                    if (m_cmbJoint.SelectedItem != null)
                        return m_cmbJoint.SelectedItem as IActionJointInfo;
                    else
                        return DefaultActionJontInfo.Instance;
                }
            }
        }
    }
}
