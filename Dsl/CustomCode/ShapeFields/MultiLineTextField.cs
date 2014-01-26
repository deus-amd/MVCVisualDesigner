using Microsoft.VisualStudio.Modeling.Diagrams;
using MVCVisualDesigner.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public class VDMultiLineTextField : TextField
    {
        public VDMultiLineTextField(string fieldname)
            : base(fieldname)
        {
        }

        public override void DoPaint(DiagramPaintEventArgs e, ShapeElement parentShape)
        {
            if (this.HasPendingEdit(parentShape, e.View)) return;
            if (!this.GetVisible(parentShape)) return;

            float magicNum = 0.04166667f;

            RectangleF clipBounds = e.Graphics.ClipBounds;
            clipBounds.Inflate(magicNum, magicNum);
            clipBounds.Height -= magicNum;
            e.Graphics.SetClip(clipBounds);

            RectangleD fieldBounds = this.GetBounds(parentShape);
            RectangleF fieldRect = RectangleD.ToRectangleF(fieldBounds);

            // drawing vertically
            Matrix prevTransform = null;
            if (!this.DefaultIsHorizontal)
            {
                PointF point = PointD.ToPointF(fieldBounds.Center);
                prevTransform = e.Graphics.Transform;
                Matrix matrix = e.Graphics.Transform;
                matrix.RotateAt(-90f, point);
                matrix.Translate(0f, (-point.X / 2f) - fieldRect.X);
                e.Graphics.Transform = matrix;
            }

            // clip the field bound
            if (parentShape.ClipWhenDrawingFields)
            {
                RectangleD boundingBox = parentShape.BoundingBox;
                fieldRect.Intersect(new RectangleF(0f, 0f, (float)boundingBox.Width, (float)boundingBox.Height));
            }

            // fill background
            if (this.FillBackground)
            {
                Color oldColor = Color.White;
                Brush brush = this.GetBackgroundBrush(e.View, parentShape, ref oldColor);
                e.Graphics.FillRectangle(brush, fieldRect);

                // restore old color
                SolidBrush brush2 = brush as SolidBrush;
                if (brush2 != null)
                {
                    brush2.Color = oldColor;
                }
            }

            // border
            if (this.DrawBorder)
            {
                Color oldColor = Color.White;
                Pen pen = this.GetPen(e.View, parentShape, ref oldColor);
                DrawHelper.SafeDrawRectangle(e.Graphics, pen, fieldRect.X, fieldRect.Y, fieldRect.Width, fieldRect.Height);
                pen.Color = oldColor;
            }

            // display text
            string displayText = this.GetDisplayText(parentShape);
            if (displayText.Length > 0)
            {
                using (Font font = this.GetFont(parentShape))
                {
                    // line number
                    int linecount = 1;
                    linecount = displayText.Split('\r').Length;
                    StringBuilder lineNums = new StringBuilder();
                    for (int i = 1; i <= linecount; i++) lineNums.Append(i).Append(Environment.NewLine);

                    SizeF lineNumSize = DiagramRenderTextHelper.Measure(e.Graphics, linecount.ToString(), font);
                    lineNumSize.Width += magicNum;

                    RectangleF lineNumRect = new RectangleF(fieldRect.X, fieldRect.Y, lineNumSize.Width, fieldRect.Height);
                    RectangleF dispTxtRect = new RectangleF(lineNumRect.Right, fieldRect.Y, fieldRect.Width - lineNumSize.Width, fieldRect.Height);

                    using (Brush brush = new SolidBrush(Color.LightSeaGreen))
                    {
                        // draw line numbers
                        DiagramRenderTextHelper.Render(e, lineNums.ToString(), font, brush, lineNumRect, this.GetStringFormat(parentShape));
                    }

                    // draw display text
                    DiagramRenderTextHelper.Render(e, displayText, font, this.GetTextBrush(e.View, parentShape), dispTxtRect, this.GetStringFormat(parentShape));
                }
            }

            // draw focus
            if (base.HasFocusedAppearance(parentShape, e.View))
            {
                RectangleF focusRect = fieldRect;
                //focusRect.Inflate(0f, magicNum);
                //focusRect.Height -= magicNum;
                Pen pen = parentShape.StyleSet.GetPen(DiagramPens.FocusIndicator);
                Pen bkgPen = parentShape.StyleSet.GetPen(DiagramPens.FocusIndicatorBackground);
                DrawHelper.SafeDrawRectangle(e.Graphics, bkgPen, focusRect.X, focusRect.Y, focusRect.Width, focusRect.Height);
                DrawHelper.SafeDrawRectangle(e.Graphics, pen, focusRect.X, focusRect.Y, focusRect.Width, focusRect.Height);
            }

            // restore the transform matrix
            if (prevTransform != null)
            {
                e.Graphics.Transform = prevTransform;
            }
        }

        /// <summary>
        /// By default, we allow the in-place edit control to resize up to 75 characters.
        /// </summary>
        public override SizeD GetMaximumInPlaceEditorSize(ShapeElement parentShape)
        {
            SizeD maxsize = this.SizeHelper(parentShape, 75.0, true);
            if (maxsize.Width < parentShape.BoundingBox.Width * 0.8)
                maxsize.Width = parentShape.BoundingBox.Width * 0.8;

            float fontheight = this.GetFontHeight(parentShape);
            maxsize.Height += fontheight;
            if (maxsize.Height < fontheight * 3.5)
                maxsize.Height = fontheight * 3.5;
            return maxsize;
        }

        /// <summary>
        /// Minimum in-place editor size is based on minimum width in characters and height in lines.
        /// </summary>
        public override SizeD GetMinimumInPlaceEditorSize(ShapeElement parentShape)
        {
            SizeD minsize = this.SizeHelper(parentShape, this.AnchoringBehavior.MinimumWidthInCharacters, true);
            if (minsize.Width < parentShape.BoundingBox.Width * 0.8)
                minsize.Width = parentShape.BoundingBox.Width * 0.8;

            float fontheight = this.GetFontHeight(parentShape);
            minsize.Height += fontheight;
            if (minsize.Height < fontheight * 3.5)
                minsize.Height = fontheight * 3.5;
            return minsize;
        }

        /// <summary>
        /// Gets the minimum width and heightfor this ShapeField in world units
        /// </summary>
        /// <param name="parentShape">The ShapeElement instance</param>
        /// <returns>The minimum size for this ShapeField in world units</returns>
        public override SizeD GetMinimumSize(ShapeElement parentShape)
        {
            return this.SizeHelper(parentShape, this.AnchoringBehavior.MinimumWidthInCharacters, true);
        }

        private SizeD SizeHelper(ShapeElement parentShape, double widthInCharacters, bool autoSize)
        {
            double num = 0.0;
            if (widthInCharacters > 0.0)
            {
                using (Font font = this.GetFont(parentShape))
                {
                    num = font.Size * widthInCharacters;
                }
            }

            double num2 = 0.0;
            if (base.AnchoringBehavior.MinimumHeightInLines > 0.0)
            {
                num2 = this.GetFontHeight(parentShape) * base.AnchoringBehavior.MinimumHeightInLines;
            }

            SizeD minimumSize = SizeD.Empty; // = base.GetMinimumSize(parentShape);
            if (num > minimumSize.Width)
            {
                minimumSize.Width = num;
            }
            if (num2 > minimumSize.Height)
            {
                minimumSize.Height = num2;
            }

            if (autoSize && this.GetAutoSize(parentShape))
            {
                SizeD maximumSize = parentShape.MaximumSize;
                //if (this.GetMultipleLine(parentShape))
                //{
                //    throw new InvalidOperationException(DesignSurfaceStrings.TextFieldAutoSizeAndMultiline);
                //}
                SizeD ed3 = this.MeasureDisplayText(this.GetDisplayText(parentShape), this.GetFont(parentShape), this.GetStringFormat(parentShape), maximumSize);
                if (ed3.Width > minimumSize.Width)
                {
                    minimumSize.Width = ed3.Width;
                }
                if (ed3.Height > minimumSize.Height)
                {
                    minimumSize.Height = ed3.Height;
                }
            }
            return minimumSize;
        }

        // bypass the \t problem for now
        public override void SetValue(ShapeElement parentShape, object value)
        {
            if (value != null)
            {
                string text = value.ToString();
                text = text.Replace("\t", "    ");
                base.SetValue(parentShape, text);
            }
            else
            {
                base.SetValue(parentShape, value);
            }
        }
    }
}
