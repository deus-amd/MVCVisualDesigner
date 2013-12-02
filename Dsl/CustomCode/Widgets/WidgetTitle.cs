using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;
using System.Drawing.Drawing2D;


// Model
namespace MVCVisualDesigner
{
}


// Shape
namespace MVCVisualDesigner
{
    public partial class VDWidgetTitlePort
    {
#region Resource/Fields/Decorators
        /// <summary>
        /// Initializes style set resources for this shape type
        /// </summary>
        /// <param name="classStyleSet">The style set for this shape class</param>
        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);

            // Custom font styles
            FontSettings fontSettings;
            fontSettings = new FontSettings();
            fontSettings.Style = FontStyle.Bold;
            fontSettings.Size = 9 / 72.0F;
            classStyleSet.AddFont(new StyleSetResourceId(string.Empty, "ShapeTextBold9"), DiagramFonts.ShapeText, fontSettings);
        }

        /// <summary>
        /// Initialize the collection of shape fields associated with this shape type.
        /// </summary>
        protected override void InitializeShapeFields(global::System.Collections.Generic.IList<ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);
            
            ImageField titleIconField = new ImageField("WidgetTitleIcon");
            titleIconField.DefaultSelectable = false;
            titleIconField.DefaultFocusable = false;
            //titleIconField.DefaultImage = ImageHelper.GetImage(MVCViewDesignerDomainModel.SingletonResourceManager.GetObject("GridLayoutToolToolboxBitmap")); 
            titleIconField.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, 0.02);
            titleIconField.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, 0.02);
            shapeFields.Add(titleIconField);

            TextField titleTextField = new TextField("WidgetTitleText");
            titleTextField.DefaultText = string.Empty;
            titleTextField.DefaultFocusable = true;
            titleTextField.DefaultAutoSize = true;
            titleTextField.AnchoringBehavior.MinimumHeightInLines = 1;
            titleTextField.AnchoringBehavior.MinimumWidthInCharacters = 1;
            titleTextField.DefaultAccessibleState = global::System.Windows.Forms.AccessibleStates.Invisible;
            titleTextField.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, 0.02);
            titleTextField.AnchoringBehavior.SetLeftAnchor(titleIconField, AnchoringBehavior.Edge.Right, 0.02);
            titleTextField.DefaultFontId = new StyleSetResourceId(string.Empty, "ShapeTextBold9");			
            shapeFields.Add(titleTextField);

            PinButtonField pinField = new PinButtonField("WidgetTitlePinIcon");
            pinField.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, 0.02);
            pinField.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, 0.02);
            pinField.DefaultSelectable = false;
            pinField.DefaultFocusable = false;
            shapeFields.Add(pinField);
        }

        /// <summary>
        /// Initialize the collection of decorators associated with this shape type.  This method also
        /// creates shape fields for outer decorators, because these are not part of the shape fields collection
        /// associated with the shape, so they must be created here rather than in InitializeShapeFields.
        /// </summary>
        protected override void InitializeDecorators(global::System.Collections.Generic.IList<ShapeField> shapeFields, global::System.Collections.Generic.IList<Decorator> decorators)
        {
            base.InitializeDecorators(shapeFields, decorators);
        }
#endregion

        public override BoundsRules BoundsRules
        {
            get { return WidgetTitlePortBoundsRules.Instance; }
        }

        // when title is selected, select its parent shape instead.
        public override void CoerceSelection(DiagramItem item, DiagramClientView view, bool isAddition)
        {
            base.CoerceSelection(item, view, isAddition);
            if (item != null && item.Shape != null && item.Shape == this && this.ParentShape != null)
            {
                view.Selection.Clear();
                view.Selection.Add(new DiagramItem(this.ParentShape));
            }
        }

        // set clip to make sure child shapes will not overlap parent's parent shape
        public override void OnPaintShape(DiagramPaintEventArgs e)
        {
            if (this.ParentShape != null 
                && this.ParentShape.ParentShape != null
                && !(this.ParentShape.ParentShape is Microsoft.VisualStudio.Modeling.Diagrams.Diagram))
            {
                RectangleD clipRect = this.ParentShape.ParentShape.AbsoluteBoundingBox;
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

        internal class WidgetTitlePortBoundsRules : BoundsRules
        {
            internal static readonly WidgetTitlePortBoundsRules Instance = new WidgetTitlePortBoundsRules();

            public override RectangleD GetCompliantBounds(ShapeElement portShape, RectangleD proposedBounds)
            {
                if (portShape == null)
                {
                    throw new ArgumentNullException("portShape");
                }

                VDWidgetShape parentShape = portShape.ParentShape as VDWidgetShape;
                if (parentShape == null || portShape.Store.InUndoRedoOrRollback)
                {
                    return proposedBounds;
                }

                PointD location = new PointD(0, -portShape.DefaultSize.Height - portShape.OutlinePenWidth * 2);
                SizeD size = portShape.DefaultSize;

                TextField titleTextField = portShape.FindShapeField("WidgetTitleText") as TextField;
                if (titleTextField != null)
                {
                    size.Width = titleTextField.GetMinimumSize(portShape).Width;
                    size.Width += size.Height + 0.03;
                }
                if (parentShape.HasWidgetTitleIcon)
                {
                    size.Width += size.Height; // add more space for icon
                }

                // should be smaller than parent shape
                if (size.Width > parentShape.Size.Width) size.Width = parentShape.Size.Width;

                return new RectangleD(location, size);
            }
        }
    }
}

