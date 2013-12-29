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
    }

    public partial class VDHoriContainer
    {
    }

    public partial class VDVertContainer
    {
    }
}


// Shape
namespace MVCVisualDesigner
{
    public partial class VDHoriContainerShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.None; } }
        public override bool CanMove { get { return false; } }
        
        protected override void SetAbsoluteBoundsValue(RectangleD newValue)
        {
            base.SetAbsoluteBoundsValue(newValue);

            // trigger bounds rules of children
            this.relayoutChildren = true;            

            //foreach(var s in this.NestedChildShapes)
            //{
            //    VDWidgetShape childShape = s as VDWidgetShape;
            //    if (childShape != null)
            //    {
            //        childShape.Bounds = new RectangleD(childShape.Bounds.Location, new SizeD(childShape.Bounds.Width, 0));
            //    }
            //}
        }

        protected override void OnChildConfiguring(ShapeElement child, bool createdDuringViewFixup)
        {
            base.OnChildConfiguring(child, createdDuringViewFixup);
        }
    }

    public partial class VDVertContainerShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.None; } }
        public override bool CanMove { get { return false; } }

        protected override void SetAbsoluteBoundsValue(RectangleD newValue)
        {
            base.SetAbsoluteBoundsValue(newValue);

            // trigger bounds rules of children
            this.relayoutChildren = true;            

            //foreach(var s in this.nestedchildshapes)
            //{
            //    vdwidgetshape childshape = s as vdwidgetshape;
            //    if (childshape != null)
            //    {
            //        childshape.size = new sized(0, childshape.size.height);
            //    }
            //}
        }
    }
}