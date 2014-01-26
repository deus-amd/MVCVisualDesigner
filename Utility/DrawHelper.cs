namespace MVCVisualDesigner
{
    namespace Utility
    {
        using System;
        using System.Drawing;
        using System.Drawing.Drawing2D;

        /// <summary>
        /// Helper class to call Graphics.DrawXxx methods wrapped in a try/catch.
        /// The Catch clause will ignore any Out-of-memory exceptions thrown by the draw method.
        /// This has been happening typically with dashed pens. 
        /// It still fails in some cases even with the workaround - setting DashCap to Flat.
        /// </summary>
        internal class DrawHelper
        {
            /// <summary>
            /// Private constructor to prevent instantiating this class.
            /// </summary>
            private DrawHelper()
            {
            }

            /// <summary>
            /// Calls Graphics.DrawLine and ignores OutOfMemoryExceptions for dashed pens.
            /// </summary>
            /// <param name="g"></param>
            /// <param name="pen"></param>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            public static void SafeDrawLine(Graphics g, Pen pen, float x1, float y1, float x2, float y2)
            {
                try
                {
                    if ((g != null) && (pen != null))
                    {
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                catch (OutOfMemoryException)
                {
                    if (pen.DashStyle == DashStyle.Solid)
                    {
                        throw;
                    }
                }
                catch (OverflowException)
                {
                }
            }

            /// <summary>
            /// Calls Graphics.DrawLines and ignores OutOfMemoryExceptions for dashed pens.
            /// </summary>
            /// <param name="g"></param>
            /// <param name="pen"></param>
            /// <param name="points"></param>
            public static void SafeDrawLines(Graphics g, Pen pen, PointF[] points)
            {
                try
                {
                    if ((g != null) && (pen != null))
                    {
                        g.DrawLines(pen, points);
                    }
                }
                catch (OutOfMemoryException)
                {
                    if (pen.DashStyle == DashStyle.Solid)
                    {
                        throw;
                    }
                }
                catch (OverflowException)
                {
                }
            }

            /// <summary>
            /// Calls Graphics.DrawPath and ignores OutOfMemoryExceptions for dashed pens.
            /// </summary>
            /// <param name="g"></param>
            /// <param name="pen"></param>
            /// <param name="path"></param>
            public static void SafeDrawPath(Graphics g, Pen pen, GraphicsPath path)
            {
                try
                {
                    if ((g != null) && (pen != null))
                    {
                        g.DrawPath(pen, path);
                    }
                }
                catch (OutOfMemoryException)
                {
                    if (pen.DashStyle == DashStyle.Solid)
                    {
                        throw;
                    }
                }
                catch (OverflowException)
                {
                }
            }

            /// <summary>
            /// Calls Graphics.DrawRectangle and ignores OutOfMemoryExceptions for dashed pens.
            /// </summary>
            /// <param name="g"></param>
            /// <param name="pen"></param>
            public static void SafeDrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height)
            {
                try
                {
                    if ((g != null) && (pen != null))
                    {
                        g.DrawRectangle(pen, x, y, width, height);
                    }
                }
                catch (OutOfMemoryException)
                {
                    if (pen.DashStyle == DashStyle.Solid)
                    {
                        throw;
                    }
                }
                catch (OverflowException)
                {
                }
            }
        }
    }
}
