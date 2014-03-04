using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCVisualDesigner
{
    public interface ICodeGeneratorProvider
    {
        List<ICodeGeneratorFactory> GetGeneratorList();
        System.Windows.Forms.Control GetGeneratorOptionsUI(ICodeGeneratorFactory factory);
    }
}
