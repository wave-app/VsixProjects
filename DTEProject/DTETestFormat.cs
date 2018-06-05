using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace DTEProject
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "DTETest")]
    [Name("DTETest")]
    [UserVisible(true)]         
    [Order(Before = Priority.Default)]          
    internal sealed class DTETestFormat : ClassificationFormatDefinition
    {
        public DTETestFormat()
        {
            this.DisplayName = "DTETest";       
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
}
