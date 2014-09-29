using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling.Shell;

namespace MVCVisualDesigner
{
    internal partial class MVCVisualDesignerDocView
    {
        #region Fields
        /// <summary>
        /// Reference to the pan/zoom panel.  We need to pass
        /// this to the pan zoom form.
        /// </summary>
        private Panel m_panPanel;

        /// <summary>
        /// The amount by which we need to resize the vertical
        /// scrollbar to make space for our additional controls.
        /// </summary>
        private int m_scrollbarHeightAdjustment;

        /// <summary>
        /// Reference to the vertical scroll bar on 
        /// the diagram view. We need to store the
        /// reference so that we can resize it every
        /// time the view is resized.
        /// </summary>
        private VScrollBar m_vscroll; 
        #endregion

        #region Public Implementation
        /// <summary>
        /// Override the base class method so that we can
        /// add some new extra controls to the view (add
        /// controls to zoom in, zoom out, pan control, 
        /// and zoom to 100%).
        /// </summary>
        /// <returns></returns>
        public override VSDiagramView CreateDiagramView()
        {
            // We are going to add four new controls to the diagram view.
            // The controls are added underneath the vertical scrollbar.
            // We need to create and site the panels. We also need to 
            // resize the vertical scroll bar so that there is room for
            // our controls.

            // Let the base class create and initialise
            // the standard view.
            VSDiagramView view = base.CreateDiagramView();

            // Get hold of the scrollbars of the view
            m_vscroll = GetControl<VScrollBar>(view.Controls) as VScrollBar;
            HScrollBar hscroll = GetControl<HScrollBar>(view.Controls) as HScrollBar;

            // Sanity checks
            Debug.Assert(m_vscroll != null, "Error - couldn't find the vertical scroll bar");
            Debug.Assert(hscroll != null, "Error - couldn't find the horizontal scroll bar");

            // Give up if we didn't find the scroll bars
            if(m_vscroll == null || hscroll == null)
            {
                return view;
            }

            Panel zoomInPanel;
            Panel zoomOutPanel;
            Panel zoom100Panel;

            int panelHeight = hscroll.Height;
            int panelWidth = m_vscroll.Width;

            // Create the panels and set the event handlers
            m_panPanel = CreatePanel("CustomCode.PanZoom.ThumbnailView.bmp",
                view, panelHeight, panelWidth);

            m_panPanel.MouseDown += new MouseEventHandler(PanelPanelMouseDownHandler);

            zoomInPanel = CreatePanel("CustomCode.PanZoom.ZoomInButton.bmp",
                view, panelHeight, panelWidth);

            zoomInPanel.Click += new EventHandler(ZoomInPanelClickHandler);

            zoom100Panel = CreatePanel("CustomCode.PanZoom.Zoom100Button.bmp",
                view, panelHeight, panelWidth);

            zoom100Panel.Click += new EventHandler(Zoom100PanelClickHandler);

            zoomOutPanel = CreatePanel("CustomCode.PanZoom.ZoomOutButton.bmp",
                view, panelHeight, panelWidth);

            zoomOutPanel.Click += new EventHandler(ZoomOutPanelClickHandler);


            // Set the position of the panels
            // Stack the panels on top of each other
            m_panPanel.Top = view.Height - m_panPanel.Height;
            zoomOutPanel.Top = m_panPanel.Top - zoomOutPanel.Height;
            zoom100Panel.Top = zoomOutPanel.Top - zoom100Panel.Height;
            zoomInPanel.Top = zoom100Panel.Top - zoomInPanel.Height;

            // All panels have the same left position
            m_panPanel.Left = view.Width - m_panPanel.Width;
            zoomOutPanel.Left = m_panPanel.Left;
            zoom100Panel.Left = m_panPanel.Left;
            zoomInPanel.Left = m_panPanel.Left;

            // Work out and store the amount by which we need
            // to resize the vertical scroll bar.
            m_scrollbarHeightAdjustment = zoom100Panel.Height +
                zoomInPanel.Height +
                zoomOutPanel.Height;

            // Need to be able to resize the vertical scrollbar
            // every time the view is resized.
            // NB the Resize and SizeChanged events fire too 
            // early i.e. before the VSDiagramView has resized
            // the vertical scroll bar.
            view.ClientSizeChanged += new EventHandler(ClientSizeChangedHandler);

            // handle Ctrl + MButton to zoom to 100%
            if (view.DiagramClientView != null)
                view.DiagramClientView.MouseClick += new MouseEventHandler(MouseClickHandler);

            return view;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handle the click on "zoom to 100%" panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom100PanelClickHandler(object sender, EventArgs e)
        {
            this.CurrentDesigner.ZoomAtViewCenter(1);
        }

        /// <summary>
        /// Handle the click on the "zoom out" panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOutPanelClickHandler(object sender, EventArgs e)
        {
            this.CurrentDesigner.ZoomOut();
        }

        /// <summary>
        /// Handle the click on "zoom out" panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomInPanelClickHandler(object sender, EventArgs e)
        {
            this.CurrentDesigner.ZoomIn();
        }

        /// <summary>
        /// Display the pan window on the mouse down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelPanelMouseDownHandler(object sender, MouseEventArgs e)
        {
            new ThumbnailViewForm(m_panPanel, this.CurrentDesigner.DiagramClientView).ShowDialog();
        }

        /// <summary>
        /// Handle the client size change event to reset
        /// the vertical scroll bar every time the view 
        /// size changes. This is because the VSDiagramView 
        /// control will have resized it over three of 
        /// "our" zoom panels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientSizeChangedHandler(object sender, EventArgs e)
        {
            // Shorten the size of the scroll bar by the
            // heights of all the panels (except for the 
            // pan/zoom panel).
            m_vscroll.Height -= m_scrollbarHeightAdjustment;
        }

        /// <summary>
        /// By default, Ctrl + Scroll Mouse button to zoom in/out
        /// Add handler here to enable Ctrl + MButton to zoom to 100%
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && Control.ModifierKeys == Keys.Control)
            {
                this.CurrentDesigner.ZoomAtViewCenter(1);
            }
        }
        #endregion

        #region Private Implementation
        /// <summary>
        /// Create and return a new panel displaying the 
        /// image in the specified resource as a background
        /// image.
        /// </summary>
        /// <param name="imageResourceName"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        private Panel CreatePanel(string imageResourceName,
            VSDiagramView view,
            int height,
            int width
            )
        {
            // Create a new panel to draw the image on
            Panel newPanel = new Panel();

            if(!string.IsNullOrEmpty(imageResourceName))
            {
                newPanel.BackgroundImage = new Bitmap(this.GetType(), imageResourceName);
                newPanel.BackgroundImageLayout = ImageLayout.Center;
            }

            // Add the new control to the view and
            // initialise it.
            view.Controls.Add(newPanel);
            newPanel.Height = height;
            newPanel.Width = width;
            newPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            newPanel.BringToFront();

            return newPanel;
        }

        /// <summary>
        /// Tries to find a control of the specified type
        /// in the collection. Returns the first matching
        /// control of the type, or null if there are no
        /// controls of that type.
        /// </summary>
        /// <param name="controls"></param>
        private static Control GetControl<TControl>(Control.ControlCollection controls)
        {
            Control foundControl = null;

            foreach(Control item in controls)
            {
                if(item.GetType() == typeof(TControl))
                {
                    foundControl = item;
                    break;
                }
            }

            return foundControl;
        } 
        #endregion
    }
}