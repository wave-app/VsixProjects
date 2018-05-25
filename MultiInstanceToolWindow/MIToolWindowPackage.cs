using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace MultiInstanceToolWindow
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(MultiInstanceToolWindow.MIToolWindow),MultiInstances = true)]
    [Guid(MIToolWindowPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MIToolWindowPackage : Package
    {
        public const string PackageGuidString = "2406c38d-5e8d-4027-9721-9c4bfc5ad297";

        public MIToolWindowPackage()
        {
        }

        #region Package Members

        protected override void Initialize()
        {
            MIToolWindowCommand.Initialize(this);
            base.Initialize();
        }

        #endregion
    }
}
