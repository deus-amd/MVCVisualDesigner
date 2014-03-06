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
    public class CodeGeneratorOptionPage : DialogPage
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
        private List<string> m_codeGeneratorAssemblyList = new List<string>() 
        {
            "./CodeGenerator.dll" 
        };

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get
            {
                if (m_control == null)
                {
                    m_control = new CodeGeneratorOptionPageControl();
                    m_control.OptionPage = this;
                    m_control.Initialize();
                }
                return m_control;
            }
        }
        private CodeGeneratorOptionPageControl m_control;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal MVCVisualDesignerPackage Package 
        {
            get
            {
                if (m_package == null)
                {
                    m_package = this.GetService(typeof(MVCVisualDesignerPackage)) as MVCVisualDesignerPackage;
                }
                return m_package;
            }
        }
        private MVCVisualDesignerPackage m_package;

        private const string OPTIONS_FILE_NAME = "MVCVisualDesignerOptions.xml";
        private const string TAGNAME_ROOT = "mvd";
        private const string TAGNAME_GENERATOR_OPTIONS = "generator_options";
        private const string TAGNAME_ASSEMBLY_LIST = "assembly_list";
        private const string TAGNAME_ASSEMBLY = "assembly";
        private const string TAGNAME_PATH_ATTR = "path";
        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            if (this.Package == null) return;
            string path = System.IO.Path.Combine(this.Package.UserLocalDataPath, OPTIONS_FILE_NAME);
            try
            {
                XElement elemRoot = XElement.Load(path);
                if (elemRoot == null) return;
                if (elemRoot.Name.LocalName != TAGNAME_ROOT) return;

                this.CodeGeneratorAssemblyList.Clear();

                XElement geneElem = elemRoot.Element(TAGNAME_GENERATOR_OPTIONS);
                if (geneElem != null)
                {
                    XElement assListElem = geneElem.Element(TAGNAME_ASSEMBLY_LIST);
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
            catch 
            { 
                // ignore exception
            }
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();
            if (this.Package != null)
            {
                string path = System.IO.Path.Combine(this.Package.UserLocalDataPath, OPTIONS_FILE_NAME);
                XElement elemRoot = new XElement(TAGNAME_ROOT);
                XElement genElem = new XElement(TAGNAME_GENERATOR_OPTIONS);
                XElement assList = new XElement(TAGNAME_ASSEMBLY_LIST);
                elemRoot.Add(genElem);
                genElem.Add(assList);
                foreach (string ass in this.CodeGeneratorAssemblyList)
                {
                    assList.Add(new XElement(TAGNAME_ASSEMBLY, new XAttribute(TAGNAME_PATH_ATTR, ass)));
                }
                elemRoot.Save(path);
            }
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            base.OnApply(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
