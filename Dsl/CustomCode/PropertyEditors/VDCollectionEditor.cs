using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            List<HTMLAttribute> oldList = HTMLAttribute.CloneHTMLAttributeList((List<HTMLAttribute>)value);
            List<HTMLAttribute> newList = base.EditValue(context, provider, value) as List<HTMLAttribute>;

            // remove empty attributes
            if (newList != null)
            {
                newList.RemoveAll(attr => string.IsNullOrEmpty(attr.AttributeName));
            }

            if (HTMLAttribute.AreAttributeListsEqual(oldList, newList))
            {
                // value are not changed
                return newList;
            }
            else
            {
                // value changed. turn on document dirty flag
                return new List<HTMLAttribute>(newList);
            }
        }

        protected override CollectionForm CreateCollectionForm()
        {
            var form = base.CreateCollectionForm();

            Button orgOKBtn = form.AcceptButton as Button;
            if (orgOKBtn == null) return form;

            // hide the original OK button
            orgOKBtn.AutoSize = false;
            orgOKBtn.Width = 0;
            orgOKBtn.Height = 0;
            orgOKBtn.Visible = false;

            // create a new OK button
            Button newOKBtn = new Button() { Text = "OK" };
            orgOKBtn.Parent.Controls.Add(newOKBtn);
            form.AcceptButton = newOKBtn;

            newOKBtn.Click += new EventHandler((obj, arg) => {
                // validate value
                List<HTMLAttribute> newAttrList = form.EditValue as List<HTMLAttribute>;
                string errMsg = HTMLAttribute.ValidateAttributes(newAttrList);
                if (errMsg != null)
                {
                    //// show message in ErrorList window
                    //var errProvider = new Microsoft.VisualStudio.Shell.ErrorListProvider(provider);
                    //errProvider.ProviderName = "test";
                    //errProvider.ProviderGuid = new Guid();
                    //errProvider.Tasks.Add(new Microsoft.VisualStudio.Shell.Task(ex));

                    MessageBox.Show(form, errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // perform original onclick handling
                // must make original ok button visible, otherwise PerformClick() doesn't work
                orgOKBtn.Visible = true;
                orgOKBtn.PerformClick(); 
            });
            return form;
        }
    }
}
