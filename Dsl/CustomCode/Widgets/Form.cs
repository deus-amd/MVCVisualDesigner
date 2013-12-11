using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Model
namespace MVCVisualDesigner
{
    public partial class VDForm
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDRadio
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDCheckBox
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDSubmit
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDInput
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDSelect
    {
        public override bool HasWidgetTitle { get { return true; } }
    }
}



// Shape
namespace MVCVisualDesigner
{
    public partial class VDFormShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("FormToolToolboxBitmap"); }
    }

    public partial class VDRadioShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("RadioToolToolboxBitmap"); }
    }

    public partial class VDCheckBoxShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("CheckBoxToolToolboxBitmap"); }
    }

    public partial class VDSubmitShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("SubmitToolToolboxBitmap"); }
    }

    public partial class VDInputShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("InputToolToolboxBitmap"); }
    }

    public partial class VDSelectShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("SelectToolToolboxBitmap"); }
    }
}