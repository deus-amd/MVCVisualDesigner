using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.Utility
{
    public static class NumberHelper
    {
        public const double DOUBLE_DIFFERENCE = 0.0001;
    }

    public static class Helper
    {
        public static void SplitFullName(string fullTypeName, out string nameSpace, out string typeName)
        {
            int idx = fullTypeName.LastIndexOf('.');
            if (idx > 0)
            {
                nameSpace = fullTypeName.Substring(0, idx);
                typeName = fullTypeName.Substring(idx + 1);
            }
            else
            {
                typeName = fullTypeName;
                nameSpace = null;
            }
        }
    }
}
