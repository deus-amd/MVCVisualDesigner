using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AutocompleteMenuNS;

namespace Tester
{
    public partial class Sandbox : Form
    {
        public Sandbox()
        {
            InitializeComponent();

            autocompleteMenu1.AddItem(new SubstringAutocompleteItem("Jack (nicholson@gmail.com)"));
            autocompleteMenu1.AddItem(new SubstringAutocompleteItem("Peter (me@gmail.com)"));
        }
    }
}
