using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDViewComponent
    {
        public virtual bool IsEventSource { get { return true; } }
        public virtual bool IsEventTarget { get { return true; } }

        protected List<string> m_supportedEvents = new List<string>() 
        { 
            "Click",
            "Change",
            
            "OnSuccess",
            "OnFail",
        };
        public virtual List<string> SupportedEvents { get { return m_supportedEvents; } }
    }

    public partial class VDEventSource
    {
    }

    public partial class VDEventTarget
    {
    }

    public partial class PerformsActionOnBuilder
    {
        private static ElementLink ConnectSourceToTarget(ModelElement source, ModelElement target)
        {
            VDEventSource eventSource = null;
            VDEventTarget eventTarget = null;

            if (source is VDViewComponent)
            {
                if (target is VDViewComponent)
                {
                    VDViewComponent sourceAccepted = (VDViewComponent)source;
                    eventSource = new VDEventSource(sourceAccepted.Partition);
                    sourceAccepted.EventSources.Add(eventSource);

                    VDViewComponent targetAccepted = (VDViewComponent)target;
                    eventTarget = new VDEventTarget(targetAccepted.Partition);
                    targetAccepted.EventTargets.Add(eventTarget);
 
                }
                else if (target is VDEventTarget)
                {
                    VDViewComponent sourceAccepted = (VDViewComponent)source;
                    eventSource = new VDEventSource(sourceAccepted.Partition);
                    sourceAccepted.EventSources.Add(eventSource);

                    eventTarget = (VDEventTarget)target;
                }
            }
            else if (source is VDEventSource)
            {
                if (target is VDViewComponent)
                {
                    eventSource = (VDEventSource)source;

                    VDViewComponent targetAccepted = (VDViewComponent)target;
                    eventTarget = new VDEventTarget(targetAccepted.Partition);
                    targetAccepted.EventTargets.Add(eventTarget);
                }
                else if (target is VDEventTarget)
                {
                    eventSource = (VDEventSource)source;
                    eventTarget = (VDEventTarget)target;
                }
            }

            if (eventSource != null && eventTarget != null 
                && eventSource.Widget != null && eventTarget.Widget != null)
            {
                EventSettingDialog dlg = new EventSettingDialog();
                dlg.SetEventList(eventSource.Widget.SupportedEvents);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PerformsActionOn action = new PerformsActionOn(eventSource, eventTarget);
                    action.ActionName = dlg.Action;
                    eventSource.EventName = dlg.Event;
                    if (DomainClassInfo.HasNameProperty(action))
                    {
                        DomainClassInfo.SetUniqueName(action);
                    }
                    return action;   
                }
                else
                {
                    return null;
                }
            }

            System.Diagnostics.Debug.Fail("Having agreed that the connection can be accepted we should never fail to make one.");
            throw new System.InvalidOperationException();
        }

        private static bool CanAcceptVDViewComponentAsSource(VDViewComponent candidate)
        {
            return candidate.IsEventSource;
        }

        private static bool CanAcceptVDViewComponentAsTarget(VDViewComponent candidate)
        {
            return candidate.IsEventTarget;
        }

        private static bool CanAcceptVDViewComponentAndVDViewComponentAsSourceAndTarget(
            VDViewComponent sourceVDViewComponent, VDViewComponent targetVDViewComponent)
        {
            return true;
        }

        private static bool CanAcceptVDViewComponentAndVDEventTargetAsSourceAndTarget(
            VDViewComponent sourceVDViewComponent, VDEventTarget targetVDEventTarget)
        {
            return true;
        }

        private static bool CanAcceptVDEventSourceAndVDViewComponentAsSourceAndTarget(
            VDEventSource sourceVDEventSource, VDViewComponent targetVDViewComponent)
        {
            return true;
        }
    }
}
