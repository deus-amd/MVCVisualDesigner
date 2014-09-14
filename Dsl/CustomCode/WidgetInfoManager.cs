using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    //todo: init this class by using option data
    public class WidgetInfoManager
    {
        public List<WidgetClientMember> GetWidgetClientMemberList(VDWidget widget)
        {
            return new List<WidgetClientMember>();
        }

        /// <summary> Get client event list supported by specified source widget </summary>
        /// <param name="sourceWidget"></param>
        /// <returns></returns>
        public List<string> GetClientEventList(VDWidget sourceWidget)
        {
            return new List<string>() 
            { 
                "click",
                "change",
                "onSuccess",
            };
        }

        /// <summary> Get client action list supported by specified target widget </summary>
        /// <param name="targetWidget"></param>
        /// <returns></returns>
        public List<string> GetClientActionList(VDWidget targetWidget)
        {
            return new List<string>() 
            { 
                "Delete",
                "Replace",
                "AjaxGet",
                "AjaxPost"
            };
        }

        /// <summary>
        /// for some actions, maybe there are some predefined model members
        /// </summary>
        /// <param name="sourceWidget"></param>
        /// <param name="targetWidget"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<ActionModelMemberInfo> GetActionModelCandidateMemberList(VDWidget sourceWidget, VDWidget targetWidget, VDClientAction action)
        {
            return new List<ActionModelMemberInfo>();
        }
    }


    public class WidgetClientMember
    {
        public string Name { get; set; }
    }

    public class WidgetClientProperty : WidgetClientMember
    {
    }

    public class WidgetClientMethod : WidgetClientMember
    {
    }

    ////////////////////////////////////////////////////////////////////////////////
    public class ActionModelMemberInfo
    {
        public string Name { get; set; }
        public string Type { get; set; } //todo: Foreign key?
        public bool Mandatory { get; set; }
    }

}
