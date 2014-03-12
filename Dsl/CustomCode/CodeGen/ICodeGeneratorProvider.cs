using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCVisualDesigner
{
    public interface ICodeGeneratorProvider
    {
        List<ICodeGeneratorController> GetGeneratorList();
    }

    public interface ICodeGeneratorController
    {
        // -- controller info --
        string Name { get; }
        string Description { get; }


        // -- code generation --
        void OnGenerateCode(VDView view, string viewPath);


        // -- settings related --

        // throw SettingUIValidationException in validation failed on UI
        void OnSaveSettings(VDView rootView, string rootViewpath);

        void OnLoadSettings(VDView rootView, string rootViewPath);

        System.Windows.Forms.Control SettingControl { get; }
    }
}