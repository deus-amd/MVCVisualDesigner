using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDDialog
    {
        public override bool HasWidgetTitle { get { return true; } }
    }
}

namespace MVCVisualDesigner
{
    public partial class VDDialogShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("DialogToolToolboxBitmap");
        }
    }
}

