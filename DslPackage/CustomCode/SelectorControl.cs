using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCVisualDesigner
{
    public partial class SelectorControl : UserControl
    {
        public SelectorControl()
        {
            InitializeComponent();
        }

        private VDWidget m_anchorWidget;
        private string m_selector;
        private string m_resolvedWidget;

        public void Init(VDWidget anchorWidget, string selector, string resolvedWidget)
        {
            m_anchorWidget = anchorWidget;
            m_selector = selector;
            m_resolvedWidget = resolvedWidget;

            if (anchorWidget != null)
            {
                this.cmbAnchorWidget.Text = anchorWidget.WidgetName + "[" + anchorWidget.WidgetType.ToString() + "]";
            }
            this.txtSelectorFunction.Text = selector;
            this.lblResolvedWidget.Text = resolvedWidget;
        }

        public VDWidget AnchorWidget { get { return m_anchorWidget; } }
        public string Selector { get { return m_selector; } }
        public string ResolvedWidget { get { return m_resolvedWidget; } }
    }
}
