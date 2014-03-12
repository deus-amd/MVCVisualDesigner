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
        //private static readonly CommandID ShowDeployWindowCommand = new CommandID(new Guid(Constants.MVCVisualDesignerCommandSetId), 0x3003);

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

        // generate code
        protected void onGenerateCodeCommand(object sender, EventArgs args)
        {
            if (this.Package == null) return;

            if (this.CurrentMVCVisualDesignerDocData != null 
                && this.CurrentMVCVisualDesignerDocData.RootElement != null
                && this.CurrentMVCVisualDesignerDocData.RootElement is VDView)
            {
                CodeGenerationForm deployWin = new CodeGenerationForm(this.ServiceProvider);
                deployWin.InitializeForm(
                    this.Package.GetCodeGeneratorAssemblyList(), 
                    (VDView)(this.CurrentMVCVisualDesignerDocData.RootElement),
                    this.activeDocumentPath);

                if (deployWin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    deployWin.Close();
                    System.Windows.Forms.MessageBox.Show("Files are generated successfully", "Message",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }

        private MVCVisualDesignerPackage m_package;
        private MVCVisualDesignerPackage Package
        {
            get
            {
                if (m_package == null)
                {
                    m_package = this.ServiceProvider.GetService(typeof(MVCVisualDesignerPackage)) as MVCVisualDesignerPackage;
                }
                return m_package;
            }
        }

#region Shell Handling
        private string activeDocumentPath
        {
            get
            {
                if (this.activeDocument != null) return this.activeDocument.Path;
                return null;
            }
        }

        private bool isProjectOpen
        {
            get
            {
                if (this.activeDocument != null &&
                    this.activeDocument.ProjectItem != null &&
                    this.activeDocument.ProjectItem.ProjectItems != null)
                    return true;
                else
                    return false;
            }
        }

        private EnvDTE.Document activeDocument
        {
            get
            {
                if (this.dte == null) return null;
                return dte.ActiveDocument;
            }
        }

        private EnvDTE.Project curProject
        {
            get
            {
                if (this.dte == null) return null;

                object obj = this.dte.ActiveSolutionProjects;
                if (obj == null) return null;

                object[] projects = (object[])obj;

                if (projects != null && projects.Length > 0)
                    return projects[0] as EnvDTE.Project;
                else
                    return null;
            }
        }

        private EnvDTE.DTE m_dte;
        private EnvDTE.DTE dte
        {
            get
            {
                if (m_dte == null)
                {
                    m_dte = ServiceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                }
                return m_dte;
            }
        }
#endregion
    }
}