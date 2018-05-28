using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace TodoList
{
    class ToolsOptions:DialogPage
    {

        private double daysAhead;

        public double DaysAhead { get => daysAhead; set => daysAhead = value; }
    }
}
