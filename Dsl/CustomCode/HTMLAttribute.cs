using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [Serializable]
    public class HTMLAttribute : ICloneable
    {
        public HTMLAttribute()
        {
            AttributeName = string.Empty;
            AttributeValue = string.Empty;
        }

        [System.ComponentModel.Category("Definition")]
        [System.ComponentModel.DisplayName("Attribute Name")]
        public string AttributeName { get; set; }

        [System.ComponentModel.Category("Definition")]
        [System.ComponentModel.DisplayName("Attribute Value")]
        public string AttributeValue { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is HTMLAttribute)
            {
                return string.Compare(this.AttributeName, ((HTMLAttribute)obj).AttributeName, true) == 0;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return AttributeName != null ? AttributeName.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return string.Format("{0}='{1}'", this.AttributeName ?? string.Empty, this.AttributeValue ?? string.Empty);
        }

        public object Clone()
        {
            return new HTMLAttribute() { AttributeName = this.AttributeName.Substring(0), AttributeValue = this.AttributeValue.Substring(0) };
        }

        public bool IsEmpty { get { return string.IsNullOrEmpty(this.AttributeName); } }
    }
}
