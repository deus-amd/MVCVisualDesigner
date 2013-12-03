using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;

namespace MVCVisualDesigner
{
    public class PinButtonField : ToggleButtonField
    {
        private Image _pinImage;
        private Image _unpinImage;
        public PinButtonField(string fieldName)
            : base(fieldName)
        {
            _pinImage = MVCVisualDesigner.Resources.MVDResources.pin;
            _unpinImage = MVCVisualDesigner.Resources.MVDResources.unpin;

            this.ToggleButtonStateChanging += OnToggleButtonStateChanging;
        }

        void OnToggleButtonStateChanging(object sender, ToggleButtonEventArgs e)
        {
            VDWidgetTitlePort title = e.ParentShape as VDWidgetTitlePort;
            if (title != null && title.ParentShape != null && title.ParentShape is VDWidgetShape)
            {
                VDWidgetShape widget = title.ParentShape as VDWidgetShape;
                using (Transaction transaction = title.Store.TransactionManager.BeginTransaction("Pin/Unpin"))
                {
                    widget.isPinned = !widget.isPinned;
                    transaction.Commit();
                }

                //
                DiagramClientView clientView = title.Diagram.ActiveDiagramView.DiagramClientView;
                widget.SelectUnpinedParentShape(new DiagramItem(), clientView);
            }
        }

        /// <summary>
        /// Provides the image for the current state of the button
        /// </summary>
        /// <param name="parentShape"></param>
        /// <returns></returns>
        protected override Image GetButtonImage(ShapeElement parentShape)
        {
            if (!(parentShape is VDWidgetTitlePort)) return null;

            Image image = _unpinImage;
            object isPined = this.GetValue(parentShape);
            if (isPined != null)
            {
                image = ((bool)isPined) ? _pinImage : _unpinImage;
            }
            return image;
        }

        /// <summary>
        /// Retrieves a mouse action that should be made active on the next
        /// MouseDown event if the mouse is over the specified point.
        /// </summary>
        /// <param name="mouseButtons">The current mouse button state.</param>
        /// <param name="point">The mouse position relative to the diagram's top-left in world units.</param>
        /// <param name="hitTestInfo">Information detailing the results of the hit testing.</param>
        /// <returns>A mouse action that should be made active on the next MouseDown event if the mouse is over the specified point.</returns>
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
