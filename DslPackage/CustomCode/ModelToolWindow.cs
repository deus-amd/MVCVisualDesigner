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
                    m_form = new ModelToolWindowForm();
                }
                return m_form;
            }
        }

        protected override void OnDocumentWindowChanged(ModelingDocView oldView, ModelingDocView newView)
        {
            base.OnDocumentWindowChanged(oldView, newView);
        }

        public override string WindowTitle { get { return "Model Window"; } }
    }
}
