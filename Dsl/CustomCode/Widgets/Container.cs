using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model
namespace MVCVisualDesigner
{
    public partial class VDContainer
    {
        protected override bool CanMerge(ProtoElementBase rootElement, ElementGroupPrototype elementGroupPrototype)
        {
            if (this.Parent != null) 
            {
                // forward to parent
                return this.Parent.internalCanMerge(this, rootElement, elementGroupPrototype);
            }
            return base.CanMerge(rootElement, elementGroupPrototype);
        }
    }
}


// Shape
namespace MVCVisualDesigner
{
    public partial class VDContainerShapeBase
    {
        public bool GetisTagDecoratorVisibleValue()
        {
            if (this.BoundingBox.Height > 0.3)
                return true;
            else
                return false;
        }
    }

    public partial class VDContainerShape
    {
        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);
            TextField tagField = shapeFields.FirstOrDefault(f => f.Name == "TagDecorator") as TextField;
            if (tagField != null)
            {
                tagField.DefaultFocusable = false;
                tagField.DefaultSelectable = false;
            }
        }

        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        protected override void SetAbsoluteBoundsValue(RectangleD newValue)
        {
            base.SetAbsoluteBoundsValue(newValue);

            // trigger bounds rules of children
            this.relayoutChildren = true; 
        }

        public override void OnRelayoutChildShapes()
        {
            foreach (var childShape in NestedChildShapes)
            {
                VDWidgetShape ws = childShape as VDWidgetShape;
                if (ws != null)
                {
                    ws.OnBoundsFixup(BoundsFixupState.ViewFixup, 1, false);
                }
            }
        }

        public override BoundsRules BoundsRules { get { return VDContainerBoundsRules.Instance; } }

        class VDContainerBoundsRules : BoundsRules
        {
            internal static readonly VDContainerBoundsRules Instance = new VDContainerBoundsRules();

            public VDContainerBoundsRules()
            {
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                // base rule
                proposedBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDWidgetShape parentShape = shape.ParentShape as VDWidgetShape;
                VDContainer thisMEL = shape.ModelElement as VDContainer;
                VDContainerShape thisPEL = shape as VDContainerShape;

                //
                if (parentShape != null && thisMEL != null && thisPEL != null)
                {
                    if (parentShape is VDHoriContainerShape)
                    {
                        // setup left anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasLeftAnchor && !thisPEL.Anchoring.HasLeftAnchor, 
                            thisMEL.LeftSibling, AnchoringBehavior.Edge.Left);
                        // setup right anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasRightAnchor && !thisPEL.Anchoring.HasRightAnchor, 
                            thisMEL.RightSibling, AnchoringBehavior.Edge.Right);
                    }
                    else if (parentShape is VDVertContainerShape)
                    {
                        // setup top anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasTopAnchor && !thisPEL.Anchoring.HasTopAnchor, 
                            thisMEL.TopSibling, AnchoringBehavior.Edge.Top);
                        // setup bottom anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasBottomAnchor && !thisPEL.Anchoring.HasBottomAnchor, 
                            thisMEL.BottomSibling, AnchoringBehavior.Edge.Bottom);
                    }
                    else if (!(parentShape is VDFullFilledContainerShape))
                    {
                        // setup left anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasLeftAnchor && !thisPEL.Anchoring.HasLeftAnchor, 
                            thisMEL.LeftSibling, AnchoringBehavior.Edge.Left);
                        // setup right anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasRightAnchor && !thisPEL.Anchoring.HasRightAnchor, 
                            thisMEL.RightSibling, AnchoringBehavior.Edge.Right);
                        // setup top anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasTopAnchor && !thisPEL.Anchoring.HasTopAnchor, 
                            thisMEL.TopSibling, AnchoringBehavior.Edge.Top);
                        // setup bottom anchor
                        setAnchor(parentShape, thisMEL, thisPEL, thisMEL.HasBottomAnchor && !thisPEL.Anchoring.HasBottomAnchor, 
                            thisMEL.BottomSibling, AnchoringBehavior.Edge.Bottom);
                    }

                    if (thisMEL is VDHoriContainer && ((VDHoriContainer)thisMEL).FixedHeight > VDConstants.DOUBLE_DIFF)
                    {
                        proposedBounds = new RectangleD(proposedBounds.Location,
                            new SizeD(proposedBounds.Width, ((VDHoriContainer)thisMEL).FixedHeight));
                    }
                    else if (thisMEL is VDVertContainer && ((VDVertContainer)thisMEL).FixedWidth > VDConstants.DOUBLE_DIFF)
                    {
                        proposedBounds = new RectangleD(proposedBounds.Location,
                            new SizeD(((VDVertContainer)thisMEL).FixedWidth, proposedBounds.Height));
                    }
                }

                return proposedBounds;
            }

            private static void setAnchor(VDWidgetShape parentShape, VDContainer thisMEL, VDContainerShape thisPEL, 
                bool setAnchor, VDWidget sibling, AnchoringBehavior.Edge edge)
            {
                if (!setAnchor) return;

                if (sibling != null) // anchor to sibling
                {
                    VDWidgetShape siblingShape = parentShape.GetChildShape<VDWidgetShape>(sibling);
                    if (siblingShape != null)
                    {
                        switch (edge)
                        {
                            case AnchoringBehavior.Edge.Bottom:
                                thisPEL.Anchoring.SetBottomAnchor(siblingShape, AnchoringBehavior.Edge.Top, thisMEL.BottomMargin);
                                break;
                            case AnchoringBehavior.Edge.Left:
                                thisPEL.Anchoring.SetLeftAnchor(siblingShape, AnchoringBehavior.Edge.Right, thisMEL.LeftMargin);
                                break;
                            case AnchoringBehavior.Edge.Right:
                                thisPEL.Anchoring.SetRightAnchor(siblingShape, AnchoringBehavior.Edge.Left, thisMEL.RightMargin);
                                break;
                            case AnchoringBehavior.Edge.Top:
                                thisPEL.Anchoring.SetTopAnchor(siblingShape, AnchoringBehavior.Edge.Bottom, thisMEL.TopMargin);
                                break;
                        }
                    }
                }
                else // anchor to parent
                {
                    switch (edge)
                    {
                        case AnchoringBehavior.Edge.Bottom:
                            thisPEL.Anchoring.SetBottomAnchor(edge, thisMEL.BottomMargin);
                            break;
                        case AnchoringBehavior.Edge.Left:
                            thisPEL.Anchoring.SetLeftAnchor(edge, thisMEL.LeftMargin);
                            break;
                        case AnchoringBehavior.Edge.Right:
                            thisPEL.Anchoring.SetRightAnchor(edge, thisMEL.RightMargin);
                            break;
                        case AnchoringBehavior.Edge.Top:
                            thisPEL.Anchoring.SetTopAnchor(edge, thisMEL.TopMargin);
                            break;
                    }
                }
            }
        }
    }
}