using MVCVisualDesigner.CodeGenerator;
using MVCVisualDesigner.CodeGenerator.RazorCodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public class CodeGeneratorProvider : ICodeGeneratorProvider
    {
        private List<ICodeGeneratorController> m_controllerList;

        public CodeGeneratorProvider()
        {
            m_controllerList = new List<ICodeGeneratorController>();
            m_controllerList.Add(new RazorCodeGeneratorController());
        }

        public List<ICodeGeneratorController> GetGeneratorList() { return m_controllerList; }
    }

    public class RazorCodeGeneratorController : ICodeGeneratorController
    {
        public string Name { get { return "MVC Razor Generator"; } }
        public string Description { get { return "Generate MVC razor view codes"; } }


        public void OnGenerateCode(VDView view, string viewPath)
        {
            List<VDView> rootWidgets = new List<VDView>();
            rootWidgets.Add(view);
            rootWidgets.AddRange(view.GetChildren<VDView>());

            //IWidgetTreeWalkerFactory walkerFactory = new LayoutWalkerFactory();
            IWidgetTreeWalkerFactory walkerFactory = new WidgetTreeWalkerFactory();
            ICodeGeneratorFactory generatorFactory = new RazorCodeGeneratorFactory();
            foreach(VDView v in rootWidgets)
            {
                string filePath = SettingsHelper.getViewPathFromView(v, viewPath);
                filePath = System.IO.Path.Combine(filePath, v.WidgetName + ".cshtml");
                string razorCode = generatorFactory.GetCodeGenerator(v).GenerateCode(generatorFactory, walkerFactory);
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath))
                {
                    w.Write(razorCode);
                }
            }
        }

        private RazorGeneratorOptionUI m_settingControl;
        public Control SettingControl { get { return internalSettingControl; } }

        private RazorGeneratorOptionUI internalSettingControl
        {
            get
            {
                if (m_settingControl == null) m_settingControl = new RazorGeneratorOptionUI();
                return m_settingControl; 
            }
        }

        public void OnLoadSettings(VDView rootView, string rootViewPath)
        {
            this.internalSettingControl.initTreeView(rootView, rootViewPath);
        }

        public void OnSaveSettings(VDView rootView, string rootViewPath)
        {
        }
    }

    internal static class SettingsHelper
    {
        internal static readonly Guid GENERATED_VIEW_PATH = new Guid("6E08C559-1695-4F16-863C-4EF98EBCD143");

        internal static string getViewPathFromView(VDView view, string viewPath)
        {
            string path = string.Empty;
            if (view.settings.ContainsKey(SettingsHelper.GENERATED_VIEW_PATH))
                path = (string)view.settings[SettingsHelper.GENERATED_VIEW_PATH];

            if (!string.IsNullOrEmpty(viewPath))
                path = Utility.PathHelper.GetAbsolutePath(path, viewPath);

            return path;
        }

        internal static void saveViewPathToView(string path, VDView view, string viewPath)
        {
            if (!string.IsNullOrEmpty(viewPath))
                path = Utility.PathHelper.GetRelativePath(path, viewPath);

            using (var trans = view.Store.TransactionManager.BeginTransaction("Update code generation settings"))
            {
                view.settings[SettingsHelper.GENERATED_VIEW_PATH] = path;
                trans.Commit();
            }
        }
    }
}
