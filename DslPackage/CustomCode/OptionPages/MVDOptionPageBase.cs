using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace MVCVisualDesigner
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class MVDOptionPageBase<TControl> : DialogPage
        where TControl : MVDOptionPageControlBase, new()
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override IWin32Window Window
        {
            get
            {
                if (m_control == null)
                {
                    m_control = new TControl();
                    m_control.Initialize(this);
                }
                return m_control;
            }
        }
        private TControl m_control;

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

        protected const string OPTIONS_FILE_NAME = "MVCVisualDesignerOptions.xml";
        protected const string TAGNAME_ROOT = "mvd";
        protected const string TAGNAME_ASSEMBLY_LIST = "assembly_list";
        protected const string TAGNAME_ASSEMBLY = "assembly";
        protected const string TAGNAME_PATH_ATTR = "path";

        protected System.Xml.Linq.XElement OpenOptionsFile()
        {
            if (this.Package == null) return null;

            string path = System.IO.Path.Combine(this.Package.UserLocalDataPath, OPTIONS_FILE_NAME);
            if (!System.IO.File.Exists(path)) return null;

            try
            {
                System.Xml.Linq.XElement elemRoot = System.Xml.Linq.XElement.Load(path);
                if (elemRoot != null && elemRoot.Name.LocalName == TAGNAME_ROOT)
                    return elemRoot;
            }
            catch
            {
            }

            return null;
        }

        protected void SaveOptionsFile(System.Xml.Linq.XElement rootElem)
        {
            if (this.Package == null) return;

            string path = System.IO.Path.Combine(this.Package.UserLocalDataPath, OPTIONS_FILE_NAME);
            rootElem.Save(path);
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

    public class MVDOptionPageControlBase : UserControl
    {
        public MVDOptionPageControlBase()
        {
        }

        protected DialogPage m_optionPage;
        internal DialogPage OptionPage { get { return m_optionPage; } }

        internal virtual void Initialize(DialogPage optionPage)
        {
            m_optionPage = optionPage;
        }
    }
}
