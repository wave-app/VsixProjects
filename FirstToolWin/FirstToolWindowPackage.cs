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

namespace FirstToolWin
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(FirstToolWindow), Style = Microsoft.VisualStudio.Shell.VsDockStyle.Tabbed,
    Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    [Guid(FirstToolWindowPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class FirstToolWindowPackage : Package
    {
        public const string PackageGuidString = "31be16bd-3975-468a-96ec-1cd857ebd582";

        public FirstToolWindowPackage()
        {
        }

        #region Package Members

        protected override void Initialize()
        {
            FirstToolWindowCommand.Initialize(this);
            base.Initialize();
        }

        #endregion
    }
}
