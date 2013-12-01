using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [ProvideToolWindow(typeof(DeployToolWindow), MultiInstances = false, Style = VsDockStyle.Linked,
        Orientation = ToolWindowOrientation.Bottom, Window = "{D78612C7-9962-4B83-95D9-268046DAD23A}")] // tab to bottom of error list window
    [ProvideToolWindowVisibility(typeof(DeployToolWindow), Constants.MVCVisualDesignerEditorFactoryId)]
    [ProvideToolWindow(typeof(ModelToolWindow), MultiInstances = false, Style = VsDockStyle.Linked,
        Orientation = ToolWindowOrientation.Bottom, Window = "{D78612C7-9962-4B83-95D9-268046DAD23A}")] // tab to bottom of error list window
    [ProvideToolWindowVisibility(typeof(ModelToolWindow), Constants.MVCVisualDesignerEditorFactoryId)]
    internal abstract partial class MVCVisualDesignerPackageBase
    {
        partial void InitializeExtensions()
        {
            // Register the deployment tool window for this DSL.
            this.AddToolWindow(typeof(DeployToolWindow));

            // Register the model tool window for this DSL.
            this.AddToolWindow(typeof(ModelToolWindow));
        }
    }
}
