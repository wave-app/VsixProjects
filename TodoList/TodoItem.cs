using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;

namespace TodoList
{
    public class TodoItem
    {

        private string name;
        private DateTime dueDate;
        private TodoWindowControl parent;

        public TodoItem(string name, TodoWindowControl control)
        {
            this.name = name;
            this.parent = control;
            this.dueDate = DateTime.Now;

            double daysAhead = 0;

            IVsPackage package = parent.parent.Package as IVsPackage;
            if (package != null)
            {
                object obj;
                package.GetAutomationObject("ToDo.General", out obj);
                ToolsOptions options = obj as ToolsOptions;
                if (options != null)
                {
                    daysAhead = options.DaysAhead;
                }
            }

            dueDate = dueDate.AddDays(daysAhead);

        }

        [Description("Name of the ToDo item")]
        [Category("ToDo Fields")]
        public string Name
        {
            get => name; set
            {
                name = value;
                parent.UpdateList(this);
            }
        }

        [Description("Due date of the ToDo item")]
        [Category("ToDo Fields")]
        public DateTime DueDate
        {
            get => dueDate; set
            {
                dueDate = value;
                parent.UpdateList(this);
                parent.CheckForErrors();
            }
        }

        public override string ToString() => name + "Due:" + dueDate.ToShortDateString();

    }
    
}
