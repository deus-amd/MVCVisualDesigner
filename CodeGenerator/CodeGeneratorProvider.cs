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
        public string FileExtension { get { return "cshtml"; } }


        public void OnGenerateCode(VDView view, string viewPath)
        {
            List<VDWidget> rootWidgets = new List<VDWidget>();
            rootWidgets.Add(view);
            rootWidgets.AddRange(view.GetChildren<VDView>());

            //IWidgetTreeWalkerFactory walkerFactory = new LayoutWalkerFactory();
            IWidgetTreeWalkerFactory walkerFactory = new WidgetTreeWalkerFactory();
            ICodeGeneratorFactory generatorFactory = new RazorCodeGeneratorFactory();
            foreach(VDWidget v in rootWidgets)
            {
                string filePath = v.WidgetName + "." + FileExtension; //todo: get file path
                string razorCode = generatorFactory.GetCodeGenerator(v).GenerateCode(generatorFactory, walkerFactory);
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath))
                {
                    w.Write(razorCode);
                }
            }
        }


        private RazorGeneratorOptionUI m_settingControl;
        public Control SettingControl
        {
            get 
            { 
                if (m_settingControl == null) m_settingControl = new RazorGeneratorOptionUI();
                return m_settingControl; 
            }
        }

        public void OnLoadSettings(VDWidget widget)
        {
            // todo: init controls
        }

        public void OnSaveSettings(VDWidget widget)
        {
        }
    }
}
