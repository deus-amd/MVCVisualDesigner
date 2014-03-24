using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDModelSelector
    {
    }
}


namespace MVCVisualDesigner
{
    public partial class VDModelSelectorShape
    {
        public override double ZOrder { get { return 10000; } set { } }

        public override bool HasHighlighting { get { return false; } }

        public override bool CanFocus { get { return false; } }

        public override bool CanSelect { get { return false; } }

        public override bool CanMove { get { return false; } }

        public override bool HasShadow { get { return false; } }

        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);

            foreach(var sf in shapeFields)
            {
                if (sf is AreaField)
                {
                    ((AreaField)sf).GradientEndingColor = Color.Empty; // no gradient color
                    break;
                }
            }
        }

        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);

            // Fill brush settings for this shape.
            BrushSettings backgroundBrush = new BrushSettings();
            backgroundBrush.Color = Color.FromArgb(100, Color.FromKnownColor(KnownColor.Gray));
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeBackground, backgroundBrush);
        }

        public override BoundsRules BoundsRules { get { return ModelSelectorBoundsRules.Instance; } }

        class ModelSelectorBoundsRules : BoundsRules
        {
            internal static readonly ModelSelectorBoundsRules Instance = new ModelSelectorBoundsRules();

            public ModelSelectorBoundsRules()
            {
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                if (shape.Diagram != null)
                {
                    proposedBounds.Location = PointD.Empty;
                    proposedBounds.Size = shape.Diagram.Bounds.Size;
                }
                
                return proposedBounds;
            }
        }


        public override void OnClick(DiagramPointEventArgs e)
        {
            base.OnClick(e);

            foreach(var shape in this.Diagram.NestedChildShapes)
            {
                if (shape is VDWidgetShape)
                {
                    VDWidgetShape ws = shape as VDWidgetShape;
                    if (ws.DoHitTest(e.MousePosition, e.DiagramHitTestInfo))
                    {
                        ws.ZOrder += this.ZOrder;
                        ws.Invalidate(true);
                        //this.Diagram.Invalidate(true);
                        break;
                    }
                }
            }
        }
    }
}
