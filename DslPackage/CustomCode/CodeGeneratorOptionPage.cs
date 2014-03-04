using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace MVCVisualDesigner
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class CodeGeneratorOptionPage : DialogPage
    {
        private int optionValue = 256;

        [Category("My Category")]
        [DisplayName("My Integer Option")]
        [Description("My integer option")]
        public int OptionInteger
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();
        }
    }
}
