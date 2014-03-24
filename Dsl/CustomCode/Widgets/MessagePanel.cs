using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDMessagePanel
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDMessagePanelShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("MessagePanelToolToolboxBitmap");
        }
    }
}

