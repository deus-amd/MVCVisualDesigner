using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [System.Runtime.InteropServices.Guid("000f27a9-6a82-4ec6-9af4-83fa5ecd0dbb")]
    public class DeployToolWindow : ToolWindow
    {
        public DeployToolWindow(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        private DeployToolWindowForm m_form;
        public override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                if (m_form == null)
                {
                    m_form = new DeployToolWindowForm();
                }
                return m_form;
            }
        }

        protected override void OnDocumentWindowChanged(ModelingDocView oldView, ModelingDocView newView)
        {
            base.OnDocumentWindowChanged(oldView, newView);
        }

        public override string WindowTitle { get { return "Deploy Window"; } }
    }
}
