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
    public partial class VDEventBase
    {
        private IEventInfo m_eventInfo;
        public IEventInfo EventInfo 
        { 
            get
            {
                if (m_eventInfo == null && this.Widget != null)
                    m_eventInfo = this.Widget.GetModelStore().GetEventInfo(this.Category, this.Name);
                return m_eventInfo;
            }
            set
            {
                m_eventInfo = value;
                this.Name = m_eventInfo.Name;
                this.Category = m_eventInfo.Category;
            }
        }
    }

    public partial class VDActionBaseBase
    {

    }

    public partial class VDActionBase : VDActionBaseBase
    {
        public abstract IActionInfo ActionInfo { get;  protected set; }

        public override VDActionData ActionData
        {
            get
            {
                if (base.ActionData == null && !this.m_deleting)
                {
                    using (var trans = this.Store.TransactionManager.BeginTransaction("Create action data for " + this.WidgetType.ToString()))
                    {
                        VDModelStore modelStore = this.GetModelStore();
                        if (modelStore != null)
                        {
                            var actions = this.Store.ElementDirectory.FindElements(VDActionBase.DomainClassId);
                            string actionDataName = null;
                            if (this.SourceEvents.Count > 0)
                            {
                                var srcEvent = SourceEvents[0];
                                actionDataName = string.Format("{0}_{1}_{2}", srcEvent.Widget.WidgetName, srcEvent.Name, this.Name);
                            }
                            else
                            {
                                actionDataName = this.Name;
                            }
                            actionDataName += "_" + actions.Count;
                            base.ActionData = modelStore.CreateConcreteType<VDActionData>(actionDataName);
                        }
                        trans.Commit();
                    }
                }
                return base.ActionData;
            }
            set
            {
                base.ActionData = value;
            }
        }
    }

    public partial class VDClientAction
    {
        private IClientActionInfo m_actionInfo;
        public override IActionInfo ActionInfo
        {
            get
            {
                return this.ClientActionInfo;
            }

            protected set
            {
                if (value is IClientActionInfo)
                    this.ClientActionInfo = (IClientActionInfo)value;
                else
                    throw new Exception("Type mismatch.");
            }
        }

        public IClientActionInfo ClientActionInfo
        {
            get
            {
                if (m_actionInfo == null)
                    m_actionInfo = this.GetModelStore().GetActionInfo(this.Category, this.Name) as IClientActionInfo;
                return m_actionInfo;
            }
            set
            {
                m_actionInfo = value;
                this.Name = m_actionInfo.Name;
                this.Category = m_actionInfo.Category;
            }
        }
    }

    public partial class VDServerAction
    {
        private IServerActionInfo m_actionInfo;
        public override IActionInfo ActionInfo
        {
            get
            {
                return this.ServerActionInfo;
            }

            protected set
            {
                if (value is IServerActionInfo)
                    this.ServerActionInfo = (IServerActionInfo)value;
                else
                    throw new Exception("Type mismatch.");
            }
        }

        public IServerActionInfo ServerActionInfo
        {
            get 
            {
                if (m_actionInfo == null)
                {
                    VDModelStore ms = this.GetModelStore();
                    m_actionInfo = new ServerActionInfo(this.Name, ms != null ? ms.GetSupportedServerActionJoints() : null);
                }
                return m_actionInfo;
            }
            set 
            { 
                m_actionInfo = value;
                this.Name = m_actionInfo.Name;
            }
        }
    }

    public partial class VDActionJoint
    {
        // calculated property - DisplayName
        internal string GetDisplayNameValue()
        {
            if (this.JointInfo == null || this.JointInfo == DefaultActionJontInfo.Instance)
                return string.Empty;
            else
                return this.Name;
        }

        private IActionJointInfo m_actionJointInfo;
        public IActionJointInfo JointInfo 
        {
            get
            {
                if (m_actionJointInfo == null && this.Widget != null)
                {
                    if (this.Widget is VDServerAction)
                        m_actionJointInfo = this.Widget.GetModelStore()
                            .GetSupportedServerActionJoints().Find(x => x.Category == this.Category && x.Name == this.Name);
                    else
                        m_actionJointInfo = this.Widget.GetModelStore().GetActionJointInfo(this.Category, this.Name);
                }
                return m_actionJointInfo;
            }
            set
            {
                m_actionJointInfo = value;
                this.Name = m_actionJointInfo.Name;
                this.Category = m_actionJointInfo.Category;
            }
        }

        public List<VDActionBase> GetActions()
        {
            List<VDActionBase> actions = new List<VDActionBase>();
            if (this.Widget is VDActionBase)
            {
                actions.Add((VDActionBase)this.Widget);
            }
            else
            {
                foreach(VDActionJoint acj in this.Widget.SourceActionJoints)
                {
                    actions.AddRange(acj.GetActions());
                }
            }
            return actions;
        }
    }

    public static partial class EventBuilder
    {
        private static bool CanAcceptVDViewComponentAsSource(VDViewComponent candidate) { return true; }

        private static bool CanAcceptVDEventBaseAsSource(VDEventBase candidate) { return true; }

        private static bool CanAcceptVDActionJointAsSource(VDActionJoint candidate) { return true; }

        private static bool CanAcceptVDViewComponentAsTarget(VDViewComponent candidate) { return true; }

        private static bool CanAcceptVDViewComponentAndVDViewComponentAsSourceAndTarget(VDViewComponent sourceComponent, VDViewComponent targetComponent)
        {
            if (sourceComponent is VDServerAction && !targetComponent.CanBeTargetOfServerAction)
                return false;
            else
                return true;
        }

        private static bool CanAcceptVDEventBaseAndVDViewComponentAsSourceAndTarget(VDEventBase sourceEvent, VDViewComponent targetComponent)
        {
            return true;
        }

        private static bool CanAcceptVDActionJointAndVDViewComponentAsSourceAndTarget(VDActionJoint sourceActionJoint, VDViewComponent targetComponent)
        {
            if (sourceActionJoint.GetActions().Find(act => act is VDServerAction) != null && !targetComponent.CanBeTargetOfServerAction)
                return false;
            else
                return true;
        }

        private static ElementLink ConnectSourceToTarget(ModelElement source, ModelElement target)
        {
            if (source is VDEventBase && target is VDActionBase)
            {
                VDEventBase evt = source as VDEventBase;
                VDActionBase act = target as VDActionBase;
                evt.TargetComponents.Add(act);
                return R_Event2Component.GetLink(evt, act);
            }
            else if (source is VDActionJoint && target is VDViewComponent)
            {
                VDActionJoint joint = source as VDActionJoint;
                VDViewComponent tgt = target as VDViewComponent;
                joint.TargetComponents.Add(tgt);
                return R_ActionJoint2Component.GetLink(joint, tgt);
            }
            else if (source is VDViewComponent && target is VDActionBase)
            {
                VDViewComponent srcComponent = source as VDViewComponent;
                VDActionBase targetAction = target as VDActionBase;
                Component2ActionDialog dlg = new Component2ActionDialog();
                dlg.SetComponentAndAction(srcComponent, targetAction);
                dlg.ShowDialog();

                if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    IEventInfo evtInfo = dlg.SelectedEvent;
                    VDEventBase newEvent = evtInfo.CreateEvent(srcComponent);
                    newEvent.Widget = srcComponent;
                    newEvent.TargetComponents.Add(targetAction);
                    return R_Event2Component.GetLink(newEvent, targetAction);
                }
            }
            else if (source is VDEventBase && target is VDViewComponent) // create new action
            {
                VDEventBase sourceEvent = source as VDEventBase;
                VDViewComponent targetComponent = target as VDViewComponent;
                Component2ComponentDialog dlg = new Component2ComponentDialog(false);
                dlg.NewAction.SetSourceEvent(sourceEvent).SetTarget(targetComponent);
                dlg.ShowDialog();

                if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    IActionInfo actInfo = dlg.NewAction.SelectedAction;
                    VDActionBase newAct = actInfo.CreateAction(sourceEvent.Partition);
                    VDWidget.GetCommonParent(sourceEvent.Widget, targetComponent).Children.Add(newAct);
                    //sourceEvent.Widget.Parent.Children.Add(newAct);
                    sourceEvent.TargetComponents.Add(newAct);

                    IActionJointInfo jointInfo = dlg.NewAction.SelectedJoint;
                    VDActionJoint newJoint = jointInfo.CreateActionJoint(newAct, targetComponent);
                    newJoint.Widget = newAct;
                    newJoint.TargetComponents.Add(targetComponent);
                    return R_ActionJoint2Component.GetLink(newJoint, targetComponent);
                }
            }
            else if (source is VDViewComponent && target is VDViewComponent) // create new action or target
            {
                VDViewComponent srcComponent = source as VDViewComponent;
                VDViewComponent tgtComponent = target as VDViewComponent;
                Component2ComponentDialog dlg = new Component2ComponentDialog();
                dlg.NewAction.SetSource(srcComponent).SetTarget(tgtComponent);
                dlg.NewTarget.SetSource(srcComponent).SetTarget(tgtComponent);
                dlg.ShowDialog();

                if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlg.IfCreateNewAction)
                    {
                        IEventInfo evtInfo = dlg.NewAction.SelectedEvent;
                        VDEventBase newEvent = evtInfo.CreateEvent(srcComponent);
                        newEvent.Widget = srcComponent;

                        IActionInfo actInfo = dlg.NewAction.SelectedAction;
                        VDActionBase newAct = actInfo.CreateAction(srcComponent.Partition);
                        VDWidget.GetCommonParent(srcComponent, tgtComponent).Children.Add(newAct);
                        //srcComponent.Parent.Children.Add(newAct);
                        newEvent.TargetComponents.Add(newAct);

                        IActionJointInfo jointInfo = dlg.NewAction.SelectedJoint;
                        VDActionJoint newJoint = jointInfo.CreateActionJoint(newAct, tgtComponent);
                        newJoint.Widget = newAct;
                        newJoint.TargetComponents.Add(tgtComponent);
                        return R_ActionJoint2Component.GetLink(newJoint, tgtComponent);
                    }
                    else
                    {
                        IActionJointInfo jointInfo = dlg.NewTarget.SelectedJoint;
                        VDActionJoint joint = jointInfo.CreateActionJoint(srcComponent, tgtComponent);
                        joint.Widget = srcComponent;
                        joint.TargetComponents.Add(tgtComponent);
                        return R_ActionJoint2Component.GetLink(joint, tgtComponent);
                    }
                }
            }

            return null;
        }
    }
#endregion


    ////////////////////////////////////////////////////////////////////////////////

#region PEL
    public partial class VDEventBasePort
    {
        public override BoundsRules BoundsRules { get { return EventPortPositionRule.Instance; } }

        class EventPortPositionRule : BoundsRules
        {
            private static EventPortPositionRule s_instance = new EventPortPositionRule();
            internal static EventPortPositionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                return PortRuleHelper.GetCompliantBounds(shape, proposedBounds, "EventNameDecorator");
            }
        }

        //!!!! Bypass MS BUG
        // when port is on a nested shape, the link lines are routed badly
        // this is a workaround to fix this issue
        public override bool IsPort
        {
            get
            {
                if (this.ParentShape != null && this.ParentShape.ParentShape is VDDiagram)
                    return true;
                else
                    return false;
            }
        }

        protected override void OnChildConfigured(Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement child, bool childWasPlaced, bool createdDuringViewFixup)
        {
            base.OnChildConfigured(child, childWasPlaced, createdDuringViewFixup);
            VDEventBasePort port = child as VDEventBasePort;
            if ((port != null) && !port.IsPort)
            {
                this.ConfiguredChildPortShape(port, childWasPlaced);
            }
        }
        protected override void OnChildConfiguring(Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement child, bool createdDuringViewFixup)
        {
            base.OnChildConfiguring(child, createdDuringViewFixup);
            VDEventBasePort port = child as VDEventBasePort;
            if ((port != null) && !port.IsPort)
            {
                port.ActiveBoundsRules = this.BoundsRules;
            }
        }
        //!!!! Bypass MS BUG

        //public override bool HasConnectionPoints { get { return true; } }

        //public override void EnsureConnectionPoints(LinkShape link)
        //{
        //    //if (this.Placement == PortPlacement.Left)
        //    //    this.CreateConnectionPoint(new PointD(this.AbsoluteBounds.X, this.AbsoluteBounds.Top + (this.AbsoluteBounds.Height / 2.0)));
        //    //else if (this.Placement == PortPlacement.Right)
        //    //    this.CreateConnectionPoint(new PointD(this.AbsoluteBounds.Right, this.AbsoluteBounds.Top + (this.AbsoluteBounds.Height / 2.0)));
        //    //else if (this.Placement == PortPlacement.Top)
        //    //    this.CreateConnectionPoint(new PointD(this.AbsoluteBounds.X + (this.AbsoluteBoundingBox.Width / 2.0), this.AbsoluteBounds.Top));
        //    //else if (this.Placement == PortPlacement.Bottom)
        //    //    this.CreateConnectionPoint(new PointD(this.AbsoluteBounds.X + (this.AbsoluteBoundingBox.Width / 2.0), this.AbsoluteBounds.Bottom));
        //    //base.EnsureConnectionPoints(link);
        //}
    }

    public partial class VDActionJointPort
    {
        public override BoundsRules BoundsRules { get { return ActionJointPortPositionRule.Instance; } }

        class ActionJointPortPositionRule : BoundsRules
        {
            private static ActionJointPortPositionRule s_instance = new ActionJointPortPositionRule();
            internal static ActionJointPortPositionRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                return PortRuleHelper.GetCompliantBounds(shape, proposedBounds, "JointNameDecorator");
            }
        }

        //!!!! Bypass MS BUG
        // when port is on a nested shape, the link lines are routed badly
        // this is a workaround to fix this issue
        public override bool IsPort
        {
            get
            {
                if (this.ParentShape != null && this.ParentShape.ParentShape is VDDiagram)
                    return true;
                else
                    return false;
            }
        }

        protected override void OnChildConfigured(Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement child, bool childWasPlaced, bool createdDuringViewFixup)
        {
            base.OnChildConfigured(child, childWasPlaced, createdDuringViewFixup);
            VDActionJointPort port = child as VDActionJointPort;
            if ((port != null) && !port.IsPort)
            {
                this.ConfiguredChildPortShape(port, childWasPlaced);
            }
        }
        protected override void OnChildConfiguring(Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement child, bool createdDuringViewFixup)
        {
            base.OnChildConfiguring(child, createdDuringViewFixup);
            VDActionJointPort port = child as VDActionJointPort;
            if ((port != null) && !port.IsPort)
            {
                port.ActiveBoundsRules = this.BoundsRules;
            }
        }
        //!!!! Bypass MS BUG
    }

    public partial class VDActionBaseShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        public override BoundsRules BoundsRules { get { return VDActionShapeRule.Instance; } }

        class VDActionShapeRule : PortMovementRule
        {
            private static VDActionShapeRule s_instance = new VDActionShapeRule();
            internal new static VDActionShapeRule Instance { get { return s_instance; } }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD rectange = DefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);
                if (rectange.Height > 0.16)
                    return new RectangleD(rectange.Location, new SizeD(rectange.Width, 0.15));
                else
                    return rectange;
            }
        }
    }

    
    static class PortRuleHelper
    {
        public static RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds, string decoratorName)
        {
            // copied from PortMovementRule
            RectangleD rectange = SnapToPerimeterFollowingRotationRule.Instance.GetCompliantBounds(shape, proposedBounds);
            RectangleD ed2 = SnapToGridRule.Instance.GetCompliantBounds(shape, rectange);
            rectange = SnapToPerimeterFollowingRotationRule.Instance.GetCompliantBounds(shape, ed2);

            if (shape.ParentShape == null) return rectange;

            foreach (ShapeDecorator decorator in shape.Decorators)
            {
                if (decorator != null && decorator.Field.Name == decoratorName)
                {
                    if (rectange.Left < 0)
                    {
                        decorator.Position = ShapeDecoratorPosition.OuterTopLeft;
                        decorator.Offset = new PointD(0.05, 0.1);
                    }
                    else if (rectange.Top < 0)
                    {
                        decorator.Position = ShapeDecoratorPosition.OuterTopRight;
                        decorator.Offset = new PointD(-0.1, 0.05);
                    }
                    else if (rectange.Top + rectange.Height > shape.ParentShape.GeometryBoundingBox.Height)
                    {
                        decorator.Position = ShapeDecoratorPosition.OuterBottomRight;
                        decorator.Offset = new PointD(-0.1, -0.05);
                    }
                    else
                    {
                        decorator.Position = ShapeDecoratorPosition.OuterTopRight;
                        decorator.Offset = new PointD(-0.05, 0.1);
                    }
                    break;
                }
            }
            return rectange;
        }
    }

    //public partial class VDEvent2ComponentConnector
    //{
    //    public override void OnShapeInserted()
    //    {
    //        if (this.FromShape != null && this.ToShape != null)
    //        {
    //            try
    //            {
    //                // FromShape is Event Port, FromShape.Parent is Widget hosting Event Port
    //                if (this.FromShape.ParentShape != null && this.FromShape.ParentShape.ParentShape is VDDiagram && this.ToShape.ParentShape is VDDiagram)
    //                    this.RoutingStyle = Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle.VGRouteRightAngle;
    //                else
    //                    this.RoutingStyle = Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle.VGRouteStraight;
    //            }
    //            catch { }
    //        }
    //    }
    //}

    //public partial class VDActionJoint2ComponentConnector
    //{
    //    public override void OnShapeInserted()
    //    {
    //        if (this.FromShape != null && this.ToShape != null)
    //        {
    //            try
    //            {
    //                // FromShape is Action Joint, FromShape.Parent is Action 
    //                if (this.FromShape.ParentShape != null && this.FromShape.ParentShape.ParentShape is VDDiagram && this.ToShape.ParentShape is VDDiagram)
    //                    this.RoutingStyle = Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle.VGRouteRightAngle;
    //                else
    //                    this.RoutingStyle = Microsoft.VisualStudio.Modeling.Diagrams.GraphObject.VGRoutingStyle.VGRouteStraight;
    //            }
    //            catch { }
    //        }
    //    }
    //}
#endregion
}
