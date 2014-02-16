using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDInternalUtility
    {
        protected override bool PropagateDeletingToParent { get { return true; } }
    }

    public static class VDConstants
    {
        public const double DOUBLE_DIFF = 0.0001;
    }
}
