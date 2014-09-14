using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator
{
    public interface IJavaScriptCodeGenerator
    {
        /// <summary>
        /// Generate widget initialization code, event binding etc.
        /// </summary>
        /// <param name="eventSource"></param>
        /// <param name="jsGenFactory"></param>
        /// <returns></returns>
        string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory);

        /// <summary>
        /// Generate code to call corresponding action on target widget
        /// For jQuery widgets, the generated code will call methods of that widget, but for other cases, may 
        /// just call some jQuery utility mehtods, such as show/hide/remove elements from DOM tree.
        /// </summary>
        /// <param name="eventSource"></param>
        /// <param name="eventTarget"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action);

        /// <summary>
        /// Generate code to collect model values from widget, different widgets may use different ways to get 
        /// values, such as for an input, $("xxx").val() is enough, but for widgets such as TableRow or others, 
        /// may call a specific API on that widget.
        /// </summary>
        /// <param name="widget"></param>
        /// <returns></returns>
        string GenerateGettingWidgetValueCode(VDWidget widget);
    }

    public interface IJavaScriptCodeGeneratorFactory
    {
        IJavaScriptCodeGenerator GetCodeGenerator(VDWidget widget);
    }
}
