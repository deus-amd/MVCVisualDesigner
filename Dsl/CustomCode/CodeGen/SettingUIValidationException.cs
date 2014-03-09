using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    [Serializable]
    public class SettingUIValidationException : Exception
    {
        public SettingUIValidationException() { }
        public SettingUIValidationException(string message) : base(message) { }
        public SettingUIValidationException(string message, Exception inner) : base(message, inner) { }
        protected SettingUIValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
