using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public interface IActionInfo
    {
        string Name { get; }
        List<IActionJointInfo> Joints { get; }
        VDActionBase CreateAction(Microsoft.VisualStudio.Modeling.Partition partition);
    }

    public interface IClientActionInfo  : IActionInfo
    {
        string Category { get; }
    }

    public interface IServerActionInfo : IActionInfo
    {
    }

    public interface IActionJointInfo
    {
        string Category { get; }
        string Name { get; }
        VDActionJoint CreateActionJoint(VDViewComponent widget, VDViewComponent target);
    }

    public class ActionJointInfo : IActionJointInfo
    {
        private string m_name;
        private string m_category;
        public ActionJointInfo(string category, string name)
        {
            m_category = category;
            m_name = name;
        }

        public string Name { get { return m_name; } }

        public string Category { get { return m_category; } }

        public VDActionJoint CreateActionJoint(VDViewComponent widget, VDViewComponent target)
        {
            var joint = new VDActionJoint(widget.Partition);
            joint.JointInfo = this;
            return joint;
        }

        //
        public override string ToString() { return string.IsNullOrWhiteSpace(m_category) ? m_name : string.Format("[{0}] {1}", m_category, m_name); }
        public override int GetHashCode() 
        { 
            int hash = this.Name == null ? 0 : this.Name.GetHashCode();
            if (string.IsNullOrWhiteSpace(m_category))
                return hash;
            else
                return m_category.GetHashCode() << 8 + hash;
        }
        public override bool Equals(object obj)
        {
            if (obj is IActionJointInfo)
            {
                IActionJointInfo other = obj as IActionJointInfo;
                return string.Equals(this.Name, other.Name) && string.Equals(this.m_category, other.Category);
            }
            return false;
        }
    }

    public class DefaultActionJontInfo : ActionJointInfo
    {
        private DefaultActionJontInfo()
            : base(null, "(Default)")
        {
        }

        private static DefaultActionJontInfo s_instance;
        public static DefaultActionJontInfo Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new DefaultActionJontInfo();
                }
                return s_instance;
            }
        }
    }

    public class ClientActionInfo : IClientActionInfo
    {
        private string m_name, m_category;
        public ClientActionInfo(string name, string category)
        {
            m_name = name;
            m_category = category;
        }

        public string Category { get { return m_category; } }

        public string Name { get { return m_name; } }

        private List<IActionJointInfo> m_joints = new List<IActionJointInfo>();
        public List<IActionJointInfo> Joints { get { return m_joints; } }

        public VDActionBase CreateAction(Microsoft.VisualStudio.Modeling.Partition partition)
        {
            VDClientAction act = new VDClientAction(partition);
            act.ClientActionInfo = this;
            return act;
        }

        // 
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Category))
                return this.Name;
            else
                return string.Format("[{0}] {1}", m_category, m_name);
        }

        public override int GetHashCode()
        {
            int hash = this.Name == null ? 0 : this.Name.GetHashCode();
            if (string.IsNullOrWhiteSpace(this.Category))
                return hash;
            else
                return this.Category.GetHashCode() << 8 + hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is ClientActionInfo)
            {
                ClientActionInfo cai = obj as ClientActionInfo;
                return string.Equals(this.Name, cai.Name) && string.Equals(this.Category, cai.Category);
            }
            return false;
        }
    }

    public class ServerActionInfo : IServerActionInfo
    {
        private string m_name;
        public ServerActionInfo(string name, List<IActionJointInfo> supportedJoints)
        {
            SetName(name);
            m_joints = supportedJoints;
            if (m_joints == null) m_joints = new List<IActionJointInfo>();
        }

        public string Name { get { return m_name; } }

        public void SetName (string name)
        {
            m_name = name;
        }

        private List<IActionJointInfo> m_joints;// = new List<IActionJointInfo>();
        public List<IActionJointInfo> Joints { get { return m_joints; } }

        public VDActionBase CreateAction(Microsoft.VisualStudio.Modeling.Partition partition)
        {
            VDServerAction act = new VDServerAction(partition);
            act.ServerActionInfo = this;
            return act;
        }

        //
        public override string ToString() { return this.Name; }

        public override int GetHashCode()
        {
            return this.Name == null ? 0 : this.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ServerActionInfo)
            {
                return string.Equals(this.Name, ((ServerActionInfo)obj).Name);
            }
            return false;
        }
    }
}
