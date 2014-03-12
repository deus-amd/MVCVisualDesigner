using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public class Logger
    {
        private ErrorListProvider m_errorListPrivider;
        public Logger(IServiceProvider privider)
        {
            m_errorListPrivider = new ErrorListProvider(privider);
            m_errorListPrivider.ProviderName = "MVC Visual Designer";
            m_errorListPrivider.ProviderGuid = new Guid("F52CF5B6-E3C7-4E12-BCB2-C11DE38697AD");
        }

        public void LogError(string message)
        {
            ErrorTask task = new ErrorTask();
            task.ErrorCategory = TaskErrorCategory.Error;
            task.Text = message;
            task.Category = TaskCategory.Misc;
            m_errorListPrivider.Tasks.Add(task);
        }

        public void LogError(Exception ex)
        {
            ErrorTask task = new ErrorTask(ex);
            task.ErrorCategory = TaskErrorCategory.Error;
            task.Category = TaskCategory.Misc;
            m_errorListPrivider.Tasks.Add(task);
        }
    }
}
