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

        private ModelElement GetParentForVDEventSource(VDEventSource eventSource)
        {
            VDWidget parent = eventSource.Parent;
            if (parent != null && parent.WidgetType != WidgetType.View)
            {
                return parent;
            }
            return null;
        }

        private ModelElement GetParentForVDEventTarget(VDEventTarget eventTarget)
        {
            VDWidget parent = eventTarget.Parent;
            if (parent != null && parent.WidgetType != WidgetType.View)
            {
                return parent;
            }
            return null;
        }

        // must be implemented in a partial class of MVCVisualDesigner.FixUpDiagram.  Given a child element,
        // this method should return the parent model element that is associated with the shape or diagram that will be the parent 
        // of the shape created for this child.  If no shape should be created, the method should return null.
        private ModelElement GetParentForSourcePerformsActionOnTarget(SourcePerformsActionOnTarget childLink)
        {
            if (childLink == null) return null;
            if (childLink.SourceVDWidget != null)
            {
                if (childLink.SourceVDWidget is VDEventSource )
                {
                    if (childLink.SourceVDWidget.Parent != null)
                        return childLink.SourceVDWidget.Parent.Parent;
                }
                else
                {
                    return childLink.SourceVDWidget.Parent;
                }
            }
            else if (childLink.TargetVDWidget != null)
            {
                if (childLink.TargetVDWidget is VDEventTarget)
                {
                    if (childLink.TargetVDWidget.Parent != null)
                        return childLink.TargetVDWidget.Parent.Parent;
                }
                else
                {
                    return childLink.TargetVDWidget.Parent;
                }
            }

            return null;
        }
    }
}