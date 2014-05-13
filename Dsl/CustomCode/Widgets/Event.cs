using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
#region MEL
    public partial class VDClientAction
    {

    }

    public partial class VDViewComponent
    {
        //public virtual bool IsEventSource { get { return true; } }
        //public virtual bool IsEventTarget { get { return true; } }

        //protected List<string> m_supportedEvents = new List<string>() 
        //{ 
        //    "Click",
        //    "Change",
        //    
        //    "OnSuccess",
        //    "OnFail",
        //};
        //public virtual List<string> SupportedEvents { get { return m_supportedEvents; } }
    }

    public static partial class EventBuilder
    {
        private static ElementLink ConnectSourceToTarget(ModelElement source, ModelElement target)
        {
            ElementLink link = null;
            if (source is VDEvent)
            {
                VDEvent anEvent = source as VDEvent;
                link = connectEventToTarget(anEvent, target);

            }
            else if (source is VDClientActionOutput)
            {
                if (target is VDViewComponent)
                {
                    ((VDClientActionOutput)source).R5_Components.Add((VDViewComponent)target);
                    link = R5_Output2Component.GetLink((VDClientActionOutput)source, (VDViewComponent)target);
                }
            }
            else if (source is VDViewComponent)
            {
                VDViewComponent eventSource = source as VDViewComponent;
                VDEvent anEvent = new VDEvent(source.Store);
                eventSource.Events.Add(anEvent);
                link = connectEventToTarget(anEvent, target);
            }

            if (link != null) return link;

            System.Diagnostics.Debug.Fail("Having agreed that the connection can be accepted we should never fail to make one.");
            throw new System.InvalidOperationException();
        }

        private static bool CanAcceptVDViewComponentAsSource(VDViewComponent candidate) { return true; }

        private static bool CanAcceptVDViewComponentAsTarget(VDViewComponent candidate) { return true; }

        private static bool CanAcceptVDEventAsSource(VDEvent candidate) { return true; }

        private static bool CanAcceptVDClientActionInputAsTarget(VDClientActionInput candiadte) { return true; }

        private static bool CanAcceptVDClientActionOutputAsSource(VDClientActionOutput candiadte) { return true; }

        private static bool CanAcceptVDViewComponentAndVDViewComponentAsSourceAndTarget(
            VDViewComponent sourceVDViewComponent, VDViewComponent targetVDViewComponent)
        {
            return true;
        }

        private static bool CanAcceptVDEventAndVDViewComponentAsSourceAndTarget(
            VDEvent sourceVDEvent, VDViewComponent targetVDViewComponent)
        {
            return true;
        }

        private static bool CanAcceptVDViewComponentAndVDClientActionInputAsSourceAndTarget(
            VDViewComponent targetVDViewComponent, VDClientActionInput sourceVDClientActionInput)
        {
            return true;
        }

        private static bool CanAcceptVDEventAndVDClientActionInputAsSourceAndTarget(
            VDEvent targetVDEvent, VDClientActionInput sourceVDClientActionInput)
        {
            return true;
        }

        private static bool CanAcceptVDClientActionOutputAndVDViewComponentAsSourceAndTarget(
            VDClientActionOutput sourceVDClientActionOutput, VDViewComponent targetVDViewComponent)
        {
            return true;
        }

        // helper
        private static ElementLink connectEventToTarget(VDEvent anEvent, ModelElement target)
        {
            if (target is VDClientActionInput)
            {
                anEvent.R4_Inputs.Add((VDClientActionInput)target);
                return R4_Event2Input.GetLink(anEvent, (VDClientActionInput)target);
            }
            else if (target is VDViewComponent)
            {
                VDClientAction eventAction = new VDClientAction(anEvent.Store);
                eventAction.Input = new VDClientActionInput(anEvent.Store);
                eventAction.Output = new VDClientActionOutput(anEvent.Store);
                VDWidget parent = findParentWhichCanAccept(anEvent.Widget, eventAction);
                parent.Children.Add(eventAction);
                anEvent.R4_Inputs.Add(eventAction.Input);
                eventAction.Output.R5_Components.Add((VDViewComponent)target);
                return R4_Event2Input.GetLink(anEvent, eventAction.Input);
            }
            return null;
        }

        // find a super widget of 'child', which can accept 'widget' as child
        private static VDWidget findParentWhichCanAccept(VDWidget child, VDWidget widget)
        {
            VDWidget parent = child.Parent;
            while (parent != null 
                && !MergeManager.Instance.CanMerge(widget.GetDomainClass().Id, parent.GetDomainClass().Id))
            {
                parent = parent.Parent;
            }
            // 
            System.Diagnostics.Debug.Assert(parent != null);
            if (parent == null) parent = child.RootView;
            return parent;
        }
    }
#endregion


    ////////////////////////////////////////////////////////////////////////////////

#region PEL
    public partial class VDEventPort
    {
        public override BoundsRules BoundsRules { get { return EventPortPositionRule.Instance; } }

        class EventPortPositionRule : BoundsRules
        {
            private static EventPortPositionRule s_instance = new EventPortPositionRule();
            internal static EventPortPositionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                // copied from PortMovementRule
                RectangleD rectange = SnapToPerimeterFollowingRotationRule.Instance.GetCompliantBounds(shape, proposedBounds);
                RectangleD ed2 = SnapToGridRule.Instance.GetCompliantBounds(shape, rectange);
                rectange = SnapToPerimeterFollowingRotationRule.Instance.GetCompliantBounds(shape, ed2);

                if (shape.ParentShape == null) return rectange;

                foreach (ShapeDecorator decorator in shape.Decorators)
                {
                    if (decorator != null && decorator.Field.Name == "EventNameDecorator")
                    {
                        if (rectange.Left < 0)
                            decorator.Position = ShapeDecoratorPosition.OuterTopLeft;
                        else if (rectange.Top < 0)
                            decorator.Position = ShapeDecoratorPosition.OuterTopRight;
                        else if (rectange.Top + rectange.Height > shape.ParentShape.GeometryBoundingBox.Height)
                            decorator.Position = ShapeDecoratorPosition.OuterBottomRight;
                        else
                            decorator.Position = ShapeDecoratorPosition.OuterTopRight;
                        break;
                    }
                }
                return rectange;
            }
        }
    }

    public partial class VDClientActionShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        public override BoundsRules BoundsRules { get { return VDClientActionRule.Instance; } }

        class VDClientActionRule : PortMovementRule
        {
            private static VDClientActionRule s_instance = new VDClientActionRule();
            internal static VDClientActionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD rectange = DefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);
                if (rectange.Height > 0.2)
                    return new RectangleD(rectange.Location, new SizeD(rectange.Width, 0.2));
                else
                    return rectange;
            }
        }
    }

    public partial class VDClientActionInputPort
    {
        public override bool HasConnectionPoints { get { return true; } }

        public override void EnsureConnectionPoints(LinkShape link)
        {
            if (this.ConnectionPoints.Count == 0)
            {
                RectangleD absoluteBoundingBox = this.AbsoluteBoundingBox;
                this.CreateConnectionPoint(new PointD(absoluteBoundingBox.Left, absoluteBoundingBox.Center.Y));
                this.CreateConnectionPoint(new PointD(absoluteBoundingBox.Right, absoluteBoundingBox.Center.Y));
            }
        }

        public override BoundsRules BoundsRules { get { return PortPositionRule.Instance; } }

        class PortPositionRule : PortMovementRule
        {
            private static PortPositionRule s_instance = new PortPositionRule();
            internal new static PortPositionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD rectange = base.GetCompliantBounds(shape, proposedBounds);
                if (shape.ParentShape == null) return rectange;

                RectangleD parentBounds = shape.ParentShape.BoundingBox;
                double left = -rectange.Width / 2;
                double top = (parentBounds.Height - rectange.Height) / 2;

                return new RectangleD(left, top, rectange.Width, rectange.Height);
            }
        }
    }

    public partial class VDClientActionOutputPort
    {
        public override bool HasConnectionPoints { get { return true; } }

        public override void EnsureConnectionPoints(LinkShape link)
        {
            if (this.ConnectionPoints.Count == 0)
            {
                RectangleD absoluteBoundingBox = this.AbsoluteBoundingBox;
                this.CreateConnectionPoint(new PointD(absoluteBoundingBox.Left, absoluteBoundingBox.Center.Y));
                this.CreateConnectionPoint(new PointD(absoluteBoundingBox.Right, absoluteBoundingBox.Center.Y));
            }
        }

        public override BoundsRules BoundsRules { get { return PortPositionRule.Instance; } }

        class PortPositionRule : PortMovementRule
        {
            private static PortPositionRule s_instance = new PortPositionRule();
            internal new static PortPositionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD rectange = base.GetCompliantBounds(shape, proposedBounds);
                if (shape.ParentShape == null) return rectange;

                RectangleD parentBounds = shape.ParentShape.BoundingBox;
                double left = parentBounds.Width - rectange.Width / 2;
                double top = (parentBounds.Height - rectange.Height) / 2;

                return new RectangleD(left, top, rectange.Width, rectange.Height);
            }
        }
    }

    public partial class VDEventTriggerActionsConnector
    {
    }
#endregion
}
