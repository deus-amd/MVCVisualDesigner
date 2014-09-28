using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MEL
namespace MVCVisualDesigner
{
    public partial class VDButton
    {
    }
}

// PEL
namespace MVCVisualDesigner
{
    public partial class VDButtonShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }
    }
}