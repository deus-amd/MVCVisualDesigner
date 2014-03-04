using MVCVisualDesigner.CodeGenerator;
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
        public List<ICodeGeneratorFactory> GetGeneratorList()
        {
            List<ICodeGeneratorFactory> factories = new List<ICodeGeneratorFactory>();
            factories.Add(new RazorCodeGeneratorFactory());
            return factories;
        }

        public Control GetGeneratorOptionsUI(ICodeGeneratorFactory factory)
        {
            if (factory is RazorCodeGeneratorFactory)
                return new MVCVisualDesigner.CodeGenerator.RazorCodeGenerator.RazorGeneratorOptionUI();
            throw new NotImplementedException();
        }
    }
}
