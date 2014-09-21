using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// create widget values
namespace MVCVisualDesigner
{
    public partial class VDTableCell
    {
        protected override VDWidgetValue createWidgetValue()
        {
            VDModelStore modelStore = this.GetModelStore();
            if (modelStore != null)
            {
                return modelStore.CreateConcreteType<VDWidgetValue>("string");
            }
            return null;
        }
    }
}