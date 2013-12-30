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
        public const string HEADS_CONTAINER_TAG = "Heads";
        public const string BODYS_CONTAINER_TAG = "Bodys";

        public override bool HasWidgetTitle { get { return true; } }

        public void CustomMergeRelate(VDWidget targetWidget, VDWidget sourceElement, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(sourceElement);

            VDTab tab = sourceElement as VDTab;
            if (tab != null)
            {
                // the order of creating children is import, why??
                // first children, then grand-children???
                VDHoriContainer headContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, HEADS_CONTAINER_TAG)) as VDHoriContainer;
                VDHoriContainer bodyContainer = this.Store.ElementFactory.CreateElement(VDHoriContainer.DomainClassId,
                    new PropertyAssignment(VDContainer.TagDomainPropertyId, BODYS_CONTAINER_TAG)) as VDHoriContainer;

                VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId) as VDHoriSeparator;
                hSeparator.TopWidget = headContainer;
                hSeparator.BottomWidget = bodyContainer;

                tab.Children.Add(headContainer);
                tab.Children.Add(bodyContainer);
                tab.Children.Add(hSeparator);

                VDTabHead head = this.Store.ElementFactory.CreateElement(
                    VDTabHead.DomainClassId,
                    new PropertyAssignment(VDTabHead.TabTitleDomainPropertyId, "New Tab")) as VDTabHead;
                VDTabBody body = this.Store.ElementFactory.CreateElement(VDTabBody.DomainClassId) as VDTabBody;
                tab.ActiveHead = head;
                head.Body = body;
                headContainer.Children.Add(head);
                bodyContainer.Children.Add(body);
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

        public override void OnClickAdditionalTitleIcon(int idx)
        {
            if (idx == ICON_INDEX_ADD_TAB) // addd new tab
            {
                VDTab tab = this.ModelElement as VDTab;
                if (tab == null) return;

                VDHoriContainer headContainer =
                    tab.Children.Find(w => w is VDHoriContainer && ((VDHoriContainer)w).Tag == VDTab.HEADS_CONTAINER_TAG) 
                        as VDHoriContainer;
                if (headContainer == null) return;

                VDHoriContainer bodyContainer =
                    tab.Children.Find(w => w is VDHoriContainer && ((VDHoriContainer)w).Tag == VDTab.BODYS_CONTAINER_TAG) 
                        as VDHoriContainer;
                if (bodyContainer == null) return;

                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Add new tab page"))
                {
                    VDTabHead head = this.Store.ElementFactory.CreateElement(
                        VDTabHead.DomainClassId,
                        new PropertyAssignment(VDTabHead.TabTitleDomainPropertyId, "New Tab")) as VDTabHead;
                    VDTabBody body = this.Store.ElementFactory.CreateElement(VDTabBody.DomainClassId) as VDTabBody;
                    head.Body = body;
                    tab.ActiveHead = head;
                    headContainer.Children.Add(head);
                    bodyContainer.Children.Add(body);
                    t.Commit();
                }
            }
        }

        protected override void OnAssociatedPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnAssociatedPropertyChanged(e);
        }

        public override ShapeElement FixUpChildShapes(ModelElement childElement)
        {
            return base.FixUpChildShapes(childElement);
        }

        protected override void OnChildConfiguring(ShapeElement child, bool createdDuringViewFixup)
        {
            base.OnChildConfiguring(child, createdDuringViewFixup);

            if (child is VDHoriContainerShape)
            {
                VDHoriContainerShape pel = child as VDHoriContainerShape;
                if (pel.Anchoring.HasLeftAnchor) return;

                VDHoriContainer mel = child.ModelElement as VDHoriContainer;
                if (mel != null)
                {
                    if (mel.Tag == VDTab.HEADS_CONTAINER_TAG)
                    {
                        if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
                        if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
                        if (!pel.Anchoring.HasTopAnchor) pel.Anchoring.SetTopAnchor(0);
                    }
                    else if (mel.Tag == VDTab.BODYS_CONTAINER_TAG)
                    {
                        if (!pel.Anchoring.HasLeftAnchor) pel.Anchoring.SetLeftAnchor(0);
                        if (!pel.Anchoring.HasRightAnchor) pel.Anchoring.SetRightAnchor(1.0);
                        if (!pel.Anchoring.HasBottomAnchor) pel.Anchoring.SetBottomAnchor(1.0);
                    }
                }
            }
        }

        protected override void OnChildConfigured(ShapeElement child, bool childWasPlaced, bool createdDuringViewFixup)
        {
            base.OnChildConfigured(child, childWasPlaced, createdDuringViewFixup);
        }
    }

    public partial class VDTabHeadShape
    {
        public override NodeSides ResizableSides { get { return NodeSides.Horizontal; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
        }
    }

    public partial class VDTabBodyShape
    {
        public override bool CanMove { get { return false; } }
        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override void OnShapeInserted()
        {
            base.OnShapeInserted();

            this.Anchoring.SetLeftAnchor(0);
            this.Anchoring.SetRightAnchor(1.0);
        }
    }
}