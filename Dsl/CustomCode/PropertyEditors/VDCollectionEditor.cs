using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public class VDCollectionEditor : CollectionEditor 
    {
        public VDCollectionEditor(Type type)
            : base(type)
        {
        }


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object o = base.EditValue(context, provider, value);
            List<HTMLAttribute> newList = new List<HTMLAttribute>((List<HTMLAttribute>)o);
            return newList;
        }

        //protected override CollectionForm CreateCollectionForm()
        //{
        //    return base.CreateCollectionForm();
        //}

        //protected override object CreateInstance(Type itemType)
        //{
        //    return base.CreateInstance(itemType);
        //}

        //public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        //{
        //    base.PaintValue(e);
        //}

        //protected override object SetItems(object editValue, object[] value)
        //{
        //    return base.SetItems(editValue, value);
        //}

        //protected override object[] GetItems(object editValue)
        //{
        //    return base.GetItems(editValue);
        //}
    }
}
