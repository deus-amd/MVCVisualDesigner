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
        string FileExtension { get; }


        // -- code generation --
        void OnGenerateCode(VDView view, string viewPath);


        // -- settings related --

        // throw SettingUIValidationException in validation failed on UI
        void OnSaveSettings(VDWidget widget);

        void OnLoadSettings(VDWidget widget);

        System.Windows.Forms.Control SettingControl { get; }
    }
}