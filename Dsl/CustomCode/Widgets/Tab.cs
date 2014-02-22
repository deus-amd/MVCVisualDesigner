using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model
namespace MVCVisualDesigner
{
    public partial class VDTab : ICustomMerge
    {
        public const string HEADS_CONTAINER_TAG = "Tab Head";
        public const string BODYS_CONTAINER_TAG = "Tab Body";

        public override bool HasWidgetTitle { get { return true; } }

        public void MergeTo(VDWidget targetWidget, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(this);

            // the order of creating children is import, why??
            // first children, then grand-children???
            VDHoriContainer headContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, HEADS_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasTopAnchorDomainPropertyId, true)) as VDHoriContainer;
            VDFullFilledContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDFullFilledContainer.DomainClassId,
                new PropertyAssignment(VDContainer.TagDomainPropertyId, BODYS_CONTAINER_TAG),
                new PropertyAssignment(VDContainer.HasLeftAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasRightAnchorDomainPropertyId, true),
                new PropertyAssignment(VDContainer.HasBottomAnchorDomainPropertyId, true)) as VDFullFilledContainer;

            VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId,
                new PropertyAssignment(VDHoriSeparator.DefaultYDomainPropertyId, 0.5)) as VDHoriSeparator;
            hSeparator.TopWidget = headContainer;
            hSeparator.BottomWidget = bodyContainer;

            this.Children.Add(headContainer);
            this.Children.Add(bodyContainer);
            this.Children.Add(hSeparator);

            VDTabHead head = this.Store.ElementFactory.CreateElement(
                VDTabHead.DomainClassId,
                new PropertyAssignment(VDTabHead.TabTitleDomainPropertyId, "New Tab")) as VDTabHead;
            VDTabBody body = this.Store.ElementFactory.CreateElement(VDTabBody.DomainClassId) as VDTabBody;
            this.ActiveHead = head;
            head.Body = body;
            headContainer.Children.Add(head);
            bodyContainer.Children.Add(body);
        }

        public VDContainer HeadContainer
        {
            get
            {
                var x = this.Children.Find(w => w is VDHoriContainer
                    && ((VDHoriContainer)w).Tag == VDTab.HEADS_CONTAINER_TAG) as VDContainer;
                return x;
            }
        }

        public VDContainer BodyContainer
        {
            get
            {
                var x = this.Children.Find(w => w is VDFullFilledContainer
                    && ((VDContainer)w).Tag == VDTab.BODYS_CONTAINER_TAG) as VDContainer;
                return x;
            }
        }

    }

    public partial class VDTabHead
    {
    }

    public partial class VDTabBody
    {
    }
}



// Shape
namespace MVCVisualDesigner
{
    public partial class VDTabShape
    {
        private const int ICON_INDEX_ADD_TAB = 0;

        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon()
        {
            return this.getImageFromResource("TabToolToolboxBitmap");
        }

        public override bool HasAdditionalWidgetTitleIcon(int idx)
        {
            return idx == ICON_INDEX_ADD_TAB;
        }

        protected override Image getAdditionalTitleIcon(int idx)
        {
            if (idx == ICON_INDEX_ADD_TAB)
            {
                return MVCVisualDesigner.Resources.MVDResources.add;
            }
            return null;
        }

        public VDContainerShape HeadContainerShape
        {
            get
            {
                VDContainerShape s = this.NestedChildShapes.Find(cs => cs is VDHoriContainerShape 
                    && cs.ModelElement != null
                    && ((VDContainer)cs.ModelElement).Tag == VDTab.HEADS_CONTAINER_TAG) as VDContainerShape;
                return s;
            }
        }

        public VDContainerShape BodyContainerShape
        {
            get
            {
                VDContainerShape s = this.NestedChildShapes.Find(cs => cs is VDFullFilledContainerShape 
                    && cs.ModelElement != null
                    && ((VDContainer)cs.ModelElement).Tag == VDTab.BODYS_CONTAINER_TAG) as VDContainerShape;
                return s;
            }
        }

        public override void OnClickAdditionalTitleIcon(int idx)
        {
            if (idx == ICON_INDEX_ADD_TAB) // addd new tab
            {
                VDTab tab = this.ModelElement as VDTab;
                if (tab == null || tab.HeadContainer == null || tab.BodyContainer == null)
                    return;

                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Add new tab page"))
                {
                    VDTabHead head = this.Store.ElementFactory.CreateElement(
                        VDTabHead.DomainClassId,
                        new PropertyAssignment(VDTabHead.TabTitleDomainPropertyId, "New Tab")) as VDTabHead;
                    VDTabBody body = this.Store.ElementFactory.CreateElement(VDTabBody.DomainClassId) as VDTabBody;
                    head.Body = body;
                    tab.ActiveHead = head;
                    tab.HeadContainer.Children.Add(head);
                    tab.BodyContainer.Children.Add(body);

                    this.relayoutChildren = true; // only show active tab's body shape, hide other body shapes

                    t.Commit();
                }
            }
        }

        public override void OnRelayoutChildShapes()
        {
            if (this.BodyContainerShape == null) return;

            VDTab tab = this.ModelElement as VDTab;
            if (tab == null) return;

            // only show active tab's body shape, hide other body shapes
            foreach(var w in this.BodyContainerShape.NestedChildShapes)
            {
                VDTabBodyShape bodyShape = w as VDTabBodyShape;
                if (bodyShape != null)
                {
                    VDTabBody body = bodyShape.ModelElement as VDTabBody;
                    if (body == null || body.Head == null || body.Head != tab.ActiveHead)
                    {
                        bodyShape.Hide(); 
                    }
                    else
                    {
                        bodyShape.Show();
                    }
                }
            }

            // set head background for active tab
            if (this.HeadContainerShape == null) return;
            foreach (var w in this.HeadContainerShape.NestedChildShapes)
            {
                VDTabHeadShape headShape = w as VDTabHeadShape;
                if (headShape != null)
                {
                    VDTabHead head = headShape.ModelElement as VDTabHead;
                    if (head == null || tab.ActiveHead != head)
                        headShape.isActiveTab = false;
                    else
                        headShape.isActiveTab = true;
                }
            }

            //this.BodyContainerShape.relayoutChildren = true; // trigger body shapes' bounds rules
        }

        //protected override void OnChildConfiguring(ShapeElement child, bool createdDuringViewFixup)
        //{
        //    base.OnChildConfiguring(child, createdDuringViewFixup);

        //    if (child is VDHoriContainerShape)
        //    {
        //        VDHoriContainerShape pel = child as VDHoriContainerShape;
        //        if (pel.Anchoring.HasLeftAnchor) return;

        //        VDHoriContainer mel = child.ModelElement as VDHoriContainer;
        //        if (mel != null)
        //        {
        //            if (mel.Tag == VDTab.HEADS_CONTAINER_TAG)
        //            {
        //                if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
        //                if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
        //                if (!pel.Anchoring.HasTopAnchor) pel.Anchoring.SetTopAnchor(0);
        //            }
        //            else if (mel.Tag == VDTab.BODYS_CONTAINER_TAG)
        //            {
        //                if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
        //                if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
        //                if (!pel.Anchoring.HasBottomAnchor) pel.Anchoring.SetBottomAnchor(1.0);
        //            }
        //        }
        //    }
        //}
    }

    public partial class VDTabHeadShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        public override void OnClick(DiagramPointEventArgs e)
        {
            if (this.ParentShape == null || this.ParentShape.ParentShape == null) return;

            VDTabShape tabShape = this.ParentShape.ParentShape as VDTabShape;
            if (tabShape == null) return;

            VDTab tab = tabShape.ModelElement as VDTab;
            if (tab == null) return;

            VDTabHead curHead = this.ModelElement as VDTabHead;
            if (curHead == null) return;

            if (tab.ActiveHead != curHead)
            {
                using(Transaction t = this.Store.TransactionManager.BeginTransaction("Set active tab"))
                {
                    tab.ActiveHead = curHead;

                    tabShape.relayoutChildren = true; // only show active tab's body shape, hide other body shapes

                    t.Commit();
                }
            }
        }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
        }

        public override void OnBoundsFixup(BoundsFixupState fixupState, int iteration, bool createdDuringViewFixup)
        {
            base.OnBoundsFixup(fixupState, iteration, createdDuringViewFixup);

            // be sure the isActiveTab property is set for Active Tab
            VDTabHead tabHead = this.ModelElement as VDTabHead;
            if (tabHead != null && tabHead.Parent != null && tabHead.Parent.Parent != null)
            {
                VDTab tab = tabHead.Parent.Parent as VDTab;
                if (tab != null && tab.ActiveHead == tabHead)
                {
                    this.isActiveTab = true;
                }
            }
        }

        protected StyleSetResourceId m_backgroundBrushId = new StyleSetResourceId("MVCVisualDesignr", "tab head background brush");
        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);
            classStyleSet.AddBrush(m_backgroundBrushId, m_backgroundBrushId, new BrushSettings() { Color = Color.PeachPuff });
        }

        private const string FIELD_NAME_ACTIVE_TAB_BACKGROUND = "Active_Background";
        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            AreaField bkg = this.CreateBackgroundGradientField(FIELD_NAME_ACTIVE_TAB_BACKGROUND);
            bkg.DefaultFocusable = false;
            bkg.DefaultSelectable = false;
            bkg.DefaultVisibility = true;
            bkg.DrawBorder = false;
            bkg.FillBackground = true;
            bkg.DefaultBackgroundBrushId = m_backgroundBrushId;
            bkg.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, 0.0);
            bkg.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, 0.0);
            bkg.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, 0.0);
            bkg.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, 0.0);
            shapeFields.Add(bkg);

            base.InitializeShapeFields(shapeFields);
        }

        static partial void onBindShapeFields(object sender, EventArgs e)
        {
            ShapeElement shape = (ShapeElement)sender;

            AssociatedPropertyInfo propInfo = new AssociatedPropertyInfo(VDTabHeadShape.isActiveTabDomainPropertyId);
            propInfo.IsShapeProperty = true;
            propInfo.FilteringValues.Add(true);

            ShapeElement.FindShapeField(shape.ShapeFields, FIELD_NAME_ACTIVE_TAB_BACKGROUND).AssociateVisibilityWith(shape.Store, propInfo);
        }
    }

    public partial class VDTabBodyShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        //public override void OnShapeInserted()
        //{
        //    base.OnShapeInserted();

        //    this.Anchoring.SetLeftAnchor(0);
        //    this.Anchoring.SetRightAnchor(1.0);
        //}
    }
}