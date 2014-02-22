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

        public HTMLAttribute(string name, string value)
        {
            AttributeName = name;
            AttributeValue = value;
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
                return string.Compare(this.AttributeName, ((HTMLAttribute)obj).AttributeName/*, true*/) == 0;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return AttributeName != null ? AttributeName.GetHashCode() : 0;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(AttributeName) || string.IsNullOrEmpty(AttributeValue))
                return string.Empty;
            return string.Format("{0}='{1}'", this.AttributeName, this.AttributeValue);
        }

        public object Clone()
        {
            return new HTMLAttribute()
            {
                AttributeName = this.AttributeName.Clone() as string,
                AttributeValue = this.AttributeValue.Clone() as string
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns>error message</returns>
        public static string ValidateAttributes(List<HTMLAttribute> attributes)
        {
            if (attributes == null || attributes.Count == 0) return null;

            HashSet<HTMLAttribute> attrHash = new HashSet<HTMLAttribute>();
            StringBuilder sb = new StringBuilder();
            foreach (HTMLAttribute attr in attributes)
            {
                if (string.IsNullOrEmpty(attr.AttributeName))
                {
                    if (string.IsNullOrEmpty(attr.AttributeValue))
                    {
                        continue; // ignore empty attribute
                    }
                    else
                    {
                        sb.AppendFormat("Attribute name can not be empty for {0}\r\n", attr);
                    }
                }
                else if (attrHash.Contains(attr))
                {
                    sb.AppendFormat("Attribute {0} has already been specified.\r\n", attr);
                }
                else
                {
                    attrHash.Add(attr);
                }
            }

            if (sb.Length > 0)
                return sb.ToString();
            else
                return null;
        }

        /// <summary>
        /// list1 and list2 are all validated using ValidateAttributes() method above
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool AreAttributeListsEqual(List<HTMLAttribute> list1, List<HTMLAttribute> list2)
        {
            if (list1 == null && list2 == null) return true;
            else if (list1 == null || list2 == null) return false;

            if (list1.Count != list2.Count) return false;

            foreach (var attr2 in list2)
            {
                var attr1 = list1.Find(a => a.Equals(attr2));
                if (attr1 == null) return false;

                if (string.Compare(attr1.AttributeValue, attr2.AttributeValue) != 0)
                    return false;
            }

            return true;
        }

        public static List<HTMLAttribute> CloneHTMLAttributeList(List<HTMLAttribute> originalList)
        {
            if (originalList == null) return null;

            List<HTMLAttribute> newList = new List<HTMLAttribute>();
            foreach (var attr in originalList)
            {
                newList.Add(attr.Clone() as HTMLAttribute);
            }
            return newList;
        }
    }
}
