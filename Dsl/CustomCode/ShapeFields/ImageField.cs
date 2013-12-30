using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Windows.Forms;
namespace MVCVisualDesigner
{
    public class VDTitleImageField : ImageField
    {
        public VDTitleImageField(string fieldName, int index)
            : base(fieldName)
        {
            m_index = index;
        }

        private int m_index;
        public int Index { get { return m_index; } }

        public override MouseAction GetPotentialMouseAction(MouseButtons mouseButtons, PointD point, DiagramHitTestInfo hitTestInfo)
        {
            DiagramItem hitDiagramItem = hitTestInfo.HitDiagramItem;
            Diagram diagram = (hitDiagramItem != null) ? hitDiagramItem.Diagram : null;
            if (diagram == null)
            {
                return null;
            }
            return diagram.SelectAction;
        }
    }

}

//namespace MVCVisualDesigner
//{
//    using System;
//    using System.Drawing;
//    using System.Windows.Forms;
//    using Microsoft.VisualStudio.Modeling.Diagrams;

//    /// <summary>
//    /// A ShapeField which can display an image in a ShapeElement.
//    /// </summary>
//    public class MVCImageField : ShapeField
//    {
//        private Image defaultImage;
//        private bool defaultUnscaled;

//        /// <summary>
//        /// Initializes a new instance of the ImageField class.
//        /// </summary>
//        /// <param name="fieldName">Unique identifier for this ShapeField.</param>
//        public MVCImageField(string fieldName)
//            : this(fieldName, null)
//        {
//        }

//        /// <summary>
//        /// Initializes a new instance of the ImageField class.
//        /// </summary>
//        /// <param name="image">Default image to display</param>
//        /// <param name="fieldName">Unique identifier for this ShapeField.</param>
//        public MVCImageField(string fieldName, Image image)
//            : base(fieldName)
//        {
//            this.defaultUnscaled = true;
//            this.DefaultImage = image;
//        }

//        /// <summary>
//        /// Draws the contents of the ShapeField.
//        /// </summary>
//        /// <param name="e">A DiagramPaintEventArgs that contains the event data.</param>
//        /// <param name="parentShape">The parent shape of this field.</param>
//        public override void DoPaint(DiagramPaintEventArgs e, ShapeElement parentShape)
//        {
//            if (this.GetVisible(parentShape))
//            {
//                Image displayImage = this.GetDisplayImage(parentShape);
//                if (displayImage != null)
//                {
//                    if (this.GetUnscaled(parentShape))
//                    {
//                        SizeF imageSize = ImageHelper.GetImageSize(displayImage);
//                        RectangleD bounds = this.GetBounds(parentShape);
//                        PointF location = new PointF((float)(bounds.Left + ((bounds.Width - imageSize.Width) / 2.0)), (float)(bounds.Top + ((bounds.Height - imageSize.Height) / 2.0)));
//                        e.Graphics.DrawImage(displayImage, new RectangleF(location, imageSize));
//                    }
//                    else
//                    {
//                        e.Graphics.DrawImage(displayImage, RectangleD.ToRectangleF(this.GetBounds(parentShape)));
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Returns the default accessible description for an image field.
//        /// </summary>
//        /// <param name="parentShape">The ShapeElement that contains this ShapeField.</param>
//        /// <returns>The accessible description.</returns>
//        public override string GetAccessibleDescription(ShapeElement parentShape)
//        {
//            if (base.DefaultAccessibleDescription != null)
//            {
//                return base.DefaultAccessibleDescription;
//            }
//            return string.Empty;
//        }

//        /// <summary>
//        /// Returns the default accessible name for an image field.
//        /// </summary>
//        /// <param name="parentShape">The ShapeElement that contains this ShapeField.</param>
//        /// <returns>The accessible name.</returns>
//        public override string GetAccessibleName(ShapeElement parentShape)
//        {
//            if (base.DefaultAccessibleName != null)
//            {
//                return base.DefaultAccessibleName;
//            }
//            return string.Empty;
//        }

//        /// <summary>
//        /// Retrieves the accessible role of this ShapeField.
//        /// </summary>
//        /// <param name="parentShape">The ShapeElement that contains this ShapeField.</param>
//        /// <returns>The AccessibleRole of this ShapeField.</returns>
//        public override AccessibleRole GetAccessibleRole(ShapeElement parentShape)
//        {
//            return AccessibleRole.Graphic;
//        }

//        /// <summary>
//        /// Gets the image to be displayed by this field.
//        /// </summary>
//        /// <param name="parentShape">The parent ShapeElement of this field.</param>
//        /// <returns>
//        /// The image to be displayed by this field.
//        /// </returns>
//        /// <remarks>
//        /// Override this method to customize your display image prior to drawing.
//        /// </remarks>
//        /// <remarks>
//        /// By default, this method returns GetValue() as Image or the default image.
//        /// </remarks>
//        public virtual Image GetDisplayImage(ShapeElement parentShape)
//        {
//            object obj2 = this.GetValue(parentShape);
//            if (obj2 == null)
//            {
//                return this.defaultImage;
//            }
//            return (obj2 as Image);
//        }

//        /// <summary>
//        /// Returns the minimum size of the ImageField.
//        /// </summary>
//        /// <remarks>
//        /// The minimum size is the size of
//        /// the image to be displayed, or the minimum size specified in the
//        /// AnchoringBehavior, whichever is larger.  If the shape field is scaled,
//        /// (GetUnscaled() returns false) then the AnchoringBehavior size is always used.
//        /// </remarks>
//        public override SizeD GetMinimumSize(ShapeElement parentShape)
//        {
//            SizeD minimumSize = base.GetMinimumSize(parentShape);
//            if (this.GetUnscaled(parentShape))
//            {
//                Image displayImage = this.GetDisplayImage(parentShape);
//                if (displayImage == null)
//                {
//                    return minimumSize;
//                }
//                SizeF imageSize = ImageHelper.GetImageSize(displayImage);
//                if (imageSize.Width > minimumSize.Width)
//                {
//                    minimumSize.Width = imageSize.Width;
//                }
//                if (imageSize.Height > minimumSize.Height)
//                {
//                    minimumSize.Height = imageSize.Height;
//                }
//            }
//            return minimumSize;
//        }

//        /// <summary>
//        /// Gets whether the image should be drawn unscaled or not.
//        /// </summary>
//        /// <param name="parentShape">The parent ShapeElement of this field.</param>
//        /// <returns>True to draw image unscaled and false otherwise.</returns>
//        public virtual bool GetUnscaled(ShapeElement parentShape)
//        {
//            return this.DefaultUnscaled;
//        }

//        /// <summary>
//        /// Gets or sets the default image associated with this ImageField.  The
//        /// default image can be customized by overriding the GetDisplayImage method.
//        /// </summary>
//        public Image DefaultImage
//        {
//            get
//            {
//                return this.defaultImage;
//            }
//            set
//            {
//                this.defaultImage = value;
//                Bitmap defaultImage = this.defaultImage as Bitmap;
//                if (defaultImage != null)
//                {
//                    defaultImage.MakeTransparent();
//                }
//            }
//        }

//        /// <summary>
//        /// Gets or sets whether image should be drawn unscaled with regards to the bounds
//        /// of this field or image should be scaled to fit the bounds.
//        /// </summary>
//        public bool DefaultUnscaled
//        {
//            get
//            {
//                return this.defaultUnscaled;
//            }
//            set
//            {
//                this.defaultUnscaled = value;
//            }
//        }
//    }
//}