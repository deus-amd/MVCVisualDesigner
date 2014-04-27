using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MVCVisualDesigner
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class PredefinedTypeOptionPage : MVDOptionPageBase<PredefinedTypeOptionPageControl>
    {
        public List<string> TypeDescriptorAssemblyList
        {
            get
            {
                if (m_typeDescriptorAssemblyList == null) m_typeDescriptorAssemblyList = new List<string>();
                return m_typeDescriptorAssemblyList;
            }
            set { m_typeDescriptorAssemblyList = value; }
        }
        private List<string> m_typeDescriptorAssemblyList = null;

        private static readonly List<string> s_defaultAssemblyList = new List<string>()
        {
            "./TypeDescriptor.dll" 
        };

        private const string TAGNAME_TYPEDESCRIPTOR_OPTIONS = "type_descriptor_options";
        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            XElement elemRoot = OpenOptionsFile();
            if (elemRoot != null)
            {
                this.TypeDescriptorAssemblyList.Clear();

                XElement typeDescElem = elemRoot.Element(TAGNAME_TYPEDESCRIPTOR_OPTIONS);
                if (typeDescElem != null)
                {
                    XElement assListElem = typeDescElem.Element(TAGNAME_ASSEMBLY_LIST);
                    if (assListElem != null)
                    {
                        foreach (XElement assElem in assListElem.Elements())
                        {
                            if (assElem.Name.LocalName == TAGNAME_ASSEMBLY)
                            {
                                XAttribute pathAttr = assElem.Attribute(TAGNAME_PATH_ATTR);
                                if (pathAttr != null && !string.IsNullOrEmpty(pathAttr.Value))
                                {
                                    this.TypeDescriptorAssemblyList.Add(pathAttr.Value);
                                }
                            }
                        }
                    }
                }
            }

            if (this.TypeDescriptorAssemblyList.Count == 0)
                this.TypeDescriptorAssemblyList.AddRange(s_defaultAssemblyList);
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            XElement elemRoot = OpenOptionsFile();
            XElement typeDescElem = null;
            XElement assListElem = null;

            if (elemRoot != null)
            {
                typeDescElem = elemRoot.Element(TAGNAME_TYPEDESCRIPTOR_OPTIONS);
                if (typeDescElem != null)
                {
                    assListElem = typeDescElem.Element(TAGNAME_ASSEMBLY_LIST);
                }
            }
            else
            {
                elemRoot = new XElement(TAGNAME_ROOT);
            }
 
            if (typeDescElem == null)
            {
                typeDescElem = new XElement(TAGNAME_TYPEDESCRIPTOR_OPTIONS);
                elemRoot.Add(typeDescElem);
            }

            if (assListElem == null)
            {
                assListElem = new XElement(TAGNAME_ASSEMBLY_LIST);
                typeDescElem.Add(assListElem);
            }

            assListElem.RemoveAll();
            foreach (string ass in this.TypeDescriptorAssemblyList)
            {
                assListElem.Add(new XElement(TAGNAME_ASSEMBLY, new XAttribute(TAGNAME_PATH_ATTR, ass)));
            }

            this.SaveOptionsFile(elemRoot);
        }
    }
}
