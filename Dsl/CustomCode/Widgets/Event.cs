using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDEventSource
    {
        public override WidgetType WidgetType
        {
            get { return MVCVisualDesigner.WidgetType.EventSource; }
        }
    }

    public partial class VDEventTarget
    {
        public override WidgetType WidgetType
        {
            get { return MVCVisualDesigner.WidgetType.EventTarget; }
        }
    }

    public partial class VDSourcePerformActionOnTargetConnector
    {
        protected override Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle DefaultRoutingStyle
        {
            get
            {
                return Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle.VGRouteCenterToCenter;
            }
        }
    }


    public partial class SourcePerformsActionOnTargetBuilder
    {
        private static bool CanAcceptVDWidgetAsSource(VDWidget candidate)
        {
            throw new NotImplementedException();
        }

        private static bool CanAcceptVDWidgetAsTarget(VDWidget candidate)
        {
            throw new NotImplementedException();
        }

        private static bool CanAcceptVDWidgetAndVDWidgetAsSourceAndTarget(VDWidget sourceVDWidget, VDWidget targetVDWidget)
        {
            throw new NotImplementedException();
        }

        private static ElementLink ConnectSourceToTarget(ModelElement source, ModelElement target)
        {
            throw new NotImplementedException();
        }
    }

    public partial class VDDiagramBase
    {
        private NodeShape GetSourceShapeForVDSourcePerformActionOnTargetConnector(VDSourcePerformActionOnTargetConnector connector)
        {
            if (connector.FromShape is VDDiagram)
                return null;
            else
                return connector.FromShape;
        }

        private NodeShape GetTargetShapeForVDSourcePerformActionOnTargetConnector(VDSourcePerformActionOnTargetConnector connector)
        {
            if (connector.ToShape is VDDiagram)
                return null;
            else
                return connector.FromShape;
        }

        private ModelElement GetSourceRolePlayerForLinkMappedByVDSourcePerformActionOnTargetConnector(VDSourcePerformActionOnTargetConnector connector)
        {
            throw new NotImplementedException();
        }

        private ModelElement GetTargetRolePlayerForLinkMappedByVDSourcePerformActionOnTargetConnector(VDSourcePerformActionOnTargetConnector connector)
        {
            throw new NotImplementedException();
        }
    }
}
