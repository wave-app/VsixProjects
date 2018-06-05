using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.PlatformUI;

namespace OpenDialogTest
{
    internal sealed class OpenDialog
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("2ab1e907-f6fe-44ee-842f-508ba368f507");

        private readonly Package package;

        private OpenDialog(Package package)
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
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public static OpenDialog Instance
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
            Instance = new OpenDialog(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "OpenDialog";

            TestDialogWindow testDialog = new TestDialogWindow();
            testDialog.ShowModal();
        }

        class TestDialogWindow : DialogWindow
        {

            internal TestDialogWindow()
            {
                this.HasMaximizeButton = true;
                this.HasMinimizeButton = true;
            }
        }

    }
}
