using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model
namespace MVCVisualDesigner
{
    public partial class VDTab : ICustomMerge
    {
        public override bool HasWidgetTitle { get { return true; } }

        public void CustomMergeRelate(VDWidget targetWidget, VDWidget sourceElement, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(sourceElement);

            VDTab tab = sourceElement as VDTab;
            if (tab != null)
            {
                // the order of creating children is import, why??
                // first children, then grand-children???
                VDHoriContainer headContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, "Heads")) as VDHoriContainer;
                VDHoriContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, "Bodys")) as VDHoriContainer;

                VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId) as VDHoriSeparator;
                hSeparator.TopWidget = headContainer;
                hSeparator.BottomWidget = bodyContainer;

                tab.Children.Add(headContainer);
                tab.Children.Add(bodyContainer);
                tab.Children.Add(hSeparator);

                VDTabHead head = this.Store.ElementFactory.CreateElement(VDTabHead.DomainClassId) as VDTabHead;
                VDTabBody body = this.Store.ElementFactory.CreateElement(VDTabBody.DomainClassId) as VDTabBody;
                tab.FirstHead = head;
                tab.ActiveHead = head;
                head.Body = body;
                headContainer.Children.Add(head);
                bodyContainer.Children.Add(body);
            }
        }
    }

    public partial class VDTabHead
    {
    }

    public partial class VDTabBody
    {
    }
}



// Shape
namespace MVCVisualDesigner
{
    public partial class VDTabShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon()
        {
            return this.getImageFromResource("TabToolToolboxBitmap");
        }

        public override ShapeElement FixUpChildShapes(ModelElement childElement)
        {
            return base.FixUpChildShapes(childElement);
        }

        protected override void OnChildConfiguring(ShapeElement child, bool createdDuringViewFixup)
        {
            base.OnChildConfiguring(child, createdDuringViewFixup);

            if (createdDuringViewFixup && child is VDHoriContainerShape)
            {
                VDHoriContainerShape pel = child as VDHoriContainerShape;
                VDHoriContainer mel = pel.ModelElement as VDHoriContainer;
                if (mel != null)
                {
                    if (mel.Tag == "Heads")
                    {
                        if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
                        if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
                        if (!pel.Anchoring.HasTopAnchor) pel.Anchoring.SetTopAnchor(0);
                    }
                    else if (mel.Tag == "Bodys")
                    {
                        if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
                        if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
                        if (!pel.Anchoring.HasBottomAnchor) pel.Anchoring.SetBottomAnchor(1.0);
                    }
                }
            }
        }

        protected override void OnChildConfigured(ShapeElement child, bool childWasPlaced, bool createdDuringViewFixup)
        {
            base.OnChildConfigured(child, childWasPlaced, createdDuringViewFixup);
        }

        protected override void SetAbsoluteBoundsValue(RectangleD newValue)
        {
            base.SetAbsoluteBoundsValue(newValue);
            //PerformShapeAnchoringRule();
        }
    }

    public partial class VDTabHeadShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
        }

        //public override BoundsRules BoundsRules { get { return TabHeadBoundsRules.Instance; } }

        //class TabHeadBoundsRules : BoundsRules
        //{
        //    internal static readonly TabHeadBoundsRules Instance = new TabHeadBoundsRules();

        //    public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
        //    {
        //        proposedBounds = DefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

        //        PointD loc = proposedBounds.Location;
        //        if (loc.Y > 0.001) loc.Y = 0;

        //        proposedBounds = new RectangleD(loc, proposedBounds.Size);
        //        return proposedBounds;
        //    }
        //}
    }

    public partial class VDTabBodyShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();

            this.Anchoring.SetLeftAnchor(0);
            this.Anchoring.SetRightAnchor(1.0);
        }
    }
}