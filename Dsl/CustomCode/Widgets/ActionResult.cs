using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MEL
namespace MVCVisualDesigner
{
    public abstract partial class VDActionResult
    {
        public override bool HasWidgetTitle { get { return true; } }

        internal virtual string GetDescriptionValue()
        {
            return string.Empty;
        }

        public override bool CanBeTargetOfServerAction { get { return true; } }
    }

    public partial class VDPartialViewResult
    {
        internal override string GetDescriptionValue()
        {
            return "Partial View";
        }
    }

    public partial class VDJSONResult
    {
        internal override string GetDescriptionValue()
        {
            return "JSON";
        }
    }
}


// PEL
namespace MVCVisualDesigner
{
    public abstract partial class VDActionResultShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }
    }

    public partial class VDPartialViewResultShape
    {
        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("PartialViewResultToolToolboxBitmap");
        }
    }

    public partial class VDJSONResultShape
    {
        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("JSONResultToolToolboxBitmap");
        }
    }
}