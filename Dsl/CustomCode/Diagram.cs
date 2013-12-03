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

        protected override void InitializeShapeFields(IList<Microsoft.VisualStudio.Modeling.Diagrams.ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);
        } 
    }
}
