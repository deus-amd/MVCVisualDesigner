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
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace MVCVisualDesigner
{
    /// <summary>
    /// A thumbnail form class to host a pan/zoom control.
    /// </summary>
    internal sealed class ThumbnailViewForm : Form
    {
        // width/height of the window.
        private const int ViewSize = 180;
        // control itself
        private PanZoomPanel m_panZoomPanel;

        internal ThumbnailViewForm(Control baseControl, DiagramClientView diagramClientView)
        {
            if (baseControl == null) throw new ArgumentNullException("baseControl");
            if (diagramClientView == null) throw new ArgumentNullException("diagramClientView");

            // Initialize the form.
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;

            // Position form so that its center lines up with the center of thumbnail control
            // at designer's bottom-right corner.
            Point location = baseControl.PointToScreen(new Point(baseControl.Width / 2, baseControl.Height / 2));
            location.Offset(-ViewSize / 2, -ViewSize / 2);
            this.Bounds = new Rectangle(location.X, location.Y, ViewSize, ViewSize);

            // Make sure thumbnail form fits the screen and doesn't go below or off the right
            // edge of the screen.
            Rectangle screenBounds = Screen.FromControl(diagramClientView).WorkingArea;
            if (this.Right > screenBounds.Right)
                this.Left = screenBounds.Right - this.Width;
            if (this.Bottom > screenBounds.Bottom)
                this.Top = screenBounds.Bottom - this.Height;

            // Initialize a panel to host pan/zoom control.
            Panel panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panel1);

            // Initialize and dock pan/zoom control on the panel.
            m_panZoomPanel = new PanZoomPanel();
            m_panZoomPanel.Dock = DockStyle.Fill;
            panel1.Controls.Add(m_panZoomPanel);
            m_panZoomPanel.InvalidateImage(diagramClientView);

            Cursor.Hide();
        }

        protected override CreateParams CreateParams
        {
            [DebuggerStepThrough]
            get
            {
                // Give this form a nice shadow.
                CreateParams createParams = base.CreateParams;
                Debug.Assert(createParams != null);
                createParams.ClassStyle |= 0x00020000;
                return createParams;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(e != null);
            Debug.Assert(m_panZoomPanel != null);

            Point initialMousePos = Cursor.Position;
            m_panZoomPanel.MouseUp += delegate(object sender, MouseEventArgs args)
            {
                // When mouse is released, the form should go away.
                Cursor.Position = initialMousePos;
                this.Close();
                Cursor.Show();
            };

            // We automatically start moving diagram view on load.
            m_panZoomPanel.StartMove();
        }
    }
}