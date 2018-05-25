using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MultiInstanceToolWindow
{
    internal sealed class MIToolWindowCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("60e24d62-08cd-4865-9cad-ee083cc036d7");

        private readonly Package package;

        private MIToolWindowCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public static MIToolWindowCommand Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new MIToolWindowCommand(package);
        }

        private void ShowToolWindow(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                ToolWindowPane window = this.package.FindToolWindow(typeof(MIToolWindow), i, false);
                if ((null == window) || (null == window.Frame))
                {
                    window = this.package.FindToolWindow(typeof(MIToolWindow), i, true);

                    if ((null == window) || (null == window.Frame))
                    {
                        throw new NotSupportedException("cannot create tool window");
                    }
                }

                IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
                break;
            }
            
        }
    }
}
