namespace RDTExplorer
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell.Interop;
    using EnvDTE;
    using EnvDTE80;

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
            //((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnAfterFirstDocumentLock");  
    return VSConstants.S_OK;  
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            //((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnBeforeLastDocumentUnlock");
            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            IVsRunningDocumentTable rdt = (IVsRunningDocumentTable) this.GetService(typeof(SVsRunningDocumentTable));

            ((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnAfterSave");
            return VSConstants.S_OK;  
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            //((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnAfterAttributeChange");
            return VSConstants.S_OK;  
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            //((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnBeforeDocumentWindowShow");
            return VSConstants.S_OK;  
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            //((RDTExplorerWindowControl)this.Content).listBox.Items.Add("Entering OnAfterDocumentWindowHide");
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
            // Release the RDT cookie.  
            IVsRunningDocumentTable rdt = (IVsRunningDocumentTable) ServiceProvider.GetGlobalServiceAsync(typeof(SVsRunningDocumentTable));
            rdt.UnadviseRunningDocTableEvents(rdtCookie);

            base.Dispose(disposing);
        }

    }
}
