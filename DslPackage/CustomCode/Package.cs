using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [ProvideToolWindow(typeof(ModelToolWindow), MultiInstances = false, Style = VsDockStyle.Linked,
        Orientation = ToolWindowOrientation.Bottom, Window = "{D78612C7-9962-4B83-95D9-268046DAD23A}")] // tab to bottom of error list window
    [ProvideToolWindowVisibility(typeof(ModelToolWindow), Constants.MVCVisualDesignerEditorFactoryId)]
    // options
    [ProvideOptionPage(typeof(CodeGeneratorOptionPage), "MVC Visual Designer", "Code Generators", 0, 0, true)]
    internal abstract partial class MVCVisualDesignerPackageBase
    {
        partial void InitializeExtensions()
        {
            // Register the model tool window for this DSL.
            this.AddToolWindow(typeof(ModelToolWindow));
        }

        public List<string> GetCodeGeneratorAssemblyList()
        {
            List<string> list = new List<string>();
            CodeGeneratorOptionPage dlg = this.GetDialogPage(typeof(CodeGeneratorOptionPage)) as CodeGeneratorOptionPage;
            if (dlg != null)
            {
                foreach (string relativePath in dlg.CodeGeneratorAssemblyList)
                {
                    list.Add(Utility.PathHelper.GetAbsolutePath(relativePath));
                }
            }
            return list;
        }

        public List<string> GetPredefinedTypes()
        {
            return null;
        }
    }
}
