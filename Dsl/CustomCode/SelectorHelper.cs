using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCVisualDesigner
{
    public class Selector
    {
        private Selector()
        {
        }

        // targetWidget = sourceWidget.Selector()
        public static Selector FromWidgets(VDWidget sourceWidget, VDWidget targetWidget)
        {
            string identifierSelector = string.Empty;

            //    if (string.IsNullOrEmpty(widget.WidgetName))
            //    {
            //        // todo: if use as id identifier
            //        //return "#" + widget.WidgetName;
            //        // todo: if use as class identifier
            //        //return "." + widget.WidgetName;
            //        // todo: if use as normal class
            //    }


            //    if (widget.Parent != null)
            //    {
            //        string parentIdentifier = GetIdentifierSelector(widget.Parent);
            //        if (!string.IsNullOrEmpty(parentIdentifier))
            //        {
            //        }
            //    }

            //    // get selecctor relative to prev widget/prev of prev/ prev of prev of prev

            //    // get selector relative to next widget/next of next/next of next

            //    // get selector relative to children

            //return identifierSelector;
            return null;
        }

        public static Selector FromSelectorString(string selectorString)
        {
            return null;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public VDWidget Source { get; set; }
        public List<SelectorFunction> FunctionList { get; set; }

        public List<VDWidget> Resolve()
        {
            //if (string.IsNullOrEmpty(selector)) return relativeWidget;

            //string firstFunc = selector;
            //int idx = selector.IndexOf(".");
            //if (idx >= 0)
            //{
            //    firstFunc = selector.Substring(0, idx).Trim();
            //    selector = selector.Substring(idx);
            //}

            //VDWidget retWidget = relativeWidget;

            //if (!string.IsNullOrEmpty(firstFunc))
            //{
            //    if (firstFunc == "closest")
            //    {
            //        retWidget = null;//todo:
            //    }
            //    else if (firstFunc == "parent")
            //    {
            //    }
            //}
            //return ResolveIdentifierSelector(retWidget, selector);
            return null;
        }
    }

    public abstract class SelectorFunction
    {
        public abstract string Name { get; }
        public abstract VDWidget Resolve(VDWidget sourceWidget);

        public List<SelectorFunctionParameter> ParameterList { get; set; }
    }

    public class SelectorFunctionParameter
    {
        public SelectorFunctionParameter NextParameter { get; set; }
    }

    // :checked, :first etc
    public class FilterParameter : SelectorFunctionParameter
    {
        public string Filter { get; set; }
    }

    // <div>, <span> etc
    public class HTMLTagParameter : SelectorFunctionParameter
    {
        public string HtmlTag { get; set; }
    }

    // [Table], [Tab]
    public class WidgetTypeParameter : SelectorFunctionParameter
    {
        public WidgetType WidgetType { get; set; }
    }

    // {Guid}
    public class WidgetReferenceParameter : SelectorFunctionParameter
    {
        public VDWidget Widget { get; set; }
    }

    // .classname
    public class HTMLClassParameter : SelectorFunctionParameter
    {
        public string ClassName { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////////
    /// <summary> </summary>
    public class SelectorFunctionProviderBase
    {
        public virtual List<SelectorFunction> GetSupportedFunctionList()
        {
            return new List<SelectorFunction>();
        }

        public virtual List<SelectorFunctionParameter> GetSupportedParameterList()
        {
            return new List<SelectorFunctionParameter>();
        }
    }

    public partial class SelectorFunctionProvider : SelectorFunctionProviderBase
    {
    }
}
