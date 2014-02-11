using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVCVisualDesigner
{
    using Microsoft.VisualStudio.Modeling;
    using Microsoft.VisualStudio.Modeling.Design;
    using Microsoft.VisualStudio.Modeling.Diagrams;

    public partial class VDCodeSnippet : ICustomMerge
    {
        public override bool HasWidgetTitle { get { return true; } }

        public void MergeTo(VDWidget targetWidget, ElementGroup elementGroup)
        {
            targetWidget.Children.Add(this);
            VDCodeSnippetBody body = this.Store.ElementFactory.CreateElement(VDCodeSnippetBody.DomainClassId) as VDCodeSnippetBody;
            this.Body = body;
        }

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
            return GetPreCodeSnippet(this.CodeSnippet2);
        }

        public void Set_PreCodeSnippetValue(string newval)
        {
            this.CodeSnippet2 = newval;
        }

        public string Get_PostCodeSnippetValue()
        {
            return GetPostCodeSnippet(this.CodeSnippet2);
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

        protected StyleSetResourceId m_brushId = new StyleSetResourceId("MVDesigner", "CodeSnippetBrush");
        protected StyleSetResourceId m_FontId = new StyleSetResourceId("MVDesigner", "CodeSnippetFont");
        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);
            classStyleSet.AddBrush(m_brushId, m_brushId, new BrushSettings() { Color = Color.PapayaWhip });

            FontSettings fontSettings = new FontSettings();
            fontSettings.Style = System.Drawing.FontStyle.Bold;
            fontSettings.Size = 8 / 72.0F;
            classStyleSet.AddFont(m_FontId, DiagramFonts.ShapeText, fontSettings);

            // set text color for fields
            BrushSettings textBrush = new BrushSettings();
            textBrush.Color = Color.FromKnownColor(KnownColor.Maroon);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeText, textBrush);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeTextSelected, textBrush);
        }

        private const double MARGIN = 0.02;
        public const string FieldName_PreContent = "PreContent";
        public const string FieldName_PostContent = "PostContent";
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
            precontent.DefaultBackgroundBrushId = m_brushId;
            precontent.DefaultFontId = m_FontId;
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
            postcontent.DefaultBackgroundBrushId = m_brushId;
            postcontent.DefaultFontId = m_FontId;
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

        public override void OnEndEdit(DiagramItemEventArgs e)
        {
            base.OnEndEdit(e);
            if (e != null && e.DiagramClientView != null && e.DiagramItem.Field != null && e.DiagramItem.Field is CodeSnippetTextField)
            {
                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Adjust bounds of codesnippet children shapes"))
                {
                    this.relayoutChildren = true;
                    t.Commit();
                }
            }
        }

        protected override void SetAbsoluteBoundsValue(RectangleD newValue)
        {
            base.SetAbsoluteBoundsValue(newValue);
            this.relayoutChildren = true;
        }

        public override void OnRelayoutChildShapes()
        {
            // trigger bounds rules
            var bodyShape = this.GetChildShape<VDCodeSnippetBodyShape>();
            if (bodyShape != null)
            {
                bodyShape.OnBoundsFixup(BoundsFixupState.ViewFixup, 0, false);
            }
        }
    }

    public partial class VDCodeSnippetBodyShape
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public VDCodeSnippetBodyShape(Store store, params PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public VDCodeSnippetBodyShape(Partition partition, params PropertyAssignment[] propertyAssignments)
            : base(partition, propertyAssignments)
        {
            this.isPinned = true;
        }

        //public override bool CanMove { get { return false; } }

        public override NodeSides ResizableSides { get { return NodeSides.None; } }

        public override BoundsRules BoundsRules { get { return CodeSnippetBodyBoundsRules.Instance; } }

        class CodeSnippetBodyBoundsRules : BoundsRules
        {
            private static CodeSnippetBodyBoundsRules _instance;
            public static CodeSnippetBodyBoundsRules Instance
            {
                get
                {
                    if (_instance == null) _instance = new CodeSnippetBodyBoundsRules();
                    return _instance;
                }
            }

            public override RectangleD GetCompliantBounds(ShapeElement shape, RectangleD proposedBounds)
            {
                RectangleD compliantBounds = VDDefaultBoundsRules.Instance.GetCompliantBounds(shape, proposedBounds);

                VDCodeSnippetBodyShape bodyShape = shape as VDCodeSnippetBodyShape;
                if (bodyShape == null) return compliantBounds;

                VDCodeSnippetShape parentShape = bodyShape.ParentShape as VDCodeSnippetShape;
                if (parentShape == null) return compliantBounds;

                compliantBounds.Size = parentShape.Bounds.Size;
                compliantBounds.X = 0;

                TextField field = parentShape.FindShapeField(VDCodeSnippetShape.FieldName_PreContent) as TextField;
                if (field != null)
                {
                    SizeD size = field.GetMinimumSize(parentShape);
                    compliantBounds.Y = size.Height;
                    compliantBounds.Height -= size.Height;
                }

                field = parentShape.FindShapeField(VDCodeSnippetShape.FieldName_PostContent) as TextField;
                if (field != null)
                {
                    SizeD size = field.GetMinimumSize(parentShape);
                    compliantBounds.Height -= size.Height;
                }

                return compliantBounds;
            }
        }
    }


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
    }
}