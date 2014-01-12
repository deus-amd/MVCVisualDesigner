using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDInternalUtility
    {
        private VDWidget m_parentToDel;
        protected override void OnDeleting()
        {
            m_parentToDel = this.Parent;
            base.OnDeleting();
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            if (m_parentToDel != null && this.Store.TransactionManager.InTransaction)
            {
                m_parentToDel.Delete();
                m_parentToDel = null;
            }
        }
    }

    public static class VDConstants
    {
        public const double DOUBLE_DIFF = 0.0001;
    }
}
