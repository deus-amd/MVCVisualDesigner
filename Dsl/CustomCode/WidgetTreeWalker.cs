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
        private const double DIFFERENCE = 0.01; // todo: global utility??

        public LayoutWalker(VDWidget rootWidget)
        {
            m_rootWidget = rootWidget;
        }

        private VDWidget m_rootWidget;
        public VDWidget RootWidget { get { return m_rootWidget; } }

        public IEnumerator<VDWidget> GetEnumerator()
        {
            throw new NotImplementedException();
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

    //public delegate bool CodeGeneratorVisitor(ICodeGenerator codeGenerator);

    //public interface ICodeGenerationWalker
    //{
    //    // openVisitor: called before child visitors
    //    // closeVisitor: called after child visitors
    //    bool DoTraverse(ICodeGenerator codeGenerator, CodeGeneratorVisitor openVisitor, CodeGeneratorVisitor closeVisitor);
    //}

    //public class CodeGenerationTreeWalker : ICodeGenerationWalker
    //{
    //    public bool DoTraverse(ICodeGenerator codeGenerator, CodeGeneratorVisitor openVisitor, CodeGeneratorVisitor closeVisitor)
    //    {
    //        return walkGenerator(codeGenerator, openVisitor, closeVisitor);
    //    }

    //    private bool walkGenerator(ICodeGenerator codeGenerator, CodeGeneratorVisitor openVisitor, CodeGeneratorVisitor closeVisitor)
    //    {
    //        if (codeGenerator != null)
    //        {
    //            if (openVisitor(codeGenerator))
    //            {
    //                foreach (ICodeGenerator child in codeGenerator.ChildGenerators)
    //                {
    //                    if (!walkGenerator(child, openVisitor, closeVisitor)) return false;
    //                }
    //            }

    //            if (!closeVisitor(codeGenerator)) return false;
    //        }
    //        return true;
    //    }
    //}

    //public class CodeGenerationLayoutWalker : ICodeGenerationWalker
    //{

    //    public bool DoTraverse(ICodeGenerator codeGenerator, CodeGeneratorVisitor openVisitor, CodeGeneratorVisitor closeVisitor)
    //    {
    //        return walkGenerator(codeGenerator, openVisitor, closeVisitor);
    //    }

    //    // todo: traverse according to layout positions
    //    private bool walkGenerator(ICodeGenerator codeGenerator, CodeGeneratorVisitor openVisitor, CodeGeneratorVisitor closeVisitor)
    //    {
    //        if (codeGenerator != null)
    //        {
    //            if (openVisitor(codeGenerator))
    //            {
    //                IEnumerable childGenerators = codeGenerator.ChildGenerators;
    //                if (childGenerators != null)
    //                {
    //                    List<KeyValuePair<PointD, ICodeGenerator>> children =
    //                        new List<KeyValuePair<PointD, ICodeGenerator>>();
    //                    foreach (ICodeGenerator child in childGenerators)
    //                    {
    //                        children.Add(new KeyValuePair<PointD, ICodeGenerator>(child.Location, child));
    //                    }

    //                    // traverse according to position, row first
    //                    children.Sort((a, b) =>
    //                    {
    //                        if (a.Key.Y - b.Key.Y > DIFFERENCE)
    //                            return 1;
    //                        else if (b.Key.Y - a.Key.Y > DIFFERENCE)
    //                            return -1;
    //                        else if (a.Key.X - b.Key.X > DIFFERENCE)
    //                            return 1;
    //                        else if (b.Key.X - a.Key.X > DIFFERENCE)
    //                            return -1;
    //                        else
    //                            return 0;
    //                    });
    //                    foreach (var child in children)
    //                    {
    //                        if (!walkGenerator(child.Value, openVisitor, closeVisitor))
    //                            return false;
    //                    }
    //                }
    //            }

    //            if (!closeVisitor(codeGenerator)) return false;

    //        }
    //        return true;
    //    }
    //}
}
