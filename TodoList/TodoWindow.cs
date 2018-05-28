namespace TodoList
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    [Guid("09eb1e99-0ae8-4c3d-ba9b-0dd9040ec576")]
    public class TodoWindow : ToolWindowPane
    {
        public TodoWindow() : base(null)
        {
            this.Caption = "TodoWindow";

            this.Content = new TodoWindowControl(this);
        }

        internal object GetVsService(Type service)
        {
            return GetService(service);
        }
    }
}
