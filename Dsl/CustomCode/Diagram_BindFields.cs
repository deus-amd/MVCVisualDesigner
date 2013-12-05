using System;
using System.Collections.Generic;
using System.Text;
namespace MVCVisualDesigner
{
    public partial class VDDiagram
    {
        private void BindShapeFieldsToDomainProperties()
        {
            VDSectionShape.DecoratorsInitialized += VDSectionShape.BindShapeFields;
            VDHoriSeparatorShape.DecoratorsInitialized += VDHoriSeparatorShape.BindShapeFields;
            VDVertSeparatorShape.DecoratorsInitialized += VDVertSeparatorShape.BindShapeFields;
            VDSectionHeadShape.DecoratorsInitialized += VDSectionHeadShape.BindShapeFields;
            VDSectionBodyShape.DecoratorsInitialized += VDSectionBodyShape.BindShapeFields;
        }
    }


    public partial class VDSectionShape
    {
        internal static void BindShapeFields(object sender, EventArgs e)
        {
            onBindShapeFields(sender, e);	
        }

        static partial void onBindShapeFields(object sender, EventArgs e);
    }
    public partial class VDHoriSeparatorShape
    {
        internal static void BindShapeFields(object sender, EventArgs e)
        {
            onBindShapeFields(sender, e);	
        }

        static partial void onBindShapeFields(object sender, EventArgs e);
    }
    public partial class VDVertSeparatorShape
    {
        internal static void BindShapeFields(object sender, EventArgs e)
        {
            onBindShapeFields(sender, e);	
        }

        static partial void onBindShapeFields(object sender, EventArgs e);
    }
    public partial class VDSectionHeadShape
    {
        internal static void BindShapeFields(object sender, EventArgs e)
        {
            onBindShapeFields(sender, e);	
        }

        static partial void onBindShapeFields(object sender, EventArgs e);
    }
    public partial class VDSectionBodyShape
    {
        internal static void BindShapeFields(object sender, EventArgs e)
        {
            onBindShapeFields(sender, e);	
        }

        static partial void onBindShapeFields(object sender, EventArgs e);
    }
}