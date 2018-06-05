using System.ComponentModel.Composition;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace DTEProject
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")]        
    internal class DTETestProvider : IClassifierProvider
    {


        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

        [Import]
        internal SVsServiceProvider ServiceProvider = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            return buffer.Properties.GetOrCreateSingletonProperty<DTETest>(creator: () => new DTETest(this.classificationRegistry));
        }

    }
}
