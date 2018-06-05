using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace DTEProject
{
    internal class DTETest : IClassifier
    {
        private readonly IClassificationType classificationType;

        internal DTETest(IClassificationTypeRegistryService registry)
        {
            this.classificationType = registry.GetClassificationType("DTETest");
        }

        #region IClassifier

#pragma warning disable 67

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationType)
            };

            return result;
        }

        #endregion
    }
}
