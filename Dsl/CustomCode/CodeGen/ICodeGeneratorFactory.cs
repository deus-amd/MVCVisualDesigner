using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public interface ICodeGeneratorFactory
    {
        ICodeGenerator GetCodeGenerator(VDWidget widget);

        string Name { get; }
        string Description { get; }
    }
}
