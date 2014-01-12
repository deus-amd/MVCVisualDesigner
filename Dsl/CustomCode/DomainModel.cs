using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class MVCVisualDesignerDomainModel : DomainModel
    {
        protected override Type[] GetCustomDomainModelTypes()
        {
            return new Type[] { 
                typeof(VDRelayoutChildrenShapeRule),
                typeof(VDTableColCountFixupRule),
                typeof(VDTableColTitleShape_BoundsFixupRule),
                typeof(VDTableRowTitleShape_BoundsFixupRule),
                typeof(VDTableRowCountFixupRule_ForRowTitle),
                typeof(VDTableRowCountFixupRule_ForTableCell),
            };
        }
    }
}
