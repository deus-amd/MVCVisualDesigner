using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    //////////////////////////////////////////////////////////////////////////////////
    // Factory
    public interface IWidgetTreeWalkerFactory
    {
        IWidgetTreeWalker CreateWalker(VDWidget widget);
    }

    public class LayoutWalkerFactory : IWidgetTreeWalkerFactory
    {
        public IWidgetTreeWalker CreateWalker(VDWidget rootWidget)
        {
            return new LayoutWalker(rootWidget);
        }
    }

    public class WidgetTreeWalkerFactory : IWidgetTreeWalkerFactory
    {
        public IWidgetTreeWalker CreateWalker(VDWidget rootWidget)
        {
            return new WidgetTreeWalker(rootWidget);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////
    // Walker
    public interface IWidgetTreeWalker : IEnumerable<VDWidget>
    {
    }

    public class LayoutWalker : IWidgetTreeWalker
    {
        public LayoutWalker(VDWidget rootWidget)
        {
            m_rootWidget = rootWidget;
        }

        private VDWidget m_rootWidget;
        public VDWidget RootWidget { get { return m_rootWidget; } }

        // traverse according to layout positions
        public IEnumerator<VDWidget> GetEnumerator()
        {
            List<NodeShape> childShapeList = new List<NodeShape>();
            var shapes = PresentationViewsSubject.GetPresentation(m_rootWidget);
            if (shapes != null && shapes.Count > 0)
            {
                NodeShape parentShape = shapes[0] as NodeShape;
                if (parentShape != null)
                {
                    foreach(var shape in parentShape.NestedChildShapes)
                    {
                        NodeShape childShape = shape as NodeShape;
                        if (childShape != null)
                        {
                            childShapeList.Add(childShape);
                        }
                    }
                }
            }

            // traverse according to position, row first
            childShapeList.Sort((a, b) =>
            {
                if (a.Location.Y - b.Location.Y > Utility.NumberHelper.DOUBLE_DIFFERENCE)
                    return 1;
                else if (b.Location.Y - a.Location.Y > Utility.NumberHelper.DOUBLE_DIFFERENCE)
                    return -1;
                else if (a.Location.X - b.Location.X > Utility.NumberHelper.DOUBLE_DIFFERENCE)
                    return 1;
                else if (b.Location.X - a.Location.X > Utility.NumberHelper.DOUBLE_DIFFERENCE)
                    return -1;
                else
                    return 0;
            });

            //
            List<VDWidget> childWidgets = new List<VDWidget>();
            foreach (NodeShape shape in childShapeList)
            {
                VDWidget widget = shape.ModelElement as VDWidget;
                if (widget != null) childWidgets.Add(widget);
            }

            return childWidgets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }

    public class WidgetTreeWalker : IWidgetTreeWalker
    {
        private VDWidget m_rootWidget;

        public WidgetTreeWalker(VDWidget rootWidget)
        {
            m_rootWidget = rootWidget;
        }

        public IEnumerator<VDWidget> GetEnumerator()
        {
            return m_rootWidget.Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
    }
}
