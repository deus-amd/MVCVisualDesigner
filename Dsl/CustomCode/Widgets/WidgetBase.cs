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

        // init MoreHtmlAttributes property
        protected List<HTMLAttribute> m_moreHTMLAttribtes;
        public List<HTMLAttribute> GetMoreHTMLAttributesValue()
        {
            if (m_moreHTMLAttribtes == null)
                m_moreHTMLAttribtes = new List<HTMLAttribute>();
            return m_moreHTMLAttribtes; 
        }
        public void SetMoreHTMLAttributesValue(List<HTMLAttribute> newValue)
        {
            m_moreHTMLAttribtes = newValue;
        }

        protected T getHTMLAttr<T>(string attrName, T defaultValue = default(T))
        {
            List<HTMLAttribute> attrs = GetMoreHTMLAttributesValue();
            HTMLAttribute attr = attrs.Find(a => string.Compare(a.AttributeName, attrName/*, true*/) == 0);
            if (attr != null)
            {
                System.ComponentModel.TypeConverter converter 
                    = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)(converter.ConvertFromString(attr.AttributeValue));
                }
            }

            return defaultValue;
        }

        protected void setHTMLAttr<T>(string attrName, T newValue)
        {
            List<HTMLAttribute> attrs = GetMoreHTMLAttributesValue();
            HTMLAttribute attr = attrs.Find(a => string.Compare(a.AttributeName, attrName/*, true*/) == 0);
            if (attr != null)
                attr.AttributeValue = newValue.ToString();
            else
                attrs.Add(new HTMLAttribute(attrName, newValue.ToString()));
        }

        public string GetIDAttributeValue() { return getHTMLAttr<string>("id", string.Empty); }

        public void SetIDAttributeValue(string newID) { setHTMLAttr<string>("id", newID); }

        public string GetClassAttributeValue() { return getHTMLAttr("class", string.Empty); }

        public void SetClassAttributeValue(string newClass) { setHTMLAttr<string>("class", newClass); }

        // settings
        // used to store some additional data, such as settings from all kinds of tool window
        protected Dictionary<Guid, string> m_settings;
        public Dictionary<Guid, string> GetsettingsValue()
        {
            if (m_settings == null) m_settings = new Dictionary<Guid, string>();
            return m_settings;
        }
        public void SetsettingsValue(Dictionary<Guid, string> newValue)
        {
            m_settings = newValue;
        }


        // calculated property IntrinsicModelType
        internal virtual string GetIntrinsicModelTypeValue()
        {
            return string.Empty;
        }

        // custom storage property ModelName
        protected string m_ModelName;
        internal virtual string GetModelNameValue()
        {
            return m_ModelName;
        }
        internal virtual void SetModelNameValue(string newValue)
        {
            m_ModelName = newValue;
        }
    }

    public partial class VDWidget
    {
#region Constructors
        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>Constructor.</summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        protected VDWidget(Partition partition, PropertyAssignment[] propertyAssignments)
        	: base(partition, propertyAssignments)
        {
            if (!this.Store.InSerializationTransaction)
            {
                //if (this.WidgetType == MVCVisualDesigner.WidgetType.View)
                //{
                //    // init the model store
                //    ((VDView)this).ModelStore = new VDModelStore(this.Partition);
                //}
            }
        }


#endregion

        abstract public WidgetType WidgetType { get; }

        virtual public bool HasWidgetTitle { get { return false; } }

        /// <summary>
        /// Returns a value indicating whether the source element represented by the
        /// specified root ProtoElement can be added to this element.
        /// </summary>
        /// <param name="rootElement">
        /// The root ProtoElement representing a source element.  This can be null, 
        /// in which case the ElementGroupPrototype does not contain an ProtoElements
        /// and the code should inspect the ElementGroupPrototype context information.
        /// </param>
        /// <param name="elementGroupPrototype">The ElementGroupPrototype that contains the root ProtoElement.</param>
        /// <returns>true if the source element represented by the ProtoElement can be added to this target element.</returns>
        protected override bool CanMerge(ProtoElementBase rootElement, ElementGroupPrototype elementGroupPrototype)
        {
            return internalCanMerge(this, rootElement, elementGroupPrototype);
        }

        internal virtual bool internalCanMerge(VDWidget origianlTargetWidget, ProtoElementBase sourceRootElement, ElementGroupPrototype elementGroupPrototype)
        {
            if (elementGroupPrototype == null) throw new ArgumentNullException("elementGroupPrototype");

            if (sourceRootElement != null)
            {
                DomainClassInfo rootElementDomainInfo = this.Partition.DomainDataDirectory.GetDomainClass(sourceRootElement.DomainClassId);
                if (rootElementDomainInfo.IsDerivedFrom(VDWidget.DomainClassId))
                {
                    return MergeManager.Instance.CanMerge(rootElementDomainInfo.Id, this.GetDomainClass().Id);
                }
            }

            return base.CanMerge(sourceRootElement, elementGroupPrototype);
        }

        protected override void MergeRelate(ModelElement sourceElement, ElementGroup elementGroup)
        {
            // setup title
            if (sourceElement != null && sourceElement is VDWidget &&
                ((VDWidget)sourceElement).WidgetType != WidgetType.View)
            {
                VDWidget widget = (VDWidget)sourceElement;
                if (widget.HasWidgetTitle && widget.Title == null)
                {
                    widget.Title = this.Store.ElementFactory.CreateElement(VDWidgetTitle.DomainClassId) as VDWidgetTitle;
                }
            }

            // custom merge relate
            if (sourceElement != null && sourceElement is ICustomMerge)
            {
                ((ICustomMerge)sourceElement).MergeTo(this, elementGroup);
                return;
            }

            base.MergeRelate(sourceElement, elementGroup);
        }

        // Deleting Propagate
        private VDWidget m_parentToDel;
        protected override void OnDeleting()
        {
            m_parentToDel = this.Parent;
            base.OnDeleting();
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();

            if (m_deleteWithoutPropagating) return;

            if (PropagateDeletingToParent && m_parentToDel != null && this.Store.TransactionManager.InTransaction)
            {
                onPropagateDeletingParent(m_parentToDel);
                m_parentToDel = null;
            }
        }

        private bool m_deleteWithoutPropagating = false;
        internal protected void DeleteWithoutPropagating()
        {
            m_deleteWithoutPropagating = true;
            Delete();
            m_deleteWithoutPropagating = false;
        }

        protected virtual bool PropagateDeletingToParent { get { return false; } }

        protected virtual bool onPropagateDeletingParent(VDWidget parentWidget)
        {
            parentWidget.Delete();
            return true;
        }

        // utilities
        public T GetChild<T>(Predicate<T> predicate = null)  where T : VDWidget
        {
            T child;
            if (predicate == null)
                child = this.Children.Find(c => c is T) as T;
            else
                child = this.Children.Find(c => (c is T) && predicate((T)c)) as T;
            return child;
        }

        public List<T> GetChildren<T>(Predicate<T> predicate = null) where T : VDWidget
        {
            List<T> children = new List<T>();;
            if (predicate == null)
            {
                List<VDWidget> widgets = this.Children.FindAll(c => c is T);
                if (widgets != null && widgets.Count > 0) widgets.ForEach(w => children.Add((T)w));
            }
            else
            {
                List<VDWidget> widgets = this.Children.FindAll(c => (c is T) && predicate((T)c));
                if (widgets != null && widgets.Count > 0) widgets.ForEach(w => children.Add((T)w));
            }
            return children;
        }

        public VDView RootView
        {
            get
            {
                VDWidget widget = this;
                while (widget.Parent != null) widget = widget.Parent;

                if (widget is VDView && widget.WidgetType == MVCVisualDesigner.WidgetType.View)
                    return (VDView)widget;

                return null;
            }
        }

        // Model related
        public VDModelStore GetModelStore() 
        { 
            return RootView != null ? RootView.ModelStore : null;
        }

        public VDModelType GetModelType()
        {
            if (this.ModelInstance == null) 
                return null;
            else 
                return this.ModelInstance.ModelType;
        }

        // More HTML Attributes
        public string GetMoreHtmlAttributeString(params string[] ignoreList)
        {
            return GetMoreHtmlAttributeString(new string[] { }, ignoreList);
        }

        public string GetMoreHtmlAttributeString(string[] additionalClasses, params string[] ignoreList)
        {
            HashSet<string> ignoreHash = new HashSet<string>(ignoreList);

            HashSet<string> classHash = new HashSet<string>();
            foreach (string cls in additionalClasses)
            {
                if (!classHash.Contains(cls)) classHash.Add(cls);
            }

            StringBuilder sb = new StringBuilder();
            foreach (HTMLAttribute attr in this.MoreHTMLAttributes)
            {
                if (string.IsNullOrEmpty(attr.AttributeValue)) continue;
                if (ignoreHash.Contains(attr.AttributeName)) continue;
                if (attr.AttributeName == "class" && !classHash.Contains(attr.AttributeValue))
                    classHash.Add(attr.AttributeName);

                sb.Append(attr.ToString()).Append(" ");
            }

            if (classHash.Count > 0) sb.Append("class='" + string.Join(" ", classHash) + "'");

            return sb.ToString();
        }

        // 
        //todo: read it from settings
        public static string CodeSnippetDelimiter { get { return "\r\n\r\n\r\n"; } }
        public static string GetPreCodeSnippet(string codeSnippet)
        {
            if (string.IsNullOrWhiteSpace(codeSnippet)) return string.Empty;

            int idx = codeSnippet.LastIndexOf(CodeSnippetDelimiter);
            if (idx > 0)
                return codeSnippet.Substring(0, idx);
            else
                return codeSnippet;
        }
        public static string GetPostCodeSnippet(string codeSnippet)
        {
            if (string.IsNullOrWhiteSpace(codeSnippet)) return string.Empty;

            int idx = codeSnippet.LastIndexOf(CodeSnippetDelimiter);
            if (idx > 0 && idx < codeSnippet.Length - CodeSnippetDelimiter.Length)
            {
                return codeSnippet.Substring(idx + CodeSnippetDelimiter.Length);
            }
            return string.Empty;
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
            ShapeElement parentShape = this.ParentShape;
            if (parentShape == null || (parentShape is Diagram))
            {
                base.OnPaintShape(e);
                return;
            }

            RectangleD thisRect = this.AbsoluteBoundingBox;
            thisRect.Inflate(new SizeD(this.OutlinePenWidth, this.OutlinePenWidth));
            while (parentShape != null && !(parentShape is Diagram))
            {
                RectangleD clipRect = parentShape.AbsoluteBoundingBox;
                clipRect.Inflate(new SizeD(-parentShape.OutlinePenWidth, -parentShape.OutlinePenWidth));

                thisRect.Intersect(clipRect);

                parentShape = parentShape.ParentShape;
            }

            Region clip = e.Graphics.Clip;
            e.Graphics.SetClip(RectangleD.ToRectangleF(thisRect), CombineMode.Intersect);
            base.OnPaintShape(e);
            e.Graphics.Clip = clip;
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
        /// <summary> Has additional title icon </summary>
        /// <param name="idx">index:0-4</param>
        /// <returns></returns>
        virtual public bool HasAdditionalWidgetTitleIcon(int idx) { return false; }

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

        /// <summary> Get image for additional title icon </summary>
        /// <param name="idx">index:0-4</param>
        /// <returns></returns>
        protected virtual Image getAdditionalTitleIcon(int idx)
        {
            return null;
        }

        // calculated domain properties
        protected internal string GettitleTextValue() { return this.getTitleText(); }
        protected internal Image GettitleIconValue() { return this.getTitleIcon(); }

        protected internal Image GettitleIcon0Value() { return this.getAdditionalTitleIcon(0); }
        protected internal Image GettitleIcon1Value() { return this.getAdditionalTitleIcon(1); }
        protected internal Image GettitleIcon2Value() { return this.getAdditionalTitleIcon(2); }
        protected internal Image GettitleIcon3Value() { return this.getAdditionalTitleIcon(3); }
        protected internal Image GettitleIcon4Value() { return this.getAdditionalTitleIcon(4); }

        public virtual void OnClickAdditionalTitleIcon(int idx)
        {
        }

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
            while ((shape.isPinned || shape is VDHoriContainerShape || shape is VDVertContainerShape) /* avoid selecting container */
                && shape.ParentShape != null && shape.ParentShape is VDWidgetShape)
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
#region Bounds Rules
        public override BoundsRules BoundsRules { get { return VDDefaultBoundsRules.Instance; } }

        public class VDDefaultBoundsRules : BoundsRules
        {
            internal static readonly VDDefaultBoundsRules Instance = new VDDefaultBoundsRules();

            public VDDefaultBoundsRules()
            {
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                proposedBounds = DefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDWidgetShape parent = shape.ParentShape as VDWidgetShape;
                if (parent != null)
                {
                    if (parent is VDHoriContainerShape)
                    {
                        if (proposedBounds.Location.Y != 0 || proposedBounds.Size.Height != parent.Bounds.Height)
                        {
                            proposedBounds = new RectangleD(new PointD(proposedBounds.Location.X, 0),
                                        new SizeD(proposedBounds.Size.Width, parent.Bounds.Height));
                        }
                    }
                    else if (parent is VDVertContainerShape)
                    {
                        if (proposedBounds.Location.X != 0 || proposedBounds.Size.Width != parent.Bounds.Width)
                        {
                            proposedBounds = new RectangleD(new PointD(0, proposedBounds.Location.Y),
                                        new SizeD(parent.Bounds.Width, proposedBounds.Size.Height));
                        }
                    }
                    else if (parent is VDFullFilledContainerShape)
                    {
                        proposedBounds = new RectangleD(PointD.Empty, parent.Bounds.Size);
                    }
                }

                return proposedBounds;
            }
        }
#endregion

        // trigger by set relayoutChildren domain property, and called by VDRelayoutChildrenShapeRule
        public virtual void OnRelayoutChildShapes()
        {
        }

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("zorder is : {0}", this.ZOrder);
        }

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

        /// <summary>Get a child shape which ModelElemnt is childMEL</summary>
        public T GetChildShape<T>(VDWidget childMEL) where T : ShapeElement
        {
            foreach(var s in this.NestedChildShapes)
            {
                if (s.ModelElement == childMEL)
                    return (T)s;
            }
            return null;
        }

        public T GetChildShape<T>(Predicate<T> predicate = null) where T : VDWidgetShape
        {
            T child = null;
            if (predicate == null)
                child = this.NestedChildShapes.Find(c => c is T) as T;
            else
                child = this.NestedChildShapes.Find(c => c is T && predicate((T)c)) as T;
            return child;
        }

        public List<T> GetChildShapes<T>(Predicate<T> predicate = null) where T : VDWidgetShape
        {
            List<T> children = new List<T>();
            if (predicate == null)
            {
                List<ShapeElement> list = this.NestedChildShapes.FindAll(c => c is T);
                if (list != null && list.Count > 0) list.ForEach(c => children.Add((T)c));
            }
            else
            {
                List<ShapeElement> list = this.NestedChildShapes.FindAll(c => c is T && predicate((T)c));
                if (list != null && list.Count > 0) list.ForEach(c => children.Add((T)c));
            }
            return children;
        }

        /// <summary>Associate shape field's content and visibility with IMS properties</summary>
        /// <param name="shape">The shape containing the shape field</param>
        /// <param name="fieldname">Field name</param>
        /// <param name="valuePropertyId">property to associate with the content of shape field</param>
        /// <param name="isValuePropertyOfShape">does 'valuePropertyId' belong to a shape object (PEL, not MEL)</param>
        /// <param name="visibilityPropertyId">property to associate with the visibility of shape field</param>
        /// <param name="isVisibilityPropOfShape">does 'visibilityPropertyId' belong to a shape object, instead of a MEL</param>
        /// <param name="visibilityFiltervalues">values to filter the visibility</param>
        static protected void associateProperty(ShapeElement shape, string fieldname, 
                Guid valuePropertyId, bool isValuePropertyOfShape,  /* associate value */
                Guid visibilityPropertyId, bool isVisibilityPropOfShape, params object[] visibilityFiltervalues) /* associate visibility*/
        {
            if (valuePropertyId != Guid.Empty)
            {
                AssociatedPropertyInfo propertyInfo = new AssociatedPropertyInfo(valuePropertyId);
                propertyInfo.IsShapeProperty = isValuePropertyOfShape;
                ShapeElement.FindShapeField(shape.ShapeFields, fieldname).AssociateValueWith(shape.Store, propertyInfo);
            }

            if (visibilityPropertyId != Guid.Empty)
            {
                AssociatedPropertyInfo propertyInfo = new AssociatedPropertyInfo(visibilityPropertyId);
                propertyInfo.IsShapeProperty = isVisibilityPropOfShape;
                if (visibilityFiltervalues != null)
                {
                    for (int i = 0; i < visibilityFiltervalues.Length; i++)
                    {
                        propertyInfo.FilteringValues.Add(visibilityFiltervalues[i]);
                    }
                }
                ShapeElement.FindShapeField(shape.ShapeFields, fieldname).AssociateVisibilityWith(shape.Store, propertyInfo);
            }
        }

        /// <summary>Associate shape field's content and visibility with IMS properties</summary>
         /// <param name="shape">The shape containing the shape field</param>
        /// <param name="fieldname">Field name</param>
        /// <param name="valuePropertyId">property to associate with the content of shape field</param>
        /// <param name="visibilityPropertyId">property to associate with the visibility of shape field</param>
        /// <param name="visibilityFiltervalues">values to filter the visibility</param>
        static protected void associateProperty(ShapeElement shape, string fieldname, 
            Guid valuePropertyId, 
            Guid visibilityPropertyId, params object[] visibilityFiltervalues)
        {
            associateProperty(shape, fieldname, valuePropertyId, false, visibilityPropertyId, false, visibilityFiltervalues);
        }
#endregion
    }

    [RuleOn(typeof(VDWidgetShape), FireTime = TimeToFire.TopLevelCommit, Priority = 0x7FFFFFFF)]
    public class VDRelayoutChildrenShapeRule : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            base.ElementPropertyChanged(e);

            if (e.ModelElement == null || e.DomainProperty == null) return;

            VDWidgetShape shape = e.ModelElement as VDWidgetShape;

            // 
            if ((e.DomainProperty.Id == VDWidgetShape.relayoutChildrenDomainPropertyId) && ((bool)e.NewValue))
            {
                shape.relayoutChildren = false;
                shape.OnRelayoutChildShapes();
            }
        }
    }

}