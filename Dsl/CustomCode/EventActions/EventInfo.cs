using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public interface IEventInfo
    {
        string Name { get; }

        string Category { get; }

        List<IEventPropertyInfo> PredefinedProperties { get; }

        VDEventBase CreateEvent(VDViewComponent widget);
    }

    public interface IEventPropertyInfo
    {
        string Name { get; set; }
        string DataType { get; set; }
    }

    public class EventInfo : IEventInfo
    {
        private string m_name, m_category;
        public EventInfo(string name, string category = "")
        {
            m_name = name;
            m_category = category;
        }
        public string Name { get { return m_name; } }

        public string Category { get { return m_category; } }

        private List<IEventPropertyInfo> m_predefinedProperties = new List<IEventPropertyInfo>();
        public List<IEventPropertyInfo> PredefinedProperties { get { return m_predefinedProperties; } }

        public VDEventBase CreateEvent(VDViewComponent widget)
        {
            VDEvent evt = new VDEvent(widget.Partition);
            evt.EventInfo = this;
            return evt;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(m_category))
                return m_name;
            else
                return string.Format("[{0}] {1}", m_category, m_name);
        }

        public override int GetHashCode()
        {
            int hash = m_name == null ? 0 : m_name.GetHashCode();
            if (string.IsNullOrWhiteSpace(m_category))
                return hash;
            else
                return m_category.GetHashCode() << 8 + hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is EventInfo)
            {
                EventInfo ei = obj as EventInfo;
                return string.Equals(this.m_category, ei.m_category) && string.Equals(this.m_name, ei.m_name);
            }

            return false;
        }
    }

    public class PseudoEventInfo : IEventInfo
    {
        private string m_name, m_category;
        public PseudoEventInfo(string category = "", string name = "THEN")
        {
            m_name = name;
            m_category = category;
        }
        public string Name { get { return m_name; } }

        public string Category { get { return m_category; } }

        private List<IEventPropertyInfo> m_predefinedProperties = new List<IEventPropertyInfo>();
        public List<IEventPropertyInfo> PredefinedProperties { get { return m_predefinedProperties; } }

        public VDEventBase CreateEvent(VDViewComponent widget)
        {
            VDPseudoEvent evt = new VDPseudoEvent(widget.Partition);
            evt.EventInfo = this;
            return evt;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(m_category))
                return m_name;
            else
                return string.Format("[{0}] {1}", m_category, m_name);
        }

        public override int GetHashCode()
        {
            int hash = m_name == null ? 0 : m_name.GetHashCode();
            if (string.IsNullOrWhiteSpace(m_category))
                return hash;
            else
                return m_category.GetHashCode() << 8 + hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is PseudoEventInfo)
            {
                PseudoEventInfo ei = obj as PseudoEventInfo;
                return string.Equals(this.m_category, ei.m_category) && string.Equals(this.m_name, ei.m_name);
            }

            return false;
        }
    }
}
