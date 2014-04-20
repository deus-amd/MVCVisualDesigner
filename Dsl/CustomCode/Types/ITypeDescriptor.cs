using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.TypeDescriptor
{
    public interface IMVDTypeDescriptor
    {
        string Name { get; }
        string NameSpace { get; }

        //
        object ParseValue(string raw);
        bool TryParseValue(string raw, out object value);
        string ValueToString(object value);

        // Code Generation
        string GenerateCSValidationCode();
        string GenerateJSValidationCode();
        string GenerateHTMLAttributes();
        string GenerateJSAcceptChars();
    }
}
