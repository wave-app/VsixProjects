namespace FirstToolWin
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    public partial class FirstToolWindowControl : UserControl
    {
        public FirstToolWindowControl()
        {
            this.InitializeComponent();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "FirstToolWindow");
        }

        public System.Windows.Controls.MediaElement MediaPlayer
        {
            get { return mediaElement1; }
        }
    }
}