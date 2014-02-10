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
        public const double MIN_COL_WIDTH = 1.0;
        public const double MIN_ROW_HEIGHT = 0.4;
    }

    ////////////////////////////////////////////////////////////////////////////////
    // MEL
    ////////////////////////////////////////////////////////////////////////////////
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

            VDVertContainer headContainer = this.Store.ElementFactory.CreateElement(VDVertContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_HEAD_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true)) as VDVertContainer;
            headContainer.TopSibling = titleContainer;

            VDVertContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDVertContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_BODY_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true)) as VDVertContainer;

            VDVertContainer footContainer = this.Store.ElementFactory.CreateElement(VDVertContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, TABLE_FOOT_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasBottomAnchorDomainPropertyId, true)) as VDVertContainer;

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
                VDContainer container = GetChild<VDVertContainer>(c => c.Tag == TABLE_HEAD_CONTAINER_TAG);
                return container;
            }
        }

        public VDContainer TableBodyContainer
        {
            get
            {
                VDContainer container = GetChild<VDVertContainer>(c => c.Tag == TABLE_BODY_CONTAINER_TAG);
                return container;
            }
        }

        public VDContainer TableFootContainer
        {
            get
            {
                VDContainer container = GetChild<VDVertContainer>(c => c.Tag == TABLE_FOOT_CONTAINER_TAG);
                return container;
            }
        }

        public List<VDTableRow> HeadRows
        {
            get
            {
                List<VDTableRow> rows = new List<VDTableRow>();
                findTableRowsRecursively(this.TableHeadContainer, rows);
                return rows;
            }
        }

        public List<VDTableRow> BodyRows
        {
            get
            {
                List<VDTableRow> rows = new List<VDTableRow>();
                findTableRowsRecursively(this.TableBodyContainer, rows);
                return rows;
            }
        }

        public List<VDTableRow> FootRows
        {
            get
            {
                List<VDTableRow> rows = new List<VDTableRow>();
                findTableRowsRecursively(this.TableFootContainer, rows);
                return rows;
            }
        }

        private void findTableRowsRecursively(VDWidget parent, List<VDTableRow> rows)
        {
            if (parent == null) return;
            foreach (var w in parent.Children)
            {
                if (w is VDTableRow)
                    rows.Add((VDTableRow)w);
                else if (w is VDTable) // stop recursion when met table
                    continue;
                else if (w is VDWidget)
                    findTableRowsRecursively((VDWidget)w, rows);
            }
        }

        // 
        // property handler
        internal sealed partial class ColCountPropertyHandler : DomainPropertyValueHandler<VDTable, UInt32>
        {
            protected override void OnValueChanging(VDTable table, uint oldValue, uint newValue)
            {
                if (oldValue > newValue)
                {
                    uint lastCol = newValue;

                    // don't allow to delete columns if there are cells crossing the delete columns (colspan > 1)
                    //VDTableCell cell = table.HeadRows.Find(c => c.Col < lastCol && c.LastCol >= lastCol);
                    //if (cell != null)
                    //{
                    //    throw new Exception(string.Format("Cell[{0},{1}] can not be deleted, please split it first", cell.Row, cell.Col));
                    //}

                    //// don't allow to delete cells which are not empty
                    //cell = table.Cells.Find(c => c.Col >= lastCol && c.Children.Count > 0);
                    //if (cell != null)
                    //{
                    //    throw new Exception(string.Format("Cell[{0},{1}] can not be deleted because it is not empty", cell.Row, cell.Col));
                    //}
                }
                base.OnValueChanging(table, oldValue, newValue);
            }
        }
    }

    public partial class VDTableRowWrapper
    {
        public override bool HasWidgetTitle { get { return true; } }
    }

    public partial class VDTableRow : ICustomMerge
    {
        public void MergeTo(VDWidget targetWidget, ElementGroup elementGroup)
        {
            VDWidget parent = targetWidget;
            bool bIsInTable = false;
            while (parent != null)
            {
                if (parent is VDTable)
                {
                    bIsInTable = true;
                    break;
                }
                else
                {
                    parent = parent.Parent;
                }
            }

            if (bIsInTable)
            {
                targetWidget.Children.Add(this);
                VDTable table = parent as VDTable;
                this.ColCount = table.ColCount;
                this.RowCount = 1; // default row count
            }
            else
            {
                // if Row widget is not inserted in to a table, create a wrapper to include it
                VDTableRowWrapper wrapper = this.Store.ElementFactory.CreateElement(VDTableRowWrapper.DomainClassId) as VDTableRowWrapper;

                // create title
                wrapper.Title = this.Store.ElementFactory.CreateElement(VDWidgetTitle.DomainClassId) as VDWidgetTitle;

                VDHoriContainer titleContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, VDTable.TITLE_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDHoriContainer.FixedHeightDomainPropertyId, TableConstants.TITLE_SIZE)) as VDHoriContainer;

                VDVertContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDVertContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, VDTable.TABLE_BODY_CONTAINER_TAG),
                    new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true),
                    new PropertyAssignment(VDContainer.HasBottomAnchorDomainPropertyId, true)) as VDVertContainer;
                bodyContainer.TopSibling = titleContainer;
                wrapper.Children.Add(titleContainer);
                wrapper.Children.Add(bodyContainer);

                // !!
                // have to create a new TableRow instead of using 'this', otherwise the table 
                // row shape can not be created, why ????
                VDTableRow row = this.Store.ElementFactory.CreateElement(VDTableRow.DomainClassId) as VDTableRow;
                bodyContainer.Children.Add(row);
                //bodyContainer.Children.Add(this);
                targetWidget.Children.Add(wrapper);
                row.RowCount = 1;
            }
        }

        public VDTable Table
        {
            get
            {
                VDTable table = null;
                if (this.Parent != null && this.Parent.Parent != null)
                {
                    VDWidget parent = this.Parent.Parent as VDWidget;
                    while (parent != null && !(parent is VDTable))
                    {
                        parent = parent.Parent as VDWidget;
                    }
                    table = parent as VDTable;
                }
                return table;
            }
        }
    }

    public partial class VDTableCell
    {
        public uint LastCol { get { return this.Col + this.ColSpan - 1; } }
        public uint LastRow { get { return this.Row + this.RowSpan - 1; } }
    }

    // rules
    [RuleOn(typeof(VDTable), FireTime = TimeToFire.TopLevelCommit, Priority = 0x6FFFFFF0)]
    public class VDTableColCountFixupRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.ModelElement != null && e.ModelElement is VDTable && e.DomainProperty != null 
                && e.DomainProperty.Id == VDTable.ColCountDomainPropertyId)
            {
                if (e.ModelElement.Store.InSerializationTransaction) return;

                VDTable table = e.ModelElement as VDTable;
                VDContainer colTitleContainer = table.ColTitleContainer;
                uint oldColCount = (uint)e.OldValue;
                uint newColCount = (uint)e.NewValue;

                if (colTitleContainer != null)
                {
                    if (oldColCount > newColCount) // remove column titles
                    {
                        List<VDTableColTitle> toDelList = colTitleContainer.GetChildren<VDTableColTitle>(t => t.Index + 1 > newColCount);
                        toDelList.ForEach(cell => cell.Delete());
                    }
                    else if (oldColCount < newColCount) // add column titles
                    {
                        for (uint idxToAdd = oldColCount; idxToAdd < newColCount; ++idxToAdd)
                        {
                            colTitleContainer.Children.Add(new VDTableColTitle(table.Partition, 
                                new PropertyAssignment(VDTableColTitle.IndexDomainPropertyId, idxToAdd)));
                        }
                    }

                    // adjust ColCount for TableHead, TableBody and TableFoot
                    foreach (VDTableRow row in table.HeadRows) { row.ColCount = newColCount; }
                    foreach (VDTableRow row in table.BodyRows) { row.ColCount = newColCount; }
                    foreach (VDTableRow row in table.FootRows) { row.ColCount = newColCount; }
                }
            }
        }
    }

    // to add and remove TableRowTitle when RowCount property changed
    [RuleOn(typeof(VDTableRow), FireTime = TimeToFire.TopLevelCommit, Priority = 0x6FFFFFFA)]
    public class VDTableRowCountFixupRule_ForRowTitle: ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.ModelElement != null && e.ModelElement is VDTableRow && e.DomainProperty != null)
            {
                if (e.ModelElement.Store.InSerializationTransaction) return;

                VDTableRow tableRow = e.ModelElement as VDTableRow;
                int oldRowCount = (int)((uint)e.OldValue);
                int newRowCount = (int)((uint)e.NewValue);

                if (e.DomainProperty.Id == VDTableRow.RowCountDomainPropertyId)
                {
                    if (oldRowCount > newRowCount) // remove row titles
                    {
                        List<VDTableRowTitle> toDelList = tableRow.GetChildren<VDTableRowTitle>(t => t.Index + 1 > newRowCount);
                        toDelList.ForEach(cell => cell.Delete());
                    }
                    else if (oldRowCount < newRowCount) // add row titles
                    {
                        for (int idxToAdd = oldRowCount; idxToAdd < newRowCount; ++idxToAdd)
                        {
                            tableRow.Children.Add(new VDTableRowTitle(
                                    tableRow.Partition, new PropertyAssignment(VDTableRowTitle.IndexDomainPropertyId, (uint)idxToAdd)));
                        }
                    }
                }
            }
        }
    }

    // to add and remove TableCell when RowCount/ColCount property changed, this is done after TableColTitle is fixed (above rule)
    [RuleOn(typeof(VDTableRow), FireTime = TimeToFire.TopLevelCommit, Priority = 0x6FFFFFFF/*lower priority then above rule*/)] 
    public class VDTableRowCountFixupRule_ForTableCell : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.ModelElement != null && e.ModelElement is VDTableRow && e.DomainProperty != null)
            {
                if (e.ModelElement.Store.InSerializationTransaction) return;
                
                VDTableRow tableRow = e.ModelElement as VDTableRow;
                VDTable table = tableRow.Table;
                if (table == null) return;

                if (e.DomainProperty.Id == VDTableRow.RowCountDomainPropertyId)
                {
                    uint oldRowCount = (uint)e.OldValue;
                    uint newRowCount = (uint)e.NewValue;

                    if (oldRowCount > newRowCount) // remove row cells
                    {
                        List<VDTableCell> toDelList = tableRow.GetChildren<VDTableCell>(c => c.Row + 1 > newRowCount);
                        toDelList.ForEach(cell => cell.Delete());
                    }
                    else if (oldRowCount < newRowCount) // add row cells
                    {
                        for (uint row = oldRowCount; row < newRowCount; ++row)
                        {
                            for (uint col = 0; col < table.ColCount; ++col)
                            {
                                tableRow.Children.Add(new VDTableCell( tableRow.Partition, 
                                    new PropertyAssignment(VDTableCell.RowDomainPropertyId, row),
                                    new PropertyAssignment(VDTableCell.ColDomainPropertyId, col)));
                            }
                        }
                    }
                }
                else if (e.DomainProperty.Id == VDTableRow.ColCountDomainPropertyId)
                {
                    uint oldColCount = (uint)e.OldValue;
                    uint newColCount = (uint)e.NewValue;

                    if (oldColCount > newColCount) // remove row cells
                    {
                        List<VDTableCell> toDelList = tableRow.GetChildren<VDTableCell>(c => c.Col + 1 > newColCount);
                        toDelList.ForEach(cell => cell.Delete());
                    }
                    else if (oldColCount < newColCount) // add row cells
                    {
                        for (uint row = 0; row < tableRow.RowCount; ++row)
                        {
                            for (uint col = oldColCount; col < newColCount; ++col)
                            {
                                tableRow.Children.Add(new VDTableCell(tableRow.Partition,
                                    new PropertyAssignment(VDTableCell.RowDomainPropertyId, row),
                                    new PropertyAssignment(VDTableCell.ColDomainPropertyId, col)));
                            }
                        }
                    }
                }
            }
        }
    }

    [RuleOn(typeof(VDTableCell), FireTime = TimeToFire.TopLevelCommit, Priority = 0x6FFFFFFF)]
    public class VDTableCellSpanFixupRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.ModelElement == null || !(e.ModelElement is VDTableCell) || e.DomainProperty == null) return;

            if (e.ModelElement.Store.InSerializationTransaction) return;

            VDTableCell cell = e.ModelElement as VDTableCell;
            VDTableRow row = cell.Parent as VDTableRow;
            if (row == null) return;
    
            int oldSpan = (int)((uint)e.OldValue);
            int newSpan = (int)((uint)e.NewValue);

                if (e.DomainProperty.Id == VDTableCell.RowSpanDomainPropertyId)
                {
                    if (oldSpan > newSpan) // decrease rowspan value,  fill with new cells
                    {
                        for (int iRow = (int)(cell.Row + newSpan); iRow < cell.Row + oldSpan; iRow++)
                        {
                            for (int iCol = (int)cell.Col; iCol < cell.Col + cell.ColSpan; iCol++)
                            {
                                row.Children.Add(new VDTableCell(
                                    cell.Partition,
                                    new PropertyAssignment(VDTableCell.RowDomainPropertyId, (uint)iRow),
                                    new PropertyAssignment(VDTableCell.ColDomainPropertyId, (uint)iCol)
                                ));
                            }
                        }
                    }
                    else if (oldSpan < newSpan) // increase rowspan value, delete neighbour cells
                    {
                        List<VDTableCell> cellsToDel = row.GetChildren<VDTableCell>(x => x.Row >= (cell.Row + oldSpan)
                                                                                      && x.Row < (cell.Row + newSpan)
                                                                                      && x.Col >= cell.Col
                                                                                      && x.LastCol <= cell.LastCol);
                        cellsToDel.ForEach(x => x.Delete());
                    }
                }
                else if (e.DomainProperty.Id == VDTableCell.ColSpanDomainPropertyId)
                {
                    if (oldSpan > newSpan) // decrease colspan value,  fill with new cells
                    {
                        for (int iCol = (int)(cell.Col + newSpan); iCol < cell.Col + oldSpan; iCol++)
                        {
                            for (int iRow = (int)cell.Row; iRow < cell.Row + cell.RowSpan; iRow++)
                            {
                                row.Children.Add(new VDTableCell(
                                    cell.Partition,
                                    new PropertyAssignment(VDTableCell.RowDomainPropertyId, (uint)iRow),
                                    new PropertyAssignment(VDTableCell.ColDomainPropertyId, (uint)iCol)
                                ));
                            }
                        }
                    }
                    else if (oldSpan < newSpan) // increase colspan value, delete neighbour cells
                    {
                        List<VDTableCell> cellsToDel = row.GetChildren<VDTableCell>(x => x.Col >= (cell.Col + oldSpan)
                                                                                      && x.Col < (cell.Col + newSpan)
                                                                                      && x.Row >= cell.Row
                                                                                      && x.LastRow <= cell.LastRow);
                        cellsToDel.ForEach(x => x.Delete());
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

        protected override Image getTitleIcon() { return this.getImageFromResource("TableToolToolboxBitmap"); }

        // utilies
        public VDHoriContainerShape ColTitleContainerShape
        {
            get
            {
                VDHoriContainerShape titleContainerShape = this.GetChildShape<VDHoriContainerShape>(
                    c => c.ModelElement != null && ((VDContainer)c.ModelElement).Tag == VDTable.TITLE_CONTAINER_TAG);
                return titleContainerShape;
            }
        }

        public VDVertContainerShape TableHeadShape
        {
            get
            {
                VDVertContainerShape headContainerShape = this.GetChildShape<VDVertContainerShape>(
                    c => c.ModelElement != null && ((VDContainer)c.ModelElement).Tag == VDTable.TABLE_HEAD_CONTAINER_TAG);
                return headContainerShape;
            }
        }

        public VDVertContainerShape TableBodyShape
        {
            get
            {
                VDVertContainerShape bodyContainerShape = this.GetChildShape<VDVertContainerShape>(
                    c => c.ModelElement != null && ((VDContainer)c.ModelElement).Tag == VDTable.TABLE_BODY_CONTAINER_TAG);
                return bodyContainerShape;
            }
        }

        public VDVertContainerShape TableFootShape
        {
            get
            {
                VDVertContainerShape footContainerShape = this.GetChildShape<VDVertContainerShape>(
                    c => c.ModelElement != null && ((VDContainer)c.ModelElement).Tag == VDTable.TABLE_FOOT_CONTAINER_TAG);
                return footContainerShape;
            }
        }

        public List<VDTableColTitleShape> ColTitleList
        {
            get
            {
                if (this.ColTitleContainerShape != null)
                {
                    List<VDTableColTitleShape> titles = this.ColTitleContainerShape.GetChildShapes<VDTableColTitleShape>();
                    return titles;
                }
                return null;
            }
        }

        public List<VDTableRowShape> HeadRowShapes
        {
            get
            {
                List<VDTableRowShape> rows = new List<VDTableRowShape>();
                findTableRowRecursively(this.TableHeadShape, rows);
                return rows;
            }
        }

        public List<VDTableRowShape> BodyRowShapes
        {
            get
            {
                List<VDTableRowShape> rows = new List<VDTableRowShape>();
                findTableRowRecursively(this.TableBodyShape, rows);
                return rows;
            }
        }

        public List<VDTableRowShape> FootRowShapes
        {
            get
            {
                List<VDTableRowShape> rows = new List<VDTableRowShape>();
                findTableRowRecursively(this.TableFootShape, rows);
                return rows;
            }
        }

        private void findTableRowRecursively(VDWidgetShape parent, List<VDTableRowShape> rows)
        {
            if (parent == null) return;
            foreach(var w in parent.NestedChildShapes)
            {
                if (w is VDTableRowShape)
                    rows.Add((VDTableRowShape)w);
                else if (w is VDTableShape) // stop recursion when met table
                    continue;
                else if (w is VDWidgetShape)
                    findTableRowRecursively((VDWidgetShape)w, rows);
            }
        }

        public List<VDTableCellShape> HeadCellShapes
        {
            get
            {
                List<VDTableCellShape> cells = new List<VDTableCellShape>();
                if (HeadRowShapes != null)
                {
                    HeadRowShapes.ForEach(row => cells.AddRange(row.GetChildShapes<VDTableCellShape>()));
                }
                return cells;
            }
        }

        public List<VDTableCellShape> BodyCellShapes
        {
            get
            {
                List<VDTableCellShape> cells = new List<VDTableCellShape>();
                if (BodyRowShapes != null)
                {
                    BodyRowShapes.ForEach(row => cells.AddRange(row.GetChildShapes<VDTableCellShape>()));
                }
                return cells;
            }
        }

        public List<VDTableCellShape> FootCellShapes
        {
            get
            {
                List<VDTableCellShape> cells = new List<VDTableCellShape>();
                if (FootRowShapes != null)
                {
                    FootRowShapes.ForEach(row => cells.AddRange(row.GetChildShapes<VDTableCellShape>()));
                }
                return cells;
            }
        }
    }

    public partial class VDTableRowWrapperShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("TableRowsToolToolboxBitmap"); }

        protected override string getTitleText() { return "Table Rows"; }
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
                VDWidgetShape parentShape = rowShape.ParentShape as VDWidgetShape;

                if (rowShape != null && parentShape != null)
                {
                    if (Math.Abs(compliantBounds.X) > VDConstants.DOUBLE_DIFF ||
                        Math.Abs(compliantBounds.Width - parentShape.BoundingBox.Width) > VDConstants.DOUBLE_DIFF)
                    {
                        compliantBounds = new RectangleD(0, compliantBounds.Y, parentShape.BoundingBox.Width, compliantBounds.Height);
                    }
                }

                return compliantBounds;
            }
        }

        public VDTableShape TableShape
        {
            get
            {
                if (this.ParentShape != null && this.ParentShape.ParentShape !=null)
                {
                    VDWidgetShape parent = this.ParentShape.ParentShape as VDWidgetShape;
                    while (parent != null && !(parent is VDTableShape))
                    {
                        parent = parent.ParentShape as VDWidgetShape;
                    }

                    VDTableShape tableShape = parent as VDTableShape;
                    return tableShape;
                }
                return null;
            }
        }

        public List<VDTableRowTitleShape> RowTitleList
        {
            get
            {
                List<VDTableRowTitleShape> titles = this.GetChildShapes<VDTableRowTitleShape>();
                return titles;
            }
        }

        public List<VDTableCellShape> CellList
        {
            get
            {
                List<VDTableCellShape> cells = this.GetChildShapes<VDTableCellShape>();
                return cells;
            }
        }
    }

    public partial class VDTableCellShape
    {
        public uint GetColSpanValue()
        {
            return this.ModelElement != null ? this.GetMEL<VDTableCell>().ColSpan : 1;
        }

        public void SetColSpanValue(uint newvalue)
        {
            if (this.ModelElement != null) { this.GetMEL<VDTableCell>().ColSpan = newvalue; }
        }

        internal sealed partial class ColSpanPropertyHandler : DomainPropertyValueHandler<VDTableCellShape, UInt32>
        {
            protected override void OnValueChanged(VDTableCellShape element, uint oldValue, uint newValue)
            {
                base.OnValueChanged(element, oldValue, newValue);

                // trigger bounds rules
                element.OnBoundsFixup(BoundsFixupState.ViewFixup, 0, false);
            }
        }

        public uint GetRowSpanValue()
        {
            return this.ModelElement != null ? this.GetMEL<VDTableCell>().RowSpan : 1;
        }

        public void SetRowSpanValue(uint newvalue)
        {
            if (this.ModelElement != null) { this.GetMEL<VDTableCell>().RowSpan = newvalue; }
        }

        internal sealed partial class RowSpanPropertyHandler : DomainPropertyValueHandler<VDTableCellShape, UInt32>
        {
            protected override void OnValueChanged(VDTableCellShape element, uint oldValue, uint newValue)
            {
                base.OnValueChanged(element, oldValue, newValue);

                //element.Size = SizeD.Empty; // trigger bounds rules
                // trigger bounds rules
                element.OnBoundsFixup(BoundsFixupState.ViewFixup, 0, false);
            }
        }

        public override bool CanMove { get { return false; } }

        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override BoundsRules BoundsRules { get { return TableCellBoundsRules.Instance; } }

        class TableCellBoundsRules : BoundsRules
        {
            private static TableCellBoundsRules _instance;
            public static TableCellBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new TableCellBoundsRules();
                    return _instance;
                }
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDTableCellShape cellShape = shape as VDTableCellShape;
                VDTableRowShape rowShape = cellShape.ParentShape as VDTableRowShape;

                if (cellShape != null && rowShape != null )
                {
                    VDTableShape tableShape = rowShape.TableShape;
                    VDTableCell cell = cellShape.GetMEL<VDTableCell>();

                    if (tableShape != null && cell != null)
                    {
                        List<VDTableColTitleShape> colTitles = tableShape.ColTitleList;
                        List<VDTableRowTitleShape> rowTitles = rowShape.RowTitleList;

                        if (colTitles != null && rowTitles != null && cell.Row < rowTitles.Count && cell.Col < colTitles.Count)
                        {
                            compliantBounds.X = colTitles[(int)cell.Col].Location.X;
                            compliantBounds.Y = rowTitles[(int)cell.Row].Location.Y;

                            // calculated width and height based on SPAN values
                            double width = 0, height = 0;
                            for (int i = (int)cell.Col; i < cell.Col + cell.ColSpan; i++) width += colTitles[i].Size.Width;
                            for (int i = (int)cell.Row; i < cell.Row + cell.RowSpan; i++) height += rowTitles[i].Size.Height;
                            compliantBounds.Width = width;
                            compliantBounds.Height = height;

                            if (compliantBounds.Width < TableConstants.MIN_COL_WIDTH)
                                compliantBounds.Width = TableConstants.MIN_COL_WIDTH;
                            if (compliantBounds.Height < TableConstants.MIN_ROW_HEIGHT)
                                compliantBounds.Height = TableConstants.MIN_ROW_HEIGHT;
                        }
                    }
                }

                return compliantBounds;
            }
        }
    }

    public partial class VDTableColTitleShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Right; } }

        public override bool CanMove { get { return false; } }

        public override BoundsRules BoundsRules { get { return ColTitleBoundsRules.Instance; } }

        public class ColTitleBoundsRules : BoundsRules
        {
            private static ColTitleBoundsRules _instance;
            public static ColTitleBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new ColTitleBoundsRules();
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
                    if (compliantBounds.Width < TableConstants.MIN_COL_WIDTH)
                        compliantBounds.Width = TableConstants.MIN_COL_WIDTH;

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

    public partial class VDTableRowTitleShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Bottom; } }

        public override bool CanMove { get { return false; } }

        public override BoundsRules BoundsRules { get { return RowTitleBoundsRules.Instance; } }

        public class RowTitleBoundsRules : BoundsRules
        {
            private static RowTitleBoundsRules _instance;
            public static RowTitleBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new RowTitleBoundsRules();
                    return _instance;
                }
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDTableRowTitleShape titleShape = shape as VDTableRowTitleShape;
                VDTableRowTitle title = titleShape.GetMEL<VDTableRowTitle>();
                if (titleShape != null && title != null)
                {
                    compliantBounds.X = 0;
                    if (compliantBounds.Y < 0) compliantBounds.Y = 0;
                    compliantBounds.Width = TableConstants.TITLE_SIZE;
                    if (compliantBounds.Height < TableConstants.MIN_ROW_HEIGHT)
                        compliantBounds.Height = TableConstants.MIN_ROW_HEIGHT;

                    if (title.Index > 0)
                    {
                        VDTableRowShape parentShape = titleShape.ParentShape as VDTableRowShape;
                        if (parentShape != null)
                        {
                            List<VDTableRowTitleShape> rowTitles = parentShape.GetChildShapes<VDTableRowTitleShape>();
                            if (rowTitles.Count >= title.Index)
                            {
                                compliantBounds.Y = rowTitles[(int)(title.Index - 1)].Bounds.Bottom;
                            }
                        }
                    }
                    else
                    {
                        compliantBounds.Y = 0;
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
            if (title == null) return;

            if (titleShape.ParentShape == null) return;
            VDHoriContainerShape titleContainerShape = titleShape.ParentShape as VDHoriContainerShape;

            if (titleContainerShape.ParentShape == null) return;
            VDTableShape tableShape = titleContainerShape.ParentShape as VDTableShape;

            // when bounds changed, adjust 
            //      1) other title cells position
            //      2) related data cells size/position
            if (e.DomainProperty.Id == VDTableColTitleShape.AbsoluteBoundsDomainPropertyId)
            {
                RectangleD oldRect = (RectangleD)e.OldValue;
                RectangleD newRect = (RectangleD)e.NewValue;

                // just moved, no need to adjust
                if (Math.Abs(oldRect.Width - newRect.Width) < VDConstants.DOUBLE_DIFF) return;

                if (title != null && titleContainerShape != null)
                {
                    // 1) fix title cells position
                    List<VDTableColTitleShape> titleShapeList = titleContainerShape.GetChildShapes<VDTableColTitleShape>();
                    titleShapeList = (from x in titleShapeList
                                      where x.ModelElement != null && x.GetMEL<VDTableColTitle>().Index > title.Index
                                      orderby x.GetMEL<VDTableColTitle>().Index
                                      select x).ToList();
                    titleShapeList.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules

                    // 2) fix data cells size/position
                    List<VDTableCellShape> cells = new List<VDTableCellShape>();
                    cells.AddRange(tableShape.HeadCellShapes);
                    cells.AddRange(tableShape.BodyCellShapes);
                    cells.AddRange(tableShape.FootCellShapes);
                    cells = (from x in cells
                             where x.ModelElement != null && x.GetMEL<VDTableCell>().LastCol >= title.Index
                             orderby x.GetMEL<VDTableCell>().Row
                             orderby x.GetMEL<VDTableCell>().Col
                             select x).ToList();
                    cells.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules
                }
            }
        }
    }

    [RuleOn(typeof(VDTableRowTitleShape), FireTime = TimeToFire.TopLevelCommit, Priority = 0x7FFFFFFF)]
    public class VDTableRowTitleShape_BoundsFixupRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            base.ElementPropertyChanged(e);

            if (e.ModelElement == null || e.DomainProperty == null) return;

            VDTableRowTitleShape titleShape = e.ModelElement as VDTableRowTitleShape;

            VDTableRowTitle title = titleShape.GetMEL<VDTableRowTitle>();
            if (title == null) return;

            if (titleShape.ParentShape == null) return;
            VDTableRowShape rowShape = titleShape.ParentShape as VDTableRowShape;

            VDTableShape tableShape = rowShape.TableShape;
            if (tableShape == null) return;

            // when bounds changed, adjust 
            //      1) other head cells position
            //      2) related data cells size/position
            if (e.DomainProperty.Id == VDTableRowTitleShape.AbsoluteBoundsDomainPropertyId)
            {
                RectangleD oldRect = (RectangleD)e.OldValue;
                RectangleD newRect = (RectangleD)e.NewValue;

                // just moved, no need to adjust
                if (Math.Abs(oldRect.Height - newRect.Height) < VDConstants.DOUBLE_DIFF) return;

                // 1) fix head cells position
                List<VDTableRowTitleShape> titleShapeList = rowShape.GetChildShapes<VDTableRowTitleShape>();
                titleShapeList = (from x in titleShapeList
                                  where x.ModelElement != null && x.GetMEL<VDTableRowTitle>().Index > title.Index
                                  orderby x.GetMEL<VDTableRowTitle>().Index
                                  select x).ToList();
                titleShapeList.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules

                // 2) fix data cells size/position
                List<VDTableCellShape> cells = new List<VDTableCellShape>();
                cells.AddRange(tableShape.HeadCellShapes);
                cells.AddRange(tableShape.BodyCellShapes);
                cells.AddRange(tableShape.FootCellShapes);
                cells = (from x in cells
                         where x.ModelElement != null && x.GetMEL<VDTableCell>().LastRow >= title.Index
                         orderby x.GetMEL<VDTableCell>().Row
                         orderby x.GetMEL<VDTableCell>().Col
                         select x).ToList();
                cells.ForEach(c => c.Size = SizeD.Empty); // trigger bounds rules
            }
        }
    }
}
