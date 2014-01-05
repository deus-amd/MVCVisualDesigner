using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    // MEL
    public partial class VDTable : ICustomMerge
    {
        public const string TITLE_CONTAINER_TAG = "Column Title";
        public const string TABLE_HEAD_CONTAINER_TAG = "Table Head";
        public const string TABLE_BODY_CONTAINER_TAG = "Table Body";
        public const string TABLE_FOOT_CONTAINER_TAG = "Table Foot";

        public override bool HasWidgetTitle { get { return true; } }

        public void CustomMergeRelate(VDWidget targetWidget, VDWidget sourceElement, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(sourceElement);

            VDTable table = sourceElement as VDTable;
            if (table != null)
            {
                VDHoriContainer titleContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, TITLE_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true)) as VDHoriContainer;

                VDFullFilledContainer headContainer = this.Store.ElementFactory.CreateElement(VDFullFilledContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_HEAD_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true)) as VDFullFilledContainer;
                headContainer.TopSibling = titleContainer;

                VDFullFilledContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDFullFilledContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_BODY_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true)) as VDFullFilledContainer;

                VDFullFilledContainer footContainer = this.Store.ElementFactory.CreateElement(VDFullFilledContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_FOOT_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasBottomAnchorDomainPropertyId, true)) as VDFullFilledContainer;

                table.Children.Add(titleContainer);
                table.Children.Add(headContainer);
                table.Children.Add(bodyContainer);
                table.Children.Add(footContainer);

                VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId) as VDHoriSeparator;
                hSeparator.TopWidget = headContainer;
                hSeparator.BottomWidget = bodyContainer;
                table.Children.Add(hSeparator);

                hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId) as VDHoriSeparator;
                hSeparator.TopWidget = bodyContainer;
                hSeparator.BottomWidget = footContainer;
                table.Children.Add(hSeparator);

                //
                VDTableHead head = this.Store.ElementFactory.CreateElement(VDTableHead.DomainClassId) as VDTableHead;
                VDTableBody body  = this.Store.ElementFactory.CreateElement(VDTableBody.DomainClassId) as VDTableBody;
                VDTableFoot foot = this.Store.ElementFactory.CreateElement(VDTableFoot.DomainClassId) as VDTableFoot;
                headContainer.Children.Add(head);
                bodyContainer.Children.Add(body);
                footContainer.Children.Add(foot);
            }
        }
    }


    // PEL
    public partial class VDTableShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon()
        {
            return this.getImageFromResource("TableToolToolboxBitmap");
        }
    }

    public partial class VDTableHeadShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }
    }

    public partial class VDTableBodyShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }
    }

    public partial class VDTableFootShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }
    }
}
