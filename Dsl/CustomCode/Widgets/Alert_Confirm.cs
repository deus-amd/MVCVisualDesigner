using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCVisualDesigner
{
    public partial class VDAlert
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDConfirmDialog
    {
        public override bool HasWidgetTitle { get { return true; } }
    }
}

namespace MVCVisualDesigner
{
    public partial class VDAlertShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("AlertToolToolboxBitmap");
        }
    }

    public partial class VDConfirmDialogShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("ConfirmDialogToolToolboxBitmap");
        }
    }
}
