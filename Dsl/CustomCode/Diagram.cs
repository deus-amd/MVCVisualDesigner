using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDDiagram
    {
        public override double GridSize
        {
            get { return 0.01; }
            set { }
        }

        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);

            VDWidgetTitlePort.DecoratorsInitialized += VDWidgetTitlePort.BindShapeFields;

            // bind other shape fields
            this.BindShapeFieldsToDomainProperties();
        } 
    }
}
