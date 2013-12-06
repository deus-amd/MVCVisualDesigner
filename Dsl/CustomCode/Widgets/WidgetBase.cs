using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//
// Model
//
namespace MVCVisualDesigner
{
    public partial class VDWidgetBase
    {
        protected void MergeRelateVDSection(VDSection sourceElement, ElementGroup elementGroup)
        {
            // Create link for path WidgetHasChildren.Children
            this.Children.Add(sourceElement);

            // create section title and body
            sourceElement.Head = this.Store.ElementFactory.CreateElement(VDSectionHead.DomainClassId) as VDSectionHead;
            sourceElement.Body = this.Store.ElementFactory.CreateElement(VDSectionBody.DomainClassId) as VDSectionBody;
            VDHoriSeparator hSeparator = this.Store.ElementFactory.CreateElement(VDHoriSeparator.DomainClassId) as VDHoriSeparator;
            hSeparator.TopWidget = sourceElement.Head;
            hSeparator.BottomWidget = sourceElement.Body;
            sourceElement.Children.Add(hSeparator);
        }

        protected void MergeDisconnectVDSection(VDSection sourceElement)
        {
        }
    }

    public partial class VDWidget : ICodeData
    {
        abstract public WidgetType WidgetType { get; }

        virtual public bool HasWidgetTitle { get { return false; } }

        protected override void MergeRelate(ModelElement sourceElement, ElementGroup elementGroup)
        {
            if (sourceElement != null && sourceElement is VDWidget &&
                ((VDWidget)sourceElement).WidgetType != WidgetType.View)
            {
                VDWidget widget = (VDWidget)sourceElement;
                if (widget.HasWidgetTitle && widget.Title == null)
                {
                    widget.Title = this.Store.ElementFactory.CreateElement(VDWidgetTitle.DomainClassId) as VDWidgetTitle;
                }
            }

            base.MergeRelate(sourceElement, elementGroup);
        }

        // ICodeData
        public string GenerateCode(ICodeGeneratorFactory cGFactory, IWidgetTreeWalkerFactory walkerFactory)
        {
            return cGFactory.GetCodeGenerator(this).GenerateCode(cGFactory, walkerFactory);
        }
    }
}


//
// Shape
//
namespace MVCVisualDesigner
{
    public partial class VDWidgetShape
    {
        ////////////////////////////////////////////////////////////////////////////////
#region UI customization
        public override bool HasHighlighting { get { return false; } }

        public override bool HasShadow { get { return false; } }

        public override double GridSize { get { return 0.01; } set { } }

        /// <summary>
        /// Ensure that nested child shapes don't go outside the bounds of parent by resizing parent
        /// </summary>
        //public override bool AllowsChildrenToResizeParent { get { return true; } }

        /// <summary>
        /// shrink parent shape when its child shapes collapsed.
        /// </summary>
        //public override ResizeDirection AllowsChildrenToShrinkParent
        //{
        //    get
        //    {
        //        //if (this.HasChildren) return ResizeDirection.Both;
        //        return base.AllowsChildrenToShrinkParent;
        //    }
        //}

        // Dash or Dot outlne
        private static ArrayList customOutlineDashPattern;
        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 10.0F, 5.0F, 10.0F, 5.0F });
                return customOutlineDashPattern;
            }
        }

        // set clip to make sure child shapes will not overlap parent shape
        public override void OnPaintShape(DiagramPaintEventArgs e)
        {
            if (this.ParentShape != null && !(this.ParentShape is Microsoft.VisualStudio.Modeling.Diagrams.Diagram))
            {
                RectangleD clipRect = this.ParentShape.AbsoluteBoundingBox;
                clipRect.Inflate(new SizeD(-this.ParentShape.OutlinePenWidth, -this.ParentShape.OutlinePenWidth));

                RectangleD thisRect = this.AbsoluteBoundingBox;
                thisRect.Inflate(new SizeD(this.ParentShape.OutlinePenWidth, this.ParentShape.OutlinePenWidth));

                clipRect.Intersect(thisRect);

                Region clip = e.Graphics.Clip;
                e.Graphics.SetClip(RectangleD.ToRectangleF(clipRect), CombineMode.Intersect);

                base.OnPaintShape(e);

                e.Graphics.Clip = clip;
            }
            else
                base.OnPaintShape(e);
        }
#endregion

        ////////////////////////////////////////////////////////////////////////////////
#region Padding & Margin
        /// <summary>
        /// if AllowsChildrenToShrinkParent is true, DefaultContainerMargin is the margin on the right and bottom
        /// </summary>
        public override SizeD DefaultContainerMargin { get { return new SizeD(PaddingRight, PaddingBottom); } }

        /// <summary>
        /// NestedShapesMargin is the margin on the left and top
        /// </summary>
        public override SizeD NestedShapesMargin { get { return new SizeD(PaddingLeft, PaddingTop); } }

        public virtual double PaddingAll { get { return 0.0; } }
        public virtual double PaddingLeft { get { return PaddingAll; } }
        public virtual double PaddingTop { get { return PaddingAll; } }
        public virtual double PaddingRight { get { return PaddingAll; } }
        public virtual double PaddingBottom { get { return PaddingAll; } }
#endregion

        ////////////////////////////////////////////////////////////////////////////////
#region disabled property
        protected bool m_disabledValue;
        protected internal bool GetdisabledValue()
        {
            return m_disabledValue;
        }

        // change the display to reflect it's disabled
        protected internal void SetdisabledValue(bool newval)
        {
            if (newval == m_disabledValue) return;

            m_disabledValue = newval;
            if (m_disabledValue)
            {
                // update font and border
                PenSettings outlinePen = new PenSettings();
                outlinePen.Color = Color.FromKnownColor(KnownColor.LightGray);
                outlinePen.DashStyle = DashStyle.Custom;
                outlinePen.DashPattern = CustomOutlineDashPattern;
                outlinePen.Width = 0.01F;
                this.ClassStyleSet.OverridePen(DiagramPens.ShapeOutline, outlinePen);

                BrushSettings textBrush = new BrushSettings();
                textBrush.Color = Color.FromKnownColor(KnownColor.LightGray);
                this.ClassStyleSet.OverrideBrush(DiagramBrushes.ShapeText, textBrush);
                this.Invalidate();
            }
            else
            {
                // update font and border
                PenSettings outlinePen = new PenSettings();
                outlinePen.Color = Color.FromKnownColor(KnownColor.Black);
                outlinePen.DashStyle = DashStyle.Custom;
                outlinePen.DashPattern = CustomOutlineDashPattern;
                outlinePen.Width = 0.01F;
                this.ClassStyleSet.OverridePen(DiagramPens.ShapeOutline, outlinePen);

                BrushSettings textBrush = new BrushSettings();
                textBrush.Color = Color.FromKnownColor(KnownColor.Black);
                this.ClassStyleSet.OverrideBrush(DiagramBrushes.ShapeText, textBrush);
                this.Invalidate();
            }
        }
#endregion

        ////////////////////////////////////////////////////////////////////////////////
#region Widget Title Port

        virtual public bool HasWidgetTitleIcon { get { return false; } }

        protected virtual string getTitleText()
        {
            if (this.ModelElement != null)
                return this.GetMEL<VDWidget>().WidgetType.ToString();
            else
                return string.Empty;
        }

        protected virtual Image getTitleIcon()
        {
            return null;
        }

        // calculated domain properties
        protected internal string GettitleTextValue() { return this.getTitleText(); }
        protected internal Image GettitleIconValue() { return this.getTitleIcon(); }

        /// <summary>
        /// Sometimes port placement will resize parent shape, but for TitlePort, its position is fixed,
        /// no need to adjust any more.
        /// </summary>
        protected override void ConfiguredChildPortShape(Port child, bool childWasPlaced)
        {
            if (!(child is VDWidgetTitlePort))
            {
                base.ConfiguredChildPortShape(child, childWasPlaced);
            }
        }

        protected Image getImageFromResource(string resourceName)
        {
            //return base.GetTitleIconValue();
            object obj = MVCVisualDesignerDomainModel.SingletonResourceManager.GetObject(resourceName);
            Image image = ImageHelper.GetImage(obj);
            Bitmap bitmap = image as System.Drawing.Bitmap;
            if (bitmap != null)
            {
                bitmap.MakeTransparent();
                return bitmap;
            }
            else
            {
                return image;
            }
        }

        // pin & unpin handling -- begin
        // ### make sure no side effect for EVAACodeSnippetShape (reference mode)
        public bool SelectUnpinedParentShape(DiagramItem item, DiagramClientView view)
        {
            if (item == null || view == null) return false;

            // find top-most unpined predecessor
            VDWidgetShape shape = this;
            bool bReSelect = false;
            while (shape.isPinned && shape.ParentShape != null && shape.ParentShape is VDWidgetShape)
            {
                shape = shape.ParentShape as VDWidgetShape;
                bReSelect = true;
            }

            if (shape != null && bReSelect)// && !object.ReferenceEquals(shape, this))
            {
                item.SetItem(shape);
                view.Selection.Clear();
                view.Selection.Add(item);
                view.Selection.PrimaryItem = item;
                view.Selection.FocusedItem = item;
                return true;
            }
            return false;
        }

        public override void CoerceSelection(DiagramItem item, DiagramClientView view, bool isAddition)
        {
            if (!SelectUnpinedParentShape(item, view))
            {
                base.CoerceSelection(item, view, isAddition);
            }
        }
#endregion

        /////////////////////////////////////////////////////////////////////////////////
#region utilities
        public T GetMEL<T>() where T : VDWidget
        {
            if (this.ModelElement != null)
            {
                return this.ModelElement as T;
            }
            return null;
        }

        public List<T> GetChildShapes<T>(Predicate<T> condition = null) where T : ShapeElement
        {
            List<T> children = new List<T>();
            foreach (var s in this.NestedChildShapes)
            {
                if (s is T)
                {
                    if (condition != null && !condition((T)s)) continue;

                    children.Add((T)s);
                }
            }
            return children;
        }
#endregion
    }
}