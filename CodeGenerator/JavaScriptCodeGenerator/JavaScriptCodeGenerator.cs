using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator
{
    public class JavaScriptCodeGeneratorFactoryBase : IJavaScriptCodeGeneratorFactory
    {
        protected Dictionary<WidgetType, IJavaScriptCodeGenerator> m_generators = new Dictionary<WidgetType, IJavaScriptCodeGenerator>() 
        {
            { WidgetType.View, new VDViewJavaScriptCodeGenerator() },

            { WidgetType.PartialView, new VDPartialViewJavaScriptCodeGenerator() },

            { WidgetType.Section, new VDSectionJavaScriptCodeGenerator() },

            { WidgetType.Form, new VDFormJavaScriptCodeGenerator() },

            { WidgetType.Radio, new VDRadioJavaScriptCodeGenerator() },

            { WidgetType.CheckBox, new VDCheckBoxJavaScriptCodeGenerator() },

            { WidgetType.Submit, new VDSubmitJavaScriptCodeGenerator() },

            { WidgetType.Input, new VDInputJavaScriptCodeGenerator() },

            { WidgetType.Select, new VDSelectJavaScriptCodeGenerator() },

            { WidgetType.Tab, new VDTabJavaScriptCodeGenerator() },

            { WidgetType.TabHead, new VDTabHeadJavaScriptCodeGenerator() },

            { WidgetType.TabBody, new VDTabBodyJavaScriptCodeGenerator() },

            { WidgetType.Table, new VDTableJavaScriptCodeGenerator() },

            { WidgetType.TableRowWrapper, new VDTableRowWrapperJavaScriptCodeGenerator() },

            { WidgetType.TableRow, new VDTableRowJavaScriptCodeGenerator() },

            { WidgetType.TableCell, new VDTableCellJavaScriptCodeGenerator() },

            { WidgetType.HTMLTag, new VDHTMLTagJavaScriptCodeGenerator() },

            { WidgetType.CodeSnippet, new VDCodeSnippetJavaScriptCodeGenerator() },

            { WidgetType.Text, new VDTextJavaScriptCodeGenerator() },

            { WidgetType.Icon, new VDIconJavaScriptCodeGenerator() },

            { WidgetType.Dialog, new VDDialogJavaScriptCodeGenerator() },

            { WidgetType.MessagePanel, new VDMessagePanelJavaScriptCodeGenerator() },

            { WidgetType.Condition, new VDConditionJavaScriptCodeGenerator() },

            { WidgetType.HoriContainer, new VDHoriContainerJavaScriptCodeGenerator() },

            { WidgetType.VertContainer, new VDVertContainerJavaScriptCodeGenerator() },

            { WidgetType.FullFilledContainer, new VDFullFilledContainerJavaScriptCodeGenerator() },

            { WidgetType.SectionHead, new VDSectionHeadJavaScriptCodeGenerator() },

            { WidgetType.SectionBody, new VDSectionBodyJavaScriptCodeGenerator() },

            { WidgetType.SelectOption, new VDSelectOptionJavaScriptCodeGenerator() },

            { WidgetType.CodeSnippetBody, new VDCodeSnippetBodyJavaScriptCodeGenerator() },

        };

        public virtual IJavaScriptCodeGenerator GetCodeGenerator(VDWidget widget)
        {
            if (m_generators.ContainsKey(widget.WidgetType))
            {
                return m_generators[widget.WidgetType];
            }
            throw new NotImplementedException();
        }
    }

    public partial class JavaScriptCodeGeneratorFactory : JavaScriptCodeGeneratorFactoryBase
    {
    }
}


namespace MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator
{
    public class VDViewJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDViewJavaScriptCodeGeneratorImpl m_impl = new VDViewJavaScriptCodeGeneratorImpl();

        public VDViewJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDViewJavaScriptCodeGenerator : VDViewJavaScriptCodeGeneratorBase
    {
        public VDViewJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDPartialViewJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDPartialViewJavaScriptCodeGeneratorImpl m_impl = new VDPartialViewJavaScriptCodeGeneratorImpl();

        public VDPartialViewJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDPartialViewJavaScriptCodeGenerator : VDPartialViewJavaScriptCodeGeneratorBase
    {
        public VDPartialViewJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSectionJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSectionJavaScriptCodeGeneratorImpl m_impl = new VDSectionJavaScriptCodeGeneratorImpl();

        public VDSectionJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSectionJavaScriptCodeGenerator : VDSectionJavaScriptCodeGeneratorBase
    {
        public VDSectionJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDFormJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDFormJavaScriptCodeGeneratorImpl m_impl = new VDFormJavaScriptCodeGeneratorImpl();

        public VDFormJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDFormJavaScriptCodeGenerator : VDFormJavaScriptCodeGeneratorBase
    {
        public VDFormJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDRadioJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDRadioJavaScriptCodeGeneratorImpl m_impl = new VDRadioJavaScriptCodeGeneratorImpl();

        public VDRadioJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDRadioJavaScriptCodeGenerator : VDRadioJavaScriptCodeGeneratorBase
    {
        public VDRadioJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDCheckBoxJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDCheckBoxJavaScriptCodeGeneratorImpl m_impl = new VDCheckBoxJavaScriptCodeGeneratorImpl();

        public VDCheckBoxJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDCheckBoxJavaScriptCodeGenerator : VDCheckBoxJavaScriptCodeGeneratorBase
    {
        public VDCheckBoxJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSubmitJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSubmitJavaScriptCodeGeneratorImpl m_impl = new VDSubmitJavaScriptCodeGeneratorImpl();

        public VDSubmitJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSubmitJavaScriptCodeGenerator : VDSubmitJavaScriptCodeGeneratorBase
    {
        public VDSubmitJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDInputJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDInputJavaScriptCodeGeneratorImpl m_impl = new VDInputJavaScriptCodeGeneratorImpl();

        public VDInputJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDInputJavaScriptCodeGenerator : VDInputJavaScriptCodeGeneratorBase
    {
        public VDInputJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSelectJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSelectJavaScriptCodeGeneratorImpl m_impl = new VDSelectJavaScriptCodeGeneratorImpl();

        public VDSelectJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSelectJavaScriptCodeGenerator : VDSelectJavaScriptCodeGeneratorBase
    {
        public VDSelectJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTabJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDTabJavaScriptCodeGeneratorImpl m_impl = new VDTabJavaScriptCodeGeneratorImpl();

        public VDTabJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTabJavaScriptCodeGenerator : VDTabJavaScriptCodeGeneratorBase
    {
        public VDTabJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTabHeadJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDTabHeadJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTabHeadJavaScriptCodeGenerator : VDTabHeadJavaScriptCodeGeneratorBase
    {
        public VDTabHeadJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTabBodyJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDTabBodyJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTabBodyJavaScriptCodeGenerator : VDTabBodyJavaScriptCodeGeneratorBase
    {
        public VDTabBodyJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTableJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDTableJavaScriptCodeGeneratorImpl m_impl = new VDTableJavaScriptCodeGeneratorImpl();

        public VDTableJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTableJavaScriptCodeGenerator : VDTableJavaScriptCodeGeneratorBase
    {
        public VDTableJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTableRowWrapperJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDTableRowWrapperJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTableRowWrapperJavaScriptCodeGenerator : VDTableRowWrapperJavaScriptCodeGeneratorBase
    {
        public VDTableRowWrapperJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTableRowJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDTableRowJavaScriptCodeGeneratorImpl m_impl = new VDTableRowJavaScriptCodeGeneratorImpl();

        public VDTableRowJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTableRowJavaScriptCodeGenerator : VDTableRowJavaScriptCodeGeneratorBase
    {
        public VDTableRowJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTableCellJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDTableCellJavaScriptCodeGeneratorImpl m_impl = new VDTableCellJavaScriptCodeGeneratorImpl();

        public VDTableCellJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTableCellJavaScriptCodeGenerator : VDTableCellJavaScriptCodeGeneratorBase
    {
        public VDTableCellJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDHTMLTagJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDHTMLTagJavaScriptCodeGeneratorImpl m_impl = new VDHTMLTagJavaScriptCodeGeneratorImpl();

        public VDHTMLTagJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDHTMLTagJavaScriptCodeGenerator : VDHTMLTagJavaScriptCodeGeneratorBase
    {
        public VDHTMLTagJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDCodeSnippetJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDCodeSnippetJavaScriptCodeGeneratorImpl m_impl = new VDCodeSnippetJavaScriptCodeGeneratorImpl();

        public VDCodeSnippetJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDCodeSnippetJavaScriptCodeGenerator : VDCodeSnippetJavaScriptCodeGeneratorBase
    {
        public VDCodeSnippetJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDTextJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDTextJavaScriptCodeGeneratorImpl m_impl = new VDTextJavaScriptCodeGeneratorImpl();

        public VDTextJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDTextJavaScriptCodeGenerator : VDTextJavaScriptCodeGeneratorBase
    {
        public VDTextJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDIconJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDIconJavaScriptCodeGeneratorImpl m_impl = new VDIconJavaScriptCodeGeneratorImpl();

        public VDIconJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDIconJavaScriptCodeGenerator : VDIconJavaScriptCodeGeneratorBase
    {
        public VDIconJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDDialogJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDDialogJavaScriptCodeGeneratorImpl m_impl = new VDDialogJavaScriptCodeGeneratorImpl();

        public VDDialogJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDDialogJavaScriptCodeGenerator : VDDialogJavaScriptCodeGeneratorBase
    {
        public VDDialogJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDMessagePanelJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDMessagePanelJavaScriptCodeGeneratorImpl m_impl = new VDMessagePanelJavaScriptCodeGeneratorImpl();

        public VDMessagePanelJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDMessagePanelJavaScriptCodeGenerator : VDMessagePanelJavaScriptCodeGeneratorBase
    {
        public VDMessagePanelJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDConditionJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDConditionJavaScriptCodeGeneratorImpl m_impl = new VDConditionJavaScriptCodeGeneratorImpl();

        public VDConditionJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDConditionJavaScriptCodeGenerator : VDConditionJavaScriptCodeGeneratorBase
    {
        public VDConditionJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDHoriContainerJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDHoriContainerJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDHoriContainerJavaScriptCodeGenerator : VDHoriContainerJavaScriptCodeGeneratorBase
    {
        public VDHoriContainerJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDVertContainerJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDVertContainerJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDVertContainerJavaScriptCodeGenerator : VDVertContainerJavaScriptCodeGeneratorBase
    {
        public VDVertContainerJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDFullFilledContainerJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDFullFilledContainerJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDFullFilledContainerJavaScriptCodeGenerator : VDFullFilledContainerJavaScriptCodeGeneratorBase
    {
        public VDFullFilledContainerJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSectionHeadJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSectionHeadJavaScriptCodeGeneratorImpl m_impl = new VDSectionHeadJavaScriptCodeGeneratorImpl();

        public VDSectionHeadJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSectionHeadJavaScriptCodeGenerator : VDSectionHeadJavaScriptCodeGeneratorBase
    {
        public VDSectionHeadJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSectionBodyJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSectionBodyJavaScriptCodeGeneratorImpl m_impl = new VDSectionBodyJavaScriptCodeGeneratorImpl();

        public VDSectionBodyJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSectionBodyJavaScriptCodeGenerator : VDSectionBodyJavaScriptCodeGeneratorBase
    {
        public VDSectionBodyJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDSelectOptionJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected VDSelectOptionJavaScriptCodeGeneratorImpl m_impl = new VDSelectOptionJavaScriptCodeGeneratorImpl();

        public VDSelectOptionJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDSelectOptionJavaScriptCodeGenerator : VDSelectOptionJavaScriptCodeGeneratorBase
    {
        public VDSelectOptionJavaScriptCodeGenerator() 
        {
        }
    }


    public class VDCodeSnippetBodyJavaScriptCodeGeneratorBase : IJavaScriptCodeGenerator
    {
        protected DefaultJavaScriptCodeGeneratorImpl m_impl = new DefaultJavaScriptCodeGeneratorImpl();

        public VDCodeSnippetBodyJavaScriptCodeGeneratorBase()
        {
        }

        public virtual string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
            return m_impl.GenerateWidgetInitCode(eventSource, jsGenFactory);
        }

         public virtual string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
         {
            return m_impl.GenerateActionCode(eventSource, eventTarget, action);
         }

         public virtual string GenerateGettingWidgetValueCode(VDWidget widget)
         {
            return m_impl.GenerateGettingWidgetValueCode(widget);
         }
    }

    public partial class VDCodeSnippetBodyJavaScriptCodeGenerator : VDCodeSnippetBodyJavaScriptCodeGeneratorBase
    {
        public VDCodeSnippetBodyJavaScriptCodeGenerator() 
        {
        }
    }


}

namespace MVCVisualDesigner.CodeGenerator.JavaScriptCodeGenerator
{
    public partial class VDViewJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDViewJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDView)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDView)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDView)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDPartialViewJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDPartialViewJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDPartialView)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDPartialView)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDPartialView)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSectionJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSectionJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSection)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSection)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSection)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDFormJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDFormJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDForm)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDForm)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDForm)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDRadioJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDRadioJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDRadio)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDRadio)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDRadio)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDCheckBoxJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDCheckBoxJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDCheckBox)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDCheckBox)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDCheckBox)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSubmitJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSubmitJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSubmit)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSubmit)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSubmit)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDInputJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDInputJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDInput)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDInput)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDInput)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSelectJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSelectJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSelect)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSelect)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSelect)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTabJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTabJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTab)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTab)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTab)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTabHeadJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTabHeadJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTabHead)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTabHead)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTabHead)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTabBodyJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTabBodyJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTabBody)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTabBody)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTabBody)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTableJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTableJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTable)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTable)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTable)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTableRowWrapperJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTableRowWrapperJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTableRowWrapper)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTableRowWrapper)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTableRowWrapper)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTableRowJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTableRowJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTableRow)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTableRow)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTableRow)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTableCellJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTableCellJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDTableCell)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDTableCell)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDTableCell)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDHTMLTagJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDHTMLTagJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDHTMLTag)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDHTMLTag)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDHTMLTag)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDCodeSnippetJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDCodeSnippetJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDCodeSnippet)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDCodeSnippet)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDCodeSnippet)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDTextJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDTextJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDText)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDText)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDText)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDIconJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDIconJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDIcon)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDIcon)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDIcon)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDDialogJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDDialogJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDDialog)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDDialog)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDDialog)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDMessagePanelJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDMessagePanelJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDMessagePanel)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDMessagePanel)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDMessagePanel)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDConditionJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDConditionJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDCondition)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDCondition)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDCondition)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDHoriContainerJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDHoriContainerJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDHoriContainer)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDHoriContainer)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDHoriContainer)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDVertContainerJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDVertContainerJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDVertContainer)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDVertContainer)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDVertContainer)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDFullFilledContainerJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDFullFilledContainerJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDFullFilledContainer)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDFullFilledContainer)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDFullFilledContainer)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSectionHeadJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSectionHeadJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSectionHead)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSectionHead)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSectionHead)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSectionBodyJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSectionBodyJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSectionBody)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSectionBody)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSectionBody)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDSelectOptionJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDSelectOptionJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDSelectOption)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDSelectOption)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDSelectOption)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
    public partial class VDCodeSnippetBodyJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public VDCodeSnippetBodyJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl((VDCodeSnippetBody)eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, (VDCodeSnippetBody)eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl((VDCodeSnippetBody)widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }

    public partial class DefaultJavaScriptCodeGeneratorImpl : IJavaScriptCodeGenerator
    {
        public DefaultJavaScriptCodeGeneratorImpl()
        {
        }

		public string GenerateWidgetInitCode(VDWidget eventSource, IJavaScriptCodeGeneratorFactory jsGenFactory)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateWidgetInitCodeImpl(eventSource, jsGenFactory);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateActionCode(VDWidget eventSource, VDWidget eventTarget, VDClientAction action)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateActionCodeImpl(eventSource, eventTarget, action);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }

        public string GenerateGettingWidgetValueCode(VDWidget widget)
        {
			//StringBuilder sbOld = this.GenerationEnvironment;
			//this.GenerationEnvironment = new StringBuilder();
            //generateGettingWidgetValueCodeImpl(widget);
			//string code = this.GenerationEnvironment.ToString();
			//this.GenerationEnvironment = sbOld;
			//return code;
			return string.Empty;
        }
    }
}

