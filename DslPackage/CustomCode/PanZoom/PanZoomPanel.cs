//------------------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

// This file contains code from "ThumbnailView.cs" in the GotDotNet project
// "PowerToys for the Visual Studio 2005 Class Designer and Distributed System Designers"
// (http://www.gotdotnet.com/Workspaces/Workspace.aspx?id=fe72608b-2b28-4cc1-9866-ea6f805f45f3).
// It has been extracted into this file to make it easier to follow.
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace MVCVisualDesigner
{
    /// <summary>
    /// Diagram thumbnail control to be used in pan/zoom window and thumbnail view features.
    /// </summary>
    internal sealed class PanZoomPanel : Control
    {
        #region Private fields, types

        private DiagramClientView m_diagramClientView;
        private Bitmap m_diagramImage;
        private double m_imageScale;

        private enum MouseMode
        {
            None,
            Move,
        }

        private MouseMode m_mouseMode = MouseMode.None;

        #endregion

        #region Properties

        private DiagramClientView DiagramClientView
        {
            [DebuggerStepThrough]
            get { return m_diagramClientView; }
        }

        private Bitmap DiagramImage
        {
            [DebuggerStepThrough]
            get { return m_diagramImage; }
        }

        private new bool Enabled
        {
            [DebuggerStepThrough]
            get { return m_diagramClientView != null && m_diagramImage != null; }
        }

        private Microsoft.VisualStudio.Modeling.Diagrams.Diagram Diagram
        {
            [DebuggerStepThrough]
            get { return m_diagramClientView != null ? m_diagramClientView.Diagram : null; }
        }

        private Size MaximumImageSize
        {
            [DebuggerStepThrough]
            get
            {
                Size size = this.Size;
                Point location = MinimalImageOffset;
                size.Width -= location.X * 2;
                size.Height -= location.Y * 2;
                return size;
            }
        }

        private static Point MinimalImageOffset
        {
            [DebuggerStepThrough]
            get { return new Point(5, 5); }
        }

        private Size ImageSize
        {
            [DebuggerStepThrough]
            get
            {
                Debug.Assert(m_diagramImage != null);
                return m_diagramImage != null ? m_diagramImage.Size : Size.Empty;
            }
            set
            {
                if (m_diagramImage != null)
                {
                    if (m_diagramImage.Size != value)
                    {
                        m_diagramImage.Dispose();
                        m_diagramImage = null;
                    }
                    else
                    {
                        return;
                    }
                }
                if (m_diagramImage == null)
                {
                    m_diagramImage = new Bitmap(value.Width, value.Height);
                }
            }
        }

        private Point ImageLocation
        {
            [DebuggerStepThrough]
            get
            {
                Point location = MinimalImageOffset;
                Size maxSize = this.MaximumImageSize;
                Size realSize = this.ImageSize;
                location.Offset((maxSize.Width - realSize.Width) / 2, (maxSize.Height - realSize.Height) / 2);
                return location;
            }
        }

        private Rectangle ImageViewBounds
        {
            [DebuggerStepThrough]
            get
            {
                Debug.Assert(this.Enabled);
                if (this.Enabled)
                {
                    RectangleD viewBounds = this.DiagramClientView.ViewBounds;
                    Rectangle imageViewBounds = new Rectangle(DiagramToImage(viewBounds.Location), DiagramToImage(viewBounds.Size));
                    imageViewBounds.Offset(this.ImageLocation);
                    return imageViewBounds;
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }

        #endregion

        #region Coordinates translation

        private Point DiagramToImage(PointD worldPoint)
        {
            Debug.Assert(this.Enabled);
            if (this.Enabled)
            {
                Size ds = this.DiagramClientView.WorldToDevice(new SizeD(worldPoint.X, worldPoint.Y));
                return new Point((int)(ds.Width * m_imageScale), (int)(ds.Height * m_imageScale));
            }
            else
            {
                return Point.Empty;
            }
        }

        private Size DiagramToImage(SizeD worldSize)
        {
            Debug.Assert(this.Enabled);
            if (this.Enabled)
            {
                Debug.Assert(this.Enabled);
                Size ds = this.DiagramClientView.WorldToDevice(worldSize);
                return new Size((int)(ds.Width * m_imageScale), (int)(ds.Height * m_imageScale));
            }
            else
            {
                return Size.Empty;
            }
        }

        private PointD ImageToDiagram(Point imagePoint)
        {
            Debug.Assert(this.Enabled);
            if (this.Enabled)
            {
                SizeD s = this.DiagramClientView.DeviceToWorld(new Size(
                    (int)(imagePoint.X / m_imageScale),
                    (int)(imagePoint.Y / m_imageScale)));
                return new PointD(s.Width, s.Height);
            }
            else
            {
                return PointD.Empty;
            }
        }

        #endregion

        #region Diagram view control

        private void SetViewLocation(PointD viewLocation)
        {
            Debug.Assert(this.Enabled);
            if (this.Enabled)
            {
                this.Invalidate(Rectangle.Inflate(this.ImageViewBounds, 2, 2));


                double scrollUnitLength = (double)typeof(DiagramClientView).GetProperty("ScrollUnitLength", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this.DiagramClientView, new object[0]);

                this.DiagramClientView.HorizontalScrollPosition = (int)(viewLocation.X / scrollUnitLength);
                this.DiagramClientView.VerticalScrollPosition = (int)(viewLocation.Y / scrollUnitLength);

                this.DiagramClientView.Invalidate();

                this.Invalidate(Rectangle.Inflate(this.ImageViewBounds, 2, 2));
            }
        }

        #endregion

        #region Thumbnail image

        internal void InvalidateImage()
        {
            this.InvalidateImage(this.DiagramClientView);
        }

        internal void InvalidateImage(DiagramClientView diagramClientView)
        {
            m_diagramClientView = diagramClientView;
            if (m_diagramClientView != null)
            {
                SizeD diagramSize = this.Diagram.Size;
                Size deviceDiagramSize = this.DiagramClientView.WorldToDevice(diagramSize);
                Size maxImageSize = this.MaximumImageSize;

                m_imageScale = Math.Min(
                    (double)maxImageSize.Width / deviceDiagramSize.Width,
                    (double)maxImageSize.Height / deviceDiagramSize.Height);

                this.ImageSize = new Size(
                    (int)(deviceDiagramSize.Width * m_imageScale),
                    (int)(deviceDiagramSize.Height * m_imageScale));

                using (Graphics g = Graphics.FromImage(this.DiagramImage))
                {
                    g.Clear(Color.White);

                    MethodInfo drawMethod = typeof(Microsoft.VisualStudio.Modeling.Diagrams.Diagram).GetMethod("DrawDiagram", BindingFlags.NonPublic | BindingFlags.Instance);
                    drawMethod.Invoke(Diagram, new object[] {
						g,
						new Rectangle(0, 0, ImageSize.Width, ImageSize.Height), // fit the image
						new PointD(0, 0), // from origin
						(float)(m_imageScale * DiagramClientView.ZoomFactor), // fit the whole diagram
						null // don't need selection etc
					});
                }
            }
            this.Invalidate();
        }

        #endregion

        #region Event overrides

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (m_diagramImage != null)
            {
                m_diagramImage.Dispose();
                m_diagramImage = null;
            }
            m_diagramClientView = null;
            base.OnHandleCreated(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Enabled)
            {
                InvalidateImage();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.Enabled)
            {
                Graphics graphics = pevent.Graphics;
                Rectangle clientRect = this.ClientRectangle;

                Point imageLocation = this.ImageLocation;
                Size imageSize = this.ImageSize;

                graphics.SetClip(new Rectangle(imageLocation, imageSize), CombineMode.Exclude);
                Brush diagramBackgroundBrush = Diagram.StyleSet.GetBrush(DiagramBrushes.DiagramBackground);
                Debug.Assert(diagramBackgroundBrush != null);
                graphics.FillRectangle(diagramBackgroundBrush, clientRect);
                graphics.ResetClip();

                graphics.DrawImage(this.DiagramImage, imageLocation.X, imageLocation.Y, imageSize.Width, imageSize.Height);

                Pen zoomLassoPen = Diagram.StyleSet.GetPen(DiagramPens.ZoomLasso);
                Debug.Assert(zoomLassoPen != null);
                graphics.DrawRectangle(zoomLassoPen, this.ImageViewBounds);
            }
            else
            {
                pevent.Graphics.Clear(SystemColors.Control);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.Enabled) return;

            switch (m_mouseMode)
            {
                case MouseMode.None:
                    {
                        this.Cursor = Cursors.SizeAll;
                        break;
                    }
                case MouseMode.Move:
                    {
                        Point p = e.Location;
                        Point imageLocation = this.ImageLocation;
                        p.Offset(-imageLocation.X, -imageLocation.Y);
                        Rectangle imageBounds = this.ImageViewBounds;
                        p.Offset(-imageBounds.Width / 2, -imageBounds.Height / 2);
                        SetViewLocation(ImageToDiagram(p));
                        break;
                    }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.Enabled) return;

            if (m_mouseMode == MouseMode.None)
            {
                m_mouseMode = MouseMode.Move;
                this.Capture = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!this.Enabled) return;

            this.Capture = false;
            m_mouseMode = MouseMode.None;
            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!this.Enabled) return;

            Point p = e.Location;
            p.Offset(-ImageLocation.X, -ImageLocation.Y);
            p.Offset(-ImageViewBounds.Width / 2, -ImageViewBounds.Height / 2);
            SetViewLocation(ImageToDiagram(p));
        }

        internal void StartMove()
        {
            Debug.Assert(this.Enabled);
            if (!this.Enabled) return;

            Debug.Assert(m_mouseMode == MouseMode.None);
            if (m_mouseMode == MouseMode.None)
            {
                this.Capture = true;
                Rectangle viewRect = this.ImageViewBounds;
                viewRect.Offset(this.Location);
                Cursor.Position = PointToScreen(new Point(viewRect.Left + viewRect.Width / 2, viewRect.Top + viewRect.Height / 2));
                m_mouseMode = MouseMode.Move;
            }
        }

        #endregion
    }
}