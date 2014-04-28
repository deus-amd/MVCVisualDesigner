using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [System.Runtime.InteropServices.Guid("771FA2A1-90B9-4E2E-81F1-1F40DD033D59")]
    public class ModelToolWindow : ToolWindow
    {
        private const string GUID_MODEL_TOOL_WINDOW = "{771FA2A1-90B9-4E2E-81F1-1F40DD033D59}";

        public ModelToolWindow(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        private ModelToolWindowForm m_form;
        public override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                if (m_form == null)
                {
                    m_form = new ModelToolWindowForm(this);
                }
                return m_form;
            }
        }

        protected override void OnDocumentWindowChanged(ModelingDocView oldView, ModelingDocView newView)
        {
            base.OnDocumentWindowChanged(oldView, newView);
            if (newView == null)
            {
                this.HideWindow();

            }
            else if (newView is MVCVisualDesignerDocView)
            {
                ShowViewModel((VDView)(newView.DocData.RootElement));
            }
        }

        public override string WindowTitle { get { return "Model Window"; } }

        public void ShowWidgetModel()
        {
            m_selectedWidget = null;

            if (this.Window != null)
            {
                m_form.ShowWidgetModel();
            }
        }

        public void ShowActionModel()
        {
            m_selectedWidget = null;

            if (this.Window != null)
            {
                m_form.ShowActionModel();
            }
        }

        public void ShowViewModel(VDView view)
        {
            m_selectedWidget = view;

            if (this.Window != null)
            {
                m_form.ShowViewModel(view);
            }
        }

        public void HideWindow()
        {
            this.Hide();
            if (this.Window != null)
            {
                m_form.ClearWindow();
            }
        }

        private VDWidget m_selectedWidget = null;

        //
        private MVCVisualDesignerPackage m_package;
        internal MVCVisualDesignerPackage GetPackage()
        {
            if (m_package == null)
            {
                m_package = this.GetService(typeof(MVCVisualDesignerPackage)) as MVCVisualDesignerPackage;
            }
            return m_package;
        }

        ////////////////////////////////////////////////////////////////////////////////
        private EnvDTE.WindowEvents WindowEvents { get; set; }
        private void WindowEvents_WindowActivated(EnvDTE.Window gotFocusWindow, EnvDTE.Window lostFocusWindow)
        {
            // Model Window got focus
            if (gotFocusWindow != null && string.Compare(gotFocusWindow.ObjectKind, GUID_MODEL_TOOL_WINDOW, true) == 0)
            {
                if (m_selectedWidget != null)
                {
                    this.SetSelectedComponents(new VDWidget[] { m_selectedWidget });
                }
                else
                {
                    this.SetSelectedComponents(null);
                }
            }
            // Model Window lost focus
            else if (lostFocusWindow != null && string.Compare(lostFocusWindow.ObjectKind, GUID_MODEL_TOOL_WINDOW, true) ==0)
            {
                if (this.Window != null) m_form.OnLostFocus();
            }
        }

        protected override void OnToolWindowCreate()
        {
            base.OnToolWindowCreate();

            EnvDTE.DTE dte = (EnvDTE.DTE)GetService(typeof(EnvDTE.DTE));
            EnvDTE80.Events2 events = (EnvDTE80.Events2)dte.Events;
            this.WindowEvents = (EnvDTE.WindowEvents)events.get_WindowEvents(null);
            this.WindowEvents.WindowActivated += new EnvDTE._dispWindowEvents_WindowActivatedEventHandler(WindowEvents_WindowActivated);
        }

        protected override void OnClose()
        {
            base.OnClose();

            this.WindowEvents.WindowActivated -= new EnvDTE._dispWindowEvents_WindowActivatedEventHandler(WindowEvents_WindowActivated);
        }
    }
}
