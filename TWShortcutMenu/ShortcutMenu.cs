namespace TWShortcutMenu
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;    
    using System.ComponentModel.Design;

    [Guid("1d34473b-561d-4b25-9c94-b5d86fe48860")]
    public class ShortcutMenu : ToolWindowPane
    {
        public ShortcutMenu() : base(null)
        {
            this.Caption = "ShortcutMenu";            
        }

        protected override void Initialize()
        {
            var commandService = (OleMenuCommandService)GetService(typeof(IMenuCommandService));
            Content = new ShortcutMenuControl(commandService);
        }
    }
}
