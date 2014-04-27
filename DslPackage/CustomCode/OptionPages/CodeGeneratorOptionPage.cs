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
    public class CodeGeneratorOptionPage : MVDOptionPageBase<CodeGeneratorOptionPageControl>
    {
        [Category("Code Generator")]
        [DisplayName("Code Generator Assembly List")]
        [Description("List of assembly which contains code generator classes")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> CodeGeneratorAssemblyList 
        {
            get 
            {
                if (m_codeGeneratorAssemblyList == null) m_codeGeneratorAssemblyList = new List<string>();
                return m_codeGeneratorAssemblyList;
            }
            set { m_codeGeneratorAssemblyList = value; } 
        }
        private List<string> m_codeGeneratorAssemblyList = null;

        private static readonly List<string> s_defaultAssemblyList = new List<string>()
        {
            "./CodeGenerator.dll" 
        };


        private const string TAGNAME_GENERATOR_OPTIONS = "generator_options";
        //
        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            XElement elemRoot = OpenOptionsFile();
            if (elemRoot != null)
            {
                this.CodeGeneratorAssemblyList.Clear();

                XElement typeDescElem = elemRoot.Element(TAGNAME_GENERATOR_OPTIONS);
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
                                    this.CodeGeneratorAssemblyList.Add(pathAttr.Value);
                                }
                            }
                        }
                    }
                }
            }

            if (this.CodeGeneratorAssemblyList.Count == 0)
                this.CodeGeneratorAssemblyList.AddRange(s_defaultAssemblyList);
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            XElement elemRoot = OpenOptionsFile();
            XElement typeDescElem = null;
            XElement assListElem = null;

            if (elemRoot != null)
            {
                typeDescElem = elemRoot.Element(TAGNAME_GENERATOR_OPTIONS);
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
                typeDescElem = new XElement(TAGNAME_GENERATOR_OPTIONS);
                elemRoot.Add(typeDescElem);
            }

            if (assListElem == null)
            {
                assListElem = new XElement(TAGNAME_ASSEMBLY_LIST);
                typeDescElem.Add(assListElem);
            }

            assListElem.RemoveAll();
            foreach (string ass in this.CodeGeneratorAssemblyList)
            {
                assListElem.Add(new XElement(TAGNAME_ASSEMBLY, new XAttribute(TAGNAME_PATH_ATTR, ass)));
            }

            this.SaveOptionsFile(elemRoot);
        }
    }
}
