using Microsoft.VisualStudio.Shell;
using MVCVisualDesigner.TypeDescriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [ProvideToolWindow(typeof(ModelToolWindow), MultiInstances = false, Style = VsDockStyle.Linked,
        Orientation = ToolWindowOrientation.Bottom, Window = "{D78612C7-9962-4B83-95D9-268046DAD23A}")] // tab to bottom of error list window
    [ProvideToolWindowVisibility(typeof(ModelToolWindow), Constants.MVCVisualDesignerEditorFactoryId)]
    // options
    [ProvideOptionPage(typeof(CodeGeneratorOptionPage), "MVC Visual Designer", "Code Generators", 0, 0, true)]
    [ProvideOptionPage(typeof(PredefinedTypeOptionPage), "MVC Visual Designer", "Predefined Types", 0, 0, true)]
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

        // load from options
        public List<IMVDTypeDescriptor> GetAllTypeDescriptors()
        {
            List<IMVDTypeDescriptor> tdList = new List<IMVDTypeDescriptor>();
            PredefinedTypeOptionPage dlg = this.GetDialogPage(typeof(PredefinedTypeOptionPage)) as PredefinedTypeOptionPage;
            if (dlg != null)
            {
                foreach(string relativeAssPath in dlg.TypeDescriptorAssemblyList)
                {
                    List<IMVDTypeDescriptor> tds = GetTypeDescriptors(relativeAssPath);
                    tdList.AddRange(tds);
                }
            }

            tdList.Sort((x, y) => string.Compare(x.FullName, y.FullName));
            return tdList;
        }

        public HashSet<string> GetAllTypeDescriptorNames()
        {
            List<IMVDTypeDescriptor> tdList = GetAllTypeDescriptors();
            if (tdList == null || tdList.Count <= 0) return null;

            HashSet<string> names = new HashSet<string>();
            tdList.ForEach(td => {
                if (!names.Contains(td.FullName)) names.Add(td.FullName);
            });

            return names;
        }

        public List<IMVDTypeDescriptor> GetTypeDescriptors(string assemblyPath)
        {
            List<IMVDTypeDescriptor> tdList = new List<IMVDTypeDescriptor>();
            if (string.IsNullOrWhiteSpace(assemblyPath)) return tdList;

            Assembly assem = Assembly.LoadFrom(assemblyPath);
            if (assem == null) return tdList;

            foreach (Type t in assem.GetTypes())
            {
                try
                {
                    if (t.IsAbstract || t.GetInterface("IMVDTypeDescriptor") == null) continue;

                    IMVDTypeDescriptor td = Activator.CreateInstance(t) as IMVDTypeDescriptor;
                    if (td != null) tdList.Add(td);
                }
                catch //(Exception ex)
                { 
                    //todo: output to ErrorList window
                }
            }
            tdList.Sort((x, y) => string.Compare(x.FullName, y.FullName));
            return tdList;
        }

        public bool IsPredefinedType(string nameSpace, string name)
        {
            string fullName = string.IsNullOrEmpty(nameSpace) ? name : nameSpace + "." + name;
            return IsPredefinedType(fullName);
        }

        public bool IsPredefinedType(string fullName)
        {
            List<IMVDTypeDescriptor> types = GetAllTypeDescriptors();
            return types.Find(td => td.FullName == fullName) != null;
        }
    }
}
