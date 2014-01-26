namespace MVCVisualDesigner
{
    namespace Utility
    {
        using Microsoft.VisualStudio.Modeling.Diagrams;
        using System;
        using System.Drawing;
        using System.Drawing.Text;
        using System.Windows.Forms;

        /// <summary>
        /// Internal helper class for rendering text. In normal cases, we will be using CDI+ API MeasureString/DrawString to render text. When text 
        /// is complex script, will have use GDI (i.e. TextRenderer.MeasureText/DrawText to render text).
        /// </summary>
        internal static class DiagramRenderTextHelper
        {
            /// <summary>
            /// Fudging width when rendering text which contains complex script
            /// </summary>
            private const int fudgeWidth = 5;

            /// <summary>
            /// Gets a TextFormatFlags value from the specified StringFormat object.
            /// </summary>
            private static TextFormatFlags GetTextFormatFlagsFromStringFormat(StringFormat format)
            {
                TextFormatFlags glyphOverhangPadding = TextFormatFlags.GlyphOverhangPadding;
                if (format != null)
                {
                    float num;
                    if (format.GetTabStops(out num) != null)
                    {
                        glyphOverhangPadding |= TextFormatFlags.ExpandTabs;
                    }
                    if ((format.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0)
                    {
                        glyphOverhangPadding |= TextFormatFlags.RightToLeft;
                    }
                    if ((format.FormatFlags & StringFormatFlags.FitBlackBox) != 0)
                    {
                        glyphOverhangPadding |= TextFormatFlags.NoPadding;
                    }
                    if ((format.FormatFlags & StringFormatFlags.NoWrap) != 0)
                    {
                        glyphOverhangPadding |= TextFormatFlags.SingleLine;
                    }
                    else
                    {
                        glyphOverhangPadding |= TextFormatFlags.WordBreak;
                    }
                    if ((format.FormatFlags & StringFormatFlags.LineLimit) != 0)
                    {
                        glyphOverhangPadding |= TextFormatFlags.TextBoxControl;
                    }
                    switch (format.Alignment)
                    {
                        case StringAlignment.Center:
                            glyphOverhangPadding |= TextFormatFlags.HorizontalCenter;
                            break;

                        case StringAlignment.Far:
                            glyphOverhangPadding |= TextFormatFlags.Right;
                            break;

                        default:
                            glyphOverhangPadding = glyphOverhangPadding;
                            break;
                    }
                    switch (format.LineAlignment)
                    {
                        case StringAlignment.Center:
                            glyphOverhangPadding |= TextFormatFlags.VerticalCenter;
                            break;

                        case StringAlignment.Far:
                            glyphOverhangPadding |= TextFormatFlags.Bottom;
                            break;

                        default:
                            glyphOverhangPadding = glyphOverhangPadding;
                            break;
                    }
                    switch (format.Trimming)
                    {
                        case StringTrimming.EllipsisCharacter:
                            glyphOverhangPadding |= TextFormatFlags.EndEllipsis | TextFormatFlags.TextBoxControl;
                            break;

                        case StringTrimming.EllipsisWord:
                            glyphOverhangPadding |= TextFormatFlags.EndEllipsis | TextFormatFlags.TextBoxControl;
                            break;

                        case StringTrimming.EllipsisPath:
                            glyphOverhangPadding |= TextFormatFlags.PathEllipsis | TextFormatFlags.TextBoxControl;
                            break;

                        default:
                            if ((format.FormatFlags & StringFormatFlags.NoClip) != 0)
                            {
                                glyphOverhangPadding |= TextFormatFlags.NoClipping;
                            }
                            break;
                    }
                    switch (format.HotkeyPrefix)
                    {
                        case HotkeyPrefix.None:
                            return (glyphOverhangPadding | TextFormatFlags.NoPrefix);

                        case HotkeyPrefix.Show:
                            return glyphOverhangPadding;

                        case HotkeyPrefix.Hide:
                            return (glyphOverhangPadding | TextFormatFlags.HidePrefix);
                    }
                }
                return glyphOverhangPadding;
            }

            /// <summary>
            /// Measure text size. If text contains complex script, we will be using TextRenderer.MeasureText API to calc the size.
            /// </summary>
            internal static SizeF Measure(Graphics graphics, string text, Font font)
            {
                if (ScriptIsComplex(text))
                {
                    using (Font font2 = new Font(font.Name, font.Size * graphics.PageScale, font.Style, GraphicsUnit.Inch, font.GdiCharSet, font.GdiVerticalFont))
                    {
                        TextRenderingHint textRenderingHint = graphics.TextRenderingHint;
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Size size = TextRenderer.MeasureText(graphics, text, font2);
                        graphics.TextRenderingHint = textRenderingHint;
                        size.Width += 5;
                        size.Height += size.Height / font2.Height;
                        return new SizeF(((float)size.Width) / graphics.DpiX, ((float)size.Height) / graphics.DpiY);
                    }
                }
                return graphics.MeasureString(text, font);
            }

            /// <summary>
            /// Measure text size. If text contains complex script, we will be using TextRenderer.MeasureText API to calc the size.
            /// </summary>
            internal static SizeD Measure(Graphics graphics, string text, Font font, SizeD maximumSize, StringFormat format)
            {
                if (ScriptIsComplex(text))
                {
                    Size proposedSize = new Size((int)((maximumSize.Width * graphics.DpiX) * graphics.PageScale), (int)((maximumSize.Height * graphics.DpiY) * graphics.PageScale));
                    using (Font font2 = new Font(font.Name, font.Size * graphics.PageScale, font.Style, GraphicsUnit.Inch, font.GdiCharSet, font.GdiVerticalFont))
                    {
                        TextFormatFlags textFormatFlagsFromStringFormat = GetTextFormatFlagsFromStringFormat(format);
                        TextRenderingHint textRenderingHint = graphics.TextRenderingHint;
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Size size2 = TextRenderer.MeasureText(graphics, text, font2, proposedSize, textFormatFlagsFromStringFormat);
                        graphics.TextRenderingHint = textRenderingHint;
                        size2.Width += 5;
                        size2.Height += size2.Height / font2.Height;
                        size2.Width = Math.Min(size2.Width, proposedSize.Width);
                        size2.Height = Math.Min(size2.Height, proposedSize.Height);
                        return new SizeD((double)(((float)size2.Width) / graphics.DpiX), (double)(((float)size2.Height) / graphics.DpiY));
                    }
                }
                SizeF ef = graphics.MeasureString(text, font, SizeD.ToSizeF(maximumSize), format);
                return new SizeD((double)ef.Width, (double)ef.Height);
            }

            /// <summary>
            /// Measure text size. If text contains complex script, we will be using TextRenderer.MeasureText API to calc the size.
            /// </summary>
            internal static SizeF Measure(Graphics graphics, string text, Font font, PointF point, StringFormat format)
            {
                if (ScriptIsComplex(text))
                {
                    using (Font font2 = new Font(font.Name, font.Size * graphics.PageScale, font.Style, GraphicsUnit.Inch, font.GdiCharSet, font.GdiVerticalFont))
                    {
                        TextFormatFlags textFormatFlagsFromStringFormat = GetTextFormatFlagsFromStringFormat(format);
                        TextRenderingHint textRenderingHint = graphics.TextRenderingHint;
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Size proposedSize = new Size(0x7fffffff, 0x7fffffff);
                        Size size2 = TextRenderer.MeasureText(graphics, text, font2, proposedSize, textFormatFlagsFromStringFormat);
                        graphics.TextRenderingHint = textRenderingHint;
                        size2.Width += 5;
                        size2.Height += size2.Height / font2.Height;
                        return new SizeF(((float)size2.Width) / graphics.DpiX, ((float)size2.Height) / graphics.DpiY);
                    }
                }
                return graphics.MeasureString(text, font, point, format);
            }

            /// <summary>
            /// Measure text size. If text contains complex script, we will be using TextRenderer.DrawText API to calc the size.
            /// </summary>
            internal static void Render(DiagramPaintEventArgs e, string text, Font textFont, Brush brush, RectangleF rect, StringFormat format)
            {
                if (ScriptIsComplex(text))
                {
                    Graphics g = e.Graphics;
                    SolidBrush brush2 = brush as SolidBrush;
                    Color foreColor = (brush2 != null) ? brush2.Color : SystemColors.HighlightText;
                    Rectangle bounds = new Rectangle((int)(((rect.X + g.Transform.OffsetX) * g.DpiX) * g.PageScale), (int)(((rect.Y + g.Transform.OffsetY) * g.DpiY) * g.PageScale), (int)((rect.Width * g.DpiX) * g.PageScale), (int)((rect.Height * g.DpiY) * g.PageScale));
                    using (Font font = new Font(textFont.Name, textFont.Size * g.PageScale, textFont.Style, GraphicsUnit.Inch, textFont.GdiCharSet, textFont.GdiVerticalFont))
                    {
                        TextFormatFlags textFormatFlagsFromStringFormat = GetTextFormatFlagsFromStringFormat(format);
                        Font font2 = null;
                        if (e.IsPrinting)
                        {
                            font2 = ScaleFont(g, font);
                        }
                        TextRenderingHint textRenderingHint = g.TextRenderingHint;
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                        TextRenderer.DrawText(g, text, (font2 != null) ? font2 : font, bounds, foreColor, textFormatFlagsFromStringFormat);
                        g.TextRenderingHint = textRenderingHint;
                        if (font2 != null)
                        {
                            font2.Dispose();
                        }
                        return;
                    }
                }
                e.Graphics.DrawString(text, textFont, brush, rect, format);
            }

            /// <summary>
            /// When printing, we will need to scale font accordingly
            /// </summary>
            private static Font ScaleFont(Graphics g, Font font)
            {
                return new Font(font.FontFamily, (font.SizeInPoints / 72f) * g.DpiY, font.Style, GraphicsUnit.Pixel, font.GdiCharSet, font.GdiVerticalFont);
            }

            /// <summary>
            /// helper test method to determine whether the passed in text contain complex script of not. Return true if text has complex script.
            /// </summary>
            private static bool ScriptIsComplex(string text)
            {
                foreach (char ch in Environment.NewLine)
                {
                    text = text.Replace(ch.ToString(), string.Empty);
                }
                return (0 == Utility.NativeMethods.ScriptIsComplex(text, text.Length, 1));
            }
        }
    }
}
