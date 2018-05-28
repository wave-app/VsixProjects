namespace TodoList
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;

    public partial class TodoWindowControl : UserControl
    {

        public TodoWindow parent;
        private SelectionContainer mySelContainer;
        private System.Collections.ArrayList mySelItems;
        private IVsWindowFrame frame = null;
        private TodoWindowTaskProvider taskProvider;
        private IVsOutputWindowPane pane;

        public TodoWindowControl(TodoWindow window)
        {
            this.InitializeComponent();
            this.parent = window;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                var item = new TodoItem(textBox.Text,this);
                listBox.Items.Add(item);

                var outputWindow = parent.GetVsService(typeof(SVsOutputWindow)) as IVsOutputWindow;                
                Guid guidGeneralPane = VSConstants.GUID_OutWindowGeneralPane;
                
                int hr;
                if (pane == null)
                {
                    hr = outputWindow.CreatePane(guidGeneralPane, "General", 1, 1);
                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
                }

                hr = outputWindow.GetPane(ref guidGeneralPane, out pane);
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
                pane.OutputString(string.Format("To Do item created: {0}\r\n", item.ToString()));
              
                TrackSelection();
                CheckForErrors();
            }
        }

        private void TrackSelection()
        {
            if (frame == null)
            {
                var shell = parent.GetVsService(typeof(SVsUIShell)) as IVsUIShell;
                if (shell != null)
                {
                    var guidPropertyBrowser = new Guid(ToolWindowGuids.PropertyBrowser);
                    shell.FindToolWindow((uint)__VSFINDTOOLWIN.FTW_fForceCreate,
           ref guidPropertyBrowser, out frame);

                }

            }

            if (frame != null)
            {
                frame.Show();
            }

            if (mySelContainer == null)
            {
                mySelContainer = new SelectionContainer();
            }

            mySelItems = new System.Collections.ArrayList();

            var selected = listBox.SelectedItem as TodoItem;
            if (selected != null)
            {
                mySelItems.Add(selected);
            }

            mySelContainer.SelectedObjects = mySelItems;

            ITrackSelection track = parent.GetVsService(typeof(STrackSelection))
                                    as ITrackSelection;
            if (track != null)
            {
                track.OnSelectChange(mySelContainer);
            }
        }

        public void CheckForErrors()
        {
            ClearError();
            foreach (TodoItem item in listBox.Items)
            {
                if (item.DueDate < DateTime.Now)
                {
                    ReportError("To Do Item is out of date: "
                        + item.ToString());
                }
            }
        }
        public void UpdateList(TodoItem item)
        {

            var index = listBox.SelectedIndex;
            listBox.Items.RemoveAt(index);
            listBox.Items.Insert(index, item);
            listBox.SelectedIndex = index;

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrackSelection();
        }

        [Guid("72de1eAD-a00c-4f57-bff7-57edb162d0be")]
        public class TodoWindowTaskProvider : TaskProvider
        {
            public TodoWindowTaskProvider(IServiceProvider sp)
                : base(sp)
            {
            }
        }

        private void CreateProvider()
        {
            if (taskProvider == null)
            {
                taskProvider = new TodoWindowTaskProvider(parent);
                taskProvider.ProviderName = "To Do";
            }
        }

        private void ClearError()
        {
            CreateProvider();
            taskProvider.Tasks.Clear();
        }

        private void ReportError(string p)
        {
            CreateProvider();
            var errorTask = new Task();
            errorTask.CanDelete = false;
            errorTask.Category = TaskCategory.Comments;
            errorTask.Text = p;

            taskProvider.Tasks.Add(errorTask);

            taskProvider.Show();

            var taskList = parent.GetVsService(typeof(SVsTaskList))
                as IVsTaskList2;
            if (taskList == null)
            {
                return;
            }

            var guidProvider = typeof(TodoWindowTaskProvider).GUID;
            taskList.SetActiveProvider(ref guidProvider);
        }

    }
}