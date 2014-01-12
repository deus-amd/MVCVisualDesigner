using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public class TableConstants
    {
        public const double TITLE_SIZE = 0.1;
        public const double MIN_ROW_WIDTH = 1.0;
        public const double MIN_COL_HEIGHT = 0.4;
    }

    ////////////////////////////////////////////////////////////////////////////////
    // MEL
    public partial class VDTable : ICustomMerge
    {
        public const string TITLE_CONTAINER_TAG = "Column Title";
        public const string TABLE_HEAD_CONTAINER_TAG = "Table Head";
        public const string TABLE_BODY_CONTAINER_TAG = "Table Body";
        public const string TABLE_FOOT_CONTAINER_TAG = "Table Foot";

        public override bool HasWidgetTitle { get { return true; } }

        public void MergeTo(VDWidget targetWidget, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(this);

            VDHoriContainer titleContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, TITLE_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true),
                new PropertyAssignment(VDHoriContainer.FixedHeightDomainPropertyId, TableConstants.TITLE_SIZE)) as VDHoriContainer;

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

            this.Children.Add(titleContainer);
            this.Children.Add(headContainer);
            this.Children.Add(bodyContainer);
            this.Children.Add(footContainer);

            VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId,
                new PropertyAssignment(VDHoriSeparator.DefaultYDomainPropertyId, TableConstants.TITLE_SIZE)) as VDHoriSeparator;
            hSeparator.TopWidget = headContainer;
            hSeparator.BottomWidget = bodyContainer;
            this.Children.Add(hSeparator);

            hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId,
                /* 3: table def height, 0.05: separator def height, 0.01: table border thickness */
                new PropertyAssignment(VDHoriSeparator.DefaultYDomainPropertyId, 3 - 0.05 - 0.01)) as VDHoriSeparator;
            hSeparator.TopWidget = bodyContainer;
            hSeparator.BottomWidget = footContainer;
            this.Children.Add(hSeparator);

            //
            VDTableHead head = this.Store.ElementFactory.CreateElement(VDTableHead.DomainClassId) as VDTableHead;
            VDTableBody body = this.Store.ElementFactory.CreateElement(VDTableBody.DomainClassId) as VDTableBody;
            VDTableFoot foot = this.Store.ElementFactory.CreateElement(VDTableFoot.DomainClassId) as VDTableFoot;
            headContainer.Children.Add(head);
            bodyContainer.Children.Add(body);
            footContainer.Children.Add(foot);
        }

        // utilities
        public VDContainer ColTitleContainer
        {
            get
            {
                VDHoriContainer container = GetChild<VDHoriContainer>(c => c.Tag == TITLE_CONTAINER_TAG);
                return container;
            }
        }

        public VDContainer TableHeadContainer
        {
            get
            {
                VDContainer container = GetChild<VDFullFilledContainer>(c => c.Tag == TABLE_HEAD_CONTAINER_TAG);
                return container;
            }
        }

        public VDContainer TableBodyContainer
        {
            get
            {
                VDContainer container = GetChild<VDFullFilledContainer>(c => c.Tag == TABLE_BODY_CONTAINER_TAG);
                return container;
            }
        }

        public VDContainer TableFootContainer
        {
            get
            {
                VDContainer container = GetChild<VDFullFilledContainer>(c => c.Tag == TABLE_FOOT_CONTAINER_TAG);
                return container;
            }
        }

        // property handler
        internal sealed partial class ColCountPropertyHandler : DomainPropertyValueHandler<VDTable, UInt32>
        {
            protected override void OnValueChanging(VDTable element, uint oldValue, uint newValue)
            {
                if (oldValue > newValue) // delete columns, in following case deleting columns is forbidden
                {
                //    uint lastCol = newValue;

                //    // don't allow to delete columns if there are cells crossing the delete columns (colspan > 1)
                //    VDTableColTitle cell = element.Cells.Find(c => c.Col < lastCol && c.LastCol >= lastCol);
                //    if (cell != null)
                //    {
                //        throw new Exception(string.Format("Cell[{0},{1}] can not be deleted, please split it first", cell.Row, cell.Col));
                //    }

                //    // don't allow to delete cells which are not empty
                //    cell = element.Cells.Find(c => c.Col >= lastCol && c.Children.Count > 0);
                //    if (cell != null)
                //    {
                //        throw new Exception(string.Format("Cell[{0},{1}] can not be deleted because it is not empty", cell.Row, cell.Col));
                //    }
                }
                base.OnValueChanging(element, oldValue, newValue);
            }

            protected override void OnValueChanged(VDTable element, uint oldValue, uint newValue)
            {
                base.OnValueChanged(element, oldValue, newValue);
            }
        }
    }

    public partial class VDTableRow : ICustomMerge
    {
        public void MergeTo(VDWidget targetWidget, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(this);

            VDVertContainer titleContainer = this.Store.ElementFactory.CreateElement(VDVertContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, VDTable.TITLE_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasBottomAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDVertContainer.FixedWidthDomainPropertyId, TableConstants.TITLE_SIZE)) as VDVertContainer;
            this.Children.Add(titleContainer);
        }
    }

    // rules
    [RuleOn(typeof(VDTable), FireTime = TimeToFire.TopLevelCommit, Priority = 0x6FFFFFFE)]
    public class VDTableColCountFixupRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.ModelElement != null && e.ModelElement is VDTable && e.DomainProperty != null)
            {
                if (e.ModelElement.Store.InSerializationTransaction) return;

                VDTable table = e.ModelElement as VDTable;
                VDContainer colTitleContainer = table.ColTitleContainer;
                int oldColCount = (int)((uint)e.OldValue);
                int newColCount = (int)((uint)e.NewValue);

                if (e.DomainProperty.Id == VDTable.ColCountDomainPropertyId && colTitleContainer != null)
                {
                    if (oldColCount > newColCount) // remove columns
                    {
                        List<VDTableColTitle> toDelList = new List<VDTableColTitle>();
                        foreach (VDTableColTitle t in colTitleContainer.GetChildren<VDTableColTitle>())
                        {
                            if (t.Index + 1 > newColCount)
                            {
                                toDelList.Add(t);
                            }
                        }
                        toDelList.ForEach(cell => cell.Delete());
                    }
                    else if (oldColCount < newColCount) // add columns
                    {
                        for (int idxToAdd = oldColCount; idxToAdd < newColCount; ++idxToAdd)
                        {
                            colTitleContainer.Children.Add(new VDTableColTitle(
                                    table.Partition, new PropertyAssignment(VDTableColTitle.IndexDomainPropertyId, (uint)idxToAdd)));
                        }
                    }
                }
            }
        }
    }



    ////////////////////////////////////////////////////////////////////////////////
    //  PEL
    ////////////////////////////////////////////////////////////////////////////////
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

    public partial class VDTableRowShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Vertical; } }

        public override BoundsRules BoundsRules { get { return TableRowBoundsRules.Instance; } }

        class TableRowBoundsRules : BoundsRules
        {
            private static TableRowBoundsRules _instance;
            public static TableRowBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new TableRowBoundsRules();
                    return _instance;
                }
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDTableRowShape rowShape = shape as VDTableRowShape;
                if (rowShape != null && rowShape.ParentShape != null)
                {
                    if (Math.Abs(compliantBounds.X) > VDConstants.DOUBLE_DIFF ||
                        Math.Abs(compliantBounds.Width - rowShape.ParentShape.BoundingBox.Width) > VDConstants.DOUBLE_DIFF)
                    {
                        compliantBounds = new RectangleD(0, compliantBounds.Y, rowShape.ParentShape.BoundingBox.Width, compliantBounds.Height);
                    }
                }

                return compliantBounds;
            }
        }
    }

    partial class VDTableColTitleShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Right; } }

        public override bool CanMove { get { return false; } }

        public override BoundsRules BoundsRules { get { return HeadCellBoundsRules.Instance; } }

        public class HeadCellBoundsRules : BoundsRules
        {
            private static HeadCellBoundsRules _instance;
            public static HeadCellBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new HeadCellBoundsRules();
                    return _instance;
                }
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDTableColTitleShape titleShape = shape as VDTableColTitleShape;
                VDTableColTitle title = titleShape.GetMEL<VDTableColTitle>();
                if (titleShape != null && title != null)
                {
                    if (compliantBounds.X < TableConstants.TITLE_SIZE)
                        compliantBounds.X = TableConstants.TITLE_SIZE;
                    compliantBounds.Y = 0;
                    compliantBounds.Height = TableConstants.TITLE_SIZE;
                    if (compliantBounds.Width < TableConstants.MIN_ROW_WIDTH)
                        compliantBounds.Width = TableConstants.MIN_ROW_WIDTH;

                    if (title.Index > 0)
                    {
                        VDHoriContainerShape container = titleShape.ParentShape as VDHoriContainerShape;
                        if (container != null)
                        {
                            List<VDTableColTitleShape> colTitles = container.GetChildShapes<VDTableColTitleShape>();
                            if (colTitles.Count >= title.Index)
                            {
                                compliantBounds.X = colTitles[(int)(title.Index - 1)].Bounds.Right;
                            }
                        }
                    }
                    else
                    {
                        compliantBounds.X = TableConstants.TITLE_SIZE;
                    }
                }

                return compliantBounds;
            }
        }
    }


    // rules
    [RuleOn(typeof(VDTableColTitleShape), FireTime = TimeToFire.TopLevelCommit, Priority = 0x7FFFFFFF)]
    public class VDTableColTitleShape_BoundsFixupRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            base.ElementPropertyChanged(e);

            if (e.ModelElement == null || e.DomainProperty == null) return;

            VDTableColTitleShape titleShape = e.ModelElement as VDTableColTitleShape;
            VDTableColTitle title = titleShape.GetMEL<VDTableColTitle>();
            VDHoriContainerShape titleContainerShape = titleShape.ParentShape as VDHoriContainerShape;

            // when bounds changed, adjust 
            //      1) other head cells position
            //      2) related data cells size/position
            if (e.DomainProperty.Id == VDTableColTitleShape.AbsoluteBoundsDomainPropertyId)
            {
                RectangleD oldRect = (RectangleD)e.OldValue;
                RectangleD newRect = (RectangleD)e.NewValue;

                // just moved, no need to adjust
                if (Math.Abs(oldRect.Width - newRect.Width) < 0.001) return;

                if (title != null && titleContainerShape != null)
                {
                    // 1) fix head cells position
                    List<VDTableColTitleShape> titleShapeList = titleContainerShape.GetChildShapes<VDTableColTitleShape>();
                    titleShapeList = (from x in titleShapeList
                           where x.ModelElement != null && x.GetMEL<VDTableColTitle>().Index > title.Index
                           orderby x.GetMEL<VDTableColTitle>().Index
                           select x).ToList();
                    titleShapeList.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules

                    //// 2) fix data cells size/position
                    ////List<MVDGridLayoutCellShape> dcs = gridShape.GetChildShapes<MVDGridLayoutCellShape>();
                    ////if (headCellMEL.HeadType == GridLayoutHeadCellType.ColHead)
                    ////{
                    ////    dcs = (from x in dcs
                    ////           where x.ModelElement != null && x.GetMEL<MVDGridLayoutCell>().LastCol >= headCellMEL.Index
                    ////           orderby x.GetMEL<MVDGridLayoutCell>().Row
                    ////           orderby x.GetMEL<MVDGridLayoutCell>().Col
                    ////           select x).ToList();
                    ////}
                    ////else
                    ////{
                    ////    dcs = (from x in dcs
                    ////           where x.ModelElement != null && x.GetMEL<MVDGridLayoutCell>().LastRow >= headCellMEL.Index
                    ////           orderby x.GetMEL<MVDGridLayoutCell>().Row
                    ////           orderby x.GetMEL<MVDGridLayoutCell>().Col
                    ////           select x).ToList();

                    ////}
                    ////dcs.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules
                }
            }
        }
    }
}
