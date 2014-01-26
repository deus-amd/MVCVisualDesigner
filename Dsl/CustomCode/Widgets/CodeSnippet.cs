using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVCVisualDesigner
{
    using Microsoft.VisualStudio.Modeling;
    using Microsoft.VisualStudio.Modeling.Design;
    using Microsoft.VisualStudio.Modeling.Diagrams;

    public partial class VDCodeSnippet
    {
        //todo: read it from settings
        public string CodeSnippetDelimiter { get { return "\r\n\r\n\r\n"; } }

        public override bool HasWidgetTitle { get { return true; } }

#region IMS Properties
        
        //
        // custom and calculated properties
        //
        
        public string GetActiveLinkedWidgetNameValue()
        {
            if (this.ActiveLinkedWidget != null)
                return ActiveLinkedWidget.WidgetName;
            else
                return String.Empty;
        }

        public void SetActiveLinkedWidgetNameValue(string newvalue)
        {
            if (newvalue != String.Empty)
            {
                foreach (VDWidget w in this.LinkedWidgets)
                {
                    if (string.Compare(newvalue, w.WidgetName, true) == 0)
                    {
                        this.ActiveLinkedWidget = w;
                        return;
                    }
                }
            }

            this.ActiveLinkedWidget = null;
        }

        public string GetCodeSnippet2Value()
        {
            string s = null;
            if (this._Mode.HasFlag(E_CodeSnippetMode.Definition))
                s = this.CodeSnippet;
            else if (this.ActiveLinkedWidget != null)
                s = this.ActiveLinkedWidget.CodeSnippet;
            else
                s = string.Empty;

            if (string.IsNullOrEmpty(s)) s = "\r\n";
            return s;
        }

        public void SetCodeSnippet2Value(string newval)
        {
            if (this._Mode.HasFlag(E_CodeSnippetMode.Definition))
                this.CodeSnippet = newval;
            else if (this.ActiveLinkedWidget != null)
                this.ActiveLinkedWidget.CodeSnippet = newval;
        }

        public E_CodeSnippetMode Get_ModeValue()
        {
            if (this.LinkedWidgets.Count == 0)
            {
                return E_CodeSnippetMode.Definition;
            }
            else
            {
                if (ActiveLinkedWidget == null)
                    return E_CodeSnippetMode.Reference_No_ActiveLinkedWidget;
                else
                    return E_CodeSnippetMode.Reference_Has_ActiveLinkedWidget;
            }
        }

        public string Get_PreCodeSnippetValue()
        {
            string wholeCodeSnippet = this.CodeSnippet2;
            if (string.IsNullOrWhiteSpace(wholeCodeSnippet)) return string.Empty;

            int idx = wholeCodeSnippet.LastIndexOf(CodeSnippetDelimiter);
            if (idx > 0)
                return wholeCodeSnippet.Substring(0, idx);
            else
                return wholeCodeSnippet;
        }

        public void Set_PreCodeSnippetValue(string newval)
        {
            this.CodeSnippet2 = newval;
        }

        public string Get_PostCodeSnippetValue()
        {
            string wholeCodeSnippet = this.CodeSnippet2;
            if (string.IsNullOrWhiteSpace(wholeCodeSnippet)) return string.Empty;

            int idx = wholeCodeSnippet.LastIndexOf(CodeSnippetDelimiter);
            if (idx > 0 && idx < wholeCodeSnippet.Length - CodeSnippetDelimiter.Length)
            {
                return wholeCodeSnippet.Substring(idx + CodeSnippetDelimiter.Length);
            }

            return string.Empty;
        }

        public bool Get_HasPostCodeSnippetValue()
        {
            return !string.IsNullOrWhiteSpace(this._PostCodeSnippet);
        }
#endregion
    }
}

namespace MVCVisualDesigner
{
    using Microsoft.VisualStudio.Modeling;
    using Microsoft.VisualStudio.Modeling.Design;

    // add Custom Type Descriptor in Dsl Explorer for VDCodeSnippeet Domain Class,
    // and set its Custom Coded as True in property window
    public partial class VDCodeSnippetTypeDescriptor : ElementTypeDescriptor
    {
        private PropertyDescriptorCollection GetCustomProperties(Attribute[] attributes)
        {
            // Get the default property descriptors from the base class
            PropertyDescriptorCollection propertyDescriptors = base.GetProperties(attributes);

            HashSet<string> hidePropNames = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            hidePropNames.Add("CodeSnippetEditor");
            hidePropNames.Add("CodeSnippet");

            // Get a reference to the model element which is active linked widget of this code snippet
            VDCodeSnippet source = this.ModelElement as VDCodeSnippet;
            if (source != null)
            {
                // for Reference mode, show code snippet for the active linked widget
                if (source._Mode.HasFlag(E_CodeSnippetMode.Reference) && source.ActiveLinkedWidget == null)
                {
                    hidePropNames.Add("CodeSnippet2");
                }
            }

            // Return the property descriptors for this element
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor desc in propertyDescriptors)
            {
                if (!hidePropNames.Contains(desc.Name))
                {
                    list.Add(desc);
                }
            }
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }


    /// <summary>
    /// customized editor for ActiveLinkedWidget property
    /// </summary>
    public class ActiveLinkedWidgetNamePropEditor : VDDomainElementListEditor<string>
    {
        protected override bool hasNoneListItem { get { return true; } }

        protected override bool initListBoxControl(ListBoxControl<string> listControl, ModelElement mel)
        {
            if (!(mel is VDCodeSnippet)) return false;

            LinkedElementCollection<VDWidget> linkedWidgets = ((VDCodeSnippet)mel).LinkedWidgets;
            if (linkedWidgets == null) return false;

            foreach (VDWidget linkedWidget in linkedWidgets)
            {
                listControl.AddItem(getWidgetString(linkedWidget), linkedWidget.WidgetName);
            }

            return true;
        }

        private string getWidgetString(VDWidget widget)
        {
            if (string.IsNullOrEmpty(widget.WidgetName))
                return string.Format("[{0}]", widget.WidgetType);
            else
                return string.Format("{0} [{1}]", widget.WidgetName, widget.WidgetType);
        }
    }
}

// PEL
namespace MVCVisualDesigner
{
    using System.Drawing;
    using Microsoft.VisualStudio.Modeling;
    using Microsoft.VisualStudio.Modeling.Design;
    using Microsoft.VisualStudio.Modeling.Diagrams;

    public partial class VDCodeSnippetShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("CodeSnippetToolToolboxBitmap"); 
        }

        protected StyleSetResourceId _brushId = new StyleSetResourceId("MVDesigner", "CodeSnippetBrush");
        protected StyleSetResourceId _FontId = new StyleSetResourceId("MVDesigner", "CodeSnippetFont");
        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);
            classStyleSet.AddBrush(_brushId, _brushId, new BrushSettings() { Color = Color.PapayaWhip });

            FontSettings fontSettings = new FontSettings();
            fontSettings.Style = System.Drawing.FontStyle.Bold;
            fontSettings.Size = 8 / 72.0F;
            classStyleSet.AddFont(_FontId, DiagramFonts.ShapeText, fontSettings);

            // set text color for fields
            BrushSettings textBrush = new BrushSettings();
            textBrush.Color = Color.FromKnownColor(KnownColor.Maroon);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeText, textBrush);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeTextSelected, textBrush);

        //    //EVAACodeSnippetShape.AssociateValueWith(this.Store, EVAACodeSnippet.CodeSnippet2DomainPropertyId);
        //    //VDCodeSnippetShape.AssociateValueWith(this.Store, VDCodeSnippet.ActiveLinkedWidgetNameDomainPropertyId);
        }

        //protected override void OnAssociatedPropertyChanged(Microsoft.VisualStudio.Modeling.Diagrams.PropertyChangedEventArgs e)
        //{
        //    base.OnAssociatedPropertyChanged(e);

        //    //if (e.PropertyName == "ActiveLinkedWidgetName")
        //    //{
        //    //    this.Invalidate();
        //    //}
        //    ////else if (e.PropertyName == "CodeSnippet2")
        //    ////{
        //    ////    using (Transaction t = this.Store.TransactionManager.BeginTransaction("Layout shapes"))
        //    ////    {
        //    ////        this.PerformResizeParentRule(true, this);
        //    ////        //this.PerformResizeParentRule();
        //    ////        this.PerformShapeAnchoringRule();
        //    ////        t.Commit();
        //    ////    }
        //    ////    this.Invalidate();
        //    ////}
        //}

        private const double MARGIN = 0.02;
        private const string FieldName_PreContent = "PreContent";
        private const string FieldName_PostContent = "PostContent";
        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            // ++ PreContent ++
            TextField precontent = new CodeSnippetTextField(FieldName_PreContent);
            precontent.DefaultVisibility = true;
            precontent.DefaultSelectable = true;
            precontent.DefaultFocusable = true;
            precontent.DrawBorder = false;
            precontent.FillBackground = true;
            precontent.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, MARGIN);
            precontent.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, MARGIN);
            precontent.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, MARGIN);
            precontent.AnchoringBehavior.MinimumHeightInLines = 1;
            precontent.AnchoringBehavior.MinimumWidthInCharacters = 5;
            precontent.AnchoringBehavior.InvisibleCollapseFlags = InvisibleCollapseFlags.VerticallyToTop;
            //
            precontent.DefaultAutoSize = true;
            precontent.DefaultCommitOnEscape = true;
            precontent.DefaultMultipleLine = true;
            //
            precontent.DefaultBackgroundBrushId = _brushId;
            precontent.DefaultFontId = _FontId;
            precontent.DefaultStringFormat.Alignment = System.Drawing.StringAlignment.Near;
            precontent.DefaultStringFormat.FormatFlags |= System.Drawing.StringFormatFlags.NoWrap;
            shapeFields.Add(precontent);

            //// ++ Post Content ++
            TextField postcontent = new VDMultiLineTextField(FieldName_PostContent);
            postcontent.DefaultVisibility = false;
            //postcontent.DefaultSelectable = true;
            //postcontent.DefaultFocusable = true;
            postcontent.DrawBorder = false;
            postcontent.FillBackground = true;
            postcontent.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, MARGIN);
            postcontent.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, MARGIN);
            postcontent.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, MARGIN);
            postcontent.AnchoringBehavior.MinimumHeightInLines = 0;
            postcontent.AnchoringBehavior.MinimumWidthInCharacters = 1;
            postcontent.AnchoringBehavior.InvisibleCollapseFlags = InvisibleCollapseFlags.VerticallyToBottom;
            //
            postcontent.DefaultAutoSize = true;
            //postcontent.DefaultCommitOnEscape = true;
            postcontent.DefaultMultipleLine = true;
            //
            postcontent.DefaultBackgroundBrushId = _brushId;
            postcontent.DefaultFontId = _FontId;
            postcontent.DefaultStringFormat.Alignment = System.Drawing.StringAlignment.Near;
            postcontent.DefaultStringFormat.FormatFlags |= System.Drawing.StringFormatFlags.NoWrap;
            shapeFields.Add(postcontent);

            //
            base.InitializeShapeFields(shapeFields);
        }

        static partial void onBindShapeFields(object sender, EventArgs e)
        {
            ShapeElement shape = (ShapeElement)sender;
            associateProperty(shape, 
                VDCodeSnippetShape.FieldName_PreContent, VDCodeSnippet._PreCodeSnippetDomainPropertyId, Guid.Empty);
            associateProperty(shape,
                VDCodeSnippetShape.FieldName_PostContent, VDCodeSnippet._PostCodeSnippetDomainPropertyId,
                VDCodeSnippet._HasPostCodeSnippetDomainPropertyId, true);
        }

        public override ShapeField DefaultShapeField { get { return this.FindShapeField(FieldName_PreContent); } }

        //public override double MinWidth { get { return 0.4; } }

        //public override double MinHeight { get { return 0.25; } }

        public override double PaddingLeft { get { return MARGIN; } }

        public override double PaddingRight { get { return MARGIN; } }

        public override double PaddingTop
        {
            get
            {
                TextField field = this.FindShapeField(FieldName_PreContent) as TextField;
                if (field != null)
                {
                    SizeD size = field.GetMinimumSize(this);
                    return size.Height + 0.1;
                }
                return 0.25;
            }
        }

        public override double PaddingBottom
        {
            get
            {
                TextField field = this.FindShapeField(FieldName_PostContent) as TextField;
                if (field != null && field.GetVisible(this))
                {
                    SizeD size = field.GetMinimumSize(this);
                    return size.Height + 0.1;
                }
                return 0.05;
            }
        }

        private bool hasChildern
        {
            get
            {
                VDWidget widget = ModelElement as VDWidget;
                return widget != null && widget.Children != null && widget.Children.Count > 0;
            }
        }

        public override bool AllowsChildrenToResizeParent { get { return true; } }

        public override void OnBeginEdit(DiagramItemEventArgs e)
        {
            base.OnBeginEdit(e);
        }

        //public override void OnEndEdit(DiagramItemEventArgs e)
        //{
        //    base.OnEndEdit(e);
        //    if (e != null && e.DiagramClientView != null && e.DiagramItem.Field != null && e.DiagramItem.Field is CodeSnippetTextField)
        //    {
        //        using (Transaction t = this.Store.TransactionManager.BeginTransaction("Layout shapes"))
        //        {
        //            this.PerformResizeParentRule(true, this);
        //            t.Commit();
        //        }
        //    }
        //}

        //public void OnRequireRepositionChildren(double yOffset)
        //{
        //    using (Transaction t = this.Store.TransactionManager.BeginTransaction("Reposition child shapes"))
        //    {
        //        this.VerticalOffsetOfChildrenShape = yOffset;
        //        t.Commit();
        //    }
        //}

        //public SizeD CalcMinSize()
        //{
        //    SizeD size = this.CalculateMinimumSizeBasedOnChildren();

        //    // when no child shape, the CalculateMinimumSizeBasedOnChildren method doesn't count in the margin size
        //    if (this.NestedChildShapes == null || this.NestedChildShapes.Count == 0)
        //    {
        //        size.Height += this.PaddingBottom;

        //        // implementation of DefaultContainerMargin() doesn't count in the top padding
        //        size.Height += this.PaddingTop - 0.05;
        //    }
        //    else
        //    {
        //        // check if the pre snippet and any child overlaped.
        //        double verticalOffset = 0.0;
        //        double paddingtop = this.PaddingTop;
        //        foreach (ShapeElement nc in this.NestedChildShapes)
        //        {
        //            NodeShape child = nc as NodeShape;
        //            if (child != null)
        //            {
        //                if (child.Location.Y < paddingtop)
        //                    verticalOffset = Math.Max(verticalOffset, paddingtop - child.Location.Y);
        //            }
        //        }
        //        if (verticalOffset > 0.01)
        //        {
        //            size.Height += verticalOffset;
        //            //this.OnRequireRepositionChildren(verticalOffset);
        //        }
        //    }

        //    //if (size.Width < this.MinWidth) size.Width = this.MinWidth;
        //    //if (size.Height < this.MinHeight) size.Height = this.MinHeight;

        //    return size;
        //}

        //public override BoundsRules BoundsRules { get { return CodeSnippetBoundsRules.Instance; } }

        //class CodeSnippetBoundsRules : BoundsRules
        //{
        //    public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
        //    {
        //        RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);
        //        VDCodeSnippetShape codeShape = shape as VDCodeSnippetShape;
        //        if (codeShape != null)
        //        {
        //            SizeD size = codeShape.CalcMinSize();
        //            if (size.Height > compliantBounds.Height) compliantBounds.Height = size.Height;
        //        }
        //        return compliantBounds;
        //    }

        //    private static CodeSnippetBoundsRules _instance;
        //    public static CodeSnippetBoundsRules Instance
        //    {
        //        get
        //        {
        //            if (_instance == null) _instance = new CodeSnippetBoundsRules();
        //            return _instance;
        //        }
        //    }
        //}
    }

    //[RuleOn(typeof(VDCodeSnippetShape), FireTime = TimeToFire.TopLevelCommit, Priority = 0x7FFFFFFF)]
    //public class CodeSnippetShapeRule : ChangeRule
    //{
    //    public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
    //    {
    //        if (e.DomainProperty != null && e.DomainProperty.Id == VDCodeSnippetShape.VerticalOffsetOfChildrenShapeDomainPropertyId && (double)e.NewValue > 0.01)
    //        {
    //            VDCodeSnippetShape shape = e.ModelElement as VDCodeSnippetShape;
    //            if (shape != null)
    //            {
    //                double yOffset = (double)e.NewValue;
    //                shape.VerticalOffsetOfChildrenShape = 0.0;

    //                foreach (ShapeElement nc in shape.NestedChildShapes)
    //                {
    //                    NodeShape child = nc as NodeShape;
    //                    if (child != null)
    //                    {
    //                        child.Location = new PointD(child.Location.X, child.Location.Y + yOffset);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    class CodeSnippetTextField : VDMultiLineTextField
    {
        public CodeSnippetTextField(string fieldname)
            : base(fieldname)
        {
        }

        public override object GetValue(ShapeElement parentShape)
        {
            if (parentShape.Diagram != null
                && parentShape.Diagram.ActiveDiagramView != null
                && parentShape.Diagram.ActiveDiagramView.DiagramClientView != null)
            {
                if (this.HasPendingEdit(parentShape, parentShape.Diagram.ActiveDiagramView.DiagramClientView))
                {
                    VDCodeSnippet codeSnippet = parentShape.ModelElement as VDCodeSnippet;
                    if (codeSnippet != null)
                    {
                        return codeSnippet.CodeSnippet2;
                    }
                }
            }

            return base.GetValue(parentShape);
        }

        public override System.Windows.Forms.Control GetActiveInPlaceEditor(ShapeElement parentShape, DiagramClientView view)
        {
            var x= base.GetActiveInPlaceEditor(parentShape, view);
            return x;
        }
    }
}