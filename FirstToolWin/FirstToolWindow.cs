namespace FirstToolWin
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.Shell.Interop;

    [Guid("ba7288d3-ac58-4dbe-83e8-5fc7c5dc4fa7")]
    public class FirstToolWindow : ToolWindowPane
    {
        public FirstToolWindowControl control;

        public FirstToolWindow() : base(null)
        {
            this.Caption = "FirstToolWindow";
            this.control = new FirstToolWindowControl();
            base.Content = control;
            this.ToolBar = new CommandID(new Guid(FirstToolWindowCommand.guidFirstToolWindowPackageCmdSet),
    FirstToolWindowCommand.ToolbarID);
            this.ToolBarLocation = (int)VSTWT_LOCATION.VSTWT_TOP;
        }
    }
}
