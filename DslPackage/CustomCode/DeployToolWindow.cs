using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private DeployToolWindowForm DeployForm 
        {
            get
            {
                if (m_form == null)
                {
                    m_form = new DeployToolWindowForm();
                    m_form.ToolWindow = this;
                }
                return m_form;
            }
        }

        public override System.Windows.Forms.IWin32Window Window { get { return DeployForm; } }

        public override string WindowTitle { get { return "Deployment Window"; } }

        protected override void OnDocumentWindowChanged(ModelingDocView oldView, ModelingDocView newView)
        {
            base.OnDocumentWindowChanged(oldView, newView);

            MVCVisualDesignerDocView docView = newView as MVCVisualDesignerDocView;
            if (docView == null) return;

            //if (newView != null)
            //{
            //    ShapeElement shape = newView.PrimarySelection as ShapeElement;
            //    if (shape.ModelElement is QSObject)
            //    {
            //        SetPropertyObjecs((QSObject)shape.ModelElement);
            //        return;
            //    }
            //    else if (shape.ModelElement is QSModel)
            //    {
            //        SetPropertyObjecs((QSModel)shape.ModelElement);
            //        return;
            //    }
            //    SetPropertyObjecs((QSModel)null);
            //}
        }

        public override int SaveUIState(out System.IO.Stream stateStream)
        {
            return base.SaveUIState(out stateStream);
        }

        public override int LoadUIState(System.IO.Stream stateStream)
        {
            return base.LoadUIState(stateStream);
        }

        protected override void OnToolWindowCreate()
        {
            base.OnToolWindowCreate();
            InitializeDeployWindowForm();
        }

        internal void InitializeDeployWindowForm()
        {
            if (this.Package == null) return;

            List<string> assemList = this.Package.GetCodeGeneratorAssemblyList();
            DeployForm.InitializeForm(assemList);
        }

        private MVCVisualDesignerPackage m_package;
        private MVCVisualDesignerPackage Package
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
    }
}
