using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.TypeDescriptor
{
    public class MVDGenericTypeDescriptor : IMVDTypeDescriptor
    {
        protected string m_name;
        protected string m_nameSpace;

        public MVDGenericTypeDescriptor(string name, string nameSpace)
        {
            m_name = name;
            m_nameSpace = nameSpace;
        }

        public string Name { get { return m_name; } }

        public string NameSpace { get { return m_nameSpace; } }

        public virtual object ParseValue(string raw)
        {
            return null;
        }

        public virtual bool TryParseValue(string raw, out object value)
        {
            value = null;
            return false;
        }

        public string ValueToString(object value) { return value == null ? string.Empty : value.ToString(); }

        public virtual string GenerateCSValidationCode() { return string.Empty; }

        public virtual string GenerateJSValidationCode() { return string.Empty; }

        public virtual string GenerateHTMLAttributes() { return string.Empty; }

        public virtual string GenerateJSAcceptChars() { return string.Empty; }

        public override bool Equals(object obj)
        {
            IMVDTypeDescriptor other = obj as IMVDTypeDescriptor;
            if (obj == null) return false;

            return string.Compare(this.m_name, other.Name) == 0 
                && string.Compare(this.m_nameSpace, other.NameSpace) == 0;
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(m_nameSpace) ? m_name.GetHashCode() : m_nameSpace.GetHashCode() * 10 + m_name.GetHashCode();
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(m_nameSpace) ? m_name : m_nameSpace + "." + m_name;
        }
    }
}
