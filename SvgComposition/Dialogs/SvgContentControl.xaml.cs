using System.Windows;
using Microsoft.VisualStudio.PlatformUI;
using SvgComposition.Model;

namespace SvgComposition.Dialogs
{
    /// <summary>
    /// Interaction logic for SvgScriptControl.xaml
    /// </summary>
    public partial class SvgContentControl : DialogWindow
    {

        private SvgElement _celement ;

        public SvgContentControl()
        {
            InitializeComponent();
        }

        public SvgElement Celement
        {
            get { return (_celement); }
            set
            {
                _celement = value;
                if (_celement != null)
                {
                    HeaderText.Text = _celement.ElementType;
                    ContentText.Text = _celement.Content;
                    this.InvalidateVisual();
                }
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            Celement.Content = ContentText.Text;
            this.DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
