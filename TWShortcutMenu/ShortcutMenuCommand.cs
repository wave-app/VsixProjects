using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace TWShortcutMenu
{
    internal sealed class ShortcutMenuCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("23144241-35de-4e20-9560-ce8ca462a89b");

        private readonly Package package;

        public const string guidShortcutMenuPackageCmdSet = "23144241-35de-4e20-9560-ce8ca462a89b"; 
        public const int ColorMenu = 0x1000;
        public const int cmdidRed = 0x102;
        public const int cmdidYellow = 0x103;
        public const int cmdidBlue = 0x104;

        private ShortcutMenuCommand(Package package)
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

        public static ShortcutMenuCommand Instance
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
            Instance = new ShortcutMenuCommand(package);
        }

        private void ShowToolWindow(object sender, EventArgs e)
        {
            ToolWindowPane window = this.package.FindToolWindow(typeof(ShortcutMenu), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
