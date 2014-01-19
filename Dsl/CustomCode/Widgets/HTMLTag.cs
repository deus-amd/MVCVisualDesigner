using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using MVCVisualDesigner.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MEL
namespace MVCVisualDesigner
{
    public partial class VDHTMLTag
    {
        public override bool HasWidgetTitle { get { return true; } }

#region IMS Properties
        // Tag Name
        //protected string m_tagName = "div";
        //public virtual string GetTagNameValue()
        //{
        //    return m_tagName;
        //}
        //public virtual void SetTagNameValue(string newValue)
        //{
        //    m_tagName = newValue;
        //}

        //// Tag Text
        //protected string m_tagText;
        //public virtual string GetTagTextValue()
        //{
        //    return m_tagText;
        //}
        //public virtual void SetTagTextValue(string newValue)
        //{
        //    m_tagText = newValue;
        //}

        // _OpenTagStr
        public string Get_OpenTagStrValue()
        {
            string moreHtmlAttrs = this.GetMoreHtmlAttributeString();
            if (this._HasTagText || (this.Children != null && this.Children.Count > 0))
            {
                if (string.IsNullOrWhiteSpace(moreHtmlAttrs))
                    return string.Format("<{0}>", this.TagName);
                else
                    return string.Format("<{0} {1}>", this.TagName, moreHtmlAttrs);
            }
            else // empty tag
            {
                if (string.IsNullOrWhiteSpace(moreHtmlAttrs))
                    return string.Format("<{0}/>", this.TagName);
                else
                    return string.Format("<{0} {1}/>", this.TagName, moreHtmlAttrs);
            }
        }
        public void Set_OpenTagStrValue(string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
            {
                this.TagName = "div"; // default value
                //todo: this.MoreHTMLAttributes.Clear();
                return;
            }

            HTMLTagStateMachine sm = new HTMLTagStateMachine();
            sm.Run(newValue);

            if (string.IsNullOrWhiteSpace(sm.TagName)) throw new Exception("Bad input");

            this.TagName = sm.TagName;
            //todo: this.HtmlAttributes = sm.Attribute
            this.TagText = sm.TagText;
        }

        // _CloseTagStr
        public string Get_CloseTagStrValue()
        {
            return string.Format("</{0}>", this.TagName);
        }

        // _IsCloseTagVisibleInFooter
        public bool Get_IsCloseTagVisibleInFooterValue()
        {
            return this.Children != null && this.Children.Count > 0;
        }

        // _IsCloseTagVisibleInHeader
        public bool Get_IsCloseTagVisibleInHeaderValue()
        {
            return Get_HasTagTextValue() && (this.Children == null || this.Children.Count < 1);
        }

        // _HasTagText
        public bool Get_HasTagTextValue()
        {
            return !string.IsNullOrWhiteSpace(this.TagText);
        }

        // notify changes to calculated & customStorage properties
        internal sealed partial class TagNamePropertyHandler : DomainPropertyValueHandler<VDHTMLTag, String>
        {
            protected override void OnValueChanged(VDHTMLTag element, string oldValue, string newValue)
            {
                base.OnValueChanged(element, oldValue, newValue);
                element.Store.DomainDataDirectory.GetDomainProperty(VDHTMLTag._OpenTagStrDomainPropertyId).NotifyValueChange(element);
                element.Store.DomainDataDirectory.GetDomainProperty(VDHTMLTag._CloseTagStrDomainPropertyId).NotifyValueChange(element);
            }
        }

        internal sealed partial class TagTextPropertyHandler : DomainPropertyValueHandler<VDHTMLTag, String>
        {
            protected override void OnValueChanged(VDHTMLTag element, string oldValue, string newValue)
            {
                base.OnValueChanged(element, oldValue, newValue);
                element.Store.DomainDataDirectory.GetDomainProperty(VDHTMLTag._HasTagTextDomainPropertyId).NotifyValueChange(element);
                element.Store.DomainDataDirectory.GetDomainProperty(VDHTMLTag._IsCloseTagVisibleInFooterDomainPropertyId).NotifyValueChange(element);
            }
        }


        class HTMLTagStateMachine : StateMachine
        {
            public string TagName { get; set; }
            public string TagText { get; set; }
            public string TagAttr { get; set; }

            protected override void initStates()
            {
                TagName = string.Empty;
                TagText = string.Empty;
                TagAttr = string.Empty;

                // create states
                State Start = createState("Start", true);
                State OpenTag = createState("OpenTag");
                State OpenTagWithRazorVar = createState("OpenTagWithRazorVar");
                State Attr = createState("Attr");
                State Text = createState("Text");
                State End = createState("End");

                // transitions
                Start.To(OpenTag).OnChar('<');
                Start.To(OpenTag).On(c => isA2Z(c), true);
                Start.To(OpenTagWithRazorVar).OnChar('@');

                OpenTag.To(OpenTag).On(c => isAlphaAndNum(c));
                OpenTag.To(Text).OnChar('>').Do((ctx, ch) => TagName = getStateToken());
                OpenTag.To(End).OnChar('/').Do((ctx, ch) => TagName = getStateToken());
                OpenTag.To(Attr).OnAnyFNSSChar(true).Do((ctx, ch) => TagName = getStateToken());
                OpenTag.To(End).OnEOL().Do((ctx, ch) => TagName = getStateToken());

                OpenTagWithRazorVar.To(OpenTagWithRazorVar).On(c => isAlphaAndNum(c) || c == '.' || c == '_');
                OpenTagWithRazorVar.To(Text).OnChar('>').Do((ctx, ch) => TagName = getStateToken());
                OpenTagWithRazorVar.To(End).OnChar('/').Do((ctx, ch) => TagName = getStateToken());
                OpenTagWithRazorVar.To(Attr).OnAnyFNSSChar(true).Do((ctx, ch) => TagName = getStateToken());
                OpenTagWithRazorVar.To(End).OnEOL().Do((ctx, ch) => TagName = getStateToken());

                Attr.To(Text).OnChar('>').Do((ctx, ch) => TagAttr = getStateToken());
                Attr.To(End).OnChar('/').Do((ctx, ch) => TagAttr = getStateToken());
                Attr.To(Attr).OnAnyChar();
                Attr.To(End).OnEOL().Do((ctx, ch) => TagAttr = getStateToken());

                Text.To(End).OnChar('<').Do((ctx, ch) => TagText = getStateToken());
                Text.To(Text).OnAnyChar();
                Text.To(End).OnEOL().Do((ctx, ch) => TagText = getStateToken());
            }

            private bool isA2Z(char c)
            {
                return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
            }

            private bool isAlphaAndNum(char c)
            {
                return isA2Z(c) || (c >= '0' && c <= '9');
            }
        }
#endregion

    }
}


// PEL
namespace MVCVisualDesigner
{
    public partial class VDHTMLTagShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override System.Drawing.Image getTitleIcon()
        {
            return this.getImageFromResource("HTMLTagToolToolboxBitmap"); 
        }

        protected override string getTitleText()
        {
            VDHTMLTag MEL = this.GetMEL<VDHTMLTag>();
            if (MEL != null && !string.IsNullOrWhiteSpace(MEL.TagName))
                return string.Format("<{0}>", MEL.TagName);
            else
                return base.getTitleText();
        }

        protected StyleSetResourceId _headFootBrushId = new StyleSetResourceId("MVDesigner", "HTMLTagHeadFootBrush");
        protected StyleSetResourceId _FontId = new StyleSetResourceId("MVDesigner", "HTMLTagFont");

        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);
            classStyleSet.AddBrush(_headFootBrushId, _headFootBrushId, new BrushSettings() { Color = Color.Azure });

            FontSettings fontSettings = new FontSettings();
            fontSettings.Style = System.Drawing.FontStyle.Bold;
            fontSettings.Size = 8 / 72.0F;
            classStyleSet.AddFont(_FontId, DiagramFonts.ShapeText, fontSettings);

            // set text color for fields
            BrushSettings textBrush = new BrushSettings();
            textBrush.Color = Color.FromKnownColor(KnownColor.Maroon);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeText, textBrush);
            classStyleSet.OverrideBrush(DiagramBrushes.ShapeTextSelected, textBrush);

            VDHTMLTagShape.AssociateValueWith(this.Store, VDHTMLTag.TagNameDomainPropertyId);
            VDHTMLTagShape.AssociateValueWith(this.Store, VDHTMLTag.TagTextDomainPropertyId);
        }

        protected override void OnAssociatedPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnAssociatedPropertyChanged(e);
            if (e.PropertyName == "TagName" || e.PropertyName == "TagText")
            {
                this.Invalidate();
            }
        }

        //
        public const string FieldName_Head = "Header";
        public const string FieldName_Foot = "Footer";
        public const string FieldName_OpenTag = "OpenTag";
        public const string FieldName_HeaderCloseTag = "HeaderCloseTag";
        public const string FieldName_FooterCloseTag = "FooterCloseTag";
        public const string FieldName_Text = "Text";

        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            double vertMargin = 0.03;
            double horiMargin = 0.02;
            double marginToParent = 0.01;

            // ++ HEAD ++
            AreaField head = this.CreateBackgroundGradientField(FieldName_Head);
            head.DefaultFocusable = false;
            head.DefaultSelectable = false;
            head.DefaultVisibility = true;
            head.DrawBorder = false;
            head.FillBackground = true;
            //head.GradientEndingColor = Color;
            head.DefaultBackgroundBrushId = _headFootBrushId;
            head.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, marginToParent);
            head.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, marginToParent);
            head.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, marginToParent);
            //head.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, marginToParent);
            head.DefaultHeight = 0.2;
            shapeFields.Add(head);

            // ++ OpenTag ++
            HtmlTagTextField openTag = new HtmlTagTextField(FieldName_OpenTag);
            openTag.DefaultFocusable = true;
            openTag.DefaultSelectable = true;
            openTag.DefaultVisibility = true;
            openTag.DefaultAutoSize = true;
            openTag.DrawBorder = false;
            openTag.FillBackground = false;
            openTag.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, vertMargin);
            openTag.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, marginToParent);
            openTag.AnchoringBehavior.MinimumHeightInLines = 1;
            openTag.AnchoringBehavior.MinimumWidthInCharacters = 1;
            openTag.DefaultFontId = _FontId;
            shapeFields.Add(openTag);

            // ++ Text ++
            TextField tagText = new TextField(FieldName_Text);
            tagText.DefaultFocusable = true;
            tagText.DefaultSelectable = true;
            tagText.DefaultVisibility = true;
            tagText.DefaultAutoSize = true;
            tagText.DrawBorder = false;
            tagText.FillBackground = false;
            tagText.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, vertMargin);
            tagText.AnchoringBehavior.SetLeftAnchor(openTag, AnchoringBehavior.Edge.Right, horiMargin);
            tagText.AnchoringBehavior.MinimumHeightInLines = 1;
            //tagText.AnchoringBehavior.MinimumWidthInCharacters = 1;
            tagText.AnchoringBehavior.InvisibleCollapseFlags = InvisibleCollapseFlags.HorizontallyToLeft;
            shapeFields.Add(tagText);

            // ++ Close Tag in Header
            TextField closeTag = new TextField(FieldName_HeaderCloseTag);
            closeTag.DefaultFocusable = false;
            closeTag.DefaultSelectable = false;
            closeTag.DefaultVisibility = true;
            closeTag.DefaultAutoSize = true;
            closeTag.DrawBorder = false;
            closeTag.FillBackground = false;
            closeTag.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, vertMargin);
            closeTag.AnchoringBehavior.SetLeftAnchor(tagText, AnchoringBehavior.Edge.Right, horiMargin);
            closeTag.AnchoringBehavior.InvisibleCollapseFlags = InvisibleCollapseFlags.HorizontallyToLeft;
            closeTag.AnchoringBehavior.MinimumHeightInLines = 1;
            //closeTag.AnchoringBehavior.MinimumWidthInCharacters = 1;
            closeTag.DefaultFontId = _FontId;
            shapeFields.Add(closeTag);

            // ++ Foot ++
            AreaField footer = this.CreateBackgroundGradientField(FieldName_Foot);
            footer.DefaultFocusable = false;
            footer.DefaultSelectable = false;
            footer.DefaultVisibility = false;
            footer.DrawBorder = false;
            footer.FillBackground = true;
            //item.GradientEndingColor = Color.Red;
            footer.DefaultBackgroundBrushId = _headFootBrushId;
            //item.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, 0.0); //!!!! this doesn't work!!, user negative top anchor to bypass
            footer.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Bottom, -0.2);
            footer.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, marginToParent);
            footer.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, marginToParent);
            footer.DefaultHeight = 0.2;
            shapeFields.Add(footer);

            // ++ close tag in footer ++
            TextField closeTag2 = new TextField(FieldName_FooterCloseTag);
            closeTag2.DefaultFocusable = false;
            closeTag2.DefaultSelectable = false;
            closeTag2.DefaultVisibility = false;
            closeTag2.DefaultAutoSize = true;
            closeTag2.DrawBorder = false;
            closeTag2.FillBackground = false;
            closeTag2.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, vertMargin);
            closeTag2.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, marginToParent);
            closeTag2.AnchoringBehavior.MinimumHeightInLines = 1;
            closeTag2.AnchoringBehavior.MinimumWidthInCharacters = 1;
            closeTag2.DefaultFontId = _FontId;
            shapeFields.Add(closeTag2);

            base.InitializeShapeFields(shapeFields);
        }

        static partial void onBindShapeFields(object sender, EventArgs e)
        {
            ShapeElement shape = (ShapeElement)sender;

            // header
            associateProperty(shape, VDHTMLTagShape.FieldName_OpenTag, 
                VDHTMLTag._OpenTagStrDomainPropertyId, 
                Guid.Empty);

            associateProperty(shape, VDHTMLTagShape.FieldName_Text,
                VDHTMLTag.TagTextDomainPropertyId,
                VDHTMLTag._HasTagTextDomainPropertyId, true);

            associateProperty(shape, VDHTMLTagShape.FieldName_HeaderCloseTag,
                VDHTMLTag._CloseTagStrDomainPropertyId,
                VDHTMLTag._IsCloseTagVisibleInHeaderDomainPropertyId, true);

            // footer
            associateProperty(shape, VDHTMLTagShape.FieldName_Foot,
                Guid.Empty,
                VDHTMLTag._IsCloseTagVisibleInFooterDomainPropertyId, true);
            associateProperty(shape, VDHTMLTagShape.FieldName_FooterCloseTag,
                VDHTMLTag._CloseTagStrDomainPropertyId,
                VDHTMLTag._IsCloseTagVisibleInFooterDomainPropertyId, true);
        }

        public override bool AllowsChildrenToResizeParent { get { return true; } }

        public override NodeSides ResizableSides
        {
            get { return this.hasChildren ? NodeSides.All : NodeSides.Horizontal; }
        }

        protected override RectangleD GetAbsoluteBoundsValue()
        {
            RectangleD r = base.GetAbsoluteBoundsValue();
            if (!this.hasChildren)
            {
                r.Height = MinimumResizableSize.Height;
            }
            return r;
        }

        public override SizeD MinimumResizableSize
        {
            get
            {
                if (!this.hasChildren)
                    return new SizeD(0.4D, 0.2D);
                else
                    return base.MinimumResizableSize;
            }
        }

        // ShapeElement.HasChildren will count in Title Port as child
        // but this property will not
        private bool hasChildren
        {
            get
            {
                ShapeField field = this.FindShapeField(FieldName_FooterCloseTag);
                return field != null ? field.GetVisible(this) : base.HasChildren;
            }
        }

        //public override double PaddingLeft { get { return 0.15; } }
        public override double PaddingTop { get { return 0.25; } }
        public override double PaddingBottom { get { return 0.25; } }

        class HtmlTagTextField : TextField
        {
            public HtmlTagTextField(string fieldName)
                : base(fieldName)
            {
                this.DefaultStringFormat.Alignment = StringAlignment.Near;
            }

            public override SizeD GetMinimumInPlaceEditorSize(ShapeElement parentShape)
            {
                if (parentShape != null && parentShape is VDHTMLTagShape)
                {
                    return ((VDHTMLTagShape)parentShape).Size;
                }
                else
                {
                    return base.GetMinimumInPlaceEditorSize(parentShape);
                }
            }

            //public override string GetDisplayText(ShapeElement parentShape)
            //{
            //    string dispText = base.GetDisplayText(parentShape);
            //    int index = dispText.IndexOfAny(new char[] { ' ', '\t', '\\', '>' });
            //    if (index > 0)
            //        return dispText.Substring(0, index);
            //    else
            //        return dispText;
            //}
        }
    }
}

