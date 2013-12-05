using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDSection
    {
        public override bool HasWidgetTitle { get { return true; } }
    }
}



namespace MVCVisualDesigner
{
    public partial class VDSectionShape
    {
        // partial method to bind shape fields
        static partial void onBindShapeFields(object sender, EventArgs e)
        {
        }

        public override bool HasWidgetTitleIcon { get { return true; } }
        
        protected override Image getTitleIcon()
        { 
            return this.getImageFromResource("SectionToolToolboxBitmap"); 
        }
    }

    public partial class VDSectionHeadShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
            VDSectionShape parent = this.ParentShape as VDSectionShape;
            if (parent == null) return;

            this.Anchoring.SetTopAnchor(0);
            this.Anchoring.SetLeftAnchor(0);
            this.Anchoring.SetRightAnchor(1.0);

        }
    }

    public partial class VDSectionBodyShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
            VDSectionShape parent = this.ParentShape as VDSectionShape;
            if (parent == null) return;

            this.Anchoring.SetBottomAnchor(1.0);
            this.Anchoring.SetLeftAnchor(0);
            this.Anchoring.SetRightAnchor(1.0);
        }
    }
}