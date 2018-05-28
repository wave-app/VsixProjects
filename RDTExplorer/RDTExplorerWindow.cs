namespace RDTExplorer
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell.Interop;
    using EnvDTE;

    [Guid("56d53fc7-b012-4a74-866d-7a273bf7de5f")]
    public class RDTExplorerWindow : ToolWindowPane, IVsRunningDocTableEvents
    {
        private uint rdtCookie;

        public RDTExplorerWindow() : base(null)
        {
            this.Caption = "RDTExplorerWindow";

            this.Content = new RDTExplorerWindowControl();
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
    return VSConstants.S_OK;  
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            IVsRunningDocumentTable rdt = (IVsRunningDocumentTable) this.GetService(typeof(SVsRunningDocumentTable));
            uint pgrfRDTFlags;
            uint pdwReadLocks;
            uint pdwEditLocks;
            string pbstrMkDocument;
            IVsHierarchy ppHier;
            uint pitemid;
            IntPtr ppunkDocData;
            rdt.GetDocumentInfo(
                docCookie, out pgrfRDTFlags, out pdwReadLocks, out pdwEditLocks,
                out pbstrMkDocument, out ppHier, out pitemid, out ppunkDocData);

            EnvDTE.DTE dte = (DTE)this.GetService(typeof(DTE));
            ProjectItem prjItem = dte.Solution.FindProjectItem(pbstrMkDocument);
            if (prjItem != null)
                OnDocumentSaved(prjItem.Document);          

            return VSConstants.S_OK;  
        }

        private void OnDocumentSaved(Document document)
        {
             ((RDTExplorerWindowControl)this.Content).listBox.Items.Add(document.FullName);
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            return VSConstants.S_OK;  
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;  
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;  
        }

        protected override void Initialize()
        {
            IVsRunningDocumentTable rdt = (IVsRunningDocumentTable)
            this.GetService(typeof(SVsRunningDocumentTable));
            rdt.AdviseRunningDocTableEvents(this, out rdtCookie);
        }

        protected override void Dispose(bool disposing)
        {
            IVsRunningDocumentTable rdt = (IVsRunningDocumentTable) ServiceProvider.GetGlobalServiceAsync(typeof(SVsRunningDocumentTable));
            rdt.UnadviseRunningDocTableEvents(rdtCookie);

            base.Dispose(disposing);
        }

        public static ProjectItem FindSolutionItemByName(DTE dte, string name, bool recursive)
        {
            ProjectItem projectItem = null;
            foreach (Project project in dte.Solution.Projects)
            {
                projectItem = FindProjectItemInProject(project, name, recursive);

                if (projectItem != null)
                {
                    break;
                }
            }
            return projectItem;
        }

        public static ProjectItem FindProjectItemInProject(Project project, string name, bool recursive)
        {
            ProjectItem projectItem = null;
            foreach (ProjectItem item in project.ProjectItems)
            {
                Project realProject = item.Object as Project;

                if (realProject != null)
                {
                    projectItem = FindProjectItemInProject(realProject, name, recursive);

                    if (projectItem != null)
                    {
                        break;
                    }
                }
            }

            return projectItem;
        }

    }
}
