using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using Microsoft.VisualStudio.Modeling;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace MVCVisualDesigner
{
    public class VDDomainElementListEditor<TValue> : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null) { throw new ArgumentNullException("context"); }
            if (provider == null) { throw new ArgumentNullException("provider"); }

            IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (editorService == null) { return value; }

            ModelElement mel = context.Instance as ModelElement; // MEL or PEL
            if (mel == null) return value;

            // get partition of MEL, not PEL's
            if (mel is PresentationElement) mel = ((PresentationElement)mel).ModelElement;
            if (mel == null) return value;

            if (!(value is TValue)) return value;

            return editValue(editorService, mel, context, provider, (TValue)value);
        }

        protected virtual TValue editValue(IWindowsFormsEditorService editorService, ModelElement mel,
            ITypeDescriptorContext context, IServiceProvider provider, TValue value)
        {
            ListBoxControl<TValue> control = new ListBoxControl<TValue>(editorService, this.hasNoneListItem);
            if (!initListBoxControl(control, mel)) return value;

            // make the old value selected in the list
            string oldval = value.ToString();
            if (string.IsNullOrEmpty(oldval))
            {
                control.SelectedIndex = 0;
            }
            else
            {
                control.SelectedIndex = control.FindString(oldval);
            }

            // show list
            editorService.DropDownControl(control);

            // get new value
            TValue newval = default(TValue);
            int selectedIndex = control.SelectedIndex;
            if (selectedIndex >= 0)
            {
                newval = (TValue)((NameValue)(control.Items[selectedIndex])).Value;
            }

            return newval;
        }

        protected virtual bool hasNoneListItem { get { return false; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listControl"></param>
        /// <returns>Succeeded or not</returns>
        protected virtual bool initListBoxControl(ListBoxControl<TValue> listControl, ModelElement mel)
        {
            return false;
        }

        public override bool IsDropDownResizable { get { return true; } }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) { return UITypeEditorEditStyle.DropDown; }
    }


    public sealed class ListBoxControl<T> : ListBox
    {
        private IWindowsFormsEditorService editorService;

        internal ListBoxControl(IWindowsFormsEditorService editorService, bool hasNoneItem = false)
        {
            if (editorService == null)
            {
                throw new ArgumentNullException("editorService");
            }
            this.editorService = editorService;
            base.Click += new EventHandler(this.ListBoxControl_Click);

            if (hasNoneItem)
                this.Items.Add(new NameValue("(None)", default(T)));
        }

        internal void AddItem(string text, T value)
        {
            this.Items.Add(new NameValue(text, value));
        }

        /// <summary>
        /// event handler when this list box loses focus. We need to close down the list box.
        /// </summary>
        private void ListBoxControl_Click(object sender, EventArgs e)
        {
            this.editorService.CloseDropDown();
        }
    }

    //
    class NameValue
    {
        public NameValue(string n, object v)
        {
            Name = n;
            Value = v;
        }

        public string Name { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Name;
        }

        //public override bool Equals(object obj)
        //{
        //    NameValue nv = obj as NameValue;
        //    if (nv == null)
        //        return false;
        //    else if (Value == null && nv.Value == null)
        //        return true;
        //    else if (Value == null || nv.Value == null)
        //        return false;
        //    else
        //        return Value.Equals(nv.Value);
        //}

        //public override int GetHashCode()
        //{
        //    return Value == null ? 0 : Value.GetHashCode();
        //}
    }
}
