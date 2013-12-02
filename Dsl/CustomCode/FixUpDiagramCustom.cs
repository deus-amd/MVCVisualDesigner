using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;

namespace MVCVisualDesigner
{
    internal sealed partial class FixUpDiagram : FixUpDiagramBase
    {
//        private ModelElement GetParentForHorizonalSeparator(HorizonalSeparator childElement)
//        {
//            return childElement.Parent;
//        }

        private ModelElement GetParentForVDWidgetTitle(VDWidgetTitle childElement)
        {
            VDWidget parent = childElement.Widget;
            if (parent != null && parent.WidgetType != WidgetType.View)
            {
                return parent;
            }
            return null;
        }
    }
}