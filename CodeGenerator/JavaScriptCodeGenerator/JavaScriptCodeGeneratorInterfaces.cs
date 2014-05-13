using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator
{
    public interface IJavaScriptCodeGenerator
    {
        string GenerateWidgetInitCode(VDWidget eventSource, VDClientAction action, IJavaScriptCodeGeneratorFactory targetFactory);
        string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action);
    }

    public interface IJavaScriptCodeGeneratorFactory
    {
        IJavaScriptCodeGenerator GetCodeGenerator(VDWidget widget);
    }
}
