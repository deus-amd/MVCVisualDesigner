#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"Jun Wang")]
[assembly: AssemblyProduct(@"MVC Visual Designer")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"MVCVisualDesigner.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100D101D6933385C13931BFB7F7B4F0BF12E3469F94921DD8C77A63AABE5E3257E1D740BD6AF08146D0309E8E78CDAF23E7AE563C6FC7621B08EF1D9FF17F05430E92C32AC9DBE3A5C61D1B23C75FAEFA8F980519D754EB6E379C8E60503FB57E94E9D9F5901A8F730F01EBDCB81FC3518C921224932CB48EA15A186D5235C80EB7")]