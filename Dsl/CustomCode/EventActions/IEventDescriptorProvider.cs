using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.EventActions
{
    public interface IEventDescriptorProvider
    {
        List<IEventDescriptor> GetSupportedEvents(VDWidget eventSource);
    }
}
