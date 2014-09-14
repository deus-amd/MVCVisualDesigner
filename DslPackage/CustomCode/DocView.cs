using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    internal partial class MVCVisualDesignerDocView
    {
        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            ShapeElement shape = this.PrimarySelection as ShapeElement;
            if (shape != null && shape.ModelElement != null)
            {
                // set active widget for code snippet
                VDWidget widget = shape.ModelElement as VDWidget;
                if (widget != null && !(widget is VDCodeSnippet) && (widget.CodeSnippetEditor != null))
                {
                    using (Transaction trans = this.DocData.Store.TransactionManager.BeginTransaction("Set Active Linked Widget"))
                    {
                        if (widget.CodeSnippetEditor != null) widget.CodeSnippetEditor.ActiveLinkedWidget = widget;
                        trans.Commit();
                    }
                    return;
                }

                // update Model Tool Window
                ModelToolWindow win = this.ModelToolWindow;
                if (win != null)
                {
                    win.ShowNoActivate();

                    if (widget != null)
                    {
                        if (widget is VDView)
                        {
                            win.ShowViewModel((VDView)widget); 
                        }
                        else if (widget is VDActionBase)
                        {
                            win.ShowActionModel((VDActionBase)widget);
                        }
                        else
                        {
                            win.ShowWidgetModel(widget); 
                        }
                    }
                    else
                    {
                        // avoid tool window blink
                        if (shape.ModelElement is VDWidgetTitle)
                            return;

                        win.HideWindow();
                    }
                }
            }
        }

        // when a widget lost its focus, reset the ActiveLinkedWidget of its CodeSnippetEditor
        protected override void OnSelectionChanging(EventArgs e)
        {
            base.OnSelectionChanging(e);

            ShapeElement shape = this.PrimarySelection as ShapeElement;
            if (shape == null || shape.ModelElement == null) return;

            VDWidget widgetLosingSelection = shape.ModelElement as VDWidget;
            if (widgetLosingSelection == null) return;

            VDCodeSnippet codeEditor2Reset = null;

            if (widgetLosingSelection is VDCodeSnippet) // reference mode codesnippet editor is losting selection
            {
                // if new selected shape is Title of code snippet, don't reset it
                var primaryItem = this.CurrentDesigner.Selection.PrimaryItem;
                if (primaryItem != null && primaryItem.Shape != null 
                    && primaryItem.Shape is VDWidgetTitlePort
                    && primaryItem.Shape.ModelElement != null
                    && ((VDWidgetTitle)primaryItem.Shape.ModelElement).Widget == widgetLosingSelection)
                {
                        // ...
                }
                else if (((VDCodeSnippet)widgetLosingSelection)._Mode.HasFlag(E_CodeSnippetMode.Reference))
                {
                    codeEditor2Reset = widgetLosingSelection as VDCodeSnippet;
                }
            }
            else if (widgetLosingSelection.CodeSnippetEditor != null)
            {
                VDCodeSnippet newSelectedCodeSnippet = null;

                // check if the new selected element is EVAACodeSnippet or EVAARawSnippet
                if (this.CurrentDesigner.Selection != null )
                {
                    var primaryItem = this.CurrentDesigner.Selection.PrimaryItem;
                    if (primaryItem != null && primaryItem.Shape != null)
                    {
                        if (primaryItem.Shape is VDCodeSnippetShape)
                        {
                            newSelectedCodeSnippet = primaryItem.Shape.ModelElement as VDCodeSnippet;
                        }
                        else if (primaryItem.Shape is VDWidgetTitlePort) // new selection is the Title of code snippet widget
                        {
                            VDWidgetTitle title = primaryItem.Shape.ModelElement as VDWidgetTitle;
                            if (title != null && title.Widget is VDCodeSnippet)
                                newSelectedCodeSnippet = title.Widget as VDCodeSnippet;
                        }
                    }
                }

                // new selection is the code editor of previously selected widget, 
                // in this case, don't reset the ActiveLinkedWidget of the code viewer
                if (newSelectedCodeSnippet == null || widgetLosingSelection.CodeSnippetEditor != newSelectedCodeSnippet)
                    codeEditor2Reset = widgetLosingSelection.CodeSnippetEditor;
            }


            if (codeEditor2Reset != null)
            {
                using (Transaction trans = this.DocData.Store.TransactionManager.BeginTransaction("Clear Active Linked Widget"))
                {
                    codeEditor2Reset.ActiveLinkedWidget = null;
                    trans.Commit();
                }
            }
        }


        protected ModelToolWindow ModelToolWindow
        {
            get
            {
                ModelToolWindow window = null;
                MVCVisualDesignerPackage package = this.GetService(typeof(MVCVisualDesignerPackage)) as MVCVisualDesignerPackage;
                if (package != null)
                {
                    window = package.GetToolWindow(typeof(ModelToolWindow), true) as ModelToolWindow;
                }

                return window;
            }
        }
    }
}
