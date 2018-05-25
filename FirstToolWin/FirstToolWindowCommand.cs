using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Forms;

namespace FirstToolWin
{
    internal sealed class FirstToolWindowCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("4ea81d48-7a3a-4bb3-805b-0d64a373679e");

        private readonly Package package;

        public const string guidFirstToolWindowPackageCmdSet = "4ea81d48-7a3a-4bb3-805b-0d64a373679e";  // get the GUID from the .vsct file  

        public const uint cmdidWindowsMedia = 0x100;

        public const int cmdidWindowsMediaOpen = 0x132;

        public const int ToolbarID = 0x1000;

        private FirstToolWindow window;

        private FirstToolWindowCommand(Package package)
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

                var toolbarbtnCmdID = new CommandID(new Guid(FirstToolWindowCommand.guidFirstToolWindowPackageCmdSet), FirstToolWindowCommand.cmdidWindowsMediaOpen);
                var menuItem2 = new MenuCommand(new EventHandler(ButtonHandler), toolbarbtnCmdID);
                commandService.AddCommand(menuItem2);

            }
        }

        public static FirstToolWindowCommand Instance
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
            Instance = new FirstToolWindowCommand(package);
        }

        private void ShowToolWindow(object sender, EventArgs e)
        {
            window = (FirstToolWindow)this.package.FindToolWindow(typeof(FirstToolWindow), 0, true);            
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());

            //var mcs = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            //var toolbarbtnCmdID = new CommandID(new Guid(FirstToolWindowCommand.guidFirstToolWindowPackageCmdSet), FirstToolWindowCommand.cmdidWindowsMediaOpen);
            //var menuItem = new MenuCommand(new EventHandler(ButtonHandler), toolbarbtnCmdID);
            //mcs.AddCommand(menuItem);

        }

        private void ButtonHandler(object sender, EventArgs arguments)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                window.control.MediaPlayer.Source = new System.Uri(openFileDialog.FileName);
            }
        }
    }
}
