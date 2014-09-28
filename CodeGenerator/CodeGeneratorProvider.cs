using MVCVisualDesigner.CodeGenerator;
using MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator;
using MVCVisualDesigner.CodeGenerator.ModelCodeGenerator;
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
            m_controllerList.Add(new CSModelCodeGeneratorController());
            m_controllerList.Add(new JavaScriptCodeGeneratorController());
        }

        public List<ICodeGeneratorController> GetGeneratorList() { return m_controllerList; }
    }

    public class RazorCodeGeneratorController : ICodeGeneratorController
    {
        public string Name { get { return "MVC Razor Generator"; } }
        public string Description { get { return "Generate MVC razor view code"; } }


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

        public Control SettingControl { get { return internalSettingControl; } }

        private RazorGeneratorOptionUI m_settingControl;
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

    public class CSModelCodeGeneratorController : ICodeGeneratorController
    {
        public string Name { get { return "C# Model Generator"; } }

        public string Description { get { return "Generate C# model code"; } }

        public void OnGenerateCode(VDView view, string viewPath)
        {
            ////////VDModelStore modelStore = view.ModelStore;
            ////////if (modelStore == null) return;

            ////////// generate code
            ////////CSModelGenerator generator = new CSModelGenerator(view);
            ////////string csModelCode = generator.TransformText();
            ////////if (string.IsNullOrEmpty(csModelCode)) return;

            ////////// save file
            ////////string filePath = SettingsHelper.getViewPathFromView(view, viewPath);
            ////////filePath = System.IO.Path.Combine(filePath, view.WidgetName + "_models.cs");
            ////////using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath))
            ////////{
            ////////    w.Write(csModelCode);
            ////////}
        }

        public void OnLoadSettings(VDView rootView, string rootViewPath)
        {
            this.internalSettingControl.initView(rootView, rootViewPath);
        }

        public void OnSaveSettings(VDView rootView, string rootViewpath)
        {
        }

        public Control SettingControl { get { return internalSettingControl; } }

        private CSModelGeneratorOptionUI m_settingControl;
        private CSModelGeneratorOptionUI internalSettingControl
        {
            get
            {
                if (m_settingControl == null) m_settingControl = new CSModelGeneratorOptionUI();
                return m_settingControl;
            }
        }
    }

    public class JavaScriptCodeGeneratorController : ICodeGeneratorController
    {
        public string Name { get { return "JavaScript Generator"; } }

        public string Description { get { return "Generate JavaScript widget initialization and event handling code"; } }

        public void OnGenerateCode(VDView view, string viewPath)
        {
        }

        public void OnLoadSettings(VDView rootView, string rootViewPath)
        {
            this.internalSettingControl.initTreeView(rootView, rootViewPath);
        }

        public void OnSaveSettings(VDView rootView, string rootViewpath)
        {
        }

        public Control SettingControl { get { return internalSettingControl; } }

        private JavaScriptGeneratorOptionUI m_settingControl;
        private JavaScriptGeneratorOptionUI internalSettingControl
        {
            get
            {
                if (m_settingControl == null) m_settingControl = new JavaScriptGeneratorOptionUI();
                return m_settingControl;
            }
        }
    }

    internal static class SettingsHelper
    {
        internal static readonly Guid GENERATED_VIEW_PATH = new Guid("6E08C559-1695-4F16-863C-4EF98EBCD143");
        internal static readonly Guid GENERATED_CS_MODEL_PATH = new Guid("26A1177A-CB80-4D1E-9728-DF7DC3E5299E");
        internal static readonly Guid GENERATED_JAVASCRIPT_PATH = new Guid("F5A4D0AE-C59A-43F3-96F5-BA2CBF12F845");

        internal static string getViewPathFromView(VDView view, string viewPath)
        {
            return getPathFromView(SettingsHelper.GENERATED_VIEW_PATH, view, viewPath);
        }

        internal static void saveViewPathToView(string path, VDView view, string viewPath)
        {
            savePathToView(SettingsHelper.GENERATED_VIEW_PATH, path, view, viewPath);
        }

        internal static string getCSModelPathFromView(VDView view, string viewPath)
        {
            return getPathFromView(SettingsHelper.GENERATED_CS_MODEL_PATH, view, viewPath);
        }

        internal static void saveCSModelPathToView(string path, VDView view, string viewPath)
        {
            savePathToView(SettingsHelper.GENERATED_CS_MODEL_PATH, path, view, viewPath);
        }

        internal static string getPathFromView(Guid propGuid, VDView view, string viewPath)
        {
            string path = string.Empty;
            if (view.settings.ContainsKey(propGuid))
                path = (string)view.settings[propGuid];

            if (!string.IsNullOrEmpty(viewPath))
                path = Utility.PathHelper.GetAbsolutePath(path, viewPath);

            return path;
        }

        internal static void savePathToView(Guid propGuid, string path, VDView view, string viewPath)
        {
            if (!string.IsNullOrEmpty(viewPath))
                path = Utility.PathHelper.GetRelativePath(path, viewPath);

            using (var trans = view.Store.TransactionManager.BeginTransaction("Update code generation settings"))
            {
                view.settings[propGuid] = path;
                trans.Commit();
            }
        }
    }
}
