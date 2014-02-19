using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDTextShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        protected override void SetAbsoluteBoundsValue(Microsoft.VisualStudio.Modeling.Diagrams.RectangleD newValue)
        {
            if (newValue.Height != 0.2) newValue.Height = 0.2;
            base.SetAbsoluteBoundsValue(newValue);
        }
    }
}
