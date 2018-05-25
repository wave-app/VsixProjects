namespace TWShortcutMenu
{
    using System;
    using System.ComponentModel.Design;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.VisualStudio.Shell;
    using System.Windows.Media;

    public partial class ShortcutMenuControl : UserControl
    {
        private OleMenuCommandService commandService;
        

        public ShortcutMenuControl(OleMenuCommandService commandService)
        {
            this.InitializeComponent();

            this.commandService = commandService;

            if (null != commandService)
            {
                Guid guid = new Guid(ShortcutMenuCommand.guidShortcutMenuPackageCmdSet);

                var red = new CommandID(guid, ShortcutMenuCommand.cmdidRed);
                var yellow = new CommandID(guid, ShortcutMenuCommand.cmdidYellow);
                var blue = new CommandID(guid, ShortcutMenuCommand.cmdidBlue);

                commandService.AddCommand( new MenuCommand(ChangeColor, red));
                commandService.AddCommand(new MenuCommand(ChangeColor, yellow));
                commandService.AddCommand(new MenuCommand(ChangeColor, blue));
            }

        }

        private void ChangeColor(object sender, EventArgs e)
        {
            var mc = sender as MenuCommand;

            switch (mc.CommandID.ID)
            {
                case ShortcutMenuCommand.cmdidRed:
                    MyToolWindow.Background = Brushes.Red;
                    break;
                case ShortcutMenuCommand.cmdidYellow:
                    MyToolWindow.Background = Brushes.Yellow;
                    break;
                case ShortcutMenuCommand.cmdidBlue:
                    MyToolWindow.Background = Brushes.Blue;
                    break;
            }
        }

            private void MyToolWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (null != commandService)
            {

                Guid guid = new Guid(ShortcutMenuCommand.guidShortcutMenuPackageCmdSet);
                CommandID menuID = new CommandID(guid, ShortcutMenuCommand.ColorMenu);
                Point p = this.PointToScreen(e.GetPosition(this));
                this.commandService.ShowContextMenu(menuID, (int)p.X, (int)p.Y);

            }
        }
    }
}