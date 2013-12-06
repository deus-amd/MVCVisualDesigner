using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public interface ICodeData
    {
        string GenerateCode(ICodeGeneratorFactory cGFactory, IWidgetTreeWalkerFactory walkerFactory);
    }
}
