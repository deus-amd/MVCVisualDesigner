using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Model
namespace MVCVisualDesigner
{
    public partial class VDForm
    {
        public override bool HasWidgetTitle { get { return true; } }

        // properties
        public string GetNameValue() { return getHTMLAttr<string>("name", string.Empty); }
        public void SetNameValue(string newValue) { setHTMLAttr<string>("name", newValue); }

        public string GetValueValue() { return getHTMLAttr<string>("value", string.Empty); }
        public void SetValueValue(string newValue) { setHTMLAttr<string>("value", newValue); }

        public string GetActionValue() { return getHTMLAttr<string>("action", string.Empty); }
        public void SetActionValue(string newValue) { setHTMLAttr<string>("action", newValue); }

        public E_HTTPRequestMethod GetMethodValue() { return getHTMLAttr<E_HTTPRequestMethod>("method", E_HTTPRequestMethod.Get); }
        public void SetMethodValue(E_HTTPRequestMethod newValue) { setHTMLAttr<E_HTTPRequestMethod>("method", newValue); }

        public E_FormTarget GetTargetValue() { return getHTMLAttr<E_FormTarget>("target", E_FormTarget.NotSpecified); }
        public void SetTargetValue(E_FormTarget newValue) { setHTMLAttr<E_FormTarget>("target", newValue); }

        public E_FormEncType GetEncTypeValue() { return getHTMLAttr<E_FormEncType>("enctype", E_FormEncType.NotSpecified); }
        public void SetEncTypeValue(E_FormEncType newValue) { setHTMLAttr<E_FormEncType>("enctype", newValue); }
        
    }

    public partial class VDRadio
    {
        public override bool HasWidgetTitle { get { return true; } }
        protected override E_InputType getTypeAttr() { return E_InputType.radio; }
        protected override void setTypeAttr(E_InputType newValue) { } 
    }

    public partial class VDCheckBox
    {
        public override bool HasWidgetTitle { get { return true; } }
        protected override E_InputType getTypeAttr() { return E_InputType.checkbox; }
        protected override void setTypeAttr(E_InputType newValue) { }
    }

    public partial class VDSubmit
    {
        public override bool HasWidgetTitle { get { return true; } }
        protected override E_InputType getTypeAttr() { return E_InputType.submit; }
        protected override void setTypeAttr(E_InputType newValue) { } 
    }

    public partial class VDInput
    {
        public override bool HasWidgetTitle { get { return true; } }

        // properties
        public string GetNameValue() { return getHTMLAttr<string>("name", string.Empty); }
        public void SetNameValue(string newValue) { setHTMLAttr<string>("name", newValue); }

        public E_InputType GetTypeValue() { return getTypeAttr(); }
        public void SetTypeValue(E_InputType newValue) { setTypeAttr(newValue); }
        virtual protected E_InputType getTypeAttr()
        {
            return getHTMLAttr<E_InputType>("type", E_InputType.text); 
        }
        virtual protected void setTypeAttr(E_InputType newValue)
        {
            setHTMLAttr<E_InputType>("type", newValue); 
        }

        public string GetValueValue() { return getHTMLAttr<string>("value", string.Empty); }
        public void SetValueValue(string newValue) { setHTMLAttr<string>("value", newValue); }

        [CLSCompliant(false)]
        public uint GetMaxLengthValue() { return getHTMLAttr<uint>("maxlength"); }
        [CLSCompliant(false)]
        public void SetMaxLengthValue(uint newValue) { setHTMLAttr<uint>("maxlength", newValue); }

        public string GetAcceptValue() { return getHTMLAttr<string>("accept", string.Empty); }
        public void SetAcceptValue(string newValue) { setHTMLAttr<string>("accept", newValue); }

        public string GetAltValue() { return getHTMLAttr<string>("alt", string.Empty); }
        public void SetAltValue(string newValue) { setHTMLAttr<string>("alt", newValue); }

        [CLSCompliant(false)]
        public uint GetSizeValue() { return getHTMLAttr<uint>("size"); }
        [CLSCompliant(false)]
        public void SetSizeValue(uint newValue) { setHTMLAttr<uint>("size", newValue); }

        public E_TripleState GetCheckedValue() { return getHTMLAttr<E_TripleState>("checked", E_TripleState.NONE); }
        public void SetCheckedValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("checked", newValue); }

        public E_TripleState GetDisabledValue() { return getHTMLAttr<E_TripleState>("disabled", E_TripleState.NONE); }
        public void SetDisabledValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("disabled", newValue); }
    }

    public partial class VDSelect
    {
        public override bool HasWidgetTitle { get { return true; } }

        // properties
        public string GetNameValue() { return getHTMLAttr<string>("name", string.Empty); }
        public void SetNameValue(string newValue) { setHTMLAttr<string>("name", newValue); }

        public E_TripleState GetDisabledValue() { return getHTMLAttr<E_TripleState>("disabled", E_TripleState.NONE); }
        public void SetDisabledValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("disabled", newValue); }

        public E_TripleState GetMultipleValue() { return getHTMLAttr<E_TripleState>("multiple", E_TripleState.NONE); }
        public void SetMultipleValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("multiple", newValue); }

        [CLSCompliant(false)]
        public uint GetSizeValue() { return getHTMLAttr<uint>("size"); }
        [CLSCompliant(false)]
        public void SetSizeValue(uint newValue) { setHTMLAttr<uint>("size", newValue); }
    }

    public partial class VDSelectOption
    {
        public string GetValueValue() { return getHTMLAttr<string>("value", string.Empty); }
        public void SetValueValue(string newValue) { setHTMLAttr<string>("value", newValue); }

        public E_TripleState GetSelectedValue() { return getHTMLAttr<E_TripleState>("selected", E_TripleState.NONE); }
        public void SetSelectedValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("selected", newValue); }

        public string GetLabelValue() { return getHTMLAttr<string>("label", string.Empty); }
        public void SetLabelValue(string newValue) { setHTMLAttr<string>("label", newValue); }

        public E_TripleState GetDisabledValue() { return getHTMLAttr<E_TripleState>("disabled", E_TripleState.NONE); }
        public void SetDisabledValue(E_TripleState newValue) { setHTMLAttr<E_TripleState>("disabled", newValue); }
    }
}



// Shape
namespace MVCVisualDesigner
{
    public partial class VDFormShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("FormToolToolboxBitmap"); }
    }

    public partial class VDRadioShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("RadioToolToolboxBitmap"); }
    }

    public partial class VDCheckBoxShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("CheckBoxToolToolboxBitmap"); }
    }

    public partial class VDSubmitShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("SubmitToolToolboxBitmap"); }
    }

    public partial class VDInputShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("InputToolToolboxBitmap"); }
    }

    public partial class VDSelectShape
    {
        public override bool HasWidgetTitleIcon { get { return true; } }

        protected override Image getTitleIcon() { return this.getImageFromResource("SelectToolToolboxBitmap"); }
    }
}