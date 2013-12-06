using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    internal partial class MVCVisualDesignerCommandSet
    {
        private static readonly CommandID generateCodeCommand = new CommandID(new Guid(Constants.MVCVisualDesignerCommandSetId), 0x3001);
        private static readonly CommandID ShowModelWindowCommand = new CommandID(new Guid(Constants.MVCVisualDesignerCommandSetId), 0x3002);
        private static readonly CommandID ShowDeployWindowCommand = new CommandID(new Guid(Constants.MVCVisualDesignerCommandSetId), 0x3003);

        protected override IList<MenuCommand> GetMenuCommands()
        {
            IList<MenuCommand> commands = base.GetMenuCommands();

            // show model window
            MenuCommand menuCommand = new CommandContextBoundMenuCommand(
                this.ServiceProvider,
                (sender, e) => { if (this.ModelToolWindow != null) this.ModelToolWindow.Show(); },
                ShowModelWindowCommand,
                typeof(MVCVisualDesignerEditorFactory).GUID);
            commands.Add(menuCommand);

            // show deploy window
            menuCommand = new CommandContextBoundMenuCommand(
                this.ServiceProvider,
                (sender, e) => { if (this.DeployToolWindow != null) this.DeployToolWindow.Show(); },
                ShowDeployWindowCommand,
                typeof(MVCVisualDesignerEditorFactory).GUID);
            commands.Add(menuCommand);

            // generate code command
            menuCommand = new CommandContextBoundMenuCommand(
                this.ServiceProvider,
                onGenerateCodeCommand,
                generateCodeCommand,
                typeof(MVCVisualDesignerEditorFactory).GUID);
            commands.Add(menuCommand);

            return commands;
        }

        /// <summary>
        /// Returns the model tool window.
        /// </summary>
        protected ModelToolWindow ModelToolWindow
        {
            get
            {
                ModelToolWindow window = null;
                ModelingPackage package = this.ServiceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Package)) as ModelingPackage;
                if (package != null)
                {
                    window = package.GetToolWindow(typeof(ModelToolWindow), true) as ModelToolWindow;
                }

                return window;
            }
        }

        /// <summary>
        /// Returns the deployment tool window.
        /// </summary>
        protected DeployToolWindow DeployToolWindow
        {
            get
            {
                DeployToolWindow window = null;
                ModelingPackage package = this.ServiceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Package)) as ModelingPackage;
                if (package != null)
                {
                    window = package.GetToolWindow(typeof(DeployToolWindow), true) as DeployToolWindow;
                }

                return window;
            }
        }


        // generate code
        protected void onGenerateCodeCommand(object sender, EventArgs args)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            if ( dlg.ShowDialog()!= System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (this.CurrentMVCVisualDesignerDocData != null 
                && this.CurrentMVCVisualDesignerDocData.RootElement != null
                && this.CurrentMVCVisualDesignerDocData.RootElement is VDView)
            {
                ICodeGeneratorFactory cgFactory = new MVCVisualDesigner.CodeGenerator.RazorCodeGeneratorFactory();
                //IWidgetTreeWalkerFactory walkerFactory = new LayoutWalkerFactory();
                IWidgetTreeWalkerFactory walkerFactory = new WidgetTreeWalkerFactory();
                VDView view = this.CurrentMVCVisualDesignerDocData.RootElement as VDView;
                string razorCode = view.GenerateCode(cgFactory, walkerFactory);

                using (System.IO.StreamWriter w = new System.IO.StreamWriter(dlg.FileName))
                {
                    w.Write(razorCode);
                }
            }
        }
    }
}
